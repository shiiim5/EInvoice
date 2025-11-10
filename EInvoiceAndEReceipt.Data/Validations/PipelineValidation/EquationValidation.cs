using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Generics;

namespace EInvoiceAndEReceipt.Data.Validations.PipelineValidation
{
    public class EquationValidation : IDocumentValidator
    {
        public string Name => nameof(EquationValidation);

        public Task<DocumentValidationResult> ValidateAsync(DocumentDTO document)
        {
            var result = new DocumentValidationResult { InternalId = document.InternalId };

            try
            {
                if (document == null)
                {
                    result.Messages.Add(new ValidatorMessage("Document is null", true));
                    return Task.FromResult(result);
                }

                // ----------------------------
                // VALIDATION ON INVOICE LINE LEVEL
                // ----------------------------
                foreach (var line in document.InvoiceLines)
                {
                    // (1) Sales Total = Quantity * Amount
                    var expectedSalesTotal = line.Quantity * line.UnitValue.AmountEGP;
                    if (Math.Round(line.SalesTotal, 2) != Math.Round(expectedSalesTotal, 2))
                    {
                        result.Messages.Add(new ValidatorMessage(
                            $"Invoice line {line.InternalCode}: SalesTotal ({line.SalesTotal}) should equal Quantity * AmountEGP ({expectedSalesTotal}).",
                            true));
                    }

                    // (2) Discount Amount = Discount.Rate * SalesTotal (if rate != 0)
                    if (line.Discount != null && line.Discount.Rate > 0)
                    {
                        var expectedDiscount = (line.Discount.Rate / 100m) * line.SalesTotal;
                        if (Math.Round(line.Discount.Amount, 2) != Math.Round(expectedDiscount, 2))
                        {
                            result.Messages.Add(new ValidatorMessage(
                                $"Invoice line {line.InternalCode}: Discount amount ({line.Discount.Amount}) should equal DiscountRate * SalesTotal ({expectedDiscount}).",
                                true));
                        }
                    }

                    // (3) Net Total = Sales Total – Discount Amount
                    var expectedNetTotal = line.SalesTotal - (line.Discount?.Amount ?? 0);
                    if (Math.Round(line.NetTotal, 2) != Math.Round(expectedNetTotal, 2))
                    {
                        result.Messages.Add(new ValidatorMessage(
                            $"Invoice line {line.InternalCode}: NetTotal ({line.NetTotal}) should equal SalesTotal - DiscountAmount ({expectedNetTotal}).",
                            true));
                    }

                    // 4️⃣ Taxable Items (T1–T12)
                    var taxableFees = 0m;
                    foreach (var item in line.TaxableItems.Where(t => t.TaxType.StartsWith("T")))
                    {
                        var taxNum = int.TryParse(item.TaxType.Substring(1), out var n) ? n : 0;

                        if (taxNum >= 5 && taxNum <= 12)
                        {
                            // (4.1)
                            if (item.Rate > 0)
                            {
                                var expected = (item.Rate / 100m) * line.NetTotal;
                                if (Math.Round(item.Amount, 2) != Math.Round(expected, 2))
                                {
                                    result.Messages.Add(new ValidatorMessage(
                                        $"Line {line.InternalCode}: TaxableItem {item.TaxType} Amount ({item.Amount}) should equal Rate * NetTotal ({expected}).",
                                        true));
                                }
                            }
                            taxableFees += item.Amount;
                        }

                        // (T2 - Table tax %)
                        if (taxNum == 2)
                        {
                            var t3Amount = line.TaxableItems.FirstOrDefault(t => t.TaxType == "T3")?.Amount ?? 0;
                            var expected = (line.NetTotal + line.TotalTaxableFees + line.ValueDifference + t3Amount) * (item.Rate / 100m);
                            if (Math.Round(item.Amount, 2) != Math.Round(expected, 2))
                            {
                                result.Messages.Add(new ValidatorMessage(
                                    $"Line {line.InternalCode}: T2 table tax amount ({item.Amount}) should equal (NetTotal + TotalTaxableFees + ValueDifference + T3.Amount) * Rate ({expected}).",
                                    true));
                            }
                        }

                        // (T3 - Table tax fixed)
                        if (taxNum == 3 && item.Rate != 0)
                        {
                            result.Messages.Add(new ValidatorMessage(
                                $"Line {line.InternalCode}: T3 tax should have rate = 0.",
                                true));
                        }

                        // (T6 - Stamping tax fixed)
                        if (taxNum == 6 && item.Rate != 0)
                        {
                            result.Messages.Add(new ValidatorMessage(
                                $"Line {line.InternalCode}: T6 tax should have rate = 0.",
                                true));
                        }

                        // (T1 - VAT)
                        if (taxNum == 1)
                        {
                            var expected = (line.NetTotal + line.TotalTaxableFees + line.ValueDifference + item.Amount) * (item.Rate / 100m);
                            if (Math.Round(item.Amount, 2) != Math.Round(expected, 2))
                            {
                                result.Messages.Add(new ValidatorMessage(
                                    $"Line {line.InternalCode}: T1 VAT amount ({item.Amount}) invalid. Expected {expected}.",
                                    true));
                            }
                        }

                        // (T4 - Withholding Tax)
                        if (taxNum == 4)
                        {
                            var expected = (item.Rate / 100m) * (line.NetTotal - line.ItemsDiscount);
                            if (Math.Round(item.Amount, 2) != Math.Round(expected, 2))
                            {
                                result.Messages.Add(new ValidatorMessage(
                                    $"Line {line.InternalCode}: T4 WHT amount ({item.Amount}) should equal Rate * (NetTotal - ItemsDiscount) ({expected}).",
                                    true));
                            }
                        }
                    }

                    // (4.2) TotalTaxableFees = sum of taxable fees
                    if (Math.Round(line.TotalTaxableFees, 2) != Math.Round(taxableFees, 2))
                    {
                        result.Messages.Add(new ValidatorMessage(
                            $"Line {line.InternalCode}: TotalTaxableFees ({line.TotalTaxableFees}) should equal sum of all taxable fee amounts ({taxableFees}).",
                            true));
                    }

                    // (5) Non-taxable (T13–T20)
                    var nonTaxFees = 0m;
                    foreach (var item in line.TaxableItems.Where(t =>
                        int.TryParse(t.TaxType.Substring(1), out var num) && num >= 13 && num <= 20))
                    {
                        if (item.Rate > 0)
                        {
                            var expected = (item.Rate / 100m) * line.NetTotal;
                            if (Math.Round(item.Amount, 2) != Math.Round(expected, 2))
                            {
                                result.Messages.Add(new ValidatorMessage(
                                    $"Line {line.InternalCode}: NonTaxableItem {item.TaxType} amount ({item.Amount}) should equal Rate * NetTotal ({expected}).",
                                    true));
                            }
                        }
                        nonTaxFees += item.Amount;
                    }

                    // (5.2)
                    var lineTotalExpected = line.NetTotal
                        + taxableFees
                        + nonTaxFees
                        - line.ItemsDiscount
                        - line.TaxableItems
                            .Where(t => t.TaxType == "T4")
                            .Sum(t => t.Amount);

                    if (Math.Round(line.Total, 2) != Math.Round(lineTotalExpected, 2))
                    {
                        result.Messages.Add(new ValidatorMessage(
                            $"Line {line.InternalCode}: Total ({line.Total}) should equal NetTotal + TaxableFees + NonTaxableFees - ItemsDiscount - WHTAmounts ({lineTotalExpected}).",
                            true));
                    }
                }

                // ----------------------------
                //  VALIDATION ON INVOICE LEVEL
                // ----------------------------
                var expectedTotalSales = document.InvoiceLines.Sum(l => l.SalesTotal);
                if (Math.Round(document.TotalSalesAmount, 2) != Math.Round(expectedTotalSales, 2))
                {
                    result.Messages.Add(new ValidatorMessage(
                        $"Invoice total sales amount ({document.TotalSalesAmount}) should equal sum of invoice line sales totals ({expectedTotalSales}).",
                        true));
                }

                var expectedTotalItemDiscount = document.InvoiceLines.Sum(l => l.ItemsDiscount);
                if (Math.Round(document.TotalItemsDiscountAmount, 2) != Math.Round(expectedTotalItemDiscount, 2))
                {
                    result.Messages.Add(new ValidatorMessage(
                        $"Invoice total item discount amount ({document.TotalItemsDiscountAmount}) should equal sum of all invoice line item discounts ({expectedTotalItemDiscount}).",
                        true));
                }

                var expectedNetAmount = document.InvoiceLines.Sum(l => l.NetTotal);
                if (Math.Round(document.NetAmount, 2) != Math.Round(expectedNetAmount, 2))
                {
                    result.Messages.Add(new ValidatorMessage(
                        $"Invoice net amount ({document.NetAmount}) should equal sum of invoice line net totals ({expectedNetAmount}).",
                        true));
                }

                var expectedTotalAmount = document.InvoiceLines.Sum(l => l.Total) - document.ExtraDiscountAmount;
                if (Math.Round(document.TotalAmount, 2) != Math.Round(expectedTotalAmount, 2))
                {
                    result.Messages.Add(new ValidatorMessage(
                        $"Invoice total amount ({document.TotalAmount}) should equal sum of line totals - extra discount ({expectedTotalAmount}).",
                        true));
                }

                var expectedTaxTotals = document.InvoiceLines
                    .SelectMany(l => l.TaxableItems)
                    .Where(t => int.TryParse(t.TaxType.Substring(1), out var num) && num >= 1 && num <= 12)
                    .Sum(t => t.Amount);

                var invoiceTaxTotalSum = document.TaxTotals.Sum(tt => tt.Amount);
                if (Math.Round(invoiceTaxTotalSum, 2) != Math.Round(expectedTaxTotals, 2))
                {
                    result.Messages.Add(new ValidatorMessage(
                        $"Invoice tax totals ({invoiceTaxTotalSum}) should equal sum of taxable items (T1–T12) ({expectedTaxTotals}).",
                        true));
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(new ValidatorMessage($"Exception during validation: {ex.Message}", true));
            }

            return Task.FromResult(result);
        }
    }
}

    
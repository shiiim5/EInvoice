using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Generics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NCalc;

namespace EInvoiceAndEReceipt.Data.Validations.PipelineValidation
{
    public class SimpleFieldValidation : SyncDocumentValidator
    {
        public override string Name => nameof(SimpleFieldValidation);
        private readonly List<ValidationRule> _rules;

        public SimpleFieldValidation()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "SimpleFieldRules.json");
            var json = File.ReadAllText(jsonPath);
            var doc = JsonDocument.Parse(json);

            _rules = doc.RootElement
            .GetProperty("SimpleFieldValidations")
            .EnumerateArray()
            .Select(e => new ValidationRule
            {
                Name = e.GetProperty("Name").GetString() ?? string.Empty,
                Level = e.GetProperty("Level").GetString() ?? string.Empty,
                Expression = e.GetProperty("Expression").GetString() ?? string.Empty,
                Condition = e.TryGetProperty("Condition", out var conditionProp) ? conditionProp.GetString() : null,
                Message = e.GetProperty("Message").GetString() ?? string.Empty
            }).ToList();
        }

        protected override void Validate(DocumentDTO document, DocumentValidationResult result)
        {

            foreach (var rule in _rules)
            {
                try
                {
                    if (rule.Level == "InvoiceLine" && document.InvoiceLines != null)
                    {
                        foreach (var line in document.InvoiceLines)
                        {
                            var context = new Dictionary<string, object>
                            {
                                ["Quantity"] = line.Quantity,
                                ["SalesTotal"] = line.SalesTotal,
                                ["DiscountRate"] = line.Discount.Rate,
                                ["DiscountAmount"] = line.Discount.Amount,
                                ["NetTotal"] = line.NetTotal,
                                ["ItemsDiscount"] = line.ItemsDiscount,
                                ["TotalTaxableFees"] = line.TotalTaxableFees,
                                ["TotalNonTaxableFees"] = line.SalesTotal,
                                ["LineTotal"] = line.NetTotal
                            };

                            if (EvaluateRule(rule, context) == false)
                            {
                                result.Messages.Add(new ValidatorMessage
                                {
                                    Validator = Name,
                                    Field = rule.Name,
                                    Message = $"{rule.Message} (Line: {line._Id})",
                                    IsError = true
                                });
                            }
                        }
                    }
                    else if (rule.Level == "Invoice")
                    {
                        var context = new Dictionary<string, object>
                        {
                            ["TotalSalesAmount"] = document.TotalSalesAmount,
                           // ["TotalItemDiscount"] = document.TotalItemDiscount,
                            ["NetAmount"] = document.NetAmount,
                            ["TotalItemsDiscountAmount"] = document.TotalItemsDiscountAmount,
                            ["TotalAmount"] = document.TotalAmount,
                            ["ExtraDiscountAmount"] = document.ExtraDiscountAmount
                        };

                        if (EvaluateRule(rule, context) == false)
                        {
                            result.Messages.Add(new ValidatorMessage
                            {
                                Validator = Name,
                                Field = rule.Name,
                                Message = rule.Message,
                                IsError = true
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Messages.Add(new ValidatorMessage
                    {
                        Validator = Name,
                        Field = rule.Name,
                        Message = $"Validation rule '{rule.Name}' failed: {ex.Message}",
                        IsError = true
                    });
                }
            }
        }


        private bool EvaluateRule(ValidationRule rule, Dictionary<string, object> context)
        {
           
            if (!string.IsNullOrWhiteSpace(rule.Condition))
            {
                var cond = new Expression(rule.Condition);
                foreach (var kvp in context)
                    cond.Parameters[kvp.Key] = kvp.Value;

                var condResult = Convert.ToBoolean(cond.Evaluate());
                if (!condResult) return true; 
            }

            
            var expr = new Expression(rule.Expression);
            foreach (var kvp in context)
                expr.Parameters[kvp.Key] = kvp.Value;

            var result = expr.Evaluate();
            if (result is bool b)
                return b;

            return false;
        }
    }
}


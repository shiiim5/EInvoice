using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Validations;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class Invoice
    {
        [Key]
        public int _Id { get; set; }
        public Issuer Issuer { get; set; }
        public Receiver Receiver { get; set; }
        [AllowedValues("i")]

        public string DocumentType { get; set; } = "i";
        [AllowedValues("1.0")]
        public string DocumentTypeVersion { get; set; }
        public DateTime DateTimeIssued { get; set; }
        [ValidValues("ActivityCodes.json")]
        public string TaxpayerActivityCode { get; set; }
        public string InternalId { get; set; }
        public string? PurchaseOrderReference { get; set; }
        public string? PurchaseOrderDescription { get; set; }
        public string? SalesOrderReference { get; set; }
        public string? SalesOrderDescription { get; set; }
        public string? ProformaInvoiceNumber { get; set; }
        public Payment? Payment { get; set; }
        public Delievry? Delievry { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
        public Decimal TotalSalesAmount { get; set; }
        public Decimal TotalDiscountAmount { get; set; }
        public Decimal NetAmount { get; set; }
        public List<TaxTotal> TaxTotals { get; set; } = new List<TaxTotal>();
        public Decimal ExtraDiscountAmount { get; set; }
        public Decimal TotalItemsDiscountAmount { get; set; }
        public Decimal TotalAmount { get; set; }
        public List<Signature> Signatures { get; set; } = new List<Signature>();
        public DateTime? ServiceDeliveryDate { get; set; }

    }
}
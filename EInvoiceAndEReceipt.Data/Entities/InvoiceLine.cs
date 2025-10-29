using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Validations;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class InvoiceLine
    {
        public int _Id { get; set; }
        public string Description { get; set; }
        [AllowedValues("GS1","EGS")]
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        [ValidValues("UnitTypes.json")]
        public string UnitType { get; set; }
        [Range(1, Double.MaxValue)]
        public Decimal Quantity { get; set; }
        public Value UnitValue { get; set; }
        public Decimal SalesTotal { get; set; }
        public Decimal Total { get; set; }
        public Decimal ValueDifference { get; set; }
        public Decimal TotalTaxableFees { get; set; }
        public Decimal NetTotal { get; set; }
        public Decimal ItemsDiscount { get; set; }
        public Discount? Discount { get; set; }

        public List<TaxableItem>? TaxableItems { get; set; }

        public string? InternalCode { get; set; }
    }
}
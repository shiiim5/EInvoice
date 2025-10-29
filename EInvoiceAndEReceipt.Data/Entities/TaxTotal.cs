using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Validations;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class TaxTotal
    {
        public int _Id { get; set; }
        [ValidValues("TaxTypes.json")]
        public string TaxType { get; set; }
        [Range(0.00001, Double.MaxValue)]
        public Decimal Amount { get; set; }
    }
}
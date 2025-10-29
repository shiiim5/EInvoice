using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Validations;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class TaxableItem
    {
        public int _Id { get; set; }
        [ValidValues("TaxTypes.json")]
        public string TaxType { get; set; }
        public Decimal Amount { get; set; }
        [ValidValues("TaxSubtypes.json")]
        public string SubType { get; set; }
        [Range(0,99)]
        public Decimal Rate { get; set; }
    }
}
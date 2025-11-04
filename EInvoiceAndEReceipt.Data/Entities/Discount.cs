using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class Discount
    {
         [Key]
        public int _Id { get; set; }
        [Range(0, 100)]
        public Decimal Rate { get; set; }
        [Range(0.00001, Double.MaxValue)]
        public Decimal Amount { get; set; }
    }
}
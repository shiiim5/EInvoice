using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class Signature
    {
         [Key]
        public int _Id { get; set; }
        [AllowedValues("I","S")]
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
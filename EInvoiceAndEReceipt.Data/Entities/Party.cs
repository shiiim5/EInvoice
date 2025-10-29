using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class Party
    {
         public int _Id { get; set; }
        public string Id { get; set; }
        [AllowedValues("B", "P", "F")]
        public string Type { get; set; }
        public string Name { get; set; }
        public IssuerAddress Address { get; set; }
    }
}
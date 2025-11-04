using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class Delievry
    {
        [Key]
        public int _Id { get; set; }
        public string? Approach { get; set; }
        public string? Packaging { get; set; }
        public string? DateValidity { get; set; }
        public string? ExportPort { get; set; }
        public string? CountryOfOrigin { get; set; }
        public Decimal? GrossWeight { get; set; }
        public Decimal? NetWeight { get; set; }
        public string? Terms { get; set; }
    }
}
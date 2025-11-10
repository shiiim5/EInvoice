using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.DTOs
{
    public class DelievryDTO
    {
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
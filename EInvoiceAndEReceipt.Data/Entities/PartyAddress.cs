using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Validations;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class PartyAddress
    {
        
        public int _Id { get; set; }
       [ValidValues("CountryCodes.json")]
        public string Country { get; set; }
        public string Governate { get; set; }
        public string RegionCity { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? Floor { get; set; }
        public string? Room { get; set; }
        public string? LandMark { get; set; }
        public string? AdditionalInformation { get; set; }
    }
}
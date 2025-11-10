using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.DTOs
{
    public class IssuerDTO
    {

        public string Id { get; set; }

        public string Type { get; set; }
        public string Name { get; set; }
        public IssuerAddressDTO Address { get; set; }
    }
    
    public class IssuerAddressDTO
    {
           public string BranchId { get; set; }
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
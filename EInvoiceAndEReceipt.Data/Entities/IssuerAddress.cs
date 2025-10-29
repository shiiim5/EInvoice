using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Validations;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class IssuerAddress:PartyAddress
    {
        [ValidBranchId(nameof(Party.Type))]
         public string BranchId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Entities;

namespace EInvoiceAndEReceipt.Application.Helpers
{
    public class InvoiceProcessingResult
    {
        public List<string> AcceptedDocumentsIds { get; set; } = new();
        public List<Invoice> AcceptedDocuments { get; set; } = new();
        public List<Invoice> RejectedDocuments { get; set; } = new();
    }
}
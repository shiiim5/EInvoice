using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Entities;

namespace EInvoiceAndEReceipt.Application.Generics
{
    public class InvoiceProcessingResult
    {
        public string SubmissionUUID { get; set; } = Guid.NewGuid().ToString();
        public List<string>? AcceptedDocumentsIds { get; set; } = new();
        public List<AcceptedInvoice>? AcceptedDocuments { get; set; } = new();
        public List<RejectedInvoice>? RejectedDocuments { get; set; } = new();
    }

    public class AcceptedInvoice
    {
        public string InternalId { get; set; }
        public string UUID { get; set; } = Guid.NewGuid().ToString();
    }

    public class RejectedInvoice
    {
        public RejectedInvoice()
        {
            
        }
        public RejectedInvoice(Error? error)
        {
            if (error != null)
                Errors.Add(error);

        }
        public string InternalId { get; set; }
        public List<Error> Errors { get; set; } = new();
    }
    
    public class Error
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? Target { get; set; }
        public Error[]? Details { get; set; }
    }
}
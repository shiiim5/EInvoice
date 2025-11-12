using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Application.Generics;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Entities;

namespace EInvoiceAndEReceipt.Application.IServices
{
    public interface IInvoiceService
    {
        public Task<InvoiceProcessingResult> SubmitDocumentsAsync(string contentType, Stream stream);
        public Task CancelDocumentAsync(string uuid);
        public Task<bool> UpdateDocumentAsync(DocumentDTO document);
        public Task<List<Invoice>> GetInvoices();
         
    }
}
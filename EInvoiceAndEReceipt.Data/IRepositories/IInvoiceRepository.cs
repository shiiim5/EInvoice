using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Entities;

namespace EInvoiceAndEReceipt.Data.IRepositories
{
    public interface IInvoiceRepository
    {
        public Task<bool> ExistsAsync(string InvoiceId);
        public Task<List<Invoice>> AddDocumentsAsync(IEnumerable<Invoice> invoices);

        
    }
}
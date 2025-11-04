using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DbContext;
using EInvoiceAndEReceipt.Data.Entities;
using EInvoiceAndEReceipt.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceAndEReceipt.Data.Repositories
{
    public class InvoiceRepository:IInvoiceRepository
    {
        private readonly EInvoiceDbContext _context;

        public InvoiceRepository(EInvoiceDbContext context)
        {
            _context = context;  
        }
        public async Task<List<Invoice>> AddDocumentsAsync(IEnumerable<Invoice> invoices)
        {

            await _context.Invoices.AddRangeAsync(invoices);
            await _context.SaveChangesAsync();
            return new List<Invoice>(invoices);
          

        }

        public async Task<bool> ExistsAsync(string InvoiceId)
        {
            return await _context.Invoices.AnyAsync(i =>
            i.InternalId == InvoiceId);
        }


    }
}
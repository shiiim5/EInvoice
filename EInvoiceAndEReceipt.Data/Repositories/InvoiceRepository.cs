using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EInvoiceAndEReceipt.Data.DbContext;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Entities;
using EInvoiceAndEReceipt.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceAndEReceipt.Data.Repositories
{
    public class InvoiceRepository:IInvoiceRepository
    {
        private readonly EInvoiceDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceRepository(EInvoiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Invoice>> AddDocumentsAsync(IEnumerable<Invoice> invoices)
        {

            await _context.Invoices.AddRangeAsync(invoices);
            await _context.SaveChangesAsync();
            return new List<Invoice>(invoices);

        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            var invoices =  _context.Invoices.ToList();
            return invoices;
        } 

        public async Task<bool> ExistsAsync(string InvoiceId)
        {
            return await _context.Invoices.AnyAsync(i =>
            i.InternalId == InvoiceId);
        }

        public async Task<Invoice> GetInvoiceByIDAsync(string id)
        {
            return await _context.Invoices.FirstOrDefaultAsync(i => i.InternalId == id);
        }

        public async Task<bool> UpdateInvoiceAsync(DocumentDTO document)
        {
            if (document is null)
                return false;

            var existingInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.InternalId == document.InternalId);
            if (existingInvoice is null)
                return false;

            _mapper.Map<Invoice>(document);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelDocumentAsync(string internalId)
        {
            var existing = await _context.Invoices.FirstOrDefaultAsync(i => i.InternalId == internalId);
            if (existing is null)
                return false;

            existing.Status = "Cancelled";
            _context.Invoices.Update(existing);
            _context.SaveChangesAsync();

            return true;

        }
    }
}
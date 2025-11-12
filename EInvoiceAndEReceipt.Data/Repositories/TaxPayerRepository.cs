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
    public class TaxPayerRepository:ITaxPayerRepository
    {
        private readonly EInvoiceDbContext _context;

        public TaxPayerRepository(EInvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<TaxPayer> GetTaxPayerByEmail(string email)
        {
            var TaxPayer = await _context.TaxPayers
                  .FirstOrDefaultAsync(tp => tp.Email == email);

            return TaxPayer;

        }
        
        public async Task AddTaxPayerAsync(TaxPayer taxPayer)
        {
            await _context.TaxPayers.AddAsync(taxPayer);
            await  _context.SaveChangesAsync();
        }
    }
}
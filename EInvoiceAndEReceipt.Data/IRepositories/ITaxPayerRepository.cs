using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Entities;

namespace EInvoiceAndEReceipt.Data.IRepositories
{
    public interface ITaxPayerRepository
    {
        public Task<TaxPayer> GetTaxPayerByEmail(string email);
        public Task AddTaxPayerAsync(TaxPayer taxPayer);
    }
}
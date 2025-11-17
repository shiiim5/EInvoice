using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Entities;

namespace EInvoiceAndEReceipt.Data.IRepositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByEmail(string email);
      //  public Task<List<string>> GetUserRolesAsync(User user);
        public Task AddUserAsync(User user);
    }
}
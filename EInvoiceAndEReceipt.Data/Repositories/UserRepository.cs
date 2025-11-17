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
    public class UserRepository:IUserRepository
    {
        private readonly EInvoiceDbContext _context;

        public UserRepository(EInvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users
                  .FirstOrDefaultAsync(tp => tp.Email == email);

            return user;

        }
        
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await  _context.SaveChangesAsync();
        }

        // public  Task<string> GetUserRoleAsync(User user)
        // {
        //    return _context.Users
        //    .Where(ur=> ur.Id == user.Id )
        //    .Select(ur => ur.Role.RoleName);
        // }
    }
}
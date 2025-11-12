using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Auth;

namespace EInvoiceAndEReceipt.Application.IServices
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponse> GetAccessTokenAsync(string email, string password);
        public Task<bool> RegisterUserAsync(string email, string password, string name);
    }
}
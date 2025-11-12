using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Auth
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
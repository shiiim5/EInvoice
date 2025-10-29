using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class Payment
    {
        public int _Id { get; set; }
        public string? BankName { get; set; }
        public string? BankAddress { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankAccountIBAN { get; set; }
        public string? SwiftCode { get; set; }
        public string? Terms { get; set; }
    }
}
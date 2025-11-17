using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceAndEReceipt.Data.DbContext
{
    public class EInvoiceDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public EInvoiceDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<EInvoiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            
        }

      
         public DbSet<Invoice> Invoices { get; set; }
         public DbSet<Delievry> Delievries { get; set; }
         public DbSet<Discount> Discounts { get; set; }
         public DbSet<InvoiceLine> InvoiceLines { get; set; }
         public DbSet<Issuer> Issuers { get; set; }
         public DbSet<IssuerAddress> IssuerAddresses { get; set; }
         public DbSet<Receiver> Receivers { get; set; }
         public DbSet<ReceiverAddress> ReceiverAddresses { get; set; }
         public DbSet<Payment> Payments { get; set; }
         public DbSet<Signature> Signatures { get; set; }
         public DbSet<TaxableItem> TaxableItems { get; set; }
         public DbSet<TaxTotal> TaxTotals { get; set; }
         public DbSet<Value> Values { get; set; }
         public DbSet<User> Users { get; set; }
         public DbSet<Role> Roles { get; set; }
     
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EInvoiceAndEReceipt.Data.DbContext
{
   public class EInvoiceDbContextFactory : IDesignTimeDbContextFactory<EInvoiceDbContext>
    {
        public EInvoiceDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EInvoiceDbContext>();
            var connectionString = configuration.GetConnectionString("conn") 
                                   ?? "Host=localhost;Database=EInvoiceAndEReceiptDb;Username=postgres;Password=postgres123";

            optionsBuilder.UseNpgsql(connectionString);

            return new EInvoiceDbContext(optionsBuilder.Options);
        }
   }
}

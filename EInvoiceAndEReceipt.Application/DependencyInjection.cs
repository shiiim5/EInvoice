using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Application.IServices;
using EInvoiceAndEReceipt.Application.Services;
using EInvoiceAndEReceipt.Data.IRepositories;
using EInvoiceAndEReceipt.Data.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace EInvoiceAndEReceipt.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITaxPayerRepository, TaxPayerRepository>();
            return services;

        }
    }
}
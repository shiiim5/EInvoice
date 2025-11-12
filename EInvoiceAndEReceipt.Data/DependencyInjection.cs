using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Configuration;
using EInvoiceAndEReceipt.Data.IRepositories;
using EInvoiceAndEReceipt.Data.Repositories;
using EInvoiceAndEReceipt.Data.Validations;
using EInvoiceAndEReceipt.Data.Validations.PipelineValidation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EInvoiceAndEReceipt.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {


            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<ETAConfig>>().Value);

            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddSingleton<ISignatureVerificationAdapter, ETIDASignatureVerificationAdapter>();
            // builder.Services.AddScoped<IDocumentValidator, CoreFieldsValidation>();
            // builder.Services.AddScoped<IDocumentValidator, SignatureValidation>();
            // builder.Services.AddScoped<IDocumentValidator, NationalIdValidation>();
            // builder.Services.AddScoped<IDocumentValidator, SimpleFieldValidation>();
            services.AddScoped<DocumentValidationPipeline>();
            services.AddScoped<IDocumentValidator, EquationValidation>();

            return services;

        }
    }
}
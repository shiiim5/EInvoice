using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Generics;

namespace EInvoiceAndEReceipt.Data.Validations
{
    public interface IDocumentValidator
    {
        string Name { get; }

        Task<DocumentValidationResult> ValidateAsync(DocumentDTO document);
    }
}
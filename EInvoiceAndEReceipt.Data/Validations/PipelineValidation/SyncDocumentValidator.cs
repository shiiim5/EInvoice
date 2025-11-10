using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Generics;

namespace EInvoiceAndEReceipt.Data.Validations
{
    public abstract class SyncDocumentValidator : IDocumentValidator
    {
        public abstract string Name { get; }

        public Task<DocumentValidationResult> ValidateAsync(DocumentDTO document)
        {
            var result = new DocumentValidationResult { InternalId = document.InternalId };
            Validate(document, result);
            return Task.FromResult(result);
        }

        protected abstract void Validate(DocumentDTO document, DocumentValidationResult result);
    }
}
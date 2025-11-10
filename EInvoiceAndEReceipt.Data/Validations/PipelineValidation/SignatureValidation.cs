using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Generics;


namespace EInvoiceAndEReceipt.Data.Validations.PipelineValidation
{
    public class SignatureValidation : IDocumentValidator
    {
        public string Name => nameof(SignatureValidation);
        private readonly ISignatureVerificationAdapter _adapter;

        public SignatureValidation(ISignatureVerificationAdapter adapter)
        {
            _adapter = adapter;
        }

     
        public async Task<DocumentValidationResult> ValidateAsync(DocumentDTO document)
        {
           var result = new DocumentValidationResult { InternalId = document.InternalId };
            var ok = await _adapter.VerifySignatureAsync(document);
            if (!ok)
            {
                result.Messages.Add(new ValidatorMessage
                {
                    Validator = Name,
                    Message = "Invalid digital signature.",
                    IsError = true,
                    Field = "Signature"


                });
            }
            return result;
        }
    }
}
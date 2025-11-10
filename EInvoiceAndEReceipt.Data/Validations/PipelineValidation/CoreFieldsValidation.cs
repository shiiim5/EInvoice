using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Generics;

namespace EInvoiceAndEReceipt.Data.Validations.PipelineValidation
{
    public class CoreFieldsValidation : SyncDocumentValidator
    {
        public override string Name => nameof(CoreFieldsValidation);

        protected override void Validate(DocumentDTO document, DocumentValidationResult result)
        {
            if (string.IsNullOrEmpty(document.InternalId))
            {
                result.Messages.Add(new ValidatorMessage
                {
                    Validator = Name,
                    Message = "InternalId is required.",
                    IsError = true,
                    Field = "InternalId"
                });
            }

            if (document.Issuer == null)
            {
                result.Messages.Add(new ValidatorMessage
                {
                    Validator = Name,
                    Message = "Issuer is required.",
                    IsError = true,
                    Field = "Issuer"
                });
            }

            if (document.Receiver == null)
            {
                result.Messages.Add(new ValidatorMessage
                {
                    Validator = Name,
                    Message = "Reciever is required.",
                    IsError = true,
                    Field = "Reciever"
                });
            }
             
             if(document.TotalAmount == 0)
            {
               result.Messages.Add(new ValidatorMessage
                {
                    Validator = Name,
                    Message = "TotalAmount must be greater than zero.",
                    IsError = true,
                    Field = "TotalAmount"
                }); 
            }
        }
    }
}
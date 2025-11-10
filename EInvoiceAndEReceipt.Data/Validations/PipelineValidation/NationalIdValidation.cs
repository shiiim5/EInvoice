using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Configuration;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Generics;

namespace EInvoiceAndEReceipt.Data.Validations.PipelineValidation
{
    public class NationalIdValidation : SyncDocumentValidator
    {
        public override string Name => nameof(NationalIdValidation);
        private readonly ETAConfig _config;

        public NationalIdValidation(ETAConfig config)
        {
            _config = config;
        }

        protected override void Validate(DocumentDTO document, DocumentValidationResult result)
        {

            if (document.Receiver.Type.Equals("Person", StringComparison.OrdinalIgnoreCase) ){
                if (document.TotalAmount >= _config.MinimalNationalIdAmount)
                {
                    if (string.IsNullOrWhiteSpace(document.Receiver.Id))
                    {
                        result.Messages.Add(new ValidatorMessage
                        {
                            Field = "Receiver.NationalId",
                            Message = "National ID is required for persons above minimal amount",
                            IsError = true
                        });
                    }
                }
            }

        }
    }
}
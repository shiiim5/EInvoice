using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;

namespace EInvoiceAndEReceipt.Data.Validations.PipelineValidation
{
    public interface ISignatureVerificationAdapter
    {
         Task<bool> VerifySignatureAsync(DocumentDTO document);
    }
}
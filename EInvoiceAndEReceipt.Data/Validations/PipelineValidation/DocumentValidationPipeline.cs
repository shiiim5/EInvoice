using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Generics;

namespace EInvoiceAndEReceipt.Data.Validations.PipelineValidation
{
    public class DocumentValidationPipeline
    {
        private readonly IList<IDocumentValidator> _validatorsOrdered;


public DocumentValidationPipeline(IEnumerable<IDocumentValidator> validatorsInOrder)
{

_validatorsOrdered = validatorsInOrder.ToList();
}


public async Task<SubmissionValidationResult> ValidateSubmissionAsync(DocumentDTO document)
{
var submissionResult = new SubmissionValidationResult();




var docResult = new DocumentValidationResult { InternalId = document.InternalId };


foreach (var validator in _validatorsOrdered)
{



var singleResult = await validator.ValidateAsync(document);



foreach (var msg in singleResult.Messages)
docResult.Messages.Add(msg);



if (singleResult.Messages.Any(m => m.IsError) &&
(validator is CoreFieldsValidation))
{
break;
}
}


submissionResult.DocumentResults.Add(docResult);



return submissionResult;
}
    }
}
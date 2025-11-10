using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Generics
{
    public class ValidatorMessage
    {
        public ValidatorMessage()
        {
            
        }
        public ValidatorMessage(string msg, bool isError)
        {
            Message = msg;
            IsError = isError;
        }
        public string Validator { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsError { get; set; } = true;
    }

    public class DocumentValidationResult
    {
        public string InternalId { get; set; } = string.Empty;
        public bool IsValid => !Messages.Any(m => m.IsError);

        public List<ValidatorMessage> Messages { get; set; } = new();
    }

    public class SubmissionValidationResult
    {
        public string SubmissionUUID { get; set; } = Guid.NewGuid().ToString();
        public List<DocumentValidationResult> DocumentResults { get; set; } = new();

        public IReadOnlyList<string> AcceptedDocumentsIds => DocumentResults
        .Where(d => d.IsValid).Select(i => i.InternalId)
        .ToList();

    }
}
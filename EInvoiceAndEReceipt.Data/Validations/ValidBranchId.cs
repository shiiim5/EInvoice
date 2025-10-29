using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Validations
{
    public class ValidBranchId:ValidationAttribute
    {
        private readonly string _type;
        public ValidBranchId(string type)
        {
            _type = type;
            
        }
        override protected ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           
            if (_type == "B" && value == null)
            {
                return new ValidationResult("BranchId cannot be null.");
            }

            return ValidationResult.Success;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Validations
{
    public class ValidateIfMandatory: ValidationAttribute
    {
        private readonly string _currencySoldPropertyName;

        public ValidateIfMandatory(string currencySoldPropertyName)
        {
            _currencySoldPropertyName = currencySoldPropertyName;
        }

        override protected ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currencySoldProperty = validationContext.ObjectType.GetProperty(_currencySoldPropertyName);
            if (currencySoldProperty == null)
            {
                return new ValidationResult($"Unknown property: {_currencySoldPropertyName}");
            }

            var currencySoldValue = currencySoldProperty.GetValue(validationContext.ObjectInstance, null) as string;

            if (currencySoldValue != "EGP")
            {
                if (value is null)
                {
                    return new ValidationResult("AmountSold can`t be null when CurrencySold is not EGP.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Validations
{
    public class ValidValues : ValidationAttribute
    {
        private readonly string _jsonFileName;
        private static  string[] _allowedValues;

        public ValidValues(string jsonFileName)
        {
            _jsonFileName = Path.Combine(AppContext.BaseDirectory, jsonFileName);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!File.Exists(_jsonFileName))
            {
                return new ValidationResult($"The JSON file '{_jsonFileName}' was not found.");
            }

            try
            {
                var json = File.ReadAllText(_jsonFileName);
                var data = System.Text.Json.JsonSerializer.Deserialize<List<CodeEntry>>(json, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _allowedValues = data?.Select(d => d.Code).ToArray() ?? Array.Empty<string>();
                if (_allowedValues.Contains(value.ToString()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"The value '{value}' is not valid. Allowed values are: {string.Join(", ", _allowedValues)}");
                }


            }
            catch (Exception ex)
            {
                return new ValidationResult($"An error occurred while validating the value: {ex.Message}");
            }

        }
          private class CodeEntry
    {
        public string Code { get; set; }
        public string Desc_En { get; set; }
        public string Desc_Ar { get; set; }
    }

    }
     
}
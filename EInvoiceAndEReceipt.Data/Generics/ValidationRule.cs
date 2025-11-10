using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInvoiceAndEReceipt.Data.Generics
{
    public class ValidationRule
    {

        public string Name { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Expression { get; set; } = string.Empty;
        public string? Condition { get; set; }
        public string Message { get; set; } = string.Empty;
    }
    
    public class ValidationRuleOptions
{
    public List<ValidationRule> SimpleFieldValidations { get; set; } = new();
}

}
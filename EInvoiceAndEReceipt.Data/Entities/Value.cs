using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Validations;

namespace EInvoiceAndEReceipt.Data.Entities
{
    public class Value
    {
        private Decimal _AmountEGP;
        public int _Id { get; set; }
        public string CurrencySold { get; set; }

        [Range(0.00001, Double.MaxValue)]
        public Decimal AmountEGP { get => _AmountEGP; set => _AmountEGP = Math.Round(value, 5, MidpointRounding.AwayFromZero); }
        [ValidateIfMandatory(nameof(CurrencySold))]
        public Decimal AmountSold { get; set; }
        [ValidateIfMandatory(nameof(CurrencySold))]
        public Decimal CurrencyExchangeRate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EInvoiceAndEReceipt.Application.Helpers;
using EInvoiceAndEReceipt.Application.IServices;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Entities;
using EInvoiceAndEReceipt.Data.IRepositories;
using Humanizer;
using Newtonsoft.Json;

namespace EInvoiceAndEReceipt.Application.Services
{
    public class InvoiceService:IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        public InvoiceService(IInvoiceRepository repository)
        {
            _repository = repository;
            
        }


        public async Task<InvoiceProcessingResult> SubmitDocumentsAsync(string contentType, Stream stream)
        {
            var result = new InvoiceProcessingResult();
            List<Invoice> invoices;

            using var reader = new StreamReader(stream);
            var body = await reader.ReadToEndAsync();

            if (contentType.Contains("application/json"))
            {
                invoices = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Invoice>>(body);
            }
            else if (contentType.Contains("application/xml") || contentType.Contains("text/xml"))
            {
                var serializer = new XmlSerializer(typeof(List<Invoice>));
                using var stringReader = new StringReader(body);
                invoices = (List<Invoice>)serializer.Deserialize(stringReader);
            }
            else
            {
                throw new InvalidDataException("Unsupported content type");

            }

            var validInvoices = new List<Invoice>();
            foreach (var invoice in invoices)
            {
                if (string.IsNullOrEmpty(invoice.InternalId))
                {
                    result.RejectedDocuments.Add(invoice);
                    continue;
                }
                bool exists = await _repository.ExistsAsync(invoice.InternalId);
                if (exists)
                {
                    result.RejectedDocuments.Add(invoice);
                    continue;
                }
                validInvoices.Add(invoice);
            }

            if (validInvoices.Any())
            {
                var savedInvoices = await _repository.AddDocumentsAsync(validInvoices);
                result.AcceptedDocuments.AddRange(savedInvoices);
                result.AcceptedDocumentsIds.AddRange(savedInvoices.Select(i => i.InternalId));

            }

            return result;

        }
        
         public async Task CancelDocumentAsync(string uuid)
        {
            var exists = await _repository.ExistsAsync(uuid);
            if (!exists)
            {
                throw new Exception("Invoice not found");
            }
            else
            {
                
            }
          
        }

    }
}
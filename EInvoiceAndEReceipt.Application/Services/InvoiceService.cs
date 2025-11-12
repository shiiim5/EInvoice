using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoMapper;
using EInvoiceAndEReceipt.Application.Generics;
using EInvoiceAndEReceipt.Application.IServices;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Entities;
using EInvoiceAndEReceipt.Data.IRepositories;
using EInvoiceAndEReceipt.Data.Validations;
using EInvoiceAndEReceipt.Data.Validations.PipelineValidation;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

namespace EInvoiceAndEReceipt.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private readonly IDocumentValidator _validator;
        // private readonly DocumentValidationPipeline _pipeline;
        private IMapper _mapper;
        public InvoiceService(IInvoiceRepository repository, IMapper mapper, IDocumentValidator validator)//, DocumentValidationPipeline pipeline)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            // _pipeline = pipeline;
        }
       public async Task<List<Invoice>> GetInvoices()
        {
            var invoices = await _repository.GetAllInvoices();
            return invoices;
        }

        // public async Task<InvoiceProcessingResult> SubmitDocumentsAsync(string contentType, Stream stream)
        // {
        //     Console.WriteLine("Inside SubmitDocumentsAsync in InvoiceService");

        //     var docs = await SerializeToDocuments(contentType, stream);
        //     var result = new InvoiceProcessingResult();
        //     var validInvoices = new List<Invoice>();





        //     foreach (var doc in docs)
        //     {
        //         if (string.IsNullOrEmpty(doc.InternalId))
        //         {
        //             //mapping
        //             Console.WriteLine("1");
        //             var rejectedInvoice = _mapper.Map<RejectedInvoice>(doc);
        //             result.RejectedDocuments.Add(rejectedInvoice);

        //             continue;
        //         }
        //         bool exists = await _repository.ExistsAsync(doc.InternalId);
        //         if (exists)
        //         {
        //             var rejectedInvoice = _mapper.Map<RejectedInvoice>(doc);
        //             result.RejectedDocuments.Add(rejectedInvoice);
        //             Console.WriteLine("2");

        //             continue;
        //         }
        //      //   var validationResult = await _pipeline.ValidateSubmissionAsync(doc);
        //         //    var validationResult = await _validator.ValidateAsync(doc);
        //         // if (!validationResult.DocumentResults.All(d => d.IsValid))
        //         // {
        //         //     var rejectedInvoice = _mapper.Map<RejectedInvoice>(doc);
        //         //     // rejectedInvoice.Errors = validationResult.DocumentResults
        //         //     //     .Select(m => m.Messages)
        //         //     //     .ToList();

        //         //     result.RejectedDocuments.Add(rejectedInvoice);
        //         //     Console.WriteLine("3");

        //         //     continue;
        //         //  }




        //         var validInvoice = _mapper.Map<Invoice>(doc);
        //         validInvoices.Add(validInvoice);
        //     }

        //     if (validInvoices.Any())
        //     {

        //         var savedInvoices = await _repository.AddDocumentsAsync(validInvoices);
        //         var acceptedDocuments = _mapper.Map<List<AcceptedInvoice>>(savedInvoices);
        //         result.AcceptedDocuments.AddRange(acceptedDocuments);
        //         result.AcceptedDocumentsIds.AddRange(savedInvoices.Select(i => i.InternalId));

        //     }

        //     return result;

        // }

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

        public async Task<InvoiceProcessingResult> SubmitDocumentsAsync(string contentType, Stream stream)
        {

            var docs = await SerializeToDocuments(contentType, stream);
            var result = new InvoiceProcessingResult();
            var validInvoices = new List<Invoice>();

            Console.WriteLine($"📦 Docs Count: {docs.Count}");
            Console.WriteLine($"🔎 First doc InternalId: {docs.FirstOrDefault()?.InternalId}");

            foreach (var doc in docs)
            {
                Console.WriteLine($"➡ Processing document: {doc.InternalId}");

                if (string.IsNullOrEmpty(doc.InternalId))
                {
                    Console.WriteLine("⛔ Skipped: Missing InternalId");
                    var rejectedInvoice = _mapper.Map<RejectedInvoice>(doc);
                    result.RejectedDocuments.Add(rejectedInvoice);
                    continue;
                }

                bool exists = await _repository.ExistsAsync(doc.InternalId);
                Console.WriteLine($"Exists in DB? {exists}");
                if (exists)
                {
                    Console.WriteLine("⛔ Skipped: Already exists in DB");
                    var rejectedInvoice = _mapper.Map<RejectedInvoice>(doc);
                    result.RejectedDocuments.Add(rejectedInvoice);
                    continue;
                }

                try
                {
                    var validInvoice = _mapper.Map<Invoice>(doc);
                    Console.WriteLine($"✅ Mapped successfully: {validInvoice.InternalId}, Issuer: {validInvoice.Issuer?.Id}");
                    validInvoices.Add(validInvoice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Mapping failed for {doc.InternalId}: {ex.Message}");
                }
            }

            Console.WriteLine($"💾 About to save {validInvoices.Count} invoices");
            return result;
        }

        public async Task<bool> UpdateDocumentAsync(DocumentDTO document)
        {
            var result = await _repository.UpdateInvoiceAsync(document);

            return result;
        }
        private async Task<List<DocumentDTO>> SerializeToDocuments(string contentType, Stream stream)
        {
            try
            {
                using var reader = new StreamReader(stream);
                var body = await reader.ReadToEndAsync();

                Console.WriteLine("Raw body:");
                Console.WriteLine(body);

                List<DocumentDTO>? docs = null;

                if (contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    docs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocumentDTO>>(body);
                }
                else if (contentType.Contains("application/xml", StringComparison.OrdinalIgnoreCase) ||
                         contentType.Contains("text/xml", StringComparison.OrdinalIgnoreCase))
                {
                    var serializer = new XmlSerializer(typeof(List<DocumentDTO>));
                    using var stringReader = new StringReader(body);
                    docs = (List<DocumentDTO>)serializer.Deserialize(stringReader);
                }
                else
                {
                    throw new InvalidDataException("Unsupported content type");
                }

                if (docs == null)
                {
                    Console.WriteLine("❌ Deserialization returned null");
                }
                else
                {
                    Console.WriteLine($"✅ Successfully deserialized {docs.Count} document(s)");

                }

                return docs ?? new List<DocumentDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Exception during deserialization: " + ex);
                return new List<DocumentDTO>();
            }
        }

          
    }
}

// [
//   {
//     "issuer": {
//       "_Id": 1,
//       "id": "203000999",
//       "type": "B",
//       "name": "Nile Tech Solutions",
//       "address": {
//         "_Id": 1,
//         "country": "EG",
//         "governate": "Cairo",
//         "regionCity": "Nasr City",
//         "street": "El Tayaran Street",
//         "buildingNumber": "15A",
//         "postalCode": "11371",
//         "floor": "3",
//         "room": "302",
//         "landMark": "Near City Stars",
//         "additionalInformation": "Headquarters",
//         "branchId": "001"
//       }
//     },
//     "receiver": {
//       "_Id": 2,
//       "id": "301000777",
//       "type": "B",
//       "name": "Delta Trading Co.",
//       "address": {
//         "_Id": 2,
//         "country": "EG",
//         "governate": "Alexandria",
//         "regionCity": "Sidi Gaber",
//         "street": "El Horreya Ave",
//         "buildingNumber": "54",
//         "postalCode": "21613",
//         "floor": "2",
//         "room": "5",
//         "landMark": "Opposite Railway Station",
//         "additionalInformation": "Warehouse Branch",
//         "branchId": "002"
//       }
//     },
//     "dateTimeIssued": "2025-11-06T10:06:01.858Z",
//     "taxpayerActivityCode": "62010",
//     "internalId": "INV-2025-001",
//     "purchaseOrderReference": "PO-1221",
//     "purchaseOrderDescription": "Purchase order for IT hardware",
//     "salesOrderReference": "SO-8789",
//     "salesOrderDescription": "Sale of laptops and accessories",
//     "proformaInvoiceNumber": "PF-123",
//     "payment": {
//       "_Id": 1,
//       "bankName": "National Bank of Egypt",
//       "bankAddress": "Nasr City Branch, Cairo",
//       "bankAccountNo": "1234567890",
//       "bankAccountIBAN": "EG1200010001234567890123456",
//       "swiftCode": "NBEGEGCX",
//       "terms": "Payment due within 30 days"
//     },
//     "delievry": {
//       "_Id": 1,
//       "approach": "Delivery via courier",
//       "packaging": "Cardboard boxes",
//       "dateValidity": "2025-11-30",
//       "exportPort": "Alexandria Port",
//       "countryOfOrigin": "EG",
//       "grossWeight": 120.5,
//       "netWeight": 115.0,
//       "terms": "Delivered Duty Paid"
//     },
//     "invoiceLines": [
//       {
//         "_Id": 1,
//         "description": "Lenovo ThinkPad E14 Laptop",
//         "itemType": "GS1",
//         "itemCode": "LTP001",
//         "unitType": "EA",
//         "quantity": 5,
//         "unitValue": {
//           "_Id": 1,
//           "currencySold": "EGP",
//           "amountEGP": 25000,
//           "amountSold": 0,
//           "currencyExchangeRate": 0
//         },
//         "salesTotal": 125000,
//         "total": 137500,
//         "valueDifference": 0,
//         "totalTaxableFees": 0,
//         "netTotal": 125000,
//         "itemsDiscount": 0,
//         "discount": {
//           "_Id": 1,
//           "rate": 10,
//           "amount": 0.00001
//         },
//         "taxableItems": [
//           {
//             "_Id": 1,
//             "taxType": "T1",
//             "amount": 12500,
//             "subType": "V001",
//             "rate": 10
//           }
//         ],
//         "internalCode": "LTP001"
//       },
//       {
//         "_Id": 2,
//         "description": "Wireless Mouse",
//         "itemType": "GS1",
//         "itemCode": "ACC002",
//         "unitType": "EA",
//         "quantity": 10,
//         "unitValue": {
//           "_Id": 2,
//           "currencySold": "EGP",
//           "amountEGP": 500,
//           "amountSold": 0,
//           "currencyExchangeRate": 0
//         },
//         "salesTotal": 5000,
//         "total": 5500,
//         "valueDifference": 0,
//         "totalTaxableFees": 0,
//         "netTotal": 5000,
//         "itemsDiscount": 0,
//         "discount": {
//           "_Id": 2,
//           "rate": 0,
//           "amount": 0.00001
//         },
//         "taxableItems": [
//           {
//             "_Id": 2,
//             "taxType": "T2",
//             "amount": 500,
//             "subType": "V002",
//             "rate": 10
//           }
//         ],
//         "internalCode": "ACC002"
//       }
//     ],
//     "totalSalesAmount": 130000,
//     "totalDiscountAmount": 12500,
//     "netAmount": 117500,
//     "taxTotals": [
//       {
//         "_Id": 1,
//         "taxType": "T1",
//         "amount": 13000
//       }
//     ],
//     "extraDiscountAmount": 0,
//     "totalItemsDiscountAmount": 12500,
//     "totalAmount": 130500,
//     "signatures": [
//       {
//         "_Id": 1,
//         "type": "I",
//         "value": "X509CertificateDataHere"
//       }
//     ],
//     "serviceDeliveryDate": "2025-11-06T10:06:01.859Z"
//   }
// ]

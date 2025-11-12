using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Antlr.Runtime;
using EInvoiceAndEReceipt.Application.IServices;
using EInvoiceAndEReceipt.Application.Services;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EInvoiceAndEReceipt.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _invoiceService.GetInvoices();
            return Ok(invoices);

        }

        [Consumes("application/json", "application/xml", "text/xml")]
        [Produces("application/json")]
        [HttpPost("documentsubmissions")]
        public async Task<IActionResult> SubmitDocumentsAsync([FromBody] List<DocumentDTO> body)
        {
            try
            {
                if (body == null || body.Count == 0)
                    return BadRequest("No documents submitted");

                var contentType = Request.ContentType ?? "application/json";
                var result = await _invoiceService.SubmitDocumentsAsync(contentType, ToStream(body));
                return Ok(
                             new
                             {
                                 Message = "Documents processed successfully",
                                 SubmissionUUID = result.SubmissionUUID,
                                 AcceptedDocuments = result.AcceptedDocuments,
                                 AcceptedDocumentsIds = result.AcceptedDocuments.Select(d => d.InternalId)
                             }
                );

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error submitting documents: {ex.Message}");

                return BadRequest(new
                {
                    Message = "Failed to process document submission.",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("documents/state/{UUID}/state")]
        public async Task<IActionResult> CancelDocumentAsync(string uuid)
        {
            try
            {
                await _invoiceService.CancelDocumentAsync(uuid);
                return Ok(new { Message = "Document cancelled successfully" });



            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("documents/update")]
        public async Task<IActionResult> UpdateDocumentAsync(DocumentDTO document)
        {
            try
            {
                await _invoiceService.UpdateDocumentAsync(document);
                return Ok(new { Message = "Document updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



        private static Stream ToStream(List<DocumentDTO> obj)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            stream.Position = 0;
            return stream;
        }
    }
}
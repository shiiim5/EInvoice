using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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


        [HttpPost("documentsubmissions")]
        public async Task<IActionResult> SubmitDocumentsAsync(List<DocumentDTO> invoices)
        {
            try
            {

                var contentType = Request.ContentType ?? "application/json";
                var result = await _invoiceService.SubmitDocumentsAsync(contentType, Request.Body);
                return Ok(
new
{
    Message = "Documents processed sucessfully",
    AcceptedDocumentsIds = result.AcceptedDocumentsIds,
    AcceptedDocuments = result.AcceptedDocuments.Select(d => d.InternalId)
}
                );

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("documents/state/{UUID}/state")]
        public async Task<IActionResult> CancelDocumentAsync(string uuid)
        {
            try
            {
                await _invoiceService.CancelDocumentAsync(uuid);
                return Ok(new { Message = "Document cancelled successfully" });
                
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
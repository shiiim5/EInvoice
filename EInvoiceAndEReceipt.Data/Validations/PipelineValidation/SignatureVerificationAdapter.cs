using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.Validations.PipelineValidation;
using EInvoiceAndEReceipt.Data.DTOs;

namespace EInvoiceAndEReceipt.Data.Validations.PipelineValidation
{
    public class ETIDASignatureVerificationAdapter : ISignatureVerificationAdapter
    {

    private readonly HttpClient _httpClient;

    public ETIDASignatureVerificationAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
        public async Task<bool> VerifySignatureAsync(DocumentDTO document)
        {
          
        var content = new StringContent(document.ToString(), Encoding.UTF8, "application/json");

       
        var response = await _httpClient.PostAsync("https://api.preprod.invoicing.eta.gov.eg/api/v1/signature/verify", content);

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadAsStringAsync();
       
        return result.Contains("\"valid\":true");
        }
    }
}
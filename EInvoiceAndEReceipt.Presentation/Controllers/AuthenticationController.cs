using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Application.IServices;
using EInvoiceAndEReceipt.Data.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EInvoiceAndEReceipt.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authenticationService.RegisterUserAsync(request.Email, request.Password,request.Name);
            if (result == false)
            {
                return BadRequest("User registration failed.");
            }


            return Ok();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
              if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Fields are missing");
            }
            var authResult = await _authenticationService.GetAccessTokenAsync(request.Email, request.Password);
            if (authResult == null)
            {
                return Unauthorized();
            }
            return Ok(authResult);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Application.IServices;
using EInvoiceAndEReceipt.Data.Auth;
using EInvoiceAndEReceipt.Data.Entities;
using EInvoiceAndEReceipt.Data.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EInvoiceAndEReceipt.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly ITaxPayerRepository _taxPayerRepository;

        public AuthenticationService(IConfiguration config, ITaxPayerRepository taxPayerRepository)
        {
            _config = config;
            _taxPayerRepository = taxPayerRepository;
        }

        public async Task<AuthenticationResponse?> GetAccessTokenAsync(string email, string password)
        {
            var existingTaxPayer = await _taxPayerRepository.GetTaxPayerByEmail(email);
            if (existingTaxPayer == null || !BCrypt.Net.BCrypt.Verify(password, existingTaxPayer.PasswordHash))
            {
                return null;
            }
          
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResponse
            {
                Token = tokenString,
                ExpiresIn = expires
            };

        }

        public async Task<bool> RegisterUserAsync(string email, string password, string name)
        {
            var existingTaxPayer = await _taxPayerRepository.GetTaxPayerByEmail(email);
            if (existingTaxPayer != null)
            {
                return false;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var taxPayer = new TaxPayer
            {
                Email = email,
                PasswordHash = hashedPassword,
                Name = name
            };

            await _taxPayerRepository.AddTaxPayerAsync(taxPayer);


            return true;
        }
    };

}
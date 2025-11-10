using AutoMapper;
using EInvoiceAndEReceipt.Application.IServices;
using EInvoiceAndEReceipt.Application.Services;
using EInvoiceAndEReceipt.Data.Configuration;
using EInvoiceAndEReceipt.Data.DbContext;
using EInvoiceAndEReceipt.Data.IRepositories;
using EInvoiceAndEReceipt.Data.Repositories;
using EInvoiceAndEReceipt.Data.Validations;
using EInvoiceAndEReceipt.Data.Validations.PipelineValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers()
.AddXmlSerializerFormatters();

//Configuration for ETA Validation
builder.Services.Configure<ETAConfig>(
    builder.Configuration.GetSection("EtaValidation")
);

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ETAConfig>>().Value);

//Dependency Injection
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddHttpClient<ETIDASignatureVerificationAdapter>();
builder.Services.AddSingleton<ISignatureVerificationAdapter, ETIDASignatureVerificationAdapter>();
// builder.Services.AddScoped<IDocumentValidator, CoreFieldsValidation>();
// builder.Services.AddScoped<IDocumentValidator, SignatureValidation>();
// builder.Services.AddScoped<IDocumentValidator, NationalIdValidation>();
// builder.Services.AddScoped<IDocumentValidator, SimpleFieldValidation>();
builder.Services.AddScoped<DocumentValidationPipeline>();
builder.Services.AddScoped<IDocumentValidator, EquationValidation>();



//Database Connection
builder.Services.AddDbContext<EInvoiceDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("conn")));

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Automapper
builder.Services.AddAutoMapper(typeof(MappingConfig));


var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();



app.Run();


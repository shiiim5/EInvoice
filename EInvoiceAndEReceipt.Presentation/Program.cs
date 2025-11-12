using System.Text;
using EInvoiceAndEReceipt.Application;

using EInvoiceAndEReceipt.Data.Configuration;
using EInvoiceAndEReceipt.Data.DbContext;

using EInvoiceAndEReceipt.Data.Validations.PipelineValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);


//Authentication Configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();
builder.Services.AddOpenApi();

builder.Services.AddControllers()
.AddXmlSerializerFormatters();

//Configuration for ETA Validation
builder.Services.Configure<ETAConfig>(
    builder.Configuration.GetSection("EtaValidation")
);
builder.Services.AddHttpClient<ETIDASignatureVerificationAdapter>();

//Dependency Injection
builder.Services.AddApplication();
builder.Services.AddData();




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
app.UseAuthentication();
app.UseAuthorization();



app.Run();






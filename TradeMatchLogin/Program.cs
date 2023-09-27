using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using TradeMatchLogin.Configuration;
using TradeMatchLogin.DataContext;
using TradeMatchLogin.DTOs;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;
using TradeMatchLogin.Validators.DTOValidators;

var builder = WebApplication.CreateBuilder(args);

// Add TradeMatchContext service to the container.
builder.Services.AddDbContext<TradeMatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TradeMatchContext")));

// Add JwtConfig service to the container.
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

// Add Authentication service to the container
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        // Switch ValidateIssuer andValidateAudience to true for production.
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false, // Look into refresh tokens as token will need to be updated in production environment
        ValidateLifetime = true
    };
});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<LoginRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<RoleRepository>();

builder.Services.AddScoped<IValidator<RegisterDTO>, RegisterDTOValidator>();

//builder.Services.AddValidatorsFromAssemblyContaining<RegisterDTO>();
//builder.Services.AddValidatorsFromAssemblyContaining<LoginDTO>();
//builder.Services.AddValidatorsFromAssemblyContaining<User>();
//builder.Services.AddValidatorsFromAssemblyContaining<Login>();
//builder.Services.AddValidatorsFromAssemblyContaining<Address>();
//builder.Services.AddValidatorsFromAssemblyContaining<Role>();

// Configure the default client.
builder.Services.AddHttpClient(Options.DefaultName, client =>
{
    client.BaseAddress = new Uri("http://localhost:5023");
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed data.
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        SeedData.Initialize(services);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred seeding the DB.");
//    }
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

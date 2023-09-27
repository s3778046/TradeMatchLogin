using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TradeMatchLogin.Configuration;
using TradeMatchLogin.DataContext;
using TradeMatchLogin.DTOs;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories;
using TradeMatchLogin.Validator;
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

// Add  Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<LoginRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<RoleRepository>();

// Add Validators
builder.Services.AddScoped<IValidator<RegisterDTO>, RegisterDTOValidator>();
builder.Services.AddScoped<IValidator<LoginDTO>, LoginDTOValidator>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Address>, AddressValidator>();
builder.Services.AddScoped<IValidator<LoginDTO>, LoginDTOValidator>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

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

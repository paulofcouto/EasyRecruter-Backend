using Easy.Application.Commands.CadastrarCandidato;
using Easy.Application.Interfaces;
using Easy.Application.Services.Autenticacao;
using Easy.Application.Validators;
using Easy.Core.Repository;
using Easy.Infrastructure.Persistence;
using Easy.Infrastructure.Persistence.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Injeção de Dependência
builder.Services.AddControllers();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

//Repository
builder.Services.AddScoped<ICandidatoRepository, CandidatoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
builder.Services.AddScoped<IJwtService, JwtService>();

//Banco de dados
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<EasyDbContext>();

//Mediator
builder.Services.AddMediatR(t => t.RegisterServicesFromAssembly(typeof(CadastrarCandidatoCommandHandler).Assembly));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostAndChromeExtension", builder =>
    {
        builder
            .SetIsOriginAllowed(origin =>
                new Uri(origin).Host == "localhost" ||
                origin == "chrome-extension://ineffafedhljcjhecomdkajcemhkplfk" ||
                origin == "https://easyrecruter.com.br" ||
                origin == "https://www.easyrecruter.com.br")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

//FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CadastrarUsuarioCommandValidator>();

// Configuração JWT
var secret = builder.Configuration["JwtConfig:Secret"] ?? throw new ArgumentNullException("JwtConfig:Secret", "A chave JWT não foi configurada.");
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Falha na autenticação: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token JWT validado com sucesso.");
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowLocalhostAndChromeExtension");

app.Run();


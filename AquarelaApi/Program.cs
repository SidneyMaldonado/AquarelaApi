using AquarelaApi.Contexts;
using AquarelaApi.Repositories;
using AquarelaApi.Repositories.Interfaces;
using AquarelaApi.UseCases;
using AquarelaApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

#if DEBUG
string sc = builder.Configuration.GetConnectionString("DefaultConnection")!;
#else
string sc = "121407171717511528154353414757331419005f161d0d6501184651505732145b161b0b080e3c16575c55461a70454652492c02083f0c185e1071573510190e15582d103e040b575c5372234a2504001605123f452a5753474428050c413b0b0a0e7623185e43570d14021013522c285c2a140c5342575a205c06101e5e3c0038160e5d42560b000b0013172555586a44427f455e4228011904330618083d002b5743475a3522101501582a0027161c09755c55330805154f311e142e5e2d404541421214071717172f04391110545951573514482713091f047026165c5e575535181a0f5231050c2e0a0c460d01067a";
sc = DecryptService.Decrypt(sc);
#endif
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(sc));

// Mappers
builder.Services.AddScoped<AquarelaApi.Mappings.ContaMapper>();
builder.Services.AddScoped<AquarelaApi.Mappings.DividaMapper>();
builder.Services.AddScoped<AquarelaApi.Mappings.UsuarioMapper>();

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IDividaRepository, DividaRepository>();

// UseCases
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<UsuarioUseCase>();
builder.Services.AddScoped<ContaUseCase>();
builder.Services.AddScoped<DividaUseCase>();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AquarelaApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira apenas o token JWT (sem o prefixo 'Bearer')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AquarelaApi v1"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;
using WebApiConfigurationn.DAL.EFCore;
using WebApiConfigurationn.DAL.Repositories.Abstract;
using WebApiConfigurationn.DAL.Repositories.Concrete.EFCore;
using WebApiConfigurationn.Entities.Auth;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApiDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddControllers().AddFluentValidation(opt=>
{
    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    opt.ImplicitlyValidateChildProperties = true;
    opt.ImplicitlyValidateRootCollectionElements = true;
});

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentity<AppUser<Guid>,IdentityRole>()
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(opt=>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    var tokenOption = builder.Configuration.GetSection("TokenOptions").Get<TokenOption>();
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOption.Issuer,
        ValidAudience = tokenOption.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey)),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

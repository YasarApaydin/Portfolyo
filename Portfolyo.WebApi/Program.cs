using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portfolyo.Business;
//using Portfolyo.DataAccess;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Options;
using Portfolyo.WebApi.Middleware;
using Scalar.AspNetCore;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));

var serviceProvider = builder.Services.BuildServiceProvider();
var jwtConfiguration = serviceProvider.GetRequiredService<IOptions<Jwt>>().Value;
builder.Services.AddAuthentication().AddJwtBearer(cfr =>
{
    cfr.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtConfiguration.Issuer,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = jwtConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
    };
});


string allowedOrigin = builder.Environment.IsDevelopment()
    ? "http://127.0.0.1:5500"
    : "https://yasarapaydinportfolyo-a5e0bwb5e8hrede5.westeurope-01.azurewebsites.net";



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddBusiness();

//builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddControllers();

//builder.Services.AddOpenApi();



builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(ctx =>
    {
        var ip = ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetTokenBucketLimiter(ip, _ => new TokenBucketRateLimiterOptions
        {
            TokenLimit = 60,
            TokensPerPeriod = 60,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0,
            ReplenishmentPeriod = TimeSpan.FromMinutes(1),
            AutoReplenishment = true
        });
    });

    options.AddPolicy("LoginPolicy", ctx =>
    {
        var ip = ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
            AutoReplenishment = true
        });
    });

    options.RejectionStatusCode = 429;
});


var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
 //   app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseDeveloperExceptionPage();
}


app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers();




app.Run();

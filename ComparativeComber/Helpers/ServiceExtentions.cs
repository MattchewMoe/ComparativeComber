using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ComparativeComber.Data;
using ComparativeComber.Helpers;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http.Features;
using ComparativeComber.Services;
using ComparativeComber.GeoServices;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;
using ComparativeComber.Entities;
using Microsoft.AspNetCore.Identity;

public static class ServiceExtensions
{
    public static IServiceCollection AddMyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddCors();
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        // Add ASP.NET Core Identity
        services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 120000000; // 120 MB
        });

        var blobServiceClient = new BlobServiceClient(configuration["AzureStorage"]);
        services.AddSingleton(blobServiceClient);

        string bingMapsApiKey = configuration["BingApiKey"];
        services.AddSingleton(new GeoLocationService(bingMapsApiKey));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<ICompSaleService, CompSaleService>();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AzureDB")));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Appraisal Buddy API", Version = "v1" });
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    public static void UseMiddlewarePipeline(this WebApplication app)
    {
        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Appraisal Buddy API V1");
            c.RoutePrefix = string.Empty;
        });
    }
}

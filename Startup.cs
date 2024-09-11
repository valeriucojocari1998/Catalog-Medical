using Catalog_Medical.Data;
using Catalog_Medical.Hubs;
using Catalog_Medical.Interfaces;
using Catalog_Medical.Models.Entities;
using Catalog_Medical.Repositories;
using Catalog_Medical.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Catalog_Medical;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Setting up the database
        Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "db_file/"));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite($"Data Source={Path.Combine(AppContext.BaseDirectory, "db_file/app.db")}");
        });

        // Configure Identity with custom table names
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // Rename Identity tables
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite($"Data Source={Path.Combine(AppContext.BaseDirectory, "db_file/app.db")}");
        });

        // Configure JWT Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
        });

        // Add CORS configuration
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                       .AllowAnyHeader()
                       .WithExposedHeaders("TokenHeader", "UsernameHeader", "Authorization");
            });
        });

        // Add SignalR
        services.AddSignalR();

        // Add Controllers with JSON options
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        // Register services and repositories with correct scope
        services.AddScoped<IMedicalTestRepository, MedicalTestRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IMedicalTestService, MedicalTestService>();
        services.AddScoped<NotificationService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svp)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Enable CORS with the defined policy
        app.UseCors("DefaultPolicy");

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // Map endpoints and SignalR hubs
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<NotificationHub>("/notificationHub");
        });
    }
}

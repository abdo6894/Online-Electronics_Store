using DAL.Data.Context;
using Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace WebAPI.Services
{
    public static class RegisterationServiceHelper
    {
        public static void RegisterationService(WebApplicationBuilder builder)
        {
            // Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.ExpireTimeSpan = TimeSpan.FromDays(14); 
               options.SlidingExpiration = true; 
               options.LoginPath = "/login";
               options.AccessDeniedPath = "/access-denied";
           });

            // Sql Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                     );

            // Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
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
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });


            // تسجيل Serilog
            //builder.Host.UseSerilog((context, services, configuration) =>
            //{
            //    configuration.ReadFrom.Configuration(context.Configuration)
            //                 .Enrich.FromLogContext()
            //                 .WriteTo.Console();
            //});

           



        }
    }
}

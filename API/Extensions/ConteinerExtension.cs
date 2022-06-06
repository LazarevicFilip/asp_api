using API.Auth;
using Application;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using DataAccess;
using Implementations.UseCases.Commands.EF;
using Implementations.UseCases.Queries.EF;
using Implementations.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Extensions
{
    public static class ConteinerExtension
    {
        public static void AddJwt(this IServiceCollection collection, AppSettings settings)
        {
            collection.AddTransient(x =>
            {
                var context = x.GetService<LibaryContext>();
                var jwtSettings = x.GetService<AppSettings>();

                return new JwtManager(context, jwtSettings.JwtSettings);
            });

            collection.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public static void AddUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var request = x.GetService<IHttpContextAccessor>();
                //var header = accessor.HttpContext.Request.Headers["Authorization"];

                //spisak cleimova tj pristup payload-u
                var claims = request.HttpContext.User;

                if (claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonymousUser();
                }

                var user = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    //mapira string u obj(zadajemo tip obj kao genericki parametar)
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };

                return user;
            });
        }
        public static void AddUseCases(this IServiceCollection collection)
        {
            collection.AddTransient<CreateAuthorValidator>();
            collection.AddTransient<RegisterUserValidator>();
            collection.AddTransient<IGetAuthorsQuery, EFGetAuthorsQuery>();
            collection.AddTransient<ICreateAuthorCommand, EFCreateAuthorCommand>();
            collection.AddTransient<IRegisterUserCommand, EFRegisteUserCommand>();

        }
        public static void AddLibaryContext(this IServiceCollection collection)
        {
            collection.AddTransient(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();

                var connection = x.GetService<AppSettings>().ConString;

                optionsBuilder.UseSqlServer(connection);
                var options = optionsBuilder.Options;
                return new LibaryContext(options);
            });
           

        }
    }
}

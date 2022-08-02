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
            collection.AddTransient<IGetAuthorsQuery, EFGetAuthorsQuery>();
            collection.AddTransient<IGetUseCaseLogsQuery, EFGetUseCaseLogsQuery>();
            collection.AddTransient<IGetCategoriesQuery, EFGetCategoriesQuery>();
            collection.AddTransient<IGetPublishersQuery, EFGetPublishersQuery>();
            collection.AddTransient<IGetBooksQuery, EFGetBooksQuery>();
            collection.AddTransient<IFindBookQuery, EfFindBookQuery>();
            collection.AddTransient<ICreateCategoryCommand, EFCreateCategoryCommand>();
            collection.AddTransient<ICreateCommentCommand, EFCreateCommentCommand>();
            collection.AddTransient<ICreatePublisherCommand, EFCreatePublisherCommand>();
            collection.AddTransient<ICreateAuthorCommand, EFCreateAuthorCommand>();
            collection.AddTransient<ICreateBookCommand, EFCreateBookCommand>();
            collection.AddTransient<ICreateOrderCommand, EFCreateOrderCommand>();
            collection.AddTransient<IGetUsersOrderQuery, EFGetUserOrdersQuery>();
            collection.AddTransient<IUpdateAuthorsCommand, EFUpdateAuthorCommand>();
            collection.AddTransient<IUpdateBookCommand, EFUpdateBookCommand>();
            collection.AddTransient<IUpdatePublisherCommand, EFUpdatePublisherCommand>();
            collection.AddTransient<IUpdateCategoryCommand, EFUpdateCategoryCommand>();
            collection.AddTransient<IRegisterUserCommand, EFRegisteUserCommand>();
            collection.AddTransient<IUpdateUserUseCasesCommand, EFUpdateUserUseCases>();
            collection.AddTransient<IDeleteAuthorCommand, EFDeleteAuthorCommand>();
            collection.AddTransient<IDeletePublisherCommand, EFDeletePublisherCommand>();
            collection.AddTransient<IDeleteCategoryCommand, EFDeleteCategoryCommand>();
            collection.AddTransient<IDeleteCommentCommand, EFDeleteCommentCommand>();
            collection.AddTransient<IDeleteBookCommand, EFDeleteBookCommand>();
            collection.AddTransient<IFindAuthorQuery, EfFindAuthorQuery>();
            collection.AddTransient<IFindCategoryQuery, EfFindCategoryQuery>();
            collection.AddTransient<IFindPublisherQuery, EFFindPublisherQuery>();
            collection.AddTransient<IFindBookCommentsQuery, EFFindBookCommentsQuery>();


        }

        public static void AddValidators(this IServiceCollection collection)
        {
            collection.AddTransient<CreateOrderValidator>();
            collection.AddTransient<UpdateBookValidator>();
            collection.AddTransient<CreateUseCaseLogSearchValidator>();
            collection.AddTransient<CreateCommentValidator>();
            collection.AddTransient<OrderLineValidator>();
            collection.AddTransient<CreateAuthorValidator>();
            collection.AddTransient<UpdatePublisherValidator>();
            collection.AddTransient<CreateCategoryValidator>();
            collection.AddTransient<RegisterUserValidator>();
            collection.AddTransient<UpdateAuthorValidator>();
            collection.AddTransient<UpdateCategoryValidator>();
            collection.AddTransient<CreateBookValidator>();
            collection.AddTransient<CreatePublisherValidator>();
            collection.AddTransient<UpdateUserUseCaseValidator>();
        }

        //public static void AddLibaryContext(this IServiceCollection collection)
        //{
        //    alternativa
        //    collection.AddDbContext<LibaryContext>(ottions => options.useSqlServer(Builder.Configuration.GetConnectionString("")));
        //    collection.AddTransient(x =>
        //    {
        //        var optionsBuilder = new DbContextOptionsBuilder();

        //        var connection = x.GetService<AppSettings>().ConString;

        //        optionsBuilder.UseSqlServer(connection);
        //        var options = optionsBuilder.Options;
        //        return new LibaryContext(options);
        //    });
        //}
    }
}

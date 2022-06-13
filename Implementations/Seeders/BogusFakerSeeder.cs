using Application.Exceptions;
using Application.Seeders;
using Bogus;
using DataAccess;
using Domain.Entities;
using Implementations.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Seeders
{
    public class BogusFakerSeeder : EFUseCaseConnection,IFakeDataSeed
    {
        public BogusFakerSeeder(LibaryContext context) : base(context)
        {
        }

        public void Seed()
        {
            if (Context.Books.Any())
            {
                throw new UseCaseConflctException("Database is already seeded.");
            }
            var authorFaker = new Faker<Author>();
            authorFaker.RuleFor(x => x.Name, x => x.Person.FullName);
            var authors = authorFaker.Generate(30);

            var bookFaker = new Faker<Book>();
            bookFaker.RuleFor(x => x.Title, x => x.Lorem.Word());
            bookFaker.RuleFor(x => x.Description, x => x.Lorem.Text());
            bookFaker.RuleFor(x => x.Format, x => x.Lorem.Slug());
            bookFaker.RuleFor(x => x.Isbn, x => x.Address.ZipCode());
            bookFaker.RuleFor(x => x.PagesCount, x => x.Random.Int(30, 2000));
            bookFaker.RuleFor(x => x.Price, x => x.Random.Decimal(1, 2000));
            bookFaker.RuleFor(x => x.Author, x => x.PickRandom(authors));
            var books = bookFaker.Generate(60);

            var bookPublisherFaker = new Faker<BookPublishers>();
            bookPublisherFaker.RuleFor(x => x.Book, x => x.PickRandom(books));

            var publisherFaker = new Faker<Publisher>();
            publisherFaker.RuleFor(x => x.Name, x => x.Person.FullName);
            publisherFaker.RuleFor(x => x.PublisherBooks, x => bookPublisherFaker.Generate(3));

            var publishers = publisherFaker.Generate(10);


            var arr = new List<int>();
            for (int i = 1; i <= 30; i++)
            {
                arr.Add(i);
            }

            var user = new User();

            user.Email = "admin@asp.com";
            user.Password = BCrypt.Net.BCrypt.HashPassword("Admin321!");
            user.UserName = "admin";
            user.FirstName = "Admin";
            user.LastName = "Admin";
            var usecase = arr.Select(x => new UserUseCase
            {
                UseCaseId = x,
                User = user
            });
            //var categoriesFaker = new Faker<Category>();
            //categoriesFaker.RuleFor(x => x.Name, x => x.Lorem.Word());
            //var cats = categoriesFaker.Generate(4);
            //categoriesFaker.RuleFor(x => x.ParentCategory, x => x.PickRandom(cats));
            //cats = categoriesFaker.Generate(8);
          
            //var catBookFaker = new Faker<BookCategories>();
            //catBookFaker.RuleFor(x => x.Category, x => x.PickRandom(cats));
            //catBookFaker.RuleFor(x => x.Book, x => x.PickRandom(books));
            //var catBooks = catBookFaker.Generate(5);


            //Context.Categories.AddRange(cats);
            //Context.BookCategories.AddRange(catBooks);
            Context.UserUseCases.AddRange(usecase);
            Context.Users.Add(user);
            Context.Publishers.AddRange(publishers);
            Context.Authors.AddRange(authors);
            Context.Books.AddRange(books);

            Context.SaveChanges();


        }
    }
}

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

            Context.Publishers.AddRange(publishers);
            Context.Authors.AddRange(authors);
            Context.Books.AddRange(books);

            Context.SaveChanges();


        }
    }
}

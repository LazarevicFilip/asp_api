using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class LibaryContext : DbContext 
    {
        public LibaryContext(DbContextOptions options)
            : base(options)
        {

        }
        //public LibaryContext()
        //{

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<BookCategories>().HasKey(x => new { x.BookId,x.CategoryId });
            modelBuilder.Entity<BookPublishers>().HasKey(x => new { x.BookId,x.PublisherId });
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId,x.UseCaseId });
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //DESKTOP-44C9J4P\SQLEXPRESS
        //    //FILIP-PC\SQLEXPRESS
        //    optionsBuilder.UseSqlServer(@"Data Source=FILIP-PC\SQLEXPRESS;Initial Catalog=libary;Integrated Security=True");
        //}
        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries())
            {
                if(entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.CreatedAt = DateTime.UtcNow;
                            e.IsActive = true;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = "";
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategories> BookCategories { get; set; }
        public DbSet<BookPublishers> BookPublishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<BookImage> BookImages { get; set; }


    }
}
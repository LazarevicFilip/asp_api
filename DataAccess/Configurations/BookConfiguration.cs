using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class BookConfiguration : EntityConfiguration<Book>
    {
        public override void BaseConfiguring(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Format).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Isbn).IsRequired().IsFixedLength(true).HasMaxLength(100);
            //builder.HasAlternateKey(x => x.Isbn);

            builder.HasMany(x => x.BookCategories)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.BookPublishers)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

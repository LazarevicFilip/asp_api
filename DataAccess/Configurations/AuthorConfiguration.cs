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
    public class AuthorConfiguration : EntityConfiguration<Author>
    {
        public override void BaseConfiguring(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);

            builder.HasIndex(x => x.Name).IsUnique(true);

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

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
    public class PublisherConfiguration : EntityConfiguration<Publisher>
    {
        public override void BaseConfiguring(EntityTypeBuilder<Publisher> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
            builder.HasIndex(x => x.Name).IsUnique(true);

            builder.HasMany(x => x.PublisherBooks)
                .WithOne(x => x.Publisher)
                .HasForeignKey(x => x.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

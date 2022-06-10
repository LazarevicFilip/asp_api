using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    internal class BookImageConfiguration : IEntityTypeConfiguration<BookImage>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BookImage> builder)
        {
            builder.Property(x => x.Path).IsRequired().HasMaxLength(100);
        }
    }
}

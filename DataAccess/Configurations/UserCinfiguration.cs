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
    public class UserCinfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired(true).HasMaxLength(80);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(40);

            builder.HasIndex(x => x.Email).IsUnique(true);
            builder.HasIndex(x => x.UserName).IsUnique(true);
            builder.HasIndex(x => x.LastName);
            builder.HasIndex(x => x.FirstName);

            builder.HasMany(x => x.UseCases).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}

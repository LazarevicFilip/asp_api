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
    public class UseCaseLoggerConfiguration : IEntityTypeConfiguration<UseCaseLogger>
    {
        public void Configure(EntityTypeBuilder<UseCaseLogger> builder)
        {
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(60);
            builder.Property(x => x.UseCaseName).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.IsAuthorized).IsRequired();
            builder.Property(x => x.ExecutionTime).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.HasIndex(x => x.UseCaseName);
            builder.HasOne(x => x.User).WithMany(x => x.UseCaseLoggs).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

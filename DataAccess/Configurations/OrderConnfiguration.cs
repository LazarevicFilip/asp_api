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
    public class OrderConnfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            builder.Property(x => x.SumPrice).IsRequired();
            builder.Property(x => x.Adress).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Recipient).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Recipient);
            builder.HasMany(x => x.OrderLines).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User).WithMany(x => x.Orders).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

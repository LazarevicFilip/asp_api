using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    internal class ImageConfiguration : EntityConfiguration<Image>
    {
        public override void BaseConfiguring(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Path).IsRequired().HasMaxLength(100);

        }
    }
}

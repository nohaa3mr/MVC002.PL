using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC002.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC002.DAL.Configuation
{
    internal class DapartmentConf : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Code).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(d => d.Name).HasColumnType("nvarchar").HasMaxLength(50); 

        }
    }
}

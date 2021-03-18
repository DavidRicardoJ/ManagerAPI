using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("VARCHAR(80)")
                .HasMaxLength(80)
                .HasColumnName("name");
            
            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnType("VARCHAR(60)")
                .HasMaxLength(60)
                .HasColumnName("password");
            
            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100)
                .HasColumnName("email");


        }
    }
}

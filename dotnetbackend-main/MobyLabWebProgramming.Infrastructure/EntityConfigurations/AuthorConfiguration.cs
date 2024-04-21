using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(e => e.Id) // This specifies which property is configured.
            .IsRequired(); // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Name)
            .HasMaxLength(255) // This specifies the maximum length for varchar type in the database.
            .IsRequired();
        builder.Property(e => e.Nationality)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Biography)
            .HasMaxLength(4095)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}

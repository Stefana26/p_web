using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(e => e.Id) // This specifies which property is configured.
            .IsRequired(); // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id); // Here it is specifies that the property Id is the primary key.
        builder.Property(e => e.Title)
            .HasMaxLength(255) // This specifies the maximum length for varchar type in the database.
            .IsRequired();
        builder.Property(e => e.Author)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Pages)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}

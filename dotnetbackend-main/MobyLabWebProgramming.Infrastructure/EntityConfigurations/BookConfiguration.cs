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
        builder.Property(e => e.Description)
            .HasMaxLength(4095)
            .IsRequired();
        builder.Property(e => e.Pages)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        //// many-to-one relation between book and author
        builder.HasOne(e => e.Author) // This specifies a one-to-many relation.
            .WithMany(e => e.Books) // This provides the reverse mapping for the one-to-many relation. 
            .HasForeignKey(e => e.AuthorId) // Here the foreign key column is specified.
            .HasPrincipalKey(e => e.Id) // This specifies the referenced key in the referenced table.
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // many-to-one relation between book and genre
        builder.HasOne(e => e.Genre) // This specifies a one-to-many relation.
            .WithMany(e => e.Books) // This provides the reverse mapping for the one-to-many relation. 
            .HasForeignKey(e => e.GenreId) // Here the foreign key column is specified.
            .HasPrincipalKey(e => e.Id) // This specifies the referenced key in the referenced table.
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

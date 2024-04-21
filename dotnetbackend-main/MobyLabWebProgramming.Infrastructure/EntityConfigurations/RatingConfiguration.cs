using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.Property(e => e.Id) // This specifies which property is configured.
            .IsRequired(); // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Value)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        // many-to-one relation between review and book
        builder.HasOne(e => e.Book) // This specifies a one-to-many relation.
            .WithMany(e => e.Ratings) // This provides the reverse mapping for the one-to-many relation. 
            .HasForeignKey(e => e.BookId) // Here the foreign key column is specified.
            .HasPrincipalKey(e => e.Id) // This specifies the referenced key in the referenced table.
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // many-to-one relation between review and user 
        builder.HasOne(e => e.User)
            .WithMany(e => e.Ratings)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

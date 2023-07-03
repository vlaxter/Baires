using Baires.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Baires.Infrastructure.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasIndex(t => t.PersonId)
            .IsUnique();

        builder.Property(t => t.FirstName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(t => t.CurrentRole)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Country)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(t => t.Industry)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.PriorityIndex)
            .HasColumnType("decimal(18,6)");
    }
}

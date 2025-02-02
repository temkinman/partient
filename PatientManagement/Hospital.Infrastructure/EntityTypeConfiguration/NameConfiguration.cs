using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.EntityTypeConfiguration;

public class NameConfiguration : IEntityTypeConfiguration<Name>
{
    public void Configure(EntityTypeBuilder<Name> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Use)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Family)
            .IsRequired()
            .HasMaxLength(100);
    }
}
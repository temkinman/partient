using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.EntityTypeConfiguration;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Name)
            .WithOne()
            .HasForeignKey<Patient>(x => x.NameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Active)
            .IsRequired();

        builder.Property(x => x.Gender)
            .IsRequired();

        builder.Property(x => x.BirthDate)
            .IsRequired();
        
        builder.HasIndex(x => x.BirthDate);
    }
}
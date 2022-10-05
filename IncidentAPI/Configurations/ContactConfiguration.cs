using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IncidentAPI.Models;

namespace IncidentAPI.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(t => t.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(t => t.Email)
               .IsUnique();
        }
    }
}

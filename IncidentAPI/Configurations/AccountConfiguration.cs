using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IncidentAPI.Models;

namespace IncidentAPI.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(t => t.Name)
               .IsUnique();

            builder.HasOne(t => t.Contact)
                .WithMany(t => t.Accounts)
                .IsRequired();

            builder.HasMany(t => t.Incidents)
                .WithOne(t => t.Account);
        }
    }
}

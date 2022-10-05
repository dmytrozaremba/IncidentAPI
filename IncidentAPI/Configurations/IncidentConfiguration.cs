using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IncidentAPI.Models;

namespace IncidentAPI.Configurations
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.HasKey(t => t.Name);

            builder.Property(t => t.Description)
                .IsRequired();
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(d => d.Title).IsRequired();
            builder.Property(d => d.Description).IsRequired();
            builder.Property(d => d.Icon).IsRequired();
        }
    }


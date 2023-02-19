
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {

        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(d => d.Name).IsRequired();
            builder.Property(d => d.About).IsRequired();
            builder.Property(d => d.Email).IsRequired();
        }
    }


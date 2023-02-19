
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(s => s.Address).IsRequired();
            builder.Property(s => s.Phone).IsRequired();
            builder.Property(s => s.Email).IsRequired();
            builder.Property(s => s.Logo).IsRequired();
        }
    }

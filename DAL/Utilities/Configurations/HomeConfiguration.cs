
using Microsoft.EntityFrameworkCore;


    public class HomeConfiguration : IEntityTypeConfiguration<Home>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Home> builder)
        {
            builder.Property(h => h.Image).IsRequired();
            builder.Property(h => h.Slogan).IsRequired();
            builder.Property(h => h.Title).IsRequired();
        }
    }


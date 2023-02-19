
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(m => m.UserName).IsRequired();
            builder.Property(m => m.Email).IsRequired();
            builder.Property(m => m.Subject).IsRequired();
            builder.Property(m => m.Letter).IsRequired();
        }
    }


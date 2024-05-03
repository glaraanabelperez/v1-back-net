using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Abrazos.Persistence.Database.Configurations
{
    public class UserLanguageConfiguration : IEntityTypeConfiguration<UserLanguage>
    {
        public void Configure(EntityTypeBuilder<UserLanguage> builder)
        {
            builder.HasKey(e => e.UserLanguageId);
            builder.ToTable("UserLanguage");
            builder.Property(e => e.UserLanguageId)
                .HasColumnType("int")
                .HasColumnName("UserLanguageId");
        
            builder.Property(e => e.UserrId)
              .HasColumnName("UserrId");


            builder.Property(e => e.LanguageId)
              .HasColumnName("LanguageId");

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Abrazos.Persistence.Database.Configurations
{
    public class ProfileDancerConfiguration : IEntityTypeConfiguration<ProfileDancer>
    {
        public void Configure(EntityTypeBuilder<ProfileDancer> builder)
        {
            builder.HasKey(e => e.ProfileDanceId);
            builder.ToTable("ProfileDancer");
            builder.Property(e => e.ProfileDanceId)
                .HasColumnType("int")
                .HasColumnName("ProfileDanceId");

            builder.Property(e => e.DanceLevelId)
              .HasColumnName("DanceLevel");
            builder.Property(e => e.DanceRolId)
             .HasColumnName("DanceRol");
            builder.Property(e => e.UserId)
              .HasColumnName("UserId");
            builder.Property(e => e.Height)
             .HasColumnName("Height");

            builder.HasOne(e => e.DanceLevel)
                .WithMany()
                .HasForeignKey(e => e.DanceLevelId);
            builder.HasOne(e => e.DanceRol)
               .WithMany()
               .HasForeignKey(e => e.DanceRolId);

            builder.HasOne(e => e.Users)
               .WithMany()
               .HasForeignKey(e => e.UserId);
        }
    }
}

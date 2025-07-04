using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Abrazos.Persistence.Database.Configurations
{
    public class UserEventInscriptionConfiguration : IEntityTypeConfiguration<UserEventInscription>
    {
        public void Configure(EntityTypeBuilder<UserEventInscription> builder)
        {
            builder.HasKey(e => e.UserEventInscriptionId);
            builder.ToTable("UserEventInscription");
            builder.Property(e => e.UserEventInscriptionId)
                .HasColumnType("int")
                .HasColumnName("UserEventInscriptionId");

            builder.Property(e => e.UserId)
              .HasColumnName("UserId");
            builder.Property(e => e.EventId)
                .HasColumnName("EventId");
            builder.Property(e => e.State)
                .HasColumnName("State");
            builder.Property(e => e.ProfileDancerId)
                          .HasColumnName("ProfileDancerId");
            builder.Property(e => e.Partner)
                          .HasColumnName("Partner");
            builder.HasOne(e => e.Profile)
                .WithMany(w => w.UserEventInscription)
                .HasForeignKey(e => e.ProfileDancerId);

            builder.HasOne(e => e.Event)
                .WithMany(w => w.UserEventInscriptions)
                .HasForeignKey(e => e.EventId);

            builder.HasOne(e => e.User)
                .WithMany(w => w.UserEventInscriptions)
                .HasForeignKey(e => e.UserId);
        }
    }
}

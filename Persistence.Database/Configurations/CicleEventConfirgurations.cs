using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Abrazos.Persistence.Database.Configurations
{
    public class CiclEventConfiguration : IEntityTypeConfiguration<CycleEvent>
    {
        public void Configure(EntityTypeBuilder<CycleEvent> builder)
        {
            builder.HasKey(e => e.CycleId);
            builder.ToTable("CycleEvent");
            builder.Property(e => e.CycleId)
                .HasColumnType("int")
                .HasColumnName("CycleEventId");

            builder.Property(e => e.EventId)
              .HasColumnName("EventId_FK");

            builder.Property(e => e.CycleId)
              .HasColumnName("CycleId_FK");

            builder.HasOne(c => c.Cycle)
                .WithMany(c => c.CycleEvents)
                .HasForeignKey(c => c.CycleId);

            builder.HasOne(c => c.Event)
               .WithMany(c => c.CycleEvents)
               .HasForeignKey(c => c.EventId);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Abrazos.Persistence.Database.Configurations
{
    public class CiclEventConfiguration : IEntityTypeConfiguration<CycleEvent>
    {
        public void Configure(EntityTypeBuilder<CycleEvent> builder)
        {
            builder.HasKey(e => e.CyclEventId);
            builder.ToTable("CycleEvent");
            builder.Property(e => e.CyclEventId)
                .HasColumnType("int")
                .HasColumnName("CycleEventId");

            builder.Property(e => e.EventId)
              .HasColumnName("EventId_FK");

            builder.Property(e => e.CycleId)
              .HasColumnName("CycleId_FK");

            builder.HasOne(c => c.Cycle)
                .WithMany(d => d.CycleEvents)
                .HasForeignKey(d => d.CycleId);

            builder.HasOne(c => c.Event)
               .WithMany(d => d.CycleEvents)
               .HasForeignKey(d => d.EventId);

        }
    }
}

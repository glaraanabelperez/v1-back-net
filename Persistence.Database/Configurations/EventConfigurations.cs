using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Abrazos.Persistence.Database.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);
            builder.ToTable("Event");
            builder.Property(e => e.EventId)
                .HasColumnType("int")
                .HasColumnName("EventId");

            builder.Property(e => e.Name)
              .HasColumnName("Name");
            builder.Property(e => e.UserIdCreator_FK)
             .HasColumnName("UserIdCreator_FK");

            builder.Property(e => e.Description)
            .HasColumnName("Description");

            builder.Property(e => e.AddressId_fk)
            .HasColumnName("AddressId_fk");

            builder.Property(e => e.Image)
            .HasColumnName("Image");
            builder.Property(e => e.DateInit)
            .HasColumnName("DateInit");
            builder.Property(e => e.DateFinish)
            .HasColumnName("DateFinish");

            builder.Property(e => e.EventStateId_fk)
            .HasColumnName("EventStateId_fk");

            builder.Property(e => e.TypeEventId_fk)
           .HasColumnName("TypeEventId_fk");

            builder.Property(e => e.Cupo)
           .HasColumnName("Cupo");

            builder.Property(e => e.LevelId)
           .HasColumnName("LevelIdFK");

            builder.Property(e => e.RolId)
           .HasColumnName("RolIdFK");

            builder.HasOne(w => w.Address)
                    .WithMany(e => e.Events)
                    .HasForeignKey(e=> e.AddressId_fk);

            builder.HasOne(w => w.UserCreator)
                    .WithMany(e => e.EventsCreated)
                    .HasForeignKey(w => w.UserIdCreator_FK);

            builder.HasOne(e => e.TypeEvent_)
               .WithMany(Events => Events.events)
               .HasForeignKey(Event => Event.TypeEventId_fk);

            builder.HasOne(e => e.EventState_)
              .WithMany(e => e.Events)
              .HasForeignKey(e => e.EventStateId_fk);

            builder.HasMany(e => e.CouplesEvents)
             .WithOne(e => e.Evento)
             .HasForeignKey(e => e.EventId_FK);

            builder.HasMany(ce => ce.CycleEvents)
             .WithOne(c => c.Event)
             .HasForeignKey(c => c.EventId);

            builder.HasOne(ce => ce.Level)
          .WithMany(c => c.Events)
          .HasForeignKey(c => c.LevelId);

            builder.HasOne(ce => ce.Rol)
          .WithMany(c => c.Events)
          .HasForeignKey(c => c.RolId);

        }
    }
}

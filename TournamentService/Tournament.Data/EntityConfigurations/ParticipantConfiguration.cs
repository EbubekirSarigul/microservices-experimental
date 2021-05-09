using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Data.Entities;

namespace Tournament.Data.EntityConfigurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Tournament.Data.Entities.Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("participant", "dft");
            builder.HasKey(x => x.Id);

            builder.Property("Id").UsePropertyAccessMode(PropertyAccessMode.Property).ValueGeneratedNever().HasColumnName("ID").IsRequired();

            builder.Property("PlayerId").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("PLAYER_ID").IsRequired();

            builder.HasOne(x => x.Tournament).WithMany(x => x.Participants).HasForeignKey(x => x.TournamentId);
            builder.Property(x => x.TournamentId).UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("TOURNAMENT_ID");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tournament.Data.EntityConfigurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament.Data.Entities.Tournament>
    {
        public void Configure(EntityTypeBuilder<Entities.Tournament> builder)
        {
            builder.ToTable("tournament", "dft");
            builder.HasKey(x => x.Id);


            builder.Property("Id").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("ID").IsRequired();

            builder.Property("Name").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("NAME").HasMaxLength(50).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property("Date").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("DATE").IsRequired();

            builder.Property("EntryPrice").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("ENTRY_PRICE").HasPrecision(10).IsRequired();

            builder.Property("Address").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("ADDRESS").HasMaxLength(500).IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Tournament.Data.Entities.Tournament.Participants));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

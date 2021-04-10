using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Player.Data.EntityConfigurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player.Data.Entities.Player>
    {
        public void Configure(EntityTypeBuilder<Entities.Player> builder)
        {
            builder.ToTable("player", "ply");
            builder.HasKey(x => x.Id);

            builder.Property("Id").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("ID").IsRequired();

            builder.Property("Name").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("NAME").HasMaxLength(50).IsRequired();

            builder.Property("Surname").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("SURNAME").HasMaxLength(50).IsRequired();

            builder.Property("PhoneNumber").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("PHONE_NUMBER").HasMaxLength(20).IsRequired();

            builder.Property("Rating").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("RATING").IsRequired();
        }
    }
}

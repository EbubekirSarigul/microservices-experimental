using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Player.Data.Entities;

namespace Player.Data.EntityConfigurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("order_details", "payment");
            builder.HasKey(x => x.Id);

            builder.Property("TournamentId").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("TOURNAMENT_ID").HasMaxLength(50).IsRequired();

            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
            builder.Property(x => x.OrderId).UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("ORDER_ID");
        }
    }
}

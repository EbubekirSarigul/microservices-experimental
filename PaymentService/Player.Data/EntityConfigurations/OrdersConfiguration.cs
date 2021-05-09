using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Player.Data.Entities;

namespace Player.Data.EntityConfigurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.ToTable("orders", "payment");
            builder.HasKey(x => x.Id);

            builder.Property("Id").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("ID").IsRequired();

            builder.Property("TotalPrice").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("TOTAL_PRICE").HasPrecision(10).IsRequired();

            builder.Property("PaymentId").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("PAYMENT_ID").IsRequired();

            builder.Property("PlayerId").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("PLAYER_ID").IsRequired();

            builder.Property("OrderStatus").UsePropertyAccessMode(PropertyAccessMode.Property).HasColumnName("ORDER_STATUS").HasMaxLength(20).IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Orders.OrderDetails));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

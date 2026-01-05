using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiConfigurationn.Entities;

namespace WebApiConfigurationn.DAL.Configuration
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(o => o.Quantity)
                .IsRequired();
         
            builder.Property(o=>o.UnitPrice)
                .IsRequired()
                .HasPrecision(18,2);

        }
    }
}

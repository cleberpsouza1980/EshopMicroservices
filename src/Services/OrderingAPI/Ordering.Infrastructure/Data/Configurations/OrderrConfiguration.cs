using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enumns;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderrConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(
                    orderId => orderId.Value,
                    dbId => OrderId.Of(dbId)
                );

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustumerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Order.OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.ShippingAdress, adressBuilder =>
                {
                    adressBuilder.Property(a => a.FirstName)
                        .HasMaxLength(50)
                        .IsRequired();
                    adressBuilder.Property(a => a.LastName)
                        .HasMaxLength(50)
                        .IsRequired();
                    adressBuilder.Property(a => a.Email)
                        .HasMaxLength(50)
                        .IsRequired();
                    adressBuilder.Property(a => a.AddressLine)
                        .HasMaxLength(180)
                        .IsRequired();
                    adressBuilder.Property(a => a.Country)
                        .HasMaxLength(50);
                    adressBuilder.Property(a => a.State)
                        .HasMaxLength(50);
                });

            builder.Property(o=>o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    status => status.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus),dbStatus)
                );

            builder.Property(o => o.TotalPrice);
                
        }
    }
}

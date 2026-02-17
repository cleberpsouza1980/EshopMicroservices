namespace Ordering.Infrastructure.Data.Configurations;

public class CustumerConfiguration : IEntityTypeConfiguration<Custumer>
{
    public void Configure(EntityTypeBuilder<Custumer> builder)
    {
        builder.HasKey(c=> c.Id);
        builder.Property(c => c.Id)
            .HasConversion(
                custumerId => custumerId.Value,
                dbId => CustumerId.Of(dbId)
            );

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(c => c.Email)
            .IsUnique();
    }
}

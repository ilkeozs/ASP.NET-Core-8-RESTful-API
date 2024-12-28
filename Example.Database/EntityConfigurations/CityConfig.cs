using Example.DomainLayer;
using Example.DomainLayer.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Database.EntityConfigurations;

public class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(nameof(City), "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Status).HasDefaultValue(DataStatus.Active);
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
    }
}
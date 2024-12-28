using Example.DomainLayer;
using Example.DomainLayer.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Database.EntityConfigurations;

public class DistrictConfig : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable(nameof(District), "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Status).HasDefaultValue(DataStatus.Active);
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();

        builder.HasOne(s => s.City).WithMany(s => s.Districts).HasForeignKey(s => s.CityId).OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }
}
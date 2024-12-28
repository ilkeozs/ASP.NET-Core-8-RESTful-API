using Example.DomainLayer.Shared;
using Example.DomainLayer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Example.Database.EntityConfigurations;

public class PersonnelConfig : IEntityTypeConfiguration<Personnel>
{
    public void Configure(EntityTypeBuilder<Personnel> builder)
    {
        builder.ToTable(nameof(Personnel), "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Status).HasDefaultValue(DataStatus.Active);
        builder.Property(x => x.FullName).HasMaxLength(500).IsRequired();

        builder.HasOne(s => s.District).WithMany(s => s.Personnels).HasForeignKey(s => s.DistrictId).OnDelete(DeleteBehavior.NoAction).IsRequired();
    }
}
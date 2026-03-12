using CardBrands.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBrands.Infrastructure.EntityConfigurations;

public class MarcaAutoConfiguration : BaseEntityConfiguration<MarcaAuto>
{
    public override void Configure(EntityTypeBuilder<MarcaAuto> builder)
    {
        base.Configure(builder);

        builder.ToTable("MarcasAutos");

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(m => m.OriginCountry)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.FoundingDate)
            .IsRequired();

        builder.Property(m => m.WebSite)
            .HasMaxLength(200);

        builder.Property(m => m.Value)
            .HasColumnType("decimal(18,2)");
    }
}

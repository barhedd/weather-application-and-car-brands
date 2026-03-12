using CardBrands.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBrands.Infrastructure.EntityConfigurations;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.IsDeleted)
            .IsRequired();

        builder.Property(e => e.CreatedDate)
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.EditedDate);

        builder.Property(e => e.EditedBy)
            .HasMaxLength(100);

        builder.Property(e => e.DeletedDate);

        builder.Property(e => e.DeletedBy)
            .HasMaxLength(100);
    }
}

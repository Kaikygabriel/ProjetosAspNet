using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsApi.Domain.BackOffice.Entities;

namespace ProductsApi.Infraestruct.Data.Mapping;

public class ProductsMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(x => x.Name)
            .HasMaxLength(100).HasColumnType("NVARCHAR").IsRequired();
        
        builder.Property(x => x.Price)
            .HasColumnType("MONEY").IsRequired();
        
        builder.OwnsOne(x => x.Category, a =>
        {
            a.Property(p => p.Name)
                .HasColumnName("Category").HasMaxLength(100).IsRequired();
        });
    }
}
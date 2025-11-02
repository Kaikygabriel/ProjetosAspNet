using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsApi.Domain.BackOffice.Entities;
using ProductsApi.Domain.BackOffice.ObjectValue;

namespace ProductsApi.Infraestruct.Data.Mapping;

public class UsersMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(70)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasConversion(v => v.Address, v => new Email(v))
            .HasColumnName("Email")
            .IsRequired();
    }
}
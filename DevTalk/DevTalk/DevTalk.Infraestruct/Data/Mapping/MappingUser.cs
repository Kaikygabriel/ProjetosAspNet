using DevTalk.Domain.BackOffice.Entities;
using DevTalk.Domain.BackOffice.ObjectValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTalk.Infraestruct.Data.Mapping;

public class MappingUser : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasConversion(x => x.Address, x => new Email(x))
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR(130)")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnType("NVARCHAR(130)")
            .IsRequired();
        
        builder.Property(x => x.Password)
            .HasColumnType("NVARCHAR(130)")
            .IsRequired();
        
    }
}
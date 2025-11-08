using DevTalk.Domain.BackOffice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTalk.Infraestruct.Data.Mapping;

public class MappingMessage : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title)
            .HasColumnType("NVARCHAR(120)")
            .IsRequired();
        builder.Property(x => x.Description)
            .HasColumnType("NVARCHAR(250)")
            .IsRequired();
    }
}
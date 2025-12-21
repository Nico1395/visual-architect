using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Authentication.Domain;

namespace VisualArchitect.Api.Authentication.Infrastructure.EntityConfigurations;

internal sealed class IdentityEntityConfiguration : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.ToTable(name: "identity", schema: "authentication");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("id").IsRequired();
        builder.Property(i => i.Email).HasColumnName("email").HasMaxLength(254).IsRequired();
        builder.Property(i => i.DisplayName).HasColumnName("display_name").HasMaxLength(100).IsRequired();
        builder.Property(i => i.AvatarUrl).HasColumnName("avatar_url").HasMaxLength(2048);
        builder.Property(i => i.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(i => i.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}

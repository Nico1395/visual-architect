using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Authentication.Domain;

namespace VisualArchitect.Api.Authentication.Infrastructure.EntityConfigurations;

internal sealed class OAuthProviderEntityConfiguration : IEntityTypeConfiguration<OAuthProvider>
{
    public void Configure(EntityTypeBuilder<OAuthProvider> builder)
    {
        builder.ToTable(name: "oauth_provider", schema: "authentication");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Key).IsUnique();

        builder.Property(p => p.Id).HasColumnName("id").UseIdentityColumn().IsRequired();
        builder.Property(p => p.Key).HasColumnName("key").HasMaxLength(15).IsRequired();
    }
}

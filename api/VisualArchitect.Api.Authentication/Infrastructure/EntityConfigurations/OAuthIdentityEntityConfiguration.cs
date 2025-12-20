using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Authentication.Domain;

namespace VisualArchitect.Api.Authentication.Infrastructure.EntityConfigurations;

internal sealed class OAuthIdentityEntityConfiguration : IEntityTypeConfiguration<OAuthIdentity>
{
    public void Configure(EntityTypeBuilder<OAuthIdentity> builder)
    {
        builder.ToTable(name: "oauth_identity", schema: "authentication");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("id").IsRequired();
        builder.Property(i => i.ExternalId).HasColumnName("external_id").HasMaxLength(255).IsRequired();
        builder.Property(i => i.ProviderId).HasColumnName("provider_id").IsRequired();
        builder.Property(i => i.IdentityId).HasColumnName("identity_id").IsRequired();
        builder.Property(i => i.CreatedAt).HasColumnName("created_at").IsRequired();

        builder.HasOne(i => i.Provider).WithMany().HasForeignKey(i => i.ProviderId).IsRequired();
        builder.HasOne(i => i.Identity).WithOne().HasForeignKey<OAuthIdentity>(i => i.IdentityId).IsRequired();
    }
}
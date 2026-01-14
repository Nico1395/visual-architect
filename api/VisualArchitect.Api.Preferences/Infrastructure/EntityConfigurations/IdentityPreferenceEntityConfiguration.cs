using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.EntityConfigurations;
using VisualArchitect.Api.Preferences.Domain;

namespace VisualArchitect.Api.Preferences.Infrastructure.EntityConfigurations;

internal sealed class IdentityPreferenceEntityConfiguration : IEntityTypeConfiguration<IdentityPreference>
{
    public void Configure(EntityTypeBuilder<IdentityPreference> builder)
    {
        builder.ToTable(name: "identity_preference", schema: "preferences");
        builder.HasKey(i => new { i.IdentityId, i.PreferenceId });

        builder.Property(i => i.IdentityId).HasColumnName("identity_id").IsRequired();
        builder.Property(i => i.PreferenceId).HasColumnName("preference_id").IsRequired();
        builder.Property(i => i.Value).HasColumnName("value").HasMaxLength(2048);
        builder.UpdatedAtProperty();

        builder.HasOne(i => i.Preference).WithMany().HasForeignKey(i => i.PreferenceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}

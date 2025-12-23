using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Preferences.Domain;

namespace VisualArchitect.Api.Preferences.Infrastructure.EntityConfigurations;

internal sealed class IdentitySettingEntityConfiguration : IEntityTypeConfiguration<IdentitySetting>
{
    public void Configure(EntityTypeBuilder<IdentitySetting> builder)
    {
        builder.ToTable(name: "identity_setting", schema: "preferences");
        builder.HasKey(i => new { i.IdentityId, i.SettingId });

        builder.Property(i => i.IdentityId).HasColumnName("identity_id").IsRequired();
        builder.Property(i => i.SettingId).HasColumnName("setting_id").IsRequired();
        builder.Property(i => i.Value).HasColumnName("value").HasMaxLength(2048);
        builder.Property(i => i.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasOne(i => i.Setting).WithMany().HasForeignKey(i => i.SettingId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Orchestration.Abstractions.Domain.Constants;
using VisualArchitect.Api.Preferences.Domain;

namespace VisualArchitect.Api.Preferences.Infrastructure.EntityConfigurations;

internal sealed class SettingEntityConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.ToTable(name: "setting", schema: "preferences");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("id").UseIdentityColumn().IsRequired();
        builder.Property(s => s.Key).HasColumnName("key").HasMaxLength(100).IsRequired();
        builder.Property(s => s.DefaultValue).HasColumnName("default_value").HasMaxLength(2048);

        builder.HasData([
            new Setting() { Id = 1, Key = PreferencesConstants.Theme.Key, DefaultValue = PreferencesConstants.Theme.DefaultValue },
            new Setting() { Id = 2, Key = PreferencesConstants.Language.Key, DefaultValue = PreferencesConstants.Language.DefaultValue },
        ]);
    }
}

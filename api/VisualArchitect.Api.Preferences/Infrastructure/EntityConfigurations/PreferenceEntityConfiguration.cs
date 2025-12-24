using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Orchestration.Abstractions.Domain.Constants;
using VisualArchitect.Api.Preferences.Domain;

namespace VisualArchitect.Api.Preferences.Infrastructure.EntityConfigurations;

internal sealed class SettingEntityConfiguration : IEntityTypeConfiguration<Preference>
{
    public void Configure(EntityTypeBuilder<Preference> builder)
    {
        builder.ToTable(name: "preference", schema: "preferences");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("id").UseIdentityColumn().IsRequired();
        builder.Property(s => s.Key).HasColumnName("key").HasMaxLength(100).IsRequired();
        builder.Property(s => s.DefaultValue).HasColumnName("default_value").HasMaxLength(2048);

        builder.HasData([
            new Preference() { Id = 1, Key = PreferenceConstants.Theme.Key, DefaultValue = PreferenceConstants.Theme.DefaultValue },
            new Preference() { Id = 2, Key = PreferenceConstants.Language.Key, DefaultValue = PreferenceConstants.Language.DefaultValue },
        ]);
    }
}

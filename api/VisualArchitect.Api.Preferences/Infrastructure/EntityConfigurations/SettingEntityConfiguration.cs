using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Authentication.Domain;

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
    }
}

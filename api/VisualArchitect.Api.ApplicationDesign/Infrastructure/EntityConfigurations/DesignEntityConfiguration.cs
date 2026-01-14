using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.EntityConfigurations;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.EntityConfigurations;

internal sealed class DesignEntityConfiguration : IEntityTypeConfiguration<Design>
{
    public void Configure(EntityTypeBuilder<Design> builder)
    {
        builder.ToTable(name: "design", schema: "application_design");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.TaskId).HasColumnName("task_id").IsRequired();
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(d => d.DescriptionPayload).HasColumnName("description_payload").HasMaxLength(4096);
        builder.Property(d => d.Type).HasColumnName("type").IsRequired();
        builder.Property(d => d.Payload).HasColumnName("payload").HasMaxLength(int.MaxValue).IsRequired();
        builder.CreatedAtProperty();
        builder.UpdatedAtProperty();
    }
}

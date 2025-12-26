using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.EntityConfigurations;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.EntityConfigurations;

internal sealed class DesignTaskEntityConfiguration : IEntityTypeConfiguration<DesignTask>
{
    public void Configure(EntityTypeBuilder<DesignTask> builder)
    {
        builder.ToTable(name: "design_task", schema: "application_design");
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => new { d.ProjectId, d.Number }).IsUnique();

        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.ProjectId).HasColumnName("project_id").IsRequired();
        builder.Property(d => d.Number).HasColumnName("number").IsRequired();
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(d => d.DescriptionPayload).HasColumnName("description_payload").HasMaxLength(4096).IsRequired();
        builder.Property(d => d.Status).HasColumnName("status").IsRequired();
        builder.CreatedAtProperty();
        builder.UpdatedAtProperty();

        builder.HasMany(d => d.Designs)
            .WithOne()
            .HasForeignKey(d => d.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}

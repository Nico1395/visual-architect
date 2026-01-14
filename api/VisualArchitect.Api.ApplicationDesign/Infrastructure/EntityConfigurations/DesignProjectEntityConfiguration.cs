using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.EntityConfigurations;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.EntityConfigurations;

internal sealed class DesignProjectEntityConfiguration : IEntityTypeConfiguration<DesignProject>
{
    public void Configure(EntityTypeBuilder<DesignProject> builder)
    {
        builder.ToTable(name: "design_project", schema: "application_design");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.IdentityId).HasColumnName("identity_id").IsRequired();
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(d => d.DescriptionPayload).HasColumnName("description_payload").HasMaxLength(4096).IsRequired();
        builder.CreatedAtProperty();
        builder.UpdatedAtProperty();

        builder.HasMany(d => d.DesignTasks)
            .WithOne()
            .HasForeignKey(d => d.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}

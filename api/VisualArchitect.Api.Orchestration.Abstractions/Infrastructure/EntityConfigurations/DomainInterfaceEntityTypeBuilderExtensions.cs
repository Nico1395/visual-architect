using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.EntityConfigurations;

public static class DomainInterfaceEntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> CreatedAtProperty<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ICreated
    {
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        return builder;
    }
    
    public static EntityTypeBuilder<TEntity> UpdatedAtProperty<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IUpdated
    {
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
        return builder;
    }
}


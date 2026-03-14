using CleanArchitecture.ApiTemplate.Domain.Entities;
using CleanArchitecture.ApiTemplate.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.ApiTemplate.Persistence.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.OwnsOne(p => p.Timeline, timelineBuilder =>
        {
            timelineBuilder.Property(dr => dr.StartDate)
                .HasColumnName("StartDate");
            timelineBuilder.Property(dr => dr.EndDate)
                .HasColumnName("EndDate");
        });
    }
}

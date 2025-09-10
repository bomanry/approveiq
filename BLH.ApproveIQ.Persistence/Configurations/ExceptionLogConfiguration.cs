using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Persistence.Constants;

namespace BLH.ApproveIQ.Persistence.Configurations;

internal sealed class ExceptionLogConfiguration : IEntityTypeConfiguration<ExceptionLog>
{
    public void Configure(EntityTypeBuilder<ExceptionLog> builder)
    {
        builder.ToTable(TableNames.ExceptionLogs);

        builder.HasKey(x => x.Id);
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Identity.Models;
using BLH.ApproveIQ.Persistence.Constants;

namespace BLH.ApproveIQ.Persistence.Configurations;

internal sealed class B2CUserConfiguration : IEntityTypeConfiguration<B2CUser>
{
    public void Configure(EntityTypeBuilder<B2CUser> builder)
    {
        builder.ToTable(TableNames.B2CUsers);

        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.Property(x => x.Role).HasDefaultValue(ApplicationIdentityConstants.Roles.NoAccess);
        
    }
}

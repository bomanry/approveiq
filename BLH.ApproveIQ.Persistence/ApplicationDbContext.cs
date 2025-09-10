using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BLH.ApproveIQ.Domain.Identity.Models;

namespace BLH.ApproveIQ.Persistence;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
    
    public ApplicationDbContext() { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Set default collation for all string columns (case-insensitive)
        builder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
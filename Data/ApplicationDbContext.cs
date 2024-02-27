#nullable disable
namespace StokvelPlatform.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<InvestementPackage> InvestementPackage { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CustomIdentity>(entity =>
        {
            entity.HasIndex(i => i.IDNo).IsUnique().HasDatabaseName("IX_IDNo");
            entity.Property(p => p.IDNo).IsRequired().HasMaxLength(13);
        });

        builder.Entity<InvestementPackage>(entity =>
        {
            entity.Property(p => p.MinimumInvestment).HasColumnType("decimal(18,2)");
            entity.Property(p => p.MaximumInvestment).HasColumnType("decimal(18,2)");
            entity.Property(p => p.LastModified).ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
        });
    }
}
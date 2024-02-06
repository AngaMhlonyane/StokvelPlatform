namespace StokvelPlatform.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CustomIdentity>( entity => { entity.HasIndex(i => i.IDNo).IsUnique().HasDatabaseName("IX_IDNo");
            entity.Property(p => p.IDNo).IsRequired().HasMaxLength(13); });
    }
}
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;


namespace NSE.ShoppingCart.API.Data;

public class ShoppingCartContext : DbContext
{
    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }
    
    public DbSet<Models.ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Models.ShoppingCartCustomer> ShoppingCartCustomer { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // only for set a default varchar to 100
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(
                         p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(100)");
        }

        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.Entity<Models.ShoppingCartCustomer>()
            .HasIndex(c => c.CustomerId, "IDX_Customer");

        modelBuilder.Entity<Models.ShoppingCartCustomer>()
            .HasMany(c => c.Items)
            .WithOne(i => i.ShoppingCartCustomer)
            .HasForeignKey(c => c.ShoppingCartId);

        foreach (var relationship in modelBuilder
                     .Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys())
                ) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }
}
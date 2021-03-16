namespace SkladoveKarty.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IDatabaseContext
    {
        DbSet<Account> Accounts { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Customer> Customers { get; set; }

        DbSet<Item> Items { get; set; }

        DbSet<StorageCard> StorageCards { get; set; }

        DbSet<StorageCardSupplier> StorageCardSuppliers { get; set; }

        DbSet<Store> Stores { get; set; }

        DbSet<Supplier> Suppliers { get; set; }

        EntityEntry Add(object entity);

        int SaveChanges();
    }
}
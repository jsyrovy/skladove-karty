namespace SkladoveKarty
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        private const string DefaultValueSql = "DATETIME()";
        private const string DatabaseName = "data.sqlite";

        private ModelBuilder modelBuilder;

        public DbSet<StorageCard> StorageCards { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<StorageCardSupplier> StorageCardSuppliers { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DatabaseName}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;

            this.CreateIndexes();
            this.CreateDefaults();
        }

        private void CreateIndexes()
        {
            this.modelBuilder.Entity<StorageCard>().HasIndex(b => b.Name).IsUnique();
            this.modelBuilder.Entity<Category>().HasIndex(b => b.Name).IsUnique();
            this.modelBuilder.Entity<Store>().HasIndex(b => b.Name).IsUnique();
            this.modelBuilder.Entity<Account>().HasIndex(b => b.Name).IsUnique();
            this.modelBuilder.Entity<Supplier>().HasIndex(b => b.Name).IsUnique();
            this.modelBuilder.Entity<Customer>().HasIndex(b => b.Name).IsUnique();
        }

        private void CreateDefaults()
        {
            this.modelBuilder.Entity<StorageCard>().Property(b => b.DateTime).HasDefaultValueSql(DefaultValueSql);
            this.modelBuilder.Entity<Category>().Property(b => b.DateTime).HasDefaultValueSql(DefaultValueSql);
            this.modelBuilder.Entity<Store>().Property(b => b.DateTime).HasDefaultValueSql(DefaultValueSql);
            this.modelBuilder.Entity<Account>().Property(b => b.DateTime).HasDefaultValueSql(DefaultValueSql);
            this.modelBuilder.Entity<Supplier>().Property(b => b.DateTime).HasDefaultValueSql(DefaultValueSql);
            this.modelBuilder.Entity<StorageCardSupplier>().Property(b => b.DateTime).HasDefaultValueSql(DefaultValueSql);
            this.modelBuilder.Entity<Item>().Property(b => b.DateTime).HasDefaultValueSql(DefaultValueSql);
            this.modelBuilder.Entity<Customer>().Property(b => b.DateTime).HasDefaultValueSql(DefaultValueSql);
        }
    }

    public class StorageCard
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Account Account { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public Store Store { get; set; }

        public List<StorageCardSupplier> StorageCardSuppliers { get; } = new List<StorageCardSupplier>();

        public List<Item> Items { get; } = new List<Item>();
    }

    public class Account
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        public List<StorageCard> StorageCards { get; } = new List<StorageCard>();
    }

    public class Category
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        public List<StorageCard> StorageCards { get; } = new List<StorageCard>();
    }

    public class Store
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        public List<StorageCard> StorageCards { get; } = new List<StorageCard>();
    }

    public class Supplier
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        public List<StorageCardSupplier> StorageCardSuppliers { get; } = new List<StorageCardSupplier>();
    }

    public class StorageCardSupplier
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public StorageCard StorageCard { get; set; }

        [Required]
        public Supplier Supplier { get; set; }
    }

    public class Item
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Movement { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Invoice { get; set; }

        [Required]
        public StorageCard StorageCard { get; set; }

        public Customer Customer { get; set; }
    }

    public class Customer
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Item> Items { get; } = new List<Item>();
    }
}

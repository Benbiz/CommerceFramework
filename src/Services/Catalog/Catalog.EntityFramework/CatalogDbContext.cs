using CommerceFramework.Services.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CommerceFramework.Services.Catalog.EntityFramework
{
    /// <summary>
    /// Base class of the CommerceFramework database context used for catalog
    /// </summary>
    /// <typeparam name="TProduct">The type of product objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    public class CatalogDbContext<TProduct, TKey> : DbContext
        where TProduct : Product<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public CatalogDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected CatalogDbContext() { }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TProduct}"/> of products.
        /// </summary>
        public virtual DbSet<TProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}

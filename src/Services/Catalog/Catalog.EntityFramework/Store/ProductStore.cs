using CommerceFramework.Services.Catalog.Domain;
using CommerceFramework.Services.Catalog.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommerceFramework.Services.Catalog.EntityFramework.Store
{
    /// <summary>
    /// Represents a new instance of a persistence store for the specified product.
    /// </summary>
    /// <typeparam name="TProduct">The type representing a product.</typeparam>
    /// <typeparam name="TKey">The type of the primary key of a product.</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    public class ProductStore<TProduct, TKey, TContext> : ProductStoreBase<TProduct, TKey>
        where TProduct : Product<TKey>
        where TKey : IEquatable<TKey>
        where TContext : DbContext
    {

        /// <summary>
        /// Creates a new instance of the store.
        /// </summary>
        /// <param name="context">The context used to access the store.</param>
        public ProductStore(TContext context)
        {
            Context = context;
        }


        /// <summary>
        /// Gets the database context for this store.
        /// </summary>
        public virtual TContext Context { get; private set; }

        private DbSet<TProduct> ProductsSet => Context.Set<TProduct>();

        public override IQueryable<TProduct> Products => ProductsSet;

        /// <summary>
        /// Gets or sets a flag indicating if changes should be persisted after CreateAsync, UpdateAsync and DeleteAsync are called.
        /// </summary>
        /// <value>
        /// True if changes should be automatically persisted, otherwise false.
        /// </value>
        public bool AutoSaveChanges { get; set; } = true;

        /// <summary>Saves the current store.</summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        protected Task SaveChanges(CancellationToken cancellationToken)
        {
            return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }

        public override async Task CreateAsync(TProduct product, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            Context.Add(product);
            await SaveChanges(cancellationToken);
        }

        public override async Task DeleteAsync(TProduct product, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Context.Remove(product);
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
                //return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }
            return; // IdentityResult.Success;
        }

        public override Task<TProduct> FindByIdAsync(TKey productId, CancellationToken cancellationToken = default)
        {
            return Products.SingleOrDefaultAsync(u => u.Id.Equals(productId), cancellationToken);
        }

        public override async Task UpdateAsync(TProduct product, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Context.Attach(product);
            Context.Update(product);
            try
            {
                await SaveChanges(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw; // return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
            }
            return;// IdentityResult.Success;
        }
    }
}

using CommerceFramework.Services.Catalog.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommerceFramework.Services.Catalog.Stores
{
    public abstract class ProductStoreBase<TProduct, TKey> : IProductStore<TProduct, TKey>
        where TProduct : Product<TKey>
        where TKey : IEquatable<TKey>
    {
        private bool _disposed;

        /// <summary>
        /// A navigation property for the products the store contains
        /// </summary>
        public abstract IQueryable<TProduct> Products { get; }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// Dispose the store.
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }

        public abstract Task CreateAsync(TProduct product, CancellationToken cancellationToken = default);

        public abstract Task DeleteAsync(TProduct product, CancellationToken cancellationToken = default);

        public abstract Task<TProduct> FindByIdAsync(TKey productId, CancellationToken cancellationToken = default);

        public abstract Task UpdateAsync(TProduct product, CancellationToken cancellationToken = default);
    }
}

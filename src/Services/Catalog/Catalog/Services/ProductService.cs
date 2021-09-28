using CommerceFramework.Services.Catalog.Domain;
using CommerceFramework.Services.Catalog.Stores;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommerceFramework.Services.Catalog.Services
{
    public class ProductService<TProduct, TKey>: IDisposable
        where TProduct: Product<TKey>
        where TKey: IEquatable<TKey>
    {
        private bool _disposed;

        /// <summary>
        /// The cancellation token used to cancel operations.
        /// </summary>
        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        public ProductService(IProductStore<TProduct, TKey> store)
        {
            Store = store;
        }

        public IProductStore<TProduct, TKey> Store { get; }

        /// <summary>
        /// Finds and returns a product, if any, who has the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The product ID to search for.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation, containing the product matching the specified <paramref name="id"/> if it exists.
        /// </returns>
        public virtual Task<TProduct> FindByIdAsync(TKey id)
        {
            ThrowIfDisposed();
            return Store.FindByIdAsync(id, CancellationToken);
        }

        /// <summary>
        /// Creates the specified <paramref name="product"/> in the backing store,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// </returns>
        public virtual async Task CreateAsync(TProduct product)
        {
            ThrowIfDisposed();
            await Store.CreateAsync(product, CancellationToken);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the role manager and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                Store.Dispose();
                _disposed = true;
            }
        }

        /// <summary>
        /// Releases all resources used by the user manager.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommerceFramework.Services.Catalog.Stores
{
    /// <summary>
    /// Provides an abstraction for a store which manages products.
    /// </summary>
    /// <typeparam name="TProduct">The type encapsulating a product.</typeparam>
    /// <typeparam name="TKey">The type of the primary key of a product</typeparam>
    public interface IProductStore<TProduct, TKey> : IDisposable
        where TProduct: class
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Finds and returns a product, if any, who has the specified <paramref name="productId"/>.
        /// </summary>
        /// <param name="productId">The product ID to search for.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation, containing the product matching the specified <paramref name="productId"/> if it exists.
        /// </returns>
        Task<TProduct> FindByIdAsync(TKey productId, CancellationToken cancellationToken);

        /// <summary>
        /// Creates the specified <paramref name="user"/> in the product store.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task CreateAsync(TProduct product, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified <paramref name="product"/> in the product store.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task UpdateAsync(TProduct product, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified <paramref name="product"/> from the product store.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task DeleteAsync(TProduct product, CancellationToken cancellationToken);
    }
}

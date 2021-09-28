using System;

namespace CommerceFramework.Services.Catalog.Domain
{
    /// <summary>
    /// Represents a product in the catalog service.
    /// </summary>
    /// <typeparam name="TKey">The type usef for the promary key for the product.</typeparam>
    public record Product<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Product{TKey}"/>.
        /// </summary>
        public Product() { }

        /// <summary>
        /// Initializes a new instance of <see cref="Product{TKey}"/>.
        /// </summary>
        /// <param name="name">The name of the product</param>
        public Product(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the primary key of this product.
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this product.
        /// </summary>
        public string Name { get; set; }
    }
}

using CommerceFramework.Services.Catalog.Domain;
using CommerceFramework.Services.Catalog.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommerceFramework.Services.Catalog
{
    public class CatalogBuilder
    {
        public CatalogBuilder(Type productType, Type keyType, IServiceCollection services)
        {
            ProductType = productType;
            Services = services;
            KeyType = keyType;
        }

        /// <summary>
        /// Gets the <see cref="Type"/> used for primary keys.
        /// </summary>
        /// <value>
        /// The <see cref="Type"/> used for primary keys.
        /// </value>
        public Type KeyType { get; private set; }

        /// <summary>
        /// Gets the <see cref="Type"/> used for products.
        /// </summary>
        /// <value>
        /// The <see cref="Type"/> used for products.
        /// </value>
        public Type ProductType { get; private set; }

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> services are attached to.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection"/> services are attached to.
        /// </value>
        public IServiceCollection Services { get; private set; }


        private CatalogBuilder AddScoped(Type serviceType, Type concreteType)
        {
            Services.AddScoped(serviceType, concreteType);
            return this;
        }

        /// <summary>
        /// Adds an <see cref="IProductStore{TProduct, TKey}"/> for the <see cref="ProductType"/> and <see cref="KeyType"/>.
        /// </summary>
        /// <typeparam name="TStore">The product store type.</typeparam>
        /// <returns>The current <see cref="CatalogBuilder"/> instance.</returns>
        public virtual CatalogBuilder AddProductStore<TStore>() where TStore : class
            => AddScoped(typeof(IProductStore<,>).MakeGenericType(ProductType, KeyType), typeof(TStore));
    }
}

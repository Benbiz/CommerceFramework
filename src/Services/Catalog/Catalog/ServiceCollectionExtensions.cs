using CommerceFramework.Services.Catalog.Domain;
using CommerceFramework.Services.Catalog.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace CommerceFramework.Services.Catalog
{
    public static class ServiceCollectionExtensions
    {
        public static CatalogBuilder AddCatalog<TProduct, TKey>(this IServiceCollection services)
            where TProduct : Product<TKey>
            where TKey : IEquatable<TKey>
        {
            services.TryAddScoped<ProductService<TProduct, TKey>>();

            return new CatalogBuilder(typeof(TProduct), typeof(TKey), services);
        }
    }
}

using CommerceFramework.Services.Catalog.EntityFramework.Store;
using CommerceFramework.Services.Catalog.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CommerceFramework.Services.Catalog.EntityFramework
{
    public static class CatalogBuilderExtensions
    {
        public static CatalogBuilder AddEntityFrameworkStores<TContext>(this CatalogBuilder catalogBuilder)
            where TContext: DbContext
        {
            catalogBuilder.Services.TryAddScoped(
                typeof(IProductStore<,>).MakeGenericType(catalogBuilder.ProductType, catalogBuilder.KeyType),
                typeof(ProductStore<,,>).MakeGenericType(catalogBuilder.ProductType, catalogBuilder.KeyType, typeof(TContext))
            );

            return catalogBuilder;
        }
    }
}

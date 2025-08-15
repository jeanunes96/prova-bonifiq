using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService
    {
        private readonly TestDbContext _ctx;
        private const int PageSize = 10;

        public ProductService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Lista produtos com paginação
        /// </summary>
        public async Task<ProductList> ListProductsAsync(int page)
        {
            if (page <= 0) page = 1;

            var totalCount = await _ctx.Products.CountAsync();
            var products = await _ctx.Products
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return new ProductList
            {
                Products = products,
                TotalCount = totalCount,
                HasNext = page * PageSize < totalCount
            };
        }
    }
}

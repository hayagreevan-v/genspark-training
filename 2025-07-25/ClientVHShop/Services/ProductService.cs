using ChienVHShopOnline.Models;
using ChienVHShopOnline.Repositories;

namespace ChienVHShopOnline.Services;

public class ProductService
{
    private ProductRepo _productRepo;
    public ProductService(ProductRepo productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<List<Product>> GetAll()
    {
        return (await _productRepo.GetAll()).ToList();
    }
    public async Task<List<Product>> GetByFilter(int? page, int? category)
    {
        var pageNumber = page ?? 1;
        var pageSize = 2;
        var products = (await _productRepo.GetAll()).ToList();
        if (category != null)
        {
            products = products.Where(x => x.CategoryId == category).ToList();
        }
            products = products
                        .OrderByDescending(x => x.ProductId)
                        .Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize)
                        .ToList();
        return products;
    }

    public async Task<Product> Get(int id)
    {
        return (await _productRepo.Get(id));
    }
}
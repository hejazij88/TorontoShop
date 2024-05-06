using TorontoShop.Application.Interfaces;
using TorontoShop.Domain.Interfaces;

namespace TorontoShop.Application.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository=productRepository;
    }
}
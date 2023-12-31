using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identity;

namespace NSE.Catalog.API.Controllers;

[Authorize]
public class CatalogController : MainController
{
    private readonly IProductRepository _productRepository;
    
    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    [AllowAnonymous]
    [HttpGet("catalog/products")]
    public async Task<IEnumerable<Product>> Index()
    {
        return await _productRepository.GetAllAsync();
    }
    
    // [ClaimAuthorize("Catalog", "Read")]
    [HttpGet("catalog/products/{id}")]
    public async Task<Product> ProductDetail(Guid id)
    {
        return await _productRepository.GetByIdAsync(id);
    }
}
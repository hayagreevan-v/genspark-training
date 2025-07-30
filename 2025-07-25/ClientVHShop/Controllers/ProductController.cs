using ChienVHShopOnline.Models;
using ChienVHShopOnline.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChienVHShopOnline.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        try
        {
            var product = (await _productService.GetAll()).ToList();
            return Ok(product);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetByFilter(int? page, int? category)
    {
        try
        {
            var product = (await _productService.GetByFilter(page, category)).ToList();
            return Ok(product);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("Details/{id}")]
    public async Task<ActionResult<Product>> Details(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        try
        {
            var product = await _productService.Get((int)id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }
}
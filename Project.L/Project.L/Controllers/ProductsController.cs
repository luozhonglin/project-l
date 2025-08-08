using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Entities.DTO;
using Project.Entities.DTO.Common;
using Project.Service;

namespace Project.L.Controllers
{
    public class ProductsController : BaseApiController
    {
        //private readonly IProductDAO _repository;
        //private readonly ILogger<ProductsController> _logger;

        public IProductService ProductService { get; set; }

        //public ProductsController(IProductService productService)
        //{
        //    ProductService = productService;
        //}

        //public ProductsController(IProductDAO repository, ILogger<ProductsController> logger)
        //{
        //    _repository = repository;
        //    _logger = logger;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        //{
        //    try
        //    {
        //        var products = await _repository.GetAllAsync();
        //        return Ok(products);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting all products");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Product>> GetById(int id)
        //{
        //    var product = await _repository.GetByIdAsync(id);
        //    return product == null ? NotFound() : Ok(product);
        //}

        //[HttpGet("active")]
        //public async Task<ActionResult<IEnumerable<Product>>> GetActiveProducts()
        //{
        //    var products = await _repository.GetActiveProductsAsync();
        //    return Ok(products);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Product>> Create(Product product)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var id = await _repository.AddAsync(product);
        //        product.Id = id;
        //        return CreatedAtAction(nameof(GetById), new { id }, product);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error creating product");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> Update(int id, Product product)
        //{
        //    if (id != product.Id)
        //        return BadRequest("ID mismatch");

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var existing = await _repository.GetByIdAsync(id);
        //        if (existing == null)
        //            return NotFound();

        //        product.UpdatedAt = DateTime.UtcNow;
        //        var result = await _repository.UpdateAsync(product);

        //        return result ? NoContent() : StatusCode(500);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error updating product {id}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var result = await _repository.DeleteAsync(id);
        //        return result ? NoContent() : NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error deleting product {id}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetActiveProductsNew()
        {

            var products = await ProductService.GetActiveProductsAsync();
            return new JsonResult(products);
        }


        [HttpGet]
        public async Task<ActionResult<PageResponse<ProductTypeDTO>>> GetPagedProductTypeList([FromQuery]PageRequest request, [FromQuery]string? whereId=null)
        {
            var userId = CurrentUserId;
            var result = await ProductService.GetPagedProductTypeList(request, whereId);
            return new JsonResult(result);
        }

    }
}

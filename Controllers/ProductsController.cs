using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiNet7.Data;
using ApiNet7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNet7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly DataContext _context;

        public ProductsController(ILogger<ProductsController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet (Name = "GetProducts")]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
       

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }else{
                    return product;
                }

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the product.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "CreateProduct")]

        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {   
                // Add the product to the database
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the product.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}", Name = "UpdateProduct")]

        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest();
                }

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product.");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
using EgitimPortali.DTO;
using EgitimPortali.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EducationsController : ControllerBase
    {
        
        private readonly EgitimlerContext _context;

        public EducationsController(EgitimlerContext context)
        {
           _context = context;
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Educations.Where(i => i.IsActive).Select(p => 
            ProductToDTO(p))
            .ToListAsync();
            return Ok(products);
        }

        // localhost:5000/api/products/1 => GET
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
           if(id == null)
            {
                return NotFound();
            }

            var p = await _context
                .Educations.Where(i => i.EducationId == id)
                .Select(p => ProductToDTO(p))
                .FirstOrDefaultAsync();

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Educations entity)
        {
            _context.Educations.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.EducationId }, entity);
        }

        // localhost:5000/api/products/5 => GET
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,  Educations entity)
        {
            if (id != entity.EducationId)
            {
                return BadRequest();
            }

            var product = await _context.Educations.FirstOrDefaultAsync(i => i.EducationId == id);

            if (product == null)
            {
                return NotFound();
            }

            product.EducationTitle = entity.EducationTitle;
            product.Time = entity.Time;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null) 
            { 
                return NotFound();
            }

            var product = await _context.Educations.FirstOrDefaultAsync(i => i.EducationId == id);

            if(product == null)
            {
                return NotFound();
            }

            _context.Educations.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
            return NoContent();

        }


        private static EducationsDTO ProductToDTO(Educations p)
        {
            var entity = new EducationsDTO();
            if(p != null)
            {
                entity.EducationId = p.EducationId;
                entity.EducationTitle = p.EducationTitle;
                entity.Time = p.Time;
            }
            return entity;
        }
    }


}
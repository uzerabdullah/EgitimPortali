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
    public class TeacherController : ControllerBase
    {
        
        private readonly EgitimlerContext _context;

        public TeacherController(EgitimlerContext context)
        {
           _context = context;
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Teachers.Where(i => i.IsActive).Select(p => 
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
                .Teachers.Where(i => i.TeacherId == id)
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
        public async Task<IActionResult> CreateProduct(Teacher entity)
        {
            _context.Teachers.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.TeacherId }, entity);
        }

        // localhost:5000/api/products/5 => GET
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Teacher entity)
        {
            if (id != entity.TeacherId)
            {
                return BadRequest();
            }

            var product = await _context.Teachers.FirstOrDefaultAsync(i => i.TeacherId == id);

            if (product == null)
            {
                return NotFound();
            }

            product.TeacherName = entity.TeacherName;
            product.Department = entity.Department;
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

            var product = await _context.Teachers.FirstOrDefaultAsync(i => i.TeacherId == id);

            if(product == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(product);

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


        private static TeacherDTO ProductToDTO(Teacher p)
        {
            var entity = new TeacherDTO();
            if(p != null)
            {
                entity.TeacherId = p.TeacherId;
                entity.TeacherName = p.TeacherName;
                entity.Department = p.Department;
            }
            return entity;
        }
    }


}
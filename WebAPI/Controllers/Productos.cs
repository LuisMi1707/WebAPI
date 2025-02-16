using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;
using WebAPI.Modelos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Productos : ControllerBase
    {
        public ProductoDbContext _context;

        public Productos(ProductoDbContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTOs>>> ObtenerProducto()
        {
            var productos = await _context.Productos.ToListAsync();
            var productosDTO = productos.Select(p => new ProductoDTOs
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock
            }).ToList();

            return Ok(productosDTO);
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTOs>> ObtenerProductoPorId(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            var productoDTO = new ProductoDTOs
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
            return Ok(productoDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CrearProducto(ProductoDTOs productoDTO)
        {
            var producto = new producto
            {
                Nombre = productoDTO.Nombre,
                Precio = productoDTO.Precio,
                Stock = productoDTO.Stock
            };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarProducto(int id, ProductoDTOs productoDTO)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            producto.Nombre = productoDTO.Nombre;
            producto.Precio = productoDTO.Precio;
            producto.Stock = productoDTO.Stock;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

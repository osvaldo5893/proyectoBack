using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }
        


        [HttpGet]
        [Route("getProductos")]
        public async Task<ActionResult<IEnumerable<Productos>>> getProductos()
        {
            try
            {
                var datos = _context.productos.FromSqlRaw("select * from productos where estatus=1;").ToList();
                if (datos.Count() > 0)
                {
                    return Ok(datos);

                }
                else
                {
                    var d = new List<string>()
                    {
                        "SIN INFO"
                    };
                    return Ok(d);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("GuardarProducto")]
        public dynamic GuardarProducto(Productos Cuenta)
        {

            try
            {
                Cuenta.CreatedAt = DateTime.Now;
                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("ActualizaProducto")]
        [HttpPost]
        public dynamic ActualizaProducto(int id, [FromBody] Productos Cuenta)
        {
            try
            {
                if (id == Cuenta.idProducto)
                {

                    Cuenta.UpdatedAt = DateTime.Now;
                    _context.Update(Cuenta);
                    _context.SaveChanges();
                    return Ok(Cuenta);

                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EliminarProducto")]
        [HttpDelete]
        public dynamic EliminarProducto(int id, [FromBody] Productos Cuenta)
        {
            try
            {
                if (id == Cuenta.idProducto)
                {
                    Cuenta.estatus = 0;
                    _context.Update(Cuenta);
                    _context.SaveChanges();
                    return Ok(Cuenta);
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("calcularEdad")]
        public dynamic CalcularEdad(string date)
        {
            try
            {
                DateTime fechaNacimiento;
                if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNacimiento))
                {
                    DateTime fechaActual = DateTime.Today;
                    int edad = fechaActual.Year - fechaNacimiento.Year;

                    if (fechaNacimiento > fechaActual.AddYears(-edad))
                        edad--;

                    return edad;
                }
                else
                {
                    throw new ArgumentException("Formato de fecha no válido. Utilice yyyy-MM-dd.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }














    }
}

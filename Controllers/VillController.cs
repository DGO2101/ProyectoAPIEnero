using MagicVill.Datos;
using MagicVill.Modelos;
using MagicVill.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillController : ControllerBase
    {
        private readonly ILogger<VillController> _logger;
        private readonly AplicationDBContext _context;
        public VillController(ILogger<VillController> logger, AplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillDTO>> GetVills()
        {
            _logger.LogInformation("obtener las villas");
            return Ok(_context.villa.ToList());
        }
        [HttpGet("id: int", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillDTO> GetVill(int Id)
        {
            if (Id == 0)
            {
                _logger.LogError("error al traer villa con id: " +  Id);
                return BadRequest();
            }
            var villa = _context.villa.FirstOrDefault(v => v.id == Id);
            //var villa = VillStore.VillList.FirstOrDefault(v => v.id == Id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillDTO> CrearVilla([FromBody] VillDTO villadto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.villa.FirstOrDefault(v => v.name.ToLower() == villadto.name.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }
            {

            }
            if (villadto == null) { return BadRequest(villadto); }
            //if (villadto.id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }
            villadto.id = VillStore.VillList.OrderByDescending(v => v.id).FirstOrDefault().id + 1;
            //VillStore.VillList.Add(villadto);
            //return CreatedAtRoute("GetVilla", new { id = villadto.id, villadto });
            Vill modelo = new()
            {
                name = villadto.name,
                detalle = villadto.detalle,
                imagenURL = villadto.imagenURL,
                ocupantes = villadto.ocupantes,
                tarifa = villadto.tarifa,
                metrosCudrados = villadto.metrosCuadrados,
                amenidad = villadto.amenidad,
            };
            _context.villa.Add(modelo);
            _context.SaveChanges();
            return CreatedAtRoute("GetVill", new { Id = villadto.id, villadto });//no se si estoy invocando al parametro correctamente
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _context.villa.FirstOrDefault(v => v.id == id);
            if (villa == null) { return NotFound(); }
            _context.villa.Remove(villa);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVill(int id, [FromBody] VillDTO villdto)
        {
            if(villdto == null || id != villdto.id) { return BadRequest(); }
            Vill modelo = new()
            {
                id = villdto.id,
                name = villdto.name,
                detalle = villdto.detalle,
                imagenURL = villdto.imagenURL,
                ocupantes = villdto.ocupantes,
                tarifa = villdto.tarifa,
                metrosCudrados = villdto.metrosCuadrados,
                amenidad = villdto.amenidad,
            };
            _context.villa.Update(modelo);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVill(int Id, JsonPatchDocument<VillDTO> patchDTO)
        {
            if (patchDTO == null || Id == 0) { return BadRequest(); }
            var villa = _context.villa.AsNoTracking().FirstOrDefault(v => v.id == Id);
            VillDTO villdto = new()
            {
                id = villa.id,
                name = villa.name,
                detalle = villa.detalle,
                imagenURL = villa.imagenURL,
                ocupantes = villa.ocupantes,
                tarifa = villa.tarifa,
                metrosCuadrados = villa.metrosCudrados,
                amenidad = villa.amenidad,
            };
            if(villa == null) { return BadRequest(); }
            patchDTO.ApplyTo(villdto, ModelState);
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            Vill modelo = new()
            {
                id = villdto.id,
                name = villdto.name,
                detalle = villdto.detalle,
                imagenURL = villdto.imagenURL,
                ocupantes = villdto.ocupantes,
                tarifa = villdto.tarifa,
                metrosCudrados = villdto.metrosCuadrados,
                amenidad = villdto.amenidad
            };
            _context.villa.Update(modelo);
            _context.SaveChanges();
            return NoContent();
        }
        //para usar el metodo patch se ejecuta el programa y despues se elimina la parte de "operation type"
        //en "patch" se escribe "/name" para modificar el nombre
        //en "op" se escribe "replace"
        //en "from" se borra
        //en value se escribe el valor que nosotros escojamos 
    }
}

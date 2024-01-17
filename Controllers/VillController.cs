using MagicVill.Datos;
using MagicVill.Modelos;
using MagicVill.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillController : ControllerBase
    {
        private readonly ILogger<VillController> _logger;
        public VillController(ILogger<VillController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<VillDTO> GetVills()
        {
            _logger.LogInformation("obtener las villas");
            return VillStore.VillList;
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
            var villa = VillStore.VillList.FirstOrDefault(v => v.id == Id);
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
            if (VillStore.VillList.FirstOrDefault(v => v.name.ToLower() == villadto.name.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }
            {

            }
            if (villadto == null) { return BadRequest(villadto); }
            //if (villadto.id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }
            villadto.id = VillStore.VillList.OrderByDescending(v => v.id).FirstOrDefault().id + 1;
            VillStore.VillList.Add(villadto);
            return CreatedAtRoute("GetVilla", new { id = villadto.id, villadto });
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
            var villa = VillStore.VillList.FirstOrDefault(v => v.id == id);
            if (villa == null) { return NotFound(); }
            VillStore.VillList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVill(int id, [FromBody] VillDTO villdto)
        {
            if(villdto == null || id != villdto.id) { return BadRequest(); }
            var villa = VillStore.VillList.FirstOrDefault(v => v.id == id);
            villa.name = villdto.name;
            villa.ocupantes = villdto.ocupantes;
            villa.metrosCuadrados = villdto.metrosCuadrados;
            return NoContent();
        }


        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVill(int Id, JsonPatchDocument<VillDTO> patchDTO)
        {
            if (patchDTO == null || Id == 0) { return BadRequest(); }
            var villa = VillStore.VillList.FirstOrDefault(v => v.id == Id);
            patchDTO.ApplyTo(villa, ModelState);
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            return NoContent();
        }
        //para usar el metodo patch se ejecuta el programa y despues se elimina la parte de "operation type"
        //en "patch" se escribe "/name" para modificar el nombre
        //en "op" se escribe "replace"
        //en "from" se borra
        //en value se escribe el valor que nosotros escojamos 
    }
}

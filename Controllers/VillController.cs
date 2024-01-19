using AutoMapper;
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
        private readonly IMapper _mapper; //se inyecta el servicio en el controlador como se ha hecho en las anteriores lineas de codigo
        public VillController(ILogger<VillController> logger, AplicationDBContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<VillDTO>>> GetVills()
        {
            _logger.LogInformation("obtener las villas");
            IEnumerable<Vill> villaList = await _context.villa.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillDTO>>(villaList));
        }
        [HttpGet("id: int", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VillDTO>> GetVill(int Id)
        {
            if (Id == 0)
            {
                _logger.LogError("error al traer villa con id: " +  Id);
                return BadRequest();
            }
            var villa = await  _context.villa.FirstOrDefaultAsync(v => v.id == Id);
            //var villa = VillStore.VillList.FirstOrDefault(v => v.id == Id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillDTO>> CrearVilla([FromBody] VillCearDTO creardto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.villa.FirstOrDefaultAsync(v => v.name.ToLower() == creardto.name.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }
            {

            }
            if (creardto == null) { return BadRequest(creardto); }
            //if (villadto.id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }
            //villadto.id = VillStore.VillList.OrderByDescending(v => v.id).FirstOrDefault().id + 1;
            //VillStore.VillList.Add(villadto);
            //return CreatedAtRoute("GetVilla", new { id = villadto.id, villadto });

            Vill modelo = _mapper.Map<Vill>(creardto);// hace todo lo que hacen las lineas 79-88

            //Vill modelo = new()
            //{
            //    name = creardto.name,
            //    detalle = creardto.detalle,
            //    imagenURL = creardto.imagenURL,
            //    ocupantes = creardto.ocupantes,
            //    tarifa = creardto.tarifa,
            //    metrosCudrados = creardto.metrosCuadrados,
            //    amenidad = creardto.amenidad,
            //};
            await _context.villa.AddAsync(modelo);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("GetVill", new { Id = modelo.id, modelo });//no se si estoy invocando al parametro correctamente
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _context.villa.FirstOrDefaultAsync(v => v.id == id);
            if (villa == null) { return NotFound(); }
            _context.villa.Remove(villa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVill(int id, [FromBody] VillActualizarDTO actualizardto)
        {
            if(actualizardto == null || id != actualizardto.id) { return BadRequest(); }
            Vill modelo = _mapper.Map<Vill>(actualizardto); // hace lo que las lineas 118-128 hacen
            //Vill modelo = new()
            //{
            //    id = actualizardto.id,
            //    name = actualizardto.name,
            //    detalle = actualizardto.detalle,
            //    imagenURL = actualizardto.imagenURL,
            //    ocupantes = actualizardto.ocupantes,
            //    tarifa = actualizardto.tarifa,
            //    metrosCudrados = actualizardto.metrosCuadrados,
            //    amenidad = actualizardto.amenidad,
            //};
            _context.villa.Update(modelo);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVill(int Id, JsonPatchDocument<VillActualizarDTO> patchDTO)
        {
            if (patchDTO == null || Id == 0) { return BadRequest(); }
            var villa = await _context.villa.AsNoTracking().FirstOrDefaultAsync(v => v.id == Id);
            VillActualizarDTO actualizardto = _mapper.Map<VillActualizarDTO>(villa);
            //VillActualizarDTO actualizardto = new()
            //{
            //    id = villa.id,
            //    name = villa.name,
            //    detalle = villa.detalle,
            //    imagenURL = villa.imagenURL,
            //    ocupantes = villa.ocupantes,
            //    tarifa = villa.tarifa,
            //    metrosCuadrados = villa.metrosCudrados,
            //    amenidad = villa.amenidad,
            //};
            if(villa == null) { return BadRequest(); }
            patchDTO.ApplyTo(actualizardto, ModelState);
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            Vill modelo = _mapper.Map<Vill>(actualizardto);

            //Vill modelo = new()
            //{
            //    id = actualizardto.id,
            //    name = actualizardto.name,
            //    detalle = actualizardto.detalle,
            //    imagenURL = actualizardto.imagenURL,
            //    ocupantes = actualizardto.ocupantes,
            //    tarifa = actualizardto.tarifa,
            //    metrosCudrados = actualizardto.metrosCuadrados,
            //    amenidad = actualizardto.amenidad
            //};
            _context.villa.Update(modelo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //para usar el metodo patch se ejecuta el programa y despues se elimina la parte de "operation type"
        //en "patch" se escribe "/name" para modificar el nombre
        //en "op" se escribe "replace"
        //en "from" se borra
        //en value se escribe el valor que nosotros escojamos 
    }
}

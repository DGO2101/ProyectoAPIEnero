using System.ComponentModel.DataAnnotations;

namespace MagicVill.Modelos.DTO
{
    public class VillActualizarDTO
    {
        [Required]
        public int id {  get; set; }
        [Required]
        [StringLength(30)]
        public string name { get; set; }
        [Required]
        public int ocupantes { get; set; }
        [Required]
        public int metrosCuadrados { get; set; }
        [Required]
        public string imagenURL { get; set; }
        public string amenidad { get; set; }
        public string detalle {  get; set; }
        [Required]
        public double tarifa { get; set; }


    }
}

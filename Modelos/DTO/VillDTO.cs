using System.ComponentModel.DataAnnotations;

namespace MagicVill.Modelos.DTO
{
    public class VillDTO
    {
        public int id {  get; set; }
        [Required]
        [StringLength(30)]
        public string name { get; set; }
        public int ocupantes { get; set; }
        public int metrosCuadrados { get; set; }
        public string imagenURL { get; set; }
        public string amenidad { get; set; }
        public string detalle {  get; set; }
        [Required]
        public double tarifa { get; set; }


    }
}

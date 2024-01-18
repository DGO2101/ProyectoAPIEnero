using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVill.Modelos
{
    public class Vill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int id {  get; set; }
        public string name { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string detalle {  get; set; }
        [Required]
        public double tarifa { get; set; }
        public int ocupantes { get; set; }
        public int metrosCudrados { get; set; }
        public string imagenURL { get; set; }
        public string amenidad { get; set; }
        public DateTime fechaActualizacion { get; set; }


    }
}

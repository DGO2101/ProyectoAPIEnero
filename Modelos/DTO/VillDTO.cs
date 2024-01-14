using System.ComponentModel.DataAnnotations;

namespace MagicVill.Modelos.DTO
{
    public class VillDTO
    {
        public int id {  get; set; }
        [Required]
        [StringLength(30)]
        public string name { get; set; }
    }
}

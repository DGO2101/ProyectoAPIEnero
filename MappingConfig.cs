using AutoMapper;
using MagicVill.Modelos;
using MagicVill.Modelos.DTO;

namespace MagicVill
{
    public class MappingConfig: Profile
    {
        public MappingConfig() 
        { 
            CreateMap<Vill, VillDTO>();
            CreateMap<VillDTO, Vill>(); 

            CreateMap<Vill, VillCearDTO>().ReverseMap(); //exactamente lo que hice en las anteriores lineas de codigo
            CreateMap<Vill, VillActualizarDTO>().ReverseMap(); //same as 14 line
        }
    }
}

﻿using MagicVill.Modelos.DTO;

namespace MagicVill.Datos
{
    public static class VillStore
    {
        public static List<VillDTO> VillList = new List<VillDTO>
        {
            new VillDTO {id=1, name= "vista a la piscina"},
            new VillDTO {id=2, name = "vista a la playa"}
        };
    }
}

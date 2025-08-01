using System;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;

namespace SIIGPP.Entidades.M_JR.RAcuerdoReparatorio
{
    public class CoordinacionDistrito
    {
        public Guid IdCoordinacionDistritos { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid DistritoId { get; set; }
        public Distrito Distrito { get; set; }
    }
}

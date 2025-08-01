using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Turnador
{
    public class AmpoTurno
    {
        public Guid IdAmpoTurno { get; set; } 
        public ModuloServicio ModuloServicio { get; set; }
        public Guid? ModuloServicioId { get; set; }
        public Turno Turno { get; set; }
        public Guid? TurnoId { get; set; }


    }
}

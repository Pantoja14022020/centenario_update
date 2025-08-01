using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Turnador
{
    public class Turno
    {
        public Guid IdTurno { get; set; } 
        public string Serie { get; set; }
        public int NoTurno { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public  DateTime? FechaHoraFin { get; set; }
        public Boolean Status { get; set; } 
        public RAtencion RAtencion { get; set; }
        public Guid RAtencionId { get; set; }
        public Agencia Agencia  { get; set; }
        public Guid AgenciaId { get; set; }
        public Boolean StatusReAsignado { get; set; }
        public string Modulo { get; set; }

        public List<AmpoTurno> AmpoTurnos { get; set; }
    }
}

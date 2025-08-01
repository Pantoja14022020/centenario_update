using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogVehiculoPersonaDesap
    {
        public Guid IdAdminVehDesaparicionPersona { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdVehDesaparicionPersona { get; set; }
        public Guid PersonaDesaparecidaId { get; set; }
        public Guid TipovId { get; set; }
        public Guid AnoId { get; set; }
        public Guid ColorId { get; set; }
        public Guid ModeloId { get; set; }
        public Guid MarcaId { get; set; }
        public string Serie { get; set; }
        public string Placas { get; set; }
        public string SenasParticulares { get; set; }
        public string NoSerieMotor { get; set; }
        public string Propietario { get; set; }
        public string Ruta { get; set; }
        public int? EstadoId { get; set; }
        public Boolean? Privado { get; set; }
    }
}

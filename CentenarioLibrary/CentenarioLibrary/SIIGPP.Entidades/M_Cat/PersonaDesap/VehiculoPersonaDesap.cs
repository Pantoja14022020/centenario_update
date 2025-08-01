using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.Entidades.M_Cat.PersonaDesap
{
    public class VehiculoPersonaDesap
    {
        public Guid IdVehDesaparicionPersona { get; set; }
        public Guid PersonaDesaparecidaId { get; set; }
        public RPersonaDesap personadesap { get; set; }
        public Guid TipovId { get; set; }
        public Tipov tipov { get; set; }
        public Guid AnoId { get; set; }
        public Ano ano { get; set; }
        public Guid ColorId { get; set; }
        public Color color { get; set; }
        public Guid ModeloId { get; set; }
        public Modelo modelo { get; set; }
        public Guid MarcaId { get; set; }
        public Marca marca { get; set; }
        public string Serie { get; set; }
        public string Placas { get; set; }
        public string SenasParticulares { get; set; }
        public string NoSerieMotor { get; set; }
        public string Propietario { get; set; }
        public string Ruta { get; set; }
        public int? EstadoId { get; set; }
        public Estado Estado { get; set; }
        public Boolean? Privado { get; set; }

    }
}

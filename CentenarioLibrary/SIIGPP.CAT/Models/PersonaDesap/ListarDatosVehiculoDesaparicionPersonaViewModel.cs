using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;

namespace SIIGPP.CAT.Models.PersonaDesap
{
    public class ListarDatosVehiculoDesaparicionPersonaViewModel
    {
        public Guid IdVehDesaparicionPersona { get; set; }
        public Guid PersonaDesaparecidaId { get; set; }
        public Guid TipovId { get; set; }
        public string Tipov { get; set; }
        public Guid AnoId { get; set; }
        public string Ano { get; set; }
        public Guid ColorId { get; set; }
        public string Color { get; set; }
        public Guid ModeloId { get; set; }
        public string Modelo { get; set; }
        public Guid MarcaId { get; set; }
        public string Marca { get; set; }
        public string Serie { get; set; }
        public string Placas { get; set; }
        public string SenasParticulares { get; set; }
        public string NoSerieMotor { get; set; }
        public string Propietario { get; set; }
        public string Ruta { get; set; }
        public int? iEstado { get; set; }
        public string sEstado { get; set; }

    }
}

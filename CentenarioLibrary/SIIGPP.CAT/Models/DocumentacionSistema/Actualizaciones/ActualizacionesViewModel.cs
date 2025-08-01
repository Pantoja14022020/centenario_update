using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DocumentacionSistema.Actualizaciones
{
    public class ActualizacionesViewModel
    {
        public Guid IdActualizacion { get; set; }
        public string ClaveActualizacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string NombreActualizacion { get; set; }
        public string DescripcionActualizacion { get; set; }
        public string LigaServidor { get; set; }
        public string RutaDocumento { get; set; }
        public string RamasRelacionadas { get; set; }
        public Boolean HayQuerys { get; set; }
        public string QuerysRelacionados { get; set; }
        public string ShaCommitRepositorioCompilado { get; set; }
        public string RealizadoPor { get; set; }
        public Boolean MostrarUsuarios { get; set; }
        public DateTime FechaSys { get; set; }
        public Boolean? MostrarAviso { get; set; }
        public string MensajeAviso { get; set; }
        public Boolean? MostrarPDFAviso { get; set; }
        public string ModuloCentenario { get; set; }
    }
}

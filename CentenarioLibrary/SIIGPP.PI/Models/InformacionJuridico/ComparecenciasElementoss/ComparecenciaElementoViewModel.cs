using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.InformacionJuridico.ComparecenciasElementoss
{
    public class ComparecenciaElementoViewModel
    {
        public Guid IdCompElementos { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Comparecencia { get; set; }
        public string Elemento { get; set; }
        public string FComparecencia { get; set; }
        public string Hora { get; set; }
        public int Noficio { get; set; }
        public string CausaPenal { get; set; }
        public string AnteAutoridad { get; set; }
        public string Notas { get; set; }
        public string Recibe { get; set; }
        public string Status { get; set; }
        public string Estado { get; set; }
        public string Respuesta { get; set; }
        public DateTime FAsignacion { get; set; }
        public DateTime FFinalizacion { get; set; }
        public DateTime FUltmimoStatus { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public DateTime FechasComparescencia { get; set; }
        public DateTime FechaCumplimiento { get; set; }
        public string FechaRecepcion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Resolucion
{
    public class ResolucionViewModel
    {
        public Guid IdResolucion { get; set; }
        public Guid RHechoId { get; set; }
        public string Victimas { get; set; }
        public string Imputados { get; set; }
        public string Delitos { get; set; }
        public string CausaPenal { get; set; }
        public string Status { get; set; }
        public string Tipo { get; set; }
        public string SubTipo { get; set; }
        public string TextoDocumento { get; set; }
        public string NumeroOficio { get; set; }
        public DateTime FechaResolucion { get; set; }
        public DateTime FechaConsulta { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public DateTime Fechasys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public string URLDocumento { get; set; }
    }
}

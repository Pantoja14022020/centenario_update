using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RArma
{
    public class CrearViewModel
    {
        public Guid RHechoId { get; set; } 
        public string TipoAr { get; set; }
        public string NombreAr { get; set; }
        public string DescripcionAr { get; set; }
        public DateTime FechaSys { get; set; }
        public string FechaRegistro { get; set; }
        public string Distrito { get; set; }
        public string Subproc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
        public string Modulo { get; set; }
        public string Matricula { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Calibre { get; set; }
    }
}

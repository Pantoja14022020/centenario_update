using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class CrearViewModelH
    {  //REGISTRO DE HECHO
       
        
      
        public Guid agenciaId { get; set; }
        public Guid rAtencionId { get; set; }
        public Guid moduloServicioId { get; set; }
        public Boolean status { get; set; }
        public string rbreve { get; set; } 
        public string FechaReporte { get; set; }
        public string NDenunciaOficio { get; set; }
        public string Texto { get; set; }
        public string Observaciones { get; set; }




    }
}

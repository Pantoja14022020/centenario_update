using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Bitacora
{
    public class CrearViewModel
    {
       
        public string Tipo { get; set; }
        public string Descipcion { get; set; }
        public string Distrito { get; set; }
        public string Dirsubproc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
        public string Fechareporte { get; set; }
        public Guid rHechoId { get; set; }
        public Guid IdPersona { get; set; }
        public string Numerooficio { get; set; }
    }
}

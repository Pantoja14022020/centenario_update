using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.MedidasProteccion
{
    public class ActualizarViewModel
    {
        public Guid IdMProteccion { get; set; }
        public string Victima { get; set; }
        public string Imputado { get; set; }
        public string Delito { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string MedidaProtecion { get; set; }
        public int Duracion { get; set; }
        public string Institucionejec { get; set; }
        public string Nomedidas { get; set; }
        public string FInicio { get; set; }
        public string FVencimiento { get; set; }
        public string Textofinal { get; set; }
        public string Textofinal2 { get; set; }
        public Boolean PetiOfiMPBool { get; set; }
        public string PetiOfiMPVar { get; set; }
        public Boolean MedidasExtraTF { get; set; }
        public string MedidasExtra { get; set; }

    }
}

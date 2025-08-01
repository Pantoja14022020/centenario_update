using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Victimas
{
    public class InsertarRAPViewModel
    {
        public Guid RAtencionId { get; set; }
        public Guid PersonaId { get; set; }
        public string ClasificacionPersona { get; set; }
        public Boolean PInicio { get; set; }

        //DIRECCION ESCUCHA 
        public string de_Calle { get; set; }
        public string de_NoInt { get; set; }
        public string de_NoExt { get; set; }
        public string de_EntreCalle1 { get; set; }
        public string de_EntreCalle2 { get; set; }
        public string de_Referencia { get; set; }
        public string de_Pais { get; set; }
        public string de_Estado { get; set; }
        public string de_Municipio { get; set; }
        public string de_Localidad { get; set; }
        public int de_CP { get; set; }
        public string de_lat { get; set; }
        public string de_lng { get; set; }
        public int de_tipoVialidad { get; set; }
        public int de_tipoAsentamiento { get; set; }
    }
}

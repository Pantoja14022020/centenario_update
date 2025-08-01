using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.FilterClass
{
    public class DatosPersonasEstadistica
    {
        //Datos generales
        public string Nombre { get; set; }
        public string Apellidopa { get; set; }
        public string Apellidoma { get; set; }
        public string Ocupacion { get; set; }
        public string Sexo { get; set; }
        public string Nacionalidad { get; set; }
        public string Tipoedad { get; set; }
        public int Edadinicial { get; set; }
        public int EdadFinal { get; set; }
        public int Edad { get; set; }

        //Datos Victima
        public string RelacionImputado { get; set; }
        public string TipoRelacion { get; set; }
    }
}

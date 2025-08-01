using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.C5i
{
    public class CrearViewModel
    {
        public Guid RHechoId { get; set; }
        public string NumeroOficio { get; set; }
        public int Status { get; set; }
        public string FechaStatus { get; set; }
        public string HoraStatus { get; set; }
        public string Encargadoc5 { get; set; }
        public string Puestoc5 { get; set; }
        public string Direccion5 { get; set; }
        public string Agentequerecibe { get; set; }
        public string NumtelefonicoS { get; set; }
        public string CorreoElecS { get; set; }
        public Boolean Op1 { get; set; }
        public Boolean Op2 { get; set; }
        public Boolean Op3 { get; set; }
        public Boolean Op4 { get; set; }
        public Boolean Op5 { get; set; }
        public string Op5Texto { get; set; }
        public Boolean Op6 { get; set; }
        public Boolean Op7 { get; set; }
        public string Descripcion { get; set; }
        public string RazonDescripcion { get; set; }
        public DateTime FechaSys { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string UUsuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
    }
}

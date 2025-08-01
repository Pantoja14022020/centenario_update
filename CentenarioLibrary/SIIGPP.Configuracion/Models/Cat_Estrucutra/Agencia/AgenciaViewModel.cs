using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Estrucutra.Agencia
{
    public class AgenciaViewModel
    {
        public Guid IdAgencia { get; set; }
        public Guid DSPId { get; set; }
        public string NombreDirSub { get; set; }
        public string  ContacotDirSub { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public Guid DistritoId { get; set; }
        public string NombreDistrito { get; set; }
        public string TipoServicio { get; set; }
        public string Clave { get; set; }
        public string cargoResponsable { get; set; }
        public Boolean Condetencion { get; set; }
        public Boolean Activa { get; set; }
        public string Municipio { get; set; }
    }
}

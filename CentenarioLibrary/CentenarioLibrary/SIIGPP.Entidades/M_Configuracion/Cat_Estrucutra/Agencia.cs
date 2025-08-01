using SIIGPP.Entidades.M_Cat.Turnador; 
using SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Estructura
{
    public class Agencia
    {
        public Guid IdAgencia { get; set; } 
        public Guid DSPId { get; set; }  
        public string Nombre { get; set; }
        public string Direccion { get; set; } 
        public string Telefono { get; set; } 
        public string Contacto { get; set; }
        public string Clave { get; set; }
        public string TipoServicio { get; set; }
        public Boolean Condetencion { get; set; }
        public Boolean Activa { get; set; }
        public string Municipio { get; set; }
        public DSP DSP { get; set; }
        public List<Turno> Turnos { get; set; }
        public List<ModuloServicio> ModuloServicios { get; set; }
    }
}

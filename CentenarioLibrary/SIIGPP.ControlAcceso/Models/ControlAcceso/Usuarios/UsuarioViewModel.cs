using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios
{
    public class UsuarioViewModel
    {
        public Guid idUsuario { get; set; } 
        public Guid rolId { get; set; }
        public string nombrerol { get; set; }
        public Guid distritoId { get; set; }
        public Guid dspId { get; set; }
        public Guid agenciaId { get; set; }
        public Guid moduloServicioId { get; set; }
        public string modulonombre { get; set; }
        public string usuario { get; set; }
        public byte[] password_hash { get; set; }
        public string nombre { get; set; } 
        public string direccion { get; set; }
        public string telefono { get; set; } 
        public string email { get; set; } 
        public string puesto { get; set; } 
        public bool condicion { get; set; }
        public int contador { get; set; }
        public string subdir { get; set; }
        public string nombreAgencia { get; set; }
        public bool Titular { get; set; }
        public string ResponsableCuenta { get; set; }
        public string UrlDistrito { get; set; }
    }
}

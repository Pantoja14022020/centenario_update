using SIIGPP.Entidades.M_Configuracion.Cat_Estructura; 
using SIIGPP.Entidades.M_Panel.M_PanelControl; 
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_ControlAcceso.Roles;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.Entidades.M_ControlAcceso.Usuarios
{
  public  class Usuario
    {
        public Guid IdUsuario { get; set; }
        public Guid clave { get; set; }
        public Rol rol { get; set; }
        public Guid RolId { get; set; } 
        public ModuloServicio ModuloServicio { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string puesto { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public bool condicion { get; set; }
        public bool Titular { get; set; }
        public string ResponsableCuenta { get; set; }
    }
}

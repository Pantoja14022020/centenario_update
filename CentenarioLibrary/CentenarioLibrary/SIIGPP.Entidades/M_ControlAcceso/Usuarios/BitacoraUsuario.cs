using SIIGPP.Entidades.M_Configuracion.Cat_Estructura; 
using SIIGPP.Entidades.M_Panel.M_PanelControl; 
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_ControlAcceso.Roles;
using SIIGPP.Entidades.M_Cat.Orientacion;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_ControlAcceso.Usuarios
{
  public  class BitacoraUsuario
    {
        [Key]
        public Guid IdBitacoraUsuario { get; set; }
        public Guid UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string DescripcionMovimiento { get; set; }
        public Guid ModuloServicioIdAnterior { get; set; }
        public string DistritoFallo { get; set; }
        public string ArregloDistritoFallo { get; set; }
        public string Proceso { get; set; }
        public bool Status { get; set; }
        public string DistritoHaceMovimiento { get; set; }
        public string DirSubHaceMovimiento { get; set; }
        public string AgenciaHaceMovimiento { get; set; }
        public string ModuloHaceMovimiento { get; set; }
        public Guid ModuloServicioIdHaceMovimiento { get; set; }
        public Guid UsuarioIdHaceMovimiento { get; set; }
        public string ResponsableCuentaHaceMovimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string RutaResponsiva { get; set; }


    }
}

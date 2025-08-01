using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios
{
    public class BitacoraUsuariosViewModel
    {


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
    public class EditarBitacoraUsuariosViewModel
    {


        public Guid IdBitacoraUsuario { get; set; }
        public string DistritoFallo { get; set; }
        public string ArregloDistritoFallo { get; set; }
        public bool Status { get; set; }
    }
}

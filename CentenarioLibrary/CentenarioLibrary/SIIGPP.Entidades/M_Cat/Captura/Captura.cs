using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.RegistrosTableroI;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Captura
{
    public class Captura
    {
        [Key]
        public Guid IdCaptura { get; set; }
        public RHecho RHecho { get; set; }
        public Guid RHechoId { get; set; }
        public RegistroTableroI RegistroTableroI { get; set; }
        public Guid RegistroTableroId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public ModuloServicio UModuloServicio { get; set; } 
        public Guid UModuloServicioId { get; set; }
        public ModuloServicio RemitioModuloServicio { get; set; }
        public Guid RemitioModuloServicioId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

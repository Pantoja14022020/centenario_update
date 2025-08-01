using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.RegistrosTableroI
{
    public class RegistroTableroI

    {
        [Key]
        public Guid IdRegistroTableroI { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid RHechoId { get; set; }
        public string TipoRegistroTableroI { get; set; }
        public string Distrito { get; set; }
        public string DirSub { get; set; }
        public string Agencia { get; set; }    
        public string Modulo { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RegistrosTableroI
{
    public class CrearViewModelRTI
    {
     
        public Guid RHechoId { get; set; }
        public string TipoRegistroTableroI { get; set; }
        public string Distrito { get; set; }
        public string DirSub { get; set; }
        public string Agencia { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Modulo { get; set; }
        public Guid UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

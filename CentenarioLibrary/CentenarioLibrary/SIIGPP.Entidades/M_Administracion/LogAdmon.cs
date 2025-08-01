using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogAdmon
    {
        public Guid IdLogAdmon { get; set; }
        public Guid UsuarioId { get; set; }
        public string Tabla { get; set; }
        public Guid RegistroId { get; set; }
        public DateTime FechaMov { get; set; }
        public Guid SolicitanteId { get; set; }
        public string RazonMov { get; set; }
        public Guid MovimientoId { get; set; }
      
    }
}

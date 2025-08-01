using SIIGPP.Entidades.M_Cat.PRegistro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.PAtencion
{
    public class PreAtencion
    {
        [Key]
        public Guid IdPAtencion { get; set;}
        public PreRegistro registros { get; set; }
        public DateTime? FechaHoraRegistro { get; set; }
        public DateTime? FechaHoraAtencion { get; set; }
        public string DirSubProcuInicial { get; set; }
        public Boolean StatusAtencion { get; set; }
        public Boolean StatusRegistro { get; set; }
        public string MedioDenuncia { get; set; }
        public Boolean ContencionVicitma { get; set; }
        public Guid PRegistroId { get; set; }
    }
}

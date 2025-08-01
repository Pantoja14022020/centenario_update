using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Registro
{
    public class RegistroViewModel
    {
        public Guid IdRAtencion { get; set; }
        public string rac { get; set; }
        public Guid racId { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public DateTime? FechaHoraAtencion { get; set; }
        public DateTime? FechaHoraCierre { get; set; }
        public string u_Nombre { get; set; }
        public string u_Puesto { get; set; }
        public string u_Modulo { get; set; }
        public string DistritoInicial { get; set; }
        public string DirSubProcuInicial { get; set; }
        public string AgenciaInicial { get; set; }
        public Boolean StatusAtencion { get; set; }
        public Boolean StatusRegistro { get; set; }
        public string MedioDenuncia { get; set; }
        public Boolean ContencionVicitma { get; set; }
        public string Numerooficio { get; set; }





    }
}

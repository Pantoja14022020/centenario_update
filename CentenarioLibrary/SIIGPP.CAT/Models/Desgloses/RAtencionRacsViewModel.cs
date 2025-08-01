using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Desgloses
{
    public class RAtencionRacsViewModel
    {
        //RAC
        public Guid IdRac { get; set; }
        public string Indicador { get; set; }
        public Guid DistritoId { get; set; }
        public string CveDistrito { get; set; }
        public int DConsecutivo { get; set; }
        public Guid AgenciaId { get; set; }
        public string CveAgencia { get; set; }
        public int AConsecutivo { get; set; }
        public int Año { get; set; }
        public string racg { get; set; }
        public Boolean Asignado { get; set; }
        public string Ndenuncia { get; set; }

        //RAtencion
        public Guid IdRAtencion { get; set; }
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
        public string ModuloServicio { get; set; }
        public string MedioLlegada { get; set; }





    }
}

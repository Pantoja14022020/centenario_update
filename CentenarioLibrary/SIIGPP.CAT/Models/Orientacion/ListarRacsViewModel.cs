using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class ListarRacsViewModel
    {
        public Guid RHechoId { get; set; }
        public Guid RAtencionId { get; set; }
        public Guid RacId { get; set; }
        // RATENCION
        //************************************************************
        public string u_Nombre { get; set; }
        public string u_Puesto { get; set; }
        public string u_Modulo { get; set; }
        public string DistritoInicial { get; set; }
        public string DirSubProcuInicial { get; set; }
        public string AgenciaInicial { get; set; }
        public Guid Agenciaid { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public string Rac { get; set; }
        // RHECHO
        //***********************************************************

        public string rBreve { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public string NDenunciaOficio { get; set; }
        public string Numerooficio { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class ListarEntrevistaInicial
    {
        public Guid RAtencionId { get; set; }
        public Guid RHechoId { get; set; }
        // RATENCION
        //************************************************************
        public string u_Nombre { get; set; }
        public string u_Puesto { get; set; }
        public string u_Modulo { get; set; }
        public string DistritoInicial { get; set; }
        public string DirSubProcuInicial { get; set; }
        public string Mediodenuncia { get; set; }
        public string AgenciaInicial { get; set; }
        // RHECHO
        //***********************************************************
        public Guid Agenciaid { get; set; }
        public Boolean Status { get; set; }
        public Guid? nucId { get; set; }
        public string nuc { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public string RBreve { get; set; }
        public string NarrativaHechos { get; set; }
        public string Vanabim { get; set; }

        public string NDenunciaOficio { get; set; }

        public string Statuscarpeta { get; set; }
        public string Etapacarpeta { get; set; }
        public string Modulos { get; set; }
        public string MedioLlegada { get; set; }

    }
}

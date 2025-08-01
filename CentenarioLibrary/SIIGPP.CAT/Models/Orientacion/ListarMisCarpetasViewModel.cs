using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Orientacion
{
    public class ListarMisCarpetasViewModel
    {
     
        public Guid RAtencionId { get; set; }
        public Guid RHechoId { get; set; }
        public string u_Nombre { get; set; }
        public string u_Puesto { get; set; }
        public string u_Modulo { get; set; }
        public string DistritoInicial { get; set; }
        public string DirSubProcuInicial { get; set; }
        public string AgenciaInicial { get; set; }
        public Guid? Agenciaid { get; set; }
        public Boolean Status { get; set; }
        public Guid? nucId { get; set; }
        public Guid? IdModuloServicio { get; set; }
        public string nuc { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public string Modulo { get; set; }
        public string RAC { get; set; }
        public Guid RacId { get; set; }
        public string NDenunciaOficio { get; set; }
        public string Modulos { get; set; }

        public string agenciaactual { get; set; }
        public string distritoactual { get; set; }
        public string moduloactual { get; set; }
        public string MedioLlegada { get; set; }
        public string Victima { get; set; }
        public string dspactual { get; set; }
    }
}

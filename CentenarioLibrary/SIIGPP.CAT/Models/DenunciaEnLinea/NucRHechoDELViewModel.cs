using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DenunciaEnLinea
{
    public class NucRHechoDELViewModel
    {

        // NUC
        //************************************************************
        public Guid idNuc { get; set; }
        public string Indicador { get; set; }
        public Guid DistritoId { get; set; }
        public int DConsecutivo { get; set; }
        public Guid AgenciaId { get; set; }
        public int AConsecutivo { get; set; }
        public int Año { get; set; }
        public string CveAgencia { get; set; }
        public string CveDistrito { get; set; }
        public string nucg { get; set; }
        public string StatusNUC { get; set; }
        public string Etapanuc { get; set; }
        // RHECHO
        //***********************************************************
        public Guid IdRHecho { get; set; }
        public Guid RAtencionId { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid AgenciaidR { get; set; }
        public string FechaReporte { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public Boolean Status { get; set; }
        public string RBreve { get; set; }
        public string NarrativaHechos { get; set; }
        public Guid? nucId { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public DateTime FechaElevaNuc2 { get; set; }
        public string Vanabim { get; set; }
        public string NDenunciaOficio { get; set; }
        public string Texto { get; set; }
        public DateTime FechaHoraSuceso2 { get; set; }
        public string Observaciones { get; set; }
    }
}

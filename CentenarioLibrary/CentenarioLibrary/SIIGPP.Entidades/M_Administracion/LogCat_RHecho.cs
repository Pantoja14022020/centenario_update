using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace SIIGPP.Entidades.M_Administracion
{
    public class LogCat_RHecho
    {        
        [Key]
        public Guid IdAdminRHecho { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdRHecho { get; set; }
        public Guid RAtencionId { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid Agenciaid { get; set; }
        public string FechaReporte { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }        
        public DateTime FechaHoraSuceso2 { get; set; }
        public Boolean Status { get; set; }
        public string RBreve { get; set; }
        public string NarrativaHechos { get; set; }
        public Guid? NucId { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public DateTime FechaElevaNuc2 { get; set; }
        public string Vanabim { get; set; }
        public string NDenunciaOficio { get; set; }
        public string Texto { get; set; }
        public string Observaciones { get; set; }
    }
}

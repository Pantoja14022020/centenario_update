 
using SIIGPP.Entidades.M_Cat.GNUC;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Orientacion
{
    public class RHecho
    {
        [Key]
        public Guid IdRHecho { get; set; }    
        public Guid RAtencionId { get; set; }
        public RAtencion RAtencion { get; set; }
        
        public Guid ModuloServicioId { get; set; }
        public ModuloServicio ModuloServicio { get; set; }
        public Guid Agenciaid { get; set; }
        public Agencia Agencia { get; set; }
        public string  FechaReporte { get; set; }
        public DateTime? FechaHoraSuceso  { get; set; }
        public DateTime FechaHoraSuceso2 { get; set; }
        public Boolean Status { get; set; }
        public string RBreve { get; set; }
        public  string NarrativaHechos { get; set; }
        public Guid? NucId { get; set; }
        [Required]
        public Nuc NUCs { get; set; } 
        public DateTime? FechaElevaNuc { get; set; }
        public DateTime FechaElevaNuc2 { get; set; }
        public string Vanabim { get; set; }
        public string NDenunciaOficio { get; set; }
        public string Texto { get; set; }
        public string Observaciones { get; set; }
    }
}

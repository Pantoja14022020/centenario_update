using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_Cat.Consultas
{
    public class queryComprobarDR
    {
        [Key]
        public Guid IdRAP { get; set; }
        public Guid IdRHecho { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public Guid? IdDDelito { get; set; }
        public string ClasificacionPersona { get; set; }
        public string StatusNUC { get; set; }
        public string Etapanuc { get; set; }
        public Guid? IdPersona { get; set; }
        public Guid? IdRDH { get; set; }
        public Guid? idAmpliacion { get; set; }


    }
    public class queryComprobarNuc
    {
        [Key]
        public Guid IdRHecho { get; set; }
        public bool FechaSuceso { get; set; }
        public bool DireccionDelito { get; set; }
        public bool Imputado { get; set; }
        public bool Victima { get; set; }
        public bool Estatus { get; set; }
        public bool Delito { get; set; }
        public bool Entrevista { get; set; }


    }
}

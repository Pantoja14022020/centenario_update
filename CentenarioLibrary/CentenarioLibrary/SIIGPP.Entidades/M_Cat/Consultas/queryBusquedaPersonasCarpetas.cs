using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_Cat.Consultas
{
    public class queryBusquedaPersonasCarpetas
    {
        [Key]
        public Guid IdPersona { get; set; }
        public string personaRelacionada { get; set; }
        public string ClasificacionPersona { get; set; }
        public string racg { get; set; }
        public string nucg { get; set; }
        public DateTime? FechaCarpeta { get; set; }
        public string genero { get; set; }
        public string status { get; set; }
        public string Delito { get; set; }
        public string DelitoEspecifico { get; set; }
        public string moduloactual { get; set; }
    }
}

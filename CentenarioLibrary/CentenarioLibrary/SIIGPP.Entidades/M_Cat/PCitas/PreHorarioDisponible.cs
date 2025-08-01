using SIIGPP.Entidades.M_Cat.PAtencion;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.PCitas
{
    public class PreHorarioDisponible
    {
        [Key]
        public Guid idHorarioDisponible { get; set; }
        public TimeSpan horaInicio { get; set; } 
        public TimeSpan horaFinal { get; set; }
        public Guid AgenciaId { get; set; }
        public Agencia agencia { get; set; }
        public int densidadPorHora { get; set; }
        
    }
}

using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Cat.PRegistro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.PRegistro
{
 public   class PreRegistro
    {
        [Key]
        public Guid IdPRegistro { get; set; }
        public string Indicador { get; set; }
        public Distrito distrito { get; set; }
        
        public Guid DistritoId { get; set; }
        public string CveDistrito { get; set; }
        public int Ano { get; set; }
        public string noReg{ get; set; }
        public Boolean Asignado { get; set; }
        public string Ndenuncia { get; set; }
        public Boolean Atendido { get; set; }
        public Guid? RacId { get; set; }
        public string RBreve { get; set; }
        public DateTime fechaSuceso { get; set; }
    }
}

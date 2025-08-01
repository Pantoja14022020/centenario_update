using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.GRAC
{
  public  class Rac
    {
        [Key]
        public Guid idRac { get; set; }
        public string Indicador { get; set; }
        public RAtencion RAtencion { get; set; }
        public Guid DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        public string CveDistrito { get; set; }
        public int DConsecutivo { get; set; }
        public Guid AgenciaId { get; set; }
        public Agencia Agencia { get; set; }
        public string CveAgencia { get; set; }
        public int AConsecutivo { get; set; }
        public int Año { get; set; }
        public string racg { get; set; }
        public string Ndenuncia { get; set; }
        public Boolean Asignado { get; set; }
    }
}

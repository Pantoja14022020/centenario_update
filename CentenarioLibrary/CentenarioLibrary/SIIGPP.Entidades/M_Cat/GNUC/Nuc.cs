using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.GNUC
{
    public class Nuc
    {
            
        [Key]
        public Guid idNuc { get; set; }
        public string Indicador { get; set; }
        public Guid DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        public string CveDistrito { get; set; }
        public int DConsecutivo { get; set; }
        public Guid AgenciaId { get; set; }
        public Agencia Agencia { get; set; }
        public string CveAgencia { get; set; }
        public int AConsecutivo { get; set; }
        public int Año { get; set; }
        public string nucg { get; set; }
        public string StatusNUC { get; set; }
        public string Etapanuc { get; set; }
    }
 
}

using SIIGPP.Entidades.M_PI.InformacionJuridico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_PI.OAprhensionBitacoras
{
    public class OAprhensionBitacora
    {
        [Key]
        public Guid IdOAprhensionBitacora { get; set; }
        public OrdenAprension OrdenAprension { get; set; }
        public Guid OrdenAprensionId { get; set; }
        public string Texto { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime FechaSys { get; set; }
    }
}

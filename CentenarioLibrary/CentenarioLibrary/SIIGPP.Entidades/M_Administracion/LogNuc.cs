using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogNuc
    {
        public Guid IdAdminNuc { get; set; }
        public Guid LogAdminId { get; set; }
        public Guid idNuc { get; set; }
        public string Indicador { get; set; }
        public Guid DistritoId { get; set; }
        public string CveDistrito { get; set; }
        public int DConsecutivo { get; set; }
        public Guid AgenciaId { get; set; }
        public string CveAgencia { get; set; }
        public int AConsecutivo { get; set; }
        public int Año { get; set; }
        public string nucg { get; set; }
        public string StatusNUC { get; set; }
        public string Etapanuc { get; set; }
    }
}

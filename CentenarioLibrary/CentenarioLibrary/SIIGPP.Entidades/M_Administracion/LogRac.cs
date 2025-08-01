using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogRac
    {
        public Guid IdAdminRac { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid idRac { get; set; }
        public string Indicador { get; set; }
        public Guid DistritoId { get; set; }
        public string CveDistrito { get; set; }
        public int DConsecutivo { get; set; }
        public Guid AgenciaId { get; set; }
        public string CveAgencia { get; set; }
        public int AConsecutivo { get; set; }
        public int Ano { get; set; }
        public string racg { get; set; }
        public Boolean Asignado { get; set; }
        public string Ndenuncia { get; set; }        
    }
}

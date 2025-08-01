using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogExpediente
    {
        public Guid IdAdminExpediente { get; set; }
        public Guid IdExpediente { get; set; }
        public Guid RHechoId { get; set; }
        public Guid LogAdmonId { get; set; }
        public string Prefijo { get; set; }
        public int Cosecutivo { get; set; }
        public int Ano { get; set; }
        public string Sede { get; set; }
        public string NoExpediente { get; set; }
        public int NoDerivacion { get; set; }
        public string StatusAcepRech { get; set; }
        public string InformacionStatus { get; set; }
        public DateTime FechaRegistroExpediente { get; set; }
        public DateTime FechaDerivacion { get; set; }

    }
}

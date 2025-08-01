using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_IL.Agendas
{
    public class SolicitudAudiencia
    {
        public Guid IdSolicitudAudiencia { get; set; }
        public Guid DistritoId { get; set; }
        public String NUC { get; set; }
        public string NumOficio { get; set; }
        public string Partes { get; set; }
        public string delitos { get; set; }
        public int Estatus { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string solicitante { get; set; }
        public Guid AgendaId { get; set; }

    }
}

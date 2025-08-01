using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PersonaDesap
{
    public class ActualizarViewModel
    {
        public Guid IdPersonaDesaparecida { get; set; }
        public Guid PersonaId { get; set; }
        public string EstadoSalud { get; set; }
        public string Adicciones { get; set; }
        public string Padecimiento { get; set; }
        public string Etnia { get; set; }
        public Boolean PortabaMedioComunicacion { get; set; }
        public string GrupoDelictivo { get; set; }
        public string ProcedenciaGrupoDelictivo { get; set; }
        public DateTime FechaHoraUltAvistamiento { get; set; }
        public string NombrePersonaAcompanaba { get; set; }
        public string RelacionPersonaAcompanaba { get; set; }
        public string LocalizacionPersonaAcompanaba { get; set; }
        public string VestimentaAccesorios { get; set; }
    }
}

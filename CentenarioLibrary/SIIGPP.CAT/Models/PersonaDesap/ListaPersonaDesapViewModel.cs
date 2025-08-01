using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PersonaDesap
{
    public class ListaPersonaDesapViewModel
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
        public string PersonaDesap { get; set; }
        public string descripcionHechos { get; set; }
        public Guid? marcaTelefonoId { get; set; }
        public string marcaTelefono { get; set; }
        public string fotografiaURL { get; set; }
        public Guid? companiaTelefonicaId { get; set; }
        public string companiaTelefonica { get; set; }
        public string RelacionPersonaDenunciante { get; set; }
        public Boolean AcompanabaDenunciante { get; set; }
        public DateTime FechaSys { get; set; }
        public string DistritoCaptura { get; set; }
        public string AgenciaCaptura { get; set; }
        public string NombreCaptura { get; set; }
        public string PersonaCondicion { get; set; }
        public string OtraMarca { get; set; }
    }
}

using System;
using SIIGPP.Entidades.M_Cat.Registro;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.PersonaDesap
{
    public class RPersonaDesap
    {
        public Guid IdPersonaDesaparecida { get; set; }
        public Guid PersonaId { get; set; }
        public Guid RHechoId { get; set; }
        public Persona Persona { get; set; }
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
        public Guid? companiaTelefonicaId { get; set; }
        public CompaniaTelefonica CompaniaTelefonica { get; set; }
        public Guid? marcaTelefonoId { get; set; }
        public MarcaTelefono MarcaTelefono {get;set;}
        public string descripcionHechos { get; set; }
        public string fotografiaURL { get; set; }
        public string RelacionPersonaDenunciante { get; set; }
        public Boolean? AcompanabaDenunciante  { get; set; }
        public DateTime FechaSys {  get; set; }
        public string DistritoCaptura { get; set; }
        public string AgenciaCaptura { get; set; }
        public string NombreCaptura { get; set; }
        public string PersonaCondicion { get; set; }
        public string OtraMarca { get; set; }
    }
}

using System;
using SIIGPP.Entidades.M_Cat.Registro;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace SIIGPP.Entidades.M_Administracion
{
    public class LogRPersonaDesap
    {
        [Key]
        public Guid IdAdminPersonaDesaparecida { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdPersonaDesaparecida { get; set; }
        public Guid PersonaId { get; set; }
        public Guid RHechoId { get; set; }
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
        public Guid? marcaTelefonoId { get; set; }
        public string descripcionHechos { get; set; }
        public string fotografiaURL { get; set; }
       // public VehiculoPersonaDesap vehiculo { get; set; }
    }
}

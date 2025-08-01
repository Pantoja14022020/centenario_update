
using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace SIIGPP.Entidades.M_Administracion
{
   public  class LogRDiligencias
    {
        [Key]
        public Guid IdAdminRDiligencias { get; set; }
        public Guid LogAdmonId { get; set; }
        public Guid IdRDiligencias { get; set; }
        public Guid rHechoId { get; set; }
        public string FechaSolicitud { get; set; }
        public DateTime FechaSys { get; set; }
        public string Dirigidoa { get; set; }
        public string DirSubPro { get; set; }
        public string EmitidoPor { get; set; }
        public string uDirSubPro { get; set; }
        public string UPuesto { get; set; }
        public string StatusRespuesta { get; set; }
        public string Servicio { get; set; }
        public string Especificaciones { get; set; }
        public string Prioridad { get; set; }
        public Guid ASPId { get; set; }
        public string Modulo { get; set; }
        public string Agencia { get; set; }
        public string Respuestas { get; set; }
        public Boolean ConIndicio { get; set; }
        public string NUC { get; set; }
        public string Textofinal { get; set; }
        public string NumeroOficio { get; set; }
        public string NodeSolicitud { get; set; }
        public string NumeroDistrito { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public Boolean Dirigido { get; set; }
        public Boolean RecibidoF { get; set; }
        public DateTime FechaRecibidoF { get; set; }
        public string Respuesta { get; set; }
        public Boolean EtapaInicial { get; set; }
        public Guid DSPDEstino { get; set; }
        public Guid DistritoId { get; set; }
        
    }
}

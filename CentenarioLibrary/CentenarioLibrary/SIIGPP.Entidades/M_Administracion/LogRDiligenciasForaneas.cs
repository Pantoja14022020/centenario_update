using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogRDiligenciasForaneas
    {

        public Guid IdAdminRdiligenciasForaneas { get; set; }
        public Guid IdRDiligenciasForaneas { get; set; }
        public Guid LogAdmonId { get; set; }
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
        public Guid AgenciaEnvia { get; set; }
        public Guid AgenciaRecibe { get; set; }
        public Boolean EtapaInicial { get; set; }
    }
}

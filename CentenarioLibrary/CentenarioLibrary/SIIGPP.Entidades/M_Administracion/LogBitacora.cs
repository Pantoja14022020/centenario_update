using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Administracion
{
    public class LogBitacora
    {
        public Guid IdAdminBitacora {get;set;}
        public Guid LogAdmonId { get; set; }
        public Guid IdBitacora { get; set; }
        public string Tipo { get; set; }
        public string Descipcion { get; set; }
        public string Distrito { get; set; }
        public string Dirsubproc  { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
        public string Fechareporte { get; set; }
        public DateTime? Fechasis { get; set; }
        public Guid rHechoId { get; set; }
        public Guid IdPersona { get; set; }
        public string Numerooficio { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.AdminInfo
{
    public class idBorrado
    {
        public Guid registroId { get; set; }
        public Guid rAtencionId { get; set; }
        public Guid rHechoId { get; set; }
        public string textoMod { get; set; }
        public string textoMod2 { get; set; }

    }

    public class EliminarNuc
    {
        public Guid solicitante { get; set; }
        public String razon { get; set; }
        public String tabla { get; set; }
        public Guid usuario { get; set; }
        public idBorrado infoBorrado { get; set; }
    }

    public class ModPersona
    {
        //public Guid rAtencionId { get; set; }
        public string nombre { get; set; }
        public string puesto { get; set; }
    }
}
 

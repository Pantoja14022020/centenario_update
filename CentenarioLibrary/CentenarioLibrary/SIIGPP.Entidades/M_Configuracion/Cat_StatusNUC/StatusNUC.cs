using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_StatusNUC
{
    public class StatusNUC
    {
        public Guid IdStatusNuc { get; set; }
        public  string NombreStatus { get; set; }
        public string Etapa { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_ActosInvestigacion
{
    public class ActoInvestigacion
    {
        public Guid IdActonvestigacion { get; set; }

        public string Nombre { get; set; }

        public string Nomenclatura { get; set; }

        public string Descripcion { get; set; }

        public Boolean RAutorizacion { get; set; } 

    }
}



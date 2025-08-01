using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.RDHecho;

namespace SIIGPP.Entidades.M_Cat.Indicios
{
    public class Indicios
    {
        public Guid IdIndicio { get; set; }

        public Guid RHechoId { get; set; }
        public RHecho RHecho { get; set; }

        public string TipoIndicio { get; set; }

        public string Descripcion { get; set; }

        public string Status { get; set; }
        public string Etiqueta { get; set; }

        public string QIniciaCadena { get; set; }

        public string InstitucionQI { get; set; }

        public string Corporacion { get; set; }

        public string UltimaUbicacion { get; set; }

        public string Distrito { get; set; }

        public string Subproc { get; set; }

        public string Agencia { get; set; }

        public string Usuario { get; set; }

        public string Puesto { get; set; }
        public string Modulo { get; set; }
        public string Matricula { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Calibre { get; set; }

    }
}

using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.MedCautelares
{
  public  class MedidasCautelares
    {
        public Guid IdMedCautelares { get; set; }
        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }

        public Guid RHechoId { get; set; }
        public RHecho RHecho { get; set; }
        public string MedidaCautelar { get; set; }
        public string Tiempo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }

        public DateTime FechaSys { get; set; }
        public string Distrito { get; set; }
        public string DirSubProc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
    }
}

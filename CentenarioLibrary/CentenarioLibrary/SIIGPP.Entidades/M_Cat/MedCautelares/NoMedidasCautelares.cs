using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.MedCautelares
{
    public class NoMedidasCautelares
    {
        public Guid IdNoMedidasCautelares { get; set; }
        public Guid MedidasCautelaresId { get; set; }
        public MedidasCautelares MedidasCautelares { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }
}

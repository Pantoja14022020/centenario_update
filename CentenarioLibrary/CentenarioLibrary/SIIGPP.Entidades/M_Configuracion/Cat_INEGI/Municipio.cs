using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_INEGI
{
    public class Municipio
    {
        [Key]
        public int IdMunicipio { get; set; }
        public int Numero_Mpio { get; set; }
        public int EstadoId { get; set; }
        public string Nombre { get; set; }
        public Estado estado { get; set; }
        public ICollection<Localidad> Localidads { get; set; }

    }
}

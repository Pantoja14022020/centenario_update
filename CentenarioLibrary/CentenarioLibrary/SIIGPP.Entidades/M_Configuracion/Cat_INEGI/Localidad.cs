using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_INEGI
{
    public class Localidad
    {
        [Key]
        public int IdLocalidad { get; set; }
        public int MunicipioId { get; set; }
        public String Nombre { get; set; }
        public int CP { get; set; }
        public string Zona { get; set; }
        public Municipio municipio { get; set; }
    }
}

using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Direcciones
{
    public class DireccionDelito
    {
        [Key]
        public Guid IdDDelito { get; set; }   
        public Guid RHechoId { get; set; } 
        public RHecho rHecho { get; set; }
        public string LugarEspecifico { get; set; }
        public string  Calle { get; set; }
        public string NoInt { get; set; }
        public string NoExt { get; set; }
        public string EntreCalle1 { get; set; }
        public string EntreCalle2 { get; set; }
        public string Referencia { get; set; }
        public string Pais { get; set; }
        public  string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public int? CP { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int? TipoVialidad { get; set; }
        public int? TipoAsentamiento { get; set; }
    }
}

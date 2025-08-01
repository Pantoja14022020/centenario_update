using System;

namespace SIIGPP.CAT.Models.Desgloses
{
    public class DireccionesViewModel
    {
        //DIRECCION PERSONAL
        public Guid idPersona { get; set; }
        public string calle { get; set; }
        public string noint { get; set; }
        public string noext { get; set; }
        public string entrecalle1 { get; set; }
        public string entrecalle2 { get; set; }
        public string referencia { get; set; }
        public string pais { get; set; }
        public string estado { get; set; }
        public string municipio { get; set; }
        public string localidad { get; set; }
        public int? cp { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int? tipoVialidad { get; set; }
        public int? tipoAsentamiento { get; set; }
        //DIRECCION ESCUCHA 
        public Guid de_idPersona { get; set; }
        public string de_calle { get; set; }
        public string de_noint { get; set; }
        public string de_noext { get; set; }
        public string de_entrecalle1 { get; set; }
        public string de_entrecalle2 { get; set; }
        public string de_referencia { get; set; }
        public string de_pais { get; set; }
        public string de_estado { get; set; }
        public string de_municipio { get; set; }
        public string de_localidad { get; set; }
        public int? de_cp { get; set; }
        public string de_lat { get; set; }
        public string de_lng { get; set; }
        public int? de_tipoVialidad { get; set; }
        public int? de_tipoAsentamiento { get; set; }
    }
}

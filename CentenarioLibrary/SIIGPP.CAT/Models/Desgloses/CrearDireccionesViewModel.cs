using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Desgloses
{
    public class CrearDireccionesViewModel
    {



        //DIRECCION PERSONAL
        public Guid idPersona { get; set; }
        public Guid RAPId { get; set; }
        public Guid IdRHecho { get; set; }
        public string LugarEspecifico { get; set; }
        public string Calle { get; set; }
        public string NoInt { get; set; }
        public string NoExt { get; set; }
        public string EntreCalle1 { get; set; }
        public string EntreCalle2 { get; set; }
        public string Referencia { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public int CP { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int? tipoVialidad { get; set; }
        public int? tipoAsentamiento { get; set; }
        //DIRECCION ESCUCHA 
        public string de_Calle { get; set; }
        public string de_NoInt { get; set; }
        public string de_NoExt { get; set; }
        public string de_EntreCalle1 { get; set; }
        public string de_EntreCalle2 { get; set; }
        public string de_Referencia { get; set; }
        public string de_Pais { get; set; }
        public string de_Estado { get; set; }
        public string de_Municipio { get; set; }
        public string de_Localidad { get; set; }
        public int de_CP { get; set; }
        public string de_lat { get; set; }
        public string de_lng { get; set; }
        public int? de_tipoVialidad { get; set; }
        public int? de_tipoAsentamiento { get; set; }
        public Boolean DatosProtegidos { get; set; }
        public string InstitutoPolicial { get; set; }
        public string InformePolicial { get; set; }

        public string DocPoderNotarial { get; set; }
        public Boolean InicioDetenido { get; set; }
        public string CumpleRequisitoLey { get; set; }
        public string DecretoLibertad { get; set; }
        public string DispusoLibertad { get; set; }

    }
}

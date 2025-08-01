using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_JuzgadoAgencias
{
    public class JuzgadosAgencias
    {
        public Guid IdJuzgadoAgencia { get; set; }
        public Distrito Distrito { get; set; }
        public Guid DistritoId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Encargado { get; set; }
    }
}




        

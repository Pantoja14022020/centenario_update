using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos
{
    public class Modelo
    {
        public Guid IdModelo { get; set; }
        public string Dato { get; set; }
        public Guid MarcaId { get; set; }
        public Marca Marca { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Generales.Telefonia
{
    public class ListarCompaniaTelefonicaViewModel
    {
        public Guid IdCompaniaTelefonica { get; set; }
        public string nombre { get; set; }
        public Boolean activa { get; set; }
    }
}


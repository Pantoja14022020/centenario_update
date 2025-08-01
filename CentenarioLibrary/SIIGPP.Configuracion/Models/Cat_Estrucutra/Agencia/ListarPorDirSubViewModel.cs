using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_Estrucutra.Agencia
{
    public class ListarPorDirSubViewModel
    {
        public Guid IdAgencia { get; set; } 
        public string Nombre { get; set; }

        public Boolean Condicion {  get; set; }
    }
}

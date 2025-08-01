using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAcuerdoReparatorio
{
    public class GET_ListarTodosViewModel
    {
        public string etiqueta { get; set; }
        public int valor { get; set; }
    }

    public class GET_CoordinacionDistritos
    {
        public Guid IdCoordinacionDistritos { get; set; }
        public Guid DistritoId { get; set; }
        public string NombreDis {  get; set; }
    }
}

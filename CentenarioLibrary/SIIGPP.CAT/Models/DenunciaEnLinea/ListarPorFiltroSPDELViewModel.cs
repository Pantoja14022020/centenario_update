using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.DenunciaEnLinea
{
    public class ListarPorFiltroSPDELViewModel
    {
        public Guid IdASP { get; set; }
        public string Responsable { get; set; }
        public Guid ServicioPericialId { get; set; }
        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public string Materia { get; set; }
        public  Boolean AtencionVictimas { get; set; }
    }
}

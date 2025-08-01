using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.Configuracion.Models.Cat_ServicioPericiales.ASP
{
    public class ListarPorFiltroSPViewModel
    {
        public Guid IdASP { get; set; }
        public Guid AgenciaId { get; set; }
        public string NombreAgencia { get; set; }
        public Guid DSPId { get; set; }
        public string NombreDirSub { get; set; } 
        public string Responsable { get; set; }
        public Guid ServicioPericialId { get; set; }
        public string Codigo { get; set; }
        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public string Materia { get; set; }
        public  Boolean AtencionVictimas { get; set; }
    }
}

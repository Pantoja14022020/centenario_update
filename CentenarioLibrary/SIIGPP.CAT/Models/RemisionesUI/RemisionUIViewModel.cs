using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RemisionesUI
{
    public class RemisionUIViewModel
    {
        public Guid IdRemisionUI { get; set; }
        public Guid RHechoId { get; set; }
        public string Fecha { get; set; }
        public string DirigidoA { get; set; }
        public string Status { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public string PuestoA { get; set; }
        public Guid Moduloqueenvia { get; set; }
        public string Rechazo { get; set; }
        public DateTime FechaRechazo { get; set; }
        public string NumeroOficio { get; set; }
        public Guid ModuloServicioId { get; set; }
        public Guid AgenciaQueenvia { get; set; }
        public string Nuc { get; set; }
        public Boolean EnvioExitosoTF { get; set; }
        public string ParaDonde { get; set; }
    }
    
    public class RemisionUIViewModelPos : RemisionUIViewModel
    {
        public long posicion { get; set; }
    }
}

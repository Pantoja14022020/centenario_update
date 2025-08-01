using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RegistrosTableroI
{
    public class ListarModulosViewModel
    {
        public Guid IdRegistroTableroI { get; set; }
        public Guid IdRHecho { get; set; }
        public Guid IdRAtencion { get; set; }
        public string NUC { get; set; }
        public string Ultimo_Registro { get; set; }
        public string Modulo { get; set; }
        public string Agencia { get; set; }
        public string Direccion { get; set; }
        public string Distrito_Procedencia { get; set; }
        public string Distrito_Actual { get; set; }
        public string FechaReporte { get; set; }
        public DateTime Fecha_del_Ultimo_Registro { get; set; }
        public int Dias_Inactiva { get; set; }

    }
}

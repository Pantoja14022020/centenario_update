using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_Cat.Consultas
{
    public class queryBusquedaCarpetas
    {
        [Key]
        public Guid IdRHecho { get; set; }
        public Guid IdRAtencion { get; set; }
        public string u_Nombre { get; set; }
        public string u_Puesto { get; set; }
        public string u_Modulo { get; set; }
        public string DistritoInicial { get; set; }
        public string DirSubProcuInicial { get; set; }
        public string AgenciaInicial { get; set; }
        public Guid? IdAgencia { get; set; }
        public string agenciaactual { get; set; }
        public string distritoactual { get; set; }
        public string moduloactual { get; set; }
        public Boolean Status { get; set; }
        public Guid? nucId { get; set; }
        public Guid RacId { get; set; }
        public string nuc { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
        public DateTime? FechaHoraRegistro { get; set; }
        public string Modulo { get; set; }
        public Guid IdModuloServicio { get; set; }
        public string RAC { get; set; }
        public string NDenunciaOficio { get; set; }
        public string dspactual { get; set; }

        
        public string Victima { get; set; }
    }
}

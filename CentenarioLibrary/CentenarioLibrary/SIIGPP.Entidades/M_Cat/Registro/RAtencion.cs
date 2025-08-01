using SIIGPP.Entidades.M_Cat.GRAC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.Registro
{
    public class RAtencion
    {

        public Guid IdRAtencion { get; set; } 
        public DateTime FechaHoraRegistro { get; set; }
        public DateTime? FechaHoraAtencion { get; set; }
        public DateTime? FechaHoraCierre { get; set; }
        public string u_Nombre { get; set; }
        public string u_Puesto { get; set; }
        public string u_Modulo { get; set; }
        public string DistritoInicial { get; set; } 
        public string DirSubProcuInicial { get; set; }
        public string AgenciaInicial { get; set; }
        public Boolean StatusAtencion { get; set; }
        public Boolean StatusRegistro { get; set; }
        public string MedioDenuncia { get; set; }
        public Boolean ContencionVicitma { get; set; }
        public Guid racId { get; set; }
        public Rac RACs { get; set; }
        public List<RAP> RAPs { get; set; }
        public string ModuloServicio { get; set; }
        public string MedioLlegada { get; set; }

    }
}

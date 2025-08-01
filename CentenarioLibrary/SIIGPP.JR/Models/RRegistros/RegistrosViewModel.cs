using SIIGPP.Entidades.M_JR.REnvio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RRegistros
{
    public class RegistrosViewModel
    {
        public Guid IdRegistro { get; set; }
        public Guid EnvioId { get; set; }
        public string Tipo { get; set; }
        public string QuienPorRegistro { get; set; }
        public string Descripcion { get; set; }
        public string Distrito { get; set; }
        public string Dirsubproc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
        public DateTime Fechasis { get; set; }
        public string Numerooficio { get; set; }

    }
}

 
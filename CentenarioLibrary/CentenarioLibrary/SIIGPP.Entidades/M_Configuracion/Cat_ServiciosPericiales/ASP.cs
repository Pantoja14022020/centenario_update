using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales
{
    public class ASP
    {
        public Guid IdASP { get; set; }
        public Agencia Agencia { get; set; }
        public Guid AgenciaId { get; set; }
        public ServicioPericial ServicioPericial { get; set; }
        public Guid ServicioPericialId { get; set; }
    }
}

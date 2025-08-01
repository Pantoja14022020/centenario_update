using SIIGPP.Entidades.M_Cat.PAtencion;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.PCitas
{
    public class PreCitas
    {
        public Guid IdPCita { get; set; }
        public Guid PRegistroId { get; set; }
        public PreRegistro registros { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public Guid AgenciaId { get; set; }

        public Agencia agencia { get; set; }

        public int atendido { get; set; }

        

        

    }
}

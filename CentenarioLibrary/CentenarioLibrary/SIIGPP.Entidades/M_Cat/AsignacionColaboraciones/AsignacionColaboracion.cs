using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.SColaboracionesMP;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;

namespace SIIGPP.Entidades.M_Cat.AsignacionColaboraciones
{
    public class AsignacionColaboracion
    {
        public Guid IdAsignacionColaboraciones { get; set; }
        public Guid ModuloServicioId { get; set; }
        public ModuloServicio ModuloServicio { get; set; }
        public Guid SColaboracionMPId { get; set; }
        public SColaboracionMP SColaboracionMP { get; set; }
     
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
    }
}

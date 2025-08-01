using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;

namespace SIIGPP.Entidades.M_PI.SolicitudesInteligencia
{
    public class SolicitudInteligenciaAsig
    {
        public Guid IdSolicitudInteligenciaAsig { get; set; }
        public Guid SolicitudInteligenciaId { get; set; }
        public SolicitudInteligencia SolicitudInteligencia { get; set; }
        public ModuloServicio ModuloServicio { get; set; }
        public Guid ModuloServicioId { get; set; }

    }
}

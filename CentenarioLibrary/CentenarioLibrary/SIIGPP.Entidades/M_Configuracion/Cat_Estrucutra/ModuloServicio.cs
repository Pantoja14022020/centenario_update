using SIIGPP.Entidades.M_Cat.Turnador;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using System;
using System.Collections.Generic;
using System.Text; 

namespace SIIGPP.Entidades.M_Configuracion.Cat_Estructura
{
    public class ModuloServicio
    {
        public Guid IdModuloServicio { get; set; }
        public Agencia Agencia { get; set; }
        public Guid AgenciaId { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public Boolean ServicioInterno { get; set; }
        public bool Condicion {  get; set; }

        public ICollection<Usuario> usuarios { get; set; }
        public List<AmpoTurno> AmpoTurnos { get; set; }
    }
}

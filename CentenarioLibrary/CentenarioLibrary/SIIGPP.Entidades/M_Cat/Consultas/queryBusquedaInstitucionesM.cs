using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_Cat.Consultas
{
    public class queryBusquedaInstitucionesM
    {
        [Key]
        public Guid IdInstitucion { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Contacto { get; set; }

    }
}

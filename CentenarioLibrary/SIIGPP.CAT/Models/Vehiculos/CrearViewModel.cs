using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Vehiculos
{
    public class CrearViewModel
    {
        public Guid RHechoId { get; set; }
        public Boolean Estado { get; set; }
        public Boolean EstadoRobado { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Placas { get; set; }
        public string Color { get; set; }
        public string Ano { get; set; }
        public Boolean Recuperado { get; set; }
        public Boolean Devuelto { get; set; }
        public string SenasParticulares { get; set; }

        public DateTime FechaSys { get; set; }

        public string FechaRegistro { get; set; }

        public string Distrito { get; set; }

        public string Subproc { get; set; }

        public string Agencia { get; set; }

        public string Usuario { get; set; }

        public string Puesto { get; set; }
        public string Modulo { get; set; }

        public string NoSerieMotor { get; set; }
        public string Recepcion { get; set; }
        public string Lugar { get; set; }
        public string Municipio { get; set; }
        public string Modalidad { get; set; }
        public string FechaRobo { get; set; }
        public string Propietario { get; set; }
        public string NombreDenunciante { get; set; }
        public string DomicilioDenunciante { get; set; }
        public string NumeroOficio { get; set; }
        public string Lugardeposito { get; set; }
        public string Estadov { get; set; }
        public string Ubicacion { get; set; }
        public Boolean StatusVehiculo { get; set; }
        public Boolean NsChasis { get; set; }
        public Boolean NsMotor { get; set; }
        public string TipoServicio { get; set; }
    }
}

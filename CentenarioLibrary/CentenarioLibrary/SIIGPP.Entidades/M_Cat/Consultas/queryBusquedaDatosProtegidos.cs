using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.Entidades.M_Cat.Consultas
{
    public class queryBusquedaDatosProtegidos
    {
        [Key]
        public Guid IdDatosProtegidos { get; set; }
        public Guid RAPId { get; set; }
        public string Alias { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string Rutadocumento { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string direc { get; set; }

    }
}

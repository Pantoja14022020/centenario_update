using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra
{
    public class DSP
    {
        [Key]
        public Guid IdDSP { get; set; }
        public Guid DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        public string NombreSubDir { get; set; }
        public string Clave { get; set; }
        public string Responsable { get; set; }
        public string Telefono { get; set; }
        public Boolean StatusInicioCarpeta { get; set; }
        public string Tipo { get; set; }
        public string NombreSub { get; set; }
        public string cargoResponsable { get; set; }
        public bool StatusDSP {  get; set; }
    }
}

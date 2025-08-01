using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Cat.Orientacion;

namespace SIIGPP.Entidades.M_Cat.MedidasProteccion
{
    public class Medidasproteccion
    {
        public Guid IdMProteccion { get; set; }
        public Guid RHechoId { get; set; }
        public RHecho RHecho { get; set; }
        public string Victima { get; set; }
        public string Imputado { get; set; }
        public string Lugar { get; set; }
        public string Fechahora { get; set; }
        public string Agente { get; set; }
        public string Nuc { get; set; }
        public string Delito { get; set; }
        public string Narrativa { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string MedidaProtecion { get; set; }
        public int Duracion { get; set; }
        public string Institucionejec { get; set; }
        public string Agencia { get; set; }
        public string Nomedidas { get; set; }
        //////////////////////////////////
        public string Destinatarion { get; set; }
        public string Domicilion { get; set; }
        public string FInicio { get; set; }
        public string FVencimiento { get; set; }
        public Boolean Ampliacion {get; set;}
        public string FAmpliacion { get; set; }
        public string FterminoAm { get; set; }
        public string Ratificacion { get; set; }
        public string Distrito { get; set; }
        public string Subproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
        public string Modulo { get; set; }
        public DateTime Fechasys { get; set; }
        public string Textofinal { get; set; }
        public string Textofinal2 { get; set; }
        public Boolean Detalleactivo { get; set; }
        public string Textofinaldetalle { get; set; }
        public string NumeroOficio { get; set; }
        public string NumeroOficioN { get; set; }
        public Boolean? PetiOfiMPBool { get; set; }
        public string PetiOfiMPVar { get; set; }
        public Boolean? MedidasExtraTF { get; set; }
        public string MedidasExtra { get; set; }

    }
}

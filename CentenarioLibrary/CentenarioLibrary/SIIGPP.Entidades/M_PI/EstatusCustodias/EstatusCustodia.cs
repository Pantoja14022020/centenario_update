using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_PI.Detenciones;


namespace SIIGPP.Entidades.M_PI.EstatusCustodias
{
    public class EstatusCustodia
    {
        public Guid IdEstatusCustodia { get; set; }
        public Guid DetencionId { get; set; }
        public Detencion Detencion { get; set; }
        public string Calle { get; set; }
        public int NoExterior { get; set; }
        public int NoInterior { get; set; }
        public string Ecalle1 { get; set; }
        public string Ecalle2 { get; set; }
        public string Referencia { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public int Cp { get; set; }
        public string Longitud { get; set; }
        public string Latitud { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public string ElementoAsignado { get; set; }
        public string Horainicio { get; set; }
        public string HoraTermino { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Motivo { get; set; }
        public string Observaciones { get; set; }

        public string Origen { get; set; }
        public string Destino { get; set; }
        public string  HoraSalida{ get; set; }
        public string HoraLLegada { get; set; }
        public string Ruta { get; set; }
        public bool Tipo { get; set; }
    }
}

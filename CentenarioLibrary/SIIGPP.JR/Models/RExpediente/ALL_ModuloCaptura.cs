using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RExpediente
{
    public class ALL_ModuloCaptura
    {

        public string NUC { get; set; }
        public Guid Idmoduloservicio { get; set; }

        //Expediente
        public Guid RhechoId { get; set; }
        public string NumeroExpediente { get; set; }
        public string Prefijo { get; set; }
        public int Consecutivo { get; set; }
        public int Ano { get; set; }


        //Envio
        public string EnDistrito { get; set; }
        public string EnDisubproc { get; set; }
        public string EnAgencia { get; set; }
        public string EnModulo { get; set; }
        public string EnNombre { get; set; }
        public string EnPuesto { get; set; }

        //Solicitantes - Requerido
        public List<PersonasModuloCaptura> Solicitantes { get; set; }

        public List<PersonasModuloCaptura> Requeridos { get; set; }

        //Delitos
        public List<DelitoModuloCaptura> Delitos { get; set; }

        //Status general

        public string Status { get; set; }

        //Status Acuerdo inmediato
        public DateTime? FechaCelebracionInme { get; set; }

        //Status Acuerdo Diferido
        public DateTime? FechaCelebracionDife { get; set; }
        public DateTime? Fechalimitecumplimiento { get; set; }
        public string StatusCumplimiento { get; set; }
        public DateTime FechaConstanciaconclusion { get; set; }
        public int SISE { get; set; }

        //Status Baja
        public string StatusBaja {get; set;}

        public decimal MontoTotal { get; set; }
    }
}

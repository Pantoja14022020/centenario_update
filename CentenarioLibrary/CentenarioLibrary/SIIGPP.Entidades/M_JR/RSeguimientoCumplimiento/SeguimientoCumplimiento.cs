using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento
{
   public  class SeguimientoCumplimiento
    {
        public Guid IdSeguimientoCumplimiento { get; set; }
        public Guid AcuerdoReparatorioId { get; set; }
        public AcuerdoReparatorio AcuerdoReparatorio  { get; set; }
        public int NoParcialidad { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaProrroga { get; set; }
        public decimal CantidadAPagar { get; set; }
        public string TipoPago { get; set; }
        public string ObjectoEspecie { get; set; }
        public String StatusPago { get; set; }

        public string Titulo { get; set; }
        public string Dirigidoa { get; set; }
        public string Direccion { get; set; }
        public string Solicitantes { get; set; }
        public string Requeridos { get; set; }



        public DateTime? FechaHoraCita { get; set; }
        public string Texto { get; set; }

        public string uf_Distrito { get; set; }
        public string uf_DirSubProc { get; set; }
        public string uf_Agencia { get; set; }
        public string uf_Modulo { get; set; }
        public string uf_Nombre { get; set; }
        public string uf_Puesto { get; set; }




    }
}

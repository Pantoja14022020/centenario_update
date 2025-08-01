using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
namespace SIIGPP.IL.Models.Agenda
{
    public class ListarAudienciasViewModel
    {

        public Guid DistritoId { get; set; }
        public String NUC { get; set; }
        public string oficio { get; set; }
        public List<delitosModel> delitos { get; set; }
        public int Estatus { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public Guid solicitud { get; set; }
        public List<partesModel> partes {get;set;}
        public string solicitante { get; set; }

    }
}

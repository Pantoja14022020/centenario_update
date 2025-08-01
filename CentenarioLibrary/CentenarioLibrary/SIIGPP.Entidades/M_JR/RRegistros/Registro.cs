using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using System.ComponentModel.DataAnnotations;
using System;

namespace SIIGPP.Entidades.M_JR.RRegistros
{
    public class Registro
    {
        [Key]
        public Guid IdRegistro { get; set; }
        public Guid EnvioId { get; set; }
        public Envio Envio { get; set; }
        public string Tipo { get; set; }
        public string QuienPorRegistro { get; set; }
        public string Descripcion { get; set; }
        public string Distrito { get; set; }
        public string Dirsubproc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
        public DateTime Fechasis { get; set; }
        public string Numerooficio { get; set; }

    }


}

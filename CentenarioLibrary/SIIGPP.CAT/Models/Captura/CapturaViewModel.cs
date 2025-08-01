using System;

namespace SIIGPP.CAT.Models.Captura
{
    public class CapturaViewModel
    {
        public Guid IdCaptura { get; set; }
        public Guid RHechoId { get; set; }
        public Guid RegistroTableroId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid UModuloServicioId { get; set; }
        public Guid RemitioModuloServicioId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Nuc { get; set; }
        public string UsuarioNombre { get; set; }
        public string LugarInicio { get; set; }
        public string LugarRemitio { get; set; }
        public string U_Nombre { get; set; }
        public string CreoDistrito { get; set; }
        public string CreoDSP { get; set; }
        public string CreoAgencia { get; set; }
        public string CreoModulo { get; set; }
        public string Victima { get; set; }
        public DateTime? FechaElevaNuc { get; set; }
    }
}

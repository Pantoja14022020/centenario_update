using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RDHechos
{
    public class TrataPSujetoPasivoViewModel
    {
        public string Mes { get; set; }
        public int Mesi { get; set; }
        public string nuc { get; set; }
        public string Agencia { get; set; }
        public string MunicipioAgencia { get; set; }
        public string EntidadDenuncia { get; set; } = "Hidalgo";
        public DateTime FechaInicioCarpeta { get; set; }
        public string TransferidaDnuc { get; set; }
        public string AgenciaTransfiere { get; set; }
        public string Nucanterior { get; set; }
        public string Reclasificada { get; set; }
        public string DelitosReclasificada { get; set; }
        public Guid Rhechoid { get; set; }
        public Guid Ratencion { get; set; }
        public Guid PersonaId { get; set; }
        public string NombreVictima { get; set; }
        public string ApellidoMaVictima { get; set; }
        public string ApellidoPaVictima { get; set; }
        public string Alias { get; set; }
        public string FechaNacimiento { get; set; }
        public int EdadImputado { get; set; }
        public string SexoVictima { get; set; }
        public string GeneroVictima { get; set; }
        public string EstadoCivilVictima { get; set; }
        public string NacionalidadVictima { get; set; }
        public string HablaEspanol { get; set; }
        public string HablaIndigena { get; set; }
        public string LeguaIndigena { get; set; }
        public string Discapacidad { get; set; }
        public string TipoDiscapadicad { get; set; }
        public string RelacionVictimario { get; set; }
        public string EscolaridadVictima { get; set; }
        public string OcupacionVictima { get; set; }
        public string PaisVictima { get; set; }
        public string EntidadVictima { get; set; }
        public string MunicipioVictima { get; set; }
        public string EntidadFederativaH { get; set; }
        public string MunicipioH { get; set; }
        public string LugarEspecificoH { get; set; }
        public string Rbreve { get; set; }
    }
}

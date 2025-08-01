using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_IL.Agendas
{
    public class Agenda
    {
        public Guid IdAgenda { get; set; }
        public Guid RHechoId { get; set; }
        public RHecho RHecho { get; set; }
        public string NumeroOficio{ get; set; }
        public string CausaPenal { get; set; }
        public string Nuc{ get; set; }
        public string Victimas{ get; set; }
        public string Imputado{ get; set; }
        public string Delitos{ get; set; }
        public string Texto { get; set; }
        public DateTime FechaCitacion{ get; set; }
        public string Status { get; set; }
        public int Tipo{ get; set; }
        public string DirigidoNombre { get; set; }
        public string DirigidoPuesto { get; set; }
        public string ReDireccion { get; set; }
        public string ReTelefono { get; set; }
        public string ReCorreo { get; set; }
        public string ArticulosSancion { get; set; }
        public string DireccionImputado { get; set; }
        public string TelefonoImputado { get; set; }
        public string CorreoImputado { get; set; }
        public string DefensorParticularImp { get; set; }
        public string DomicilioDPI { get; set; }
        public string TelefonoDPI { get; set; }
        public string CorreoDPI { get; set; }
        public string InformacionVicAseJu { get; set; }
        public string InformacionImpDeP { get; set; }
        public string InformacionImp { get; set; }
        public string InformacionDelito { get; set; }
        public string HoraCitacion { get; set; }
        public string LugarCitacion { get; set; }
        public string DescripcionCitacion { get; set; }
        public string DireccionHecho { get; set; }
        public string HechosIII { get; set; }
        public string ClasificacionjIII { get; set; }
        public string CorrelacionArtIII { get; set; }
        public string ArticuloIII { get; set; }
        public string ModaidadesVI { get; set; }
        public string AutoriaV { get; set; }
        public string Autoria2V { get; set; }
        public string PreceptosVI { get; set; }
        public string TestimonialVII { get; set; }
        public string PericialVII { get; set; }
        public string DocumentalesVII { get; set; }
        public string MaterialVII { get; set; }
        public string AnticipadaVII { get; set; }
        public string ArticulosVIII { get; set; }
        public string MontoVIII { get; set; }
        public string NumeroLetraVIII { get; set; }
        public string TestimonialVIII { get; set; }
        public string PericialVIII { get; set; }
        public string DocumentalesVIII { get; set; }
        public string MaterialVIII { get; set; }
        public string ArticulosIX { get; set; }
        public string PenaIX { get; set; }
        public string TestimonialesX { get; set; }
        public string TestimonialX { get; set; }
        public string PericialX { get; set; }
        public string DocumentalesX { get; set; }
        public string MaterialX { get; set; }
        public string DecomisoXI { get; set; }
        public string PropuestaXII { get; set; }
        public string TerminacionXIII { get; set; }
        public string Resumen { get; set; }
        public Boolean Viculada { get; set; }
        public string PlazoInvestigacion { get; set; }
        public Boolean Prorroga { get; set; }
        public string TiempoProrroga { get; set; }
        public string PersonaPresentar { get; set; }
        public string Tipo2 { get; set; }
        public string UDistrito { get; set; }
        public string USubproc { get; set; }
        public string UAgencia { get; set; }
        public string Usuario { get; set; }
        public string UPuesto { get; set; }
        public string UModulo { get; set; }
        public DateTime Fechasys { get; set; }
        public Boolean Aux { get; set; }
        public string SexoPersonaCitada { get; set; }
        public Boolean Status2 { get; set; }
        public int CUPRE { get; set; }
        public string Comparece { get; set; }
        public DateTime? Chequeo { get; set; }
    }

}

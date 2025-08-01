using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SIIGPP.Entidades.M_Cat.MedAfiliacion;
using SIIGPP.Entidades.M_Configuracion.Cat_MedFiliacionLarga;
using System.ComponentModel.DataAnnotations.Schema;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;

namespace SIIGPP.Entidades.M_Cat.MedFiliacionDesaparecido
{
    public class MediaFiliacionDesaparecido
    {
        [Key]
        public Guid IdMediaFiliacionDesaparecido { get; set; }
        public Guid MediaFiliacionId { get; set; }
        public MediaAfiliacion MediaFiliacion { get; set; }
        // MANCHAS
        //public string ClasificacionPersona { get; set; }
        public string Manchas { get; set; }
        public string TipoManchas { get; set; }
        public string ManchasOtrasCyU { get; set; }
        //LUNARES
        public string Lunares { get; set; }
        public string LunaresCyU { get; set; }
        //CICATRICES
        public string Cicatrices { get; set; }
        public string TipoCicatrices { get; set; }
        public string CicatricesTraumaticasCyU { get; set; }
        public string CicatricesQuirurgicasTipo { get; set; }
        public int CicatricesQuirurgicasCesareaNumero { get; set; }
        public string CicatricesQuirurgicasCesareaOrientacion { get; set; }
        public string CicatricesQuirurgicasOperacionMyU { get; set; }
        //TATUAJES
        public string Tatuajes { get; set; }
        public int TatuajesNumero { get; set; }
        public string TatuajesDescripcion { get; set; }
        //PIERCING
        public string Piercing { get; set; }
        public int PiercingNumero { get; set; }
        public string PiercingDescripcion { get; set; }
        //UÑAS
        public string UñasEstado { get; set; }
        public string UñasNoSaludables { get; set; }
        public string UñasPostizas { get; set; }
        //DEFORMIDADES
        public string Deformidades { get; set; }
        public string TipoDeformidades { get; set; }
        public string CongenitasUbicacion { get; set; }
        public string AdquiridasUbicacion { get; set; }
        //CAVIDAD ORAL
        public string ProtesisDental { get; set; }
        public string ProtesisDentalUbicacion { get; set; }
        public string DentaduraCaracteristicas { get; set; }
        public string DentaduraDetalles { get; set; }
        //INFORMACION MEDICA
        public string Traumatismos { get; set; }
        public string TipoTraumatismos { get; set; }
        public string UbicacionFracturas { get; set; }
        public string TipoLesiones { get; set; }
        public string CausaMordedura { get; set; }
        public string TipoLesionesOtra { get; set; }
        public string UbicacionLesiones { get; set; }
        public string FacultadesMentales { get; set; }
        public string TipoRetraso { get; set; }
        public string EnfermedadesCronicas { get; set; }
        public string EnfermedadTipo { get; set; }
        public string EnfermedadTiempoDiagnostico { get; set; }
        public string TratamientoEnfermedadCronica { get; set; }
        public string TratamientoMedicamento { get; set; }
        public string TratamientoPeriocidad { get; set; }
        public string Alergias { get; set; }
        public string TratamientoAlergia { get; set; }
        public string MdicamentoTratamientoAlergia { get; set; }
        public string PeriocidadTratamientoAlergia { get; set; }
        public string Lentes { get; set; }
        public string TipoLentes { get; set; }
        public string LentesGraduacion { get; set; }
        public string AparatosAuditivos { get; set; }
        public string oidos { get; set; }
    }
}

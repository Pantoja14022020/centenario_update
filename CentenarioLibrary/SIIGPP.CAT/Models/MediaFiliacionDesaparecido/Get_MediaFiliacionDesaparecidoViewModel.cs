
using System;
using System.ComponentModel.DataAnnotations;

namespace SIIGPP.CAT.Models.MediaFiliacionDesaparecido
{
    public class Get_MediaFiliacionDesaparecidoViewModel
    {
        [Key]
        public Guid IdMediaFiliacionDesaparecido { get; set; }
        public Guid MediaFiliacionId { get; set; }
        // MANCHAS
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

        //MEDIA FILIACION

        //public Guid idMediaAfiliacion { get; set; }
        public Guid PersonaId { get; set; }
        public string NombreImputado { get; set; }

        public Guid RHechoId { get; set; }
        public string Complexion { get; set; }
        public decimal Peso { get; set; }
        public decimal Estatura { get; set; }
        public string FormaCara { get; set; }
        public string ColoOjos { get; set; }
        public string Tez { get; set; }
        public string FormaCabello { get; set; }
        public string ColorCabello { get; set; }
        public string LargoCabello { get; set; }
        public string TamañoNariz { get; set; }
        public string TipoNariz { get; set; }
        public string GrosorLabios { get; set; }
        public string TipoFrente { get; set; }
        public string Cejas { get; set; }
        public string TipoCejas { get; set; }
        public string TamañoBoca { get; set; }
        public string TamañoOrejas { get; set; }
        public string FormaMenton { get; set; }
        public string TipoOjo { get; set; }
        public string TipoOjo2 { get; set; }

        public string Numerooficio { get; set; }
        public DateTime FechaSys { get; set; }
        public string Distrito { get; set; }
        public string DirSubProc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }


        public string Gruposanguineo { get; set; }
        public string Calvicie { get; set; }
        public string AdherenciaOreja { get; set; }
        public string TratamientosQuimicosCabello { get; set; }
        public string FormaOjo { get; set; }
        public string ImplantacionCeja { get; set; }
        public string PuntaNariz { get; set; }
        public string TipoMenton { get; set; }
        public Boolean Cicatriz { get; set; }
        public string DescripcionCicatriz { get; set; }
        public Boolean Tatuaje { get; set; }
        public string DescripcionTatuaje { get; set; }
        public string OtrasCaracteristicas { get; set; }
        public string TamanoDental { get; set; }
        public string TratamientoDental { get; set; }
        public Boolean DentaduraCompleta { get; set; }
        public string DientesAusentes { get; set; }
        public string TipoDentadura { get; set; }

        public string Pomulos { get; set; }
        public string Lateralidad { get; set; }
        public Boolean Pupilentes { get; set; }
        public string Pupilentes2 { get; set; }

        // persona

        public Boolean? Registro { get; set; }
        public string ClasificacionPersona { get; set; }
    }
}

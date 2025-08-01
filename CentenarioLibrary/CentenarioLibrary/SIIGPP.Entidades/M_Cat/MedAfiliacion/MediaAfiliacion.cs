using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Entidades.M_Cat.MedAfiliacion
{
    public class MediaAfiliacion
    {
        public Guid idMediaAfiliacion { get; set; }
        public Guid PersonaId { get; set; }
        public Persona Persona { get; set; }
        public Guid RHechoId { get; set; }
        public RHecho RHecho { get; set; }
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
        public string Tipo2Ojos { get; set; }

        public DateTime FechaSys { get; set; }
        public string Distrito { get; set; }
        public string DirSubProc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
        public string Numerooficio { get; set; }

        //-----------------------------------------------

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

    }
}

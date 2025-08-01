using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
////////////////////////////////////////////////////////////////
//                       DATOS
////////////////////////////////////////////////////////////////
//////gbo
//////GO
using SIIGPP.Datos.M_Cat.GRAC;
using SIIGPP.Datos.M_Cat.GNUC;
using SIIGPP.Datos.M_Cat.Registro;
using SIIGPP.Datos.M_Cat.Direcciones;
using SIIGPP.Datos.M_Cat.Turnador;
using SIIGPP.Datos.M_Cat.Orientacion;
using SIIGPP.Datos.M_Cat.Diligencias;
using SIIGPP.Datos.M_Cat.DDerivacion;
using SIIGPP.Datos.M_Cat.Ampliacion;
using SIIGPP.Datos.M_Cat.RDHecho;
using SIIGPP.Datos.M_Cat.Bitacora;
using SIIGPP.Datos.M_Cat.Citatorio_Notificacion;
using SIIGPP.Datos.M_Cat.VehiculoImplicito;
using SIIGPP.Datos.M_Cat.Arma;
using SIIGPP.Datos.M_Cat.MedidasProteccion;
using SIIGPP.Datos.M_Cat.MedAfiliacion;
using SIIGPP.Datos.M_Cat.MedCautelares;
using SIIGPP.Datos.M_Cat.ImpProceso;
using SIIGPP.Datos.M_Cat.RActosInvestigacion;
using SIIGPP.Datos.M_Cat.Terminacion;
using SIIGPP.Datos.M_Cat.Representantes;
using SIIGPP.Datos.M_Cat.CArchivo;
using SIIGPP.Datos.M_Cat.Datos_Relacionados;
using SIIGPP.Datos.M_Cat.C5i;
using SIIGPP.Datos.M_Cat.NoEjerciciosApenal;
using SIIGPP.Datos.M_Cat.DatosProtegidos;
using SIIGPP.Datos.M_Cat.Oficios;
using SIIGPP.Datos.M_Cat.Historialcarpetas;
using SIIGPP.Datos.M_Cat.RemisionesUI;
using SIIGPP.Datos.M_Cat.SColaboracionesMP;
using SIIGPP.Datos.M_Cat.AsignacionColaboraciones;
using SIIGPP.Datos.M_Cat.ArchivosVehiculos;
using SIIGPP.Datos.M_Cat.ProcedimientoAbreviados;
using SIIGPP.Datos.M_Cat.AcumulacionCarpetas;
using SIIGPP.Datos.M_Cat.ContencionesPersonas;
using SIIGPP.Datos.M_Cat.Resoluciones;
using SIIGPP.Datos.M_Cat.PRegistro;
using SIIGPP.Datos.M_Cat.PAtencion;
using SIIGPP.Datos.M_Cat.PCitas;
using SIIGPP.Datos.M_Cat.PersonaDesap;

using SIIGPP.Datos.M_Configuracion.Cat_ConfGlobal;
using SIIGPP.Datos.M_Configuracion.Cat_Configuracion;
using SIIGPP.Datos.M_Configuracion.Cat_Estructura;
using SIIGPP.Datos.M_Configuracion.Cat_Generales;
using SIIGPP.Datos.M_Configuracion.Cat_INEGI;
using SIIGPP.Datos.M_Configuracion.Cat_ServiciosPericiales;
using SIIGPP.Datos.M_Configuracion.Cat_Delito;
using SIIGPP.Datos.M_Configuracion.Cat_StatusNUC;
using SIIGPP.Datos.M_Configuracion.Cat_Indicios;
using SIIGPP.Datos.M_Configuracion.Cat_Vehiculos;
using SIIGPP.Datos.M_Configuracion.Cat_Armas;
using SIIGPP.Datos.M_Configuracion.Cat_ActosInvestigacion;
using SIIGPP.Datos.M_Configuracion.Cat_Incompetencias;
using SIIGPP.Datos.M_Configuracion.Cat_TRepresentantes;
using SIIGPP.Datos.M_Configuracion.Cat_FiscaliaOestados;
using SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales;
using SIIGPP.Datos.M_Configuracion.Cat_MedFiliacionLarga;
using SIIGPP.Datos.M_Configuracion.Cat_MultasCitatorios;
using SIIGPP.Datos.M_Configuracion.Cat_JuzgadosAgencia;
using SIIGPP.Datos.M_Configuracion.Cat_Hipotesis;
using SIIGPP.Datos.M_Configuracion.Cat_Medidas;
using SIIGPP.Datos.M_Configuracion.Cat_SpPi_Ligaciones;

using SIIGPP.Datos.M_PC.M_PanelControl;
using SIIGPP.Datos.M_ControAcceso.Roles;
using SIIGPP.Datos.M_ControlAcceso.Usuarios;
using SIIGPP.Datos.M_ControlAcceso.PanelUsuarios;
using SIIGPP.Datos.M_ControlAcceso.UAlmacenamiento;
using SIIGPP.Datos.M_ControlAcceso.Menus;

using SIIGPP.Datos.M_SP.PeritosAsignado;
using SIIGPP.Datos.M_SP.DocsDiligencias;
using SIIGPP.Datos.M_SP.DiligenciaIndicios;
using SIIGPP.Datos.M_SP.DocsDiligenciasForaneas;


using SIIGPP.Datos.M_JR.RExpediente;
using SIIGPP.Datos.M_JR.REnvio;
using SIIGPP.Datos.M_JR.RSolicitanteRequerido;
using SIIGPP.Datos.M_JR.RDelitodelitosDerivados;
using SIIGPP.Datos.M_JR.RCitatorioRecordatorio;
using SIIGPP.Datos.M_JR.RFacilitadorNotificador;
using SIIGPP.Datos.M_JR.RSesion;
using SIIGPP.Datos.M_JR.RAcuerdoReparatorio;
using SIIGPP.Datos.M_JR.RSeguimientoCumplimiento;
using SIIGPP.Datos.M_JR.RAsignacionEnvios;
using SIIGPP.Datos.M_JR.RRegistro;
using SIIGPP.Datos.M_JR.RNotificacion;

using SIIGPP.Datos.M_PI.CFotos;
using SIIGPP.Datos.M_PI.Detenciones;
using SIIGPP.Datos.M_PI.PersonasVisita;
using SIIGPP.Datos.M_PI.Direcciones;
using SIIGPP.Datos.M_PI.FPersonas;
using SIIGPP.Datos.M_PI.Visitas;
using SIIGPP.Datos.M_PI.CMedicosPSR;
using SIIGPP.Datos.M_PI.PeritosAsignadosPI;
using SIIGPP.Datos.M_PI.CMedicosPR;
using SIIGPP.Datos.M_PI.Informes;
using SIIGPP.Datos.M_PI.SolicitudesInteligencia;
using SIIGPP.Datos.M_PI.InformacionJuridico;
using SIIGPP.Datos.M_PI.EstatusCustodias;
using SIIGPP.Datos.M_PI.SubirArchivos;
using SIIGPP.Datos.M_PI.EgresosTemporales;
using SIIGPP.Datos.M_PI.OAprhensionBitacoras;
using SIIGPP.Datos.M_PI.ArchivosColaboraciones;

using SIIGPP.Datos.M_IL.Agendas;
using SIIGPP.Datos.M_IL.Citatorios;
using SIIGPP.Datos.M_IL.SolicitudesInformacion;
using SIIGPP.Datos.M_IL.PJudicial;

using SIIGPP.Datos.M_FEDC.NoAccionPenal;

using SIIGPP.Datos.M_SP.PeritosAsignados;
using SIIGPP.Datos.M_JR.RResponsable;
using SIIGPP.Datos.M_Administracion;





////////////////////////////////////////////////////////////////
//                       ENTIDADES
////////////////////////////////////////////////////////////////

using SIIGPP.Entidades.M_Cat.GRAC;
using SIIGPP.Entidades.M_Cat.GNUC;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Cat.Direcciones;
using SIIGPP.Entidades.M_Cat.Turnador;
using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.Diligencias;
using SIIGPP.Entidades.M_Cat.DDerivacion;
using SIIGPP.Entidades.M_Cat.Ampliacion;
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.Entidades.M_Cat.Bitacora;
using SIIGPP.Entidades.M_Cat.Citatorio_Notificacion;
using SIIGPP.Entidades.M_Cat.VehiculoImplicito;
using SIIGPP.Entidades.M_Cat.Arma;
using SIIGPP.Entidades.M_Cat.MedidasProteccion;
using SIIGPP.Entidades.M_Cat.Indicios;
using SIIGPP.Entidades.M_Cat.MedAfiliacion;
using SIIGPP.Entidades.M_Cat.MedCautelares;
using SIIGPP.Entidades.M_Cat.ImpProceso;
using SIIGPP.Entidades.M_Cat.RActosInvestigacion;
using SIIGPP.Entidades.M_Cat.Terminacion;
using SIIGPP.Entidades.M_Cat.Representantes;
using SIIGPP.Entidades.M_Cat.CArchivos;
using SIIGPP.Entidades.M_Cat.Datos_Relacionados;
using SIIGPP.Entidades.M_Cat.C5i;
using SIIGPP.Entidades.M_Cat.Historialcarpetas;
using SIIGPP.Entidades.M_Cat.RemisionesUI;
using SIIGPP.Entidades.M_Cat.SColaboracionesMP;
using SIIGPP.Entidades.M_Cat.AsignacionColaboraciones;
using SIIGPP.Entidades.M_Cat.ArchivosVehiculos;
using SIIGPP.Entidades.M_Cat.DatosProtegidos;
using SIIGPP.Entidades.M_Cat.Oficios;
using SIIGPP.Entidades.M_Cat.NoEjerciciosApenal;
using SIIGPP.Entidades.M_Cat.ProcedimientoAbreviados;
using SIIGPP.Entidades.M_Cat.AcumulacionCarpetas;
using SIIGPP.Entidades.M_Cat.ContencionesPersonas;
using SIIGPP.Entidades.M_Cat.Resoluciones;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.PAtencion;
using SIIGPP.Entidades.M_Cat.PCitas;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
using SIIGPP.Entidades.M_Cat.Consultas;
using SIIGPP.Entidades.M_Cat.InstrumentoComision;

using SIIGPP.Entidades.M_Configuracion.Cat_ConfiGlobal;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Configuracion.Cat_Generales;
using SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales;
using SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra;
using SIIGPP.Entidades.M_Configuracion.Cat_Delito;
using SIIGPP.Entidades.M_Configuracion.Cat_StatusNUC;
using SIIGPP.Entidades.M_Configuracion.Cat_Indicios;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;
using SIIGPP.Entidades.M_Configuracion.Cat_Armas;
using SIIGPP.Entidades.M_Configuracion.Cat_ActosInvestigacion;
using SIIGPP.Entidades.M_Configuracion.Cat_Incompetencias;
using SIIGPP.Entidades.M_Configuracion.Cat_TRepresentantes;
using SIIGPP.Entidades.M_Configuracion.Cat_FiscaliaOestados;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;
using SIIGPP.Entidades.M_Configuracion.Cat_MedFiliacionLarga;
using SIIGPP.Entidades.M_Configuracion.Cat_MultasCitatorios;
using SIIGPP.Entidades.M_Configuracion.Cat_JuzgadoAgencias;
using SIIGPP.Entidades.M_Configuracion.Cat_Hipotesis;
using SIIGPP.Entidades.M_Configuracion.Cat_Medidas;
using SIIGPP.Entidades.M_Configuracion.Cat_SpPi_Ligaciones;

using SIIGPP.Entidades.M_Panel.M_PanelControl;

using SIIGPP.Entidades.M_ControlAcceso.Roles;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using SIIGPP.Entidades.M_ControlAcceso.PanelUsuarios;
using SIIGPP.Entidades.M_ControlAcceso.UAlmacenamiento;
using SIIGPP.Entidades.M_ControlAcceso.Menu;

using SIIGPP.Entidades.M_SP.PeritosAsignados;
using SIIGPP.Entidades.M_SP.DocsDiligencias;
using SIIGPP.Entidades.M_SP.DocsDiligenciasForaneas;
using SIIGPP.Entidades.M_SP.DiligenciaIndicios;

using SIIGPP.Entidades.M_JR.RExpediente;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RSolicitanteRequerido;
using SIIGPP.Entidades.M_JR.RDelitodelitosDerivados;
using SIIGPP.Entidades.M_JR.RCitatorioRecordatorio;
using SIIGPP.Entidades.M_JR.RFacilitadorNotificador;
using SIIGPP.Entidades.M_JR.RSesion;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using SIIGPP.Entidades.M_JR.RSeguimientoCumplimiento;
using SIIGPP.Entidades.M_JR.RAsignacionEnvios;
using SIIGPP.Entidades.M_JR.RRegistro;
using SIIGPP.Entidades.M_JR.RNotificacion;
using SIIGPP.Entidades.M_JR.consultas;


using SIIGPP.Entidades.M_PI.CFotos;
using SIIGPP.Entidades.M_PI.Detenciones;
using SIIGPP.Entidades.M_PI.PersonasVisita;
using SIIGPP.Entidades.M_PI.Direcciones;
using SIIGPP.Entidades.M_PI.FPersonas;
using SIIGPP.Entidades.M_PI.Visitas;
using SIIGPP.Entidades.M_PI.CMedicosPSR;
using SIIGPP.Entidades.M_PI.PeritosAsignadosPI;
using SIIGPP.Entidades.M_PI.CMedicosPR;
using SIIGPP.Entidades.M_PI.Informes;
using SIIGPP.Entidades.M_PI.SolicitudesInteligencia;
using SIIGPP.Entidades.M_PI.InformacionJuridico;
using SIIGPP.Entidades.M_PI.EstatusCustodias;
using SIIGPP.Entidades.M_PI.SubirArchivos;
using SIIGPP.Entidades.M_PI.EgresosTemporales;
using SIIGPP.Entidades.M_PI.OAprhensionBitacoras;
using SIIGPP.Entidades.M_PI.ArchivosColaboraciones;

using SIIGPP.Entidades.M_IL.Agendas;
using SIIGPP.Entidades.M_IL.Citatorios;
using SIIGPP.Entidades.M_IL.SolicitudesInformacion;
using SIIGPP.Entidades.M_IL.PJudicial;

using SIIGPP.Entidades.M_FEDC.NoAccionPenal;
using SIIGPP.Entidades.M_JR.RResponsable;


using SIIGPP.Entidades.M_Administracion;
using SIIGPP.Entidades.M_Cat.RegistrosTableroI;
using SIIGPP.Datos.M_Cat.RegistrosTableroI;
using SIIGPP.Entidades.M_Cat.Desglose;
using SIIGPP.Datos.M_Cat.Desgloses;
using SIIGPP.Datos.M_PC;
using SIIGPP.Entidades.M_JR.RRegistros;
using SIIGPP.Entidades.M_Cat.TipoViolencia;
using SIIGPP.Datos.M_Cat.TViolencia;
using SIIGPP.Datos.M_Configuracion.DocumentacionSistema;
using SIIGPP.Entidades.M_Configuracion.DocumentacionSistema;
using SIIGPP.Entidades.M_Cat.MedFiliacionDesaparecido;
using SIIGPP.Datos.M_Cat.MedFiliacionDesaparecido;
using SIIGPP.Datos.M_Cat.InsComision;
using SIIGPP.Entidades.M_Cat.Captura;
using SIIGPP.Datos.M_Cat.CapturaC;


////////////////////////////////////////////////////////////////
namespace SIIGPP.Datos
{
    public class DbContextSIIGPP : DbContext
    {

        //MODULO CAT
        public DbSet<Rac> Racs { get; set; }
        public DbSet<Nuc> Nucs { get; set; }
        public DbSet<RAtencion> RAtencions { get; set; }
        public DbSet<RAP> RAPs { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<DireccionPersonal> DireccionPersonals { get; set; }
        public DbSet<DireccionEscucha> DireccionEscuchas { get; set; }
        public DbSet<MediaAfiliacion> MediaAfiliacions { get; set; }
        public DbSet<ArchivosMediaAfiliacion> ArchivosMediaAfiliacions { get; set; }
        public DbSet<MedidasCautelares> MedidasCautelares { get; set; }
        public DbSet<CondImpProceso> CondImpProcesos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<AmpoTurno> AmpoTurnos { get; set; }
        public DbSet<ModuloServicio> ModuloServicios { get; set; }
        public DbSet<RDH> RDHs { get; set; }
        public DbSet<TipoViolencia> TipoViolencias { get; set; }
        public DbSet<InstrumentoComision> InstrumentosComision { get; set; }
        public DbSet<RHecho> RHechoes { get; set; }
        public DbSet<RDiligencias> RDiligencias { get; set; }
        public DbSet<RDNum> RDNum { get; set; }
        public DbSet<RDiligenciasForaneas> RDiligenciasForaneas { get; set; }
        public DbSet<RDDerivacion> RDDerivaciones { get; set; }
        public DbSet<AmpDec> AmpDecs { get; set; }
        public DbSet<DireccionDelito> DireccionDelitos { get; set; }
        public DbSet<RBitacora> Bitacoras { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<DevolucionVehiculo> DevolucionVehiculos { get; set; }
        public DbSet<RArma> rArmas { get; set; }
        public DbSet<Medidasproteccion> Medidasproteccions { get; set; }
        public DbSet<ArchivoVehiculo> ArchivoVehiculos { get; set; }

        public DbSet<Indicios> Indicios { get; set; }
        public DbSet<DetalleSeguimientoIndicio> DetalleSeguimientoIndicios { get; set; }
        public DbSet<RCitatorio_Notificacion> RCitatorio_Notificacions { get; set; }
        public DbSet<DocumentosPesona> DocumentosPesonas { get; set; }
        public DbSet<DeterminacionArchivo> DeterminacionArchivos { get; set; }
        public DbSet<RInconpentencia> RInconpentencias { get; set; }
        public DbSet<Representante> Representantes { get; set; }
        public DbSet<ConjuntoDerivaciones> ConjuntoDerivaciones { get; set; }

        public DbSet<DocsRepresentantes> DocsRepresentantes { get; set; }
        public DbSet<Archivos> Archivos { get; set; }
        public DbSet<DatosRelacionados> DatosRelacionados { get; set; }
        public DbSet<C5> C5s { get; set; }
        public DbSet<DatoProtegido> DatoProtegidos { get; set; }
        public DbSet<Oficio> Oficios { get; set; }
        public DbSet<NoEjercicioApenal> noEjercicioApenals { get; set; }
        public DbSet<HistorialCarpeta> HistorialCarpetas { get; set; }
        public DbSet<RegistroTableroI> RegistrosTableroI { get; set; }
        public DbSet<Desglose> Desgloses { get; set; }
        public DbSet<RemisionUI> RemisionUIs { get; set; }
        public DbSet<RemisionUIPos> RemisionUIPoss { get; set; }
        public DbSet<SColaboracionMP> SColaboracionMPs { get; set; }
        public DbSet<AsignacionColaboracion> AsignacionColaboracions { get; set; }
        public DbSet<ProcedimientoAbreviado> ProcedimientoAbreviados { get; set; }
        public DbSet<AcumulacionCarpeta> AcumulacionCarpetas { get; set; }
        public DbSet<ContencionesPersona> ContencionesPersonas { get; set; }
        public DbSet<NoMedidasCautelares> NoMedidasCautelares { get; set; }
        public DbSet<NoMedidasProteccion> NoMedidasProteccions { get; set; }

        //************** CAT_Captura
        public DbSet<Captura> Capturas { get; set; }

        // MODULOS DE PRE REGISTRO DE INFORMACION
        public DbSet<PreRegistro> PreRegistros { get; set; }
        public DbSet<PreAtencion> PreAtenciones { get; set; }
        public DbSet<PreRap> PreRaps { get; set; }
        public DbSet<PreCitas> PreCitas { get; set; }
        public DbSet<PreHorarioDisponible> PreHorariosDisponibles { get; set; }

        // MODULO DE DESAPARICION PERSONA
        public DbSet<RPersonaDesap> PersonaDesaps { get; set; }
        public DbSet<DireccionOcupacion> DireccionOcupacion { get; set; }
        public DbSet<RedesSociales> RedesSociales { get; set; }
        public DbSet<RedesSocialesPersonal> RedesSocialesPersonal { get; set; }
        public DbSet<VehiculoPersonaDesap> VehiculoPersonaDesap { get; set; }
        public DbSet<MarcaTelefono> MarcaTelefonos { get; set; }
        public DbSet<CompaniaTelefonica> CompaniaTelefonicas { get; set; }

        //
        public DbSet<Resolucion> Resolucions { get; set; }
        //MODULO CONFIGURACION
        //************* CAT_Configuracion Global
        public DbSet<ConfGlobal> ConfGlobals { get; set; }
        //************* CAT_Configuracion
        public DbSet<DocIdentificacion> DocIdentificacions { get; set; }
        public DbSet<MedioNotificacion> MedioNotificacions { get; set; }
        public DbSet<ClasificacionPersona> ClasificacionPersonas { get; set; }
        public DbSet<NivelEstudios> NivelEstudios { get; set; }
        public DbSet<MultaCitatorios> MultaCitatorios { get; set; }
        public DbSet<JuzgadosAgencias> JuzgadosAgencias { get; set; }
        public DbSet<Hipotesis> Hipoteses { get; set; }

        //************* CAT_Estrucutra
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<DSP> DSPs { get; set; }
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<DependeciasDerivacion> DependeciasDerivacions { get; set; }
        public DbSet<ErrorReplicacion> Replicacion { get; set; }
        //************* CAT_Delito
        public DbSet<Delito> Delitos { get; set; }
        public DbSet<ClasificaOrdenResult> ClasificaOrdenResults { get; set; }
        public DbSet<IntensionDelito> IntensionDelitos { get; set; }
        public DbSet<ResultadoDelito> ResultadoDelitos { get; set; }
        public DbSet<TipoDeclaracion> TipoDeclaracions { get; set; }
        public DbSet<TipoFuero> TipoFueros { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<DelitoEspecifico> DelitoEspecificos { get; set; }
        //************* CAT_Generales 
        public DbSet<EstadoCivil> EstadoCivils { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Sexo> Sexos { get; set; }
        public DbSet<RangoEdad> RangoEdads { get; set; }

        //************* CAT_INEGI
        public DbSet<Estado> Estadoes { get; set; }
        public DbSet<Lengua> Lenguas { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Localidad> Localidads { get; set; }
        public DbSet<Ocupacion> Ocupacions { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Nacionalidad> Nacionalidads { get; set; }
        public DbSet<Discapacidad> Discapacidads { get; set; }
        public DbSet<Vialidad> Vialidades { get; set; }
        public DbSet<Asentamiento> Asentamientos { get; set; }
        public DbSet<LugarEspecifico> LugaresEspecificos { get; set; }
        //************* CAT Servicios periciales 
        public DbSet<ASP> ASPs { get; set; }
        public DbSet<ServicioPericial> ServicioPericiales { get; set; }
        //************* CAT Status NUC
        public DbSet<StatusNUC> StatusNUCs { get; set; }
        public DbSet<Etapa> Etapas { get; set; }
        //************* CAT_ Indicios
        public DbSet<TipoIndicio> TipoIndicios { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        //************* CAT_Vehiculos
        public DbSet<Ano> Anos { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Tipov> Tipovs { get; set; }
        //************** CAT_Armas
        public DbSet<ClasificacionArma> ClasificacionArmas { get; set; }
        public DbSet<ArmaObjeto> ArmaObjetos { get; set; }
        public DbSet<MarcaArma> MarcaArmas { get; set; }
        public DbSet<Calibre> Calibres { get; set; }
        

        //************** CAT_RActosInvestigacion
        public DbSet<RActoInvestigacion> RActoInvestigacions { get; set; }
        public DbSet<ActosInDetalle> ActosInDetalles { get; set; }
        //************** Cat_ActosInvestigacion
        public DbSet<ActoInvestigacion> ActoInvestigacions { get; set; }

        //************** Cat_Incompetencias
        public DbSet<Incompetencia> Incompetencias { get; set; }
        //************** Cat_TiposRepresentantes
        public DbSet<TiposRepresentantes> TiposRepresentantes { get; set; }
        //************** Cat_FiscaliOestados
        public DbSet<FiscaliaOestado> FiscaliaOestados { get; set; }
        //************** Cat_RasgosFaciales
        public DbSet<Complexion> Complexions { get; set; }
        public DbSet<Cantidad_de_cabello> Cantidad_De_Cabellos { get; set; }
        public DbSet<Forma_de_cabello> Forma_De_Cabellos { get; set; }
        public DbSet<Largo_de_Cabello> Largo_De_Cabellos { get; set; }
        public DbSet<Color_de_Cabello> Color_De_Cabellos { get; set; }
        public DbSet<Forma_de_Cara> Forma_De_Caras { get; set; }
        public DbSet<Tipo_de_Frente> Tipo_De_Frentes { get; set; }
        public DbSet<Cejas> Cejas { get; set; }
        public DbSet<Tipo_de_Cejas> Tipo_De_Cejas { get; set; }
        public DbSet<Color_Ojos> Color_Ojos { get; set; }
        public DbSet<TipoNariz> TipoNarizs { get; set; }
        public DbSet<Tamaño_de_Boca> Tamaño_De_Bocas { get; set; }
        public DbSet<Forma_de_Menton> Forma_De_Mentons { get; set; }
        public DbSet<Tipo_de_Orejas> Tipo_De_Orejas { get; set; }
        public DbSet<Tez> Tezs { get; set; }
        public DbSet<Grosor_de_labios> Grosor_De_Labios { get; set; }
        public DbSet<Tamaño_Nariz> Tamaño_Narizs { get; set; }
        public DbSet<TipoOjos> TipoOjos { get; set; }
        public DbSet<Tipo2Ojos> Tipo2Ojos { get; set; }
        public DbSet<Adherencia_Oreja> Adherencia_Orejas { get; set; }
        public DbSet<Calvicie> Calvicies { get; set; }
        public DbSet<Forma_de_ojo> Forma_De_Ojos { get; set; }
        public DbSet<Implantacion_Ceja> Implantacion_Cejas { get; set; }
        public DbSet<Punta_Nariz> Punta_Narizs { get; set; }
        public DbSet<Tamano_Dental> Tamano_Dentals { get; set; }
        public DbSet<Tipo_de_Menton> Tipo_De_Mentons { get; set; }
        public DbSet<Tipo_Dentadura> Tipo_Dentaduras { get; set; }
        public DbSet<Tratamiento_Dental> Tratamiento_Dentals { get; set; }
        public DbSet<Tratamientos_Quimicos_Cabello> Tratamientos_Quimicos_Cabellos { get; set; }
        public DbSet<Pomulos> Pomulos { get; set; }

        //****************** Cat_MedFiliacionLarga
        public DbSet<UnasNoSaludables> UnasNoSaludable { get; set; }
        public DbSet<SituacionDental> SituacionDentals { get; set; }
        public DbSet<TipoLesiones> TipoLesiones { get; set; }
        public DbSet<TipoRetraso> TipoRetrasos { get; set; }
        public DbSet<MediaFiliacionDesaparecido> MediaFiliacionDesaparecidos { get; set; }


        //************** Cat_Medidas
        public DbSet<MedidasCautelaresC> MedidasCautelaresCs { get; set; }
        public DbSet<MedidasProteccionC> MedidasProteccionCs { get; set; }

        //************** Cat_LigacionesSPPI
        public DbSet<SPPiligaciones> SPPiligaciones { get; set; }
        //************** Documentacion del Sistema
        public DbSet<Actualizaciones> Actualizaciones { get; set; }

        //CAT CONSULTAS
        public DbSet<queryBusquedaCarpetas> qBusquedaCarpetas { get; set; }
        public DbSet<queryBusquedaRepresentantes> qBusquedaRepresentantes { get; set; }
        public DbSet<queryBusquedaVictimasM> qVictimaMedida { get; set; }
        public DbSet<queryBusquedaDelitoM> qBusquedaDelitoM { get; set; }
        public DbSet<queryBusquedaInstitucionesM> qBusquedaInstitucionM { get; set; }
        public DbSet<queryComprobarD> qComprobarDatos { get; set; }
        public DbSet<queryComprobarDR> qComprobarDatosR { get; set; }
        public DbSet<queryComprobarNuc> qComprobarDatosNuc { get; set; }
        public DbSet<queryListarTableroModulo> qComprobarModulo { get; set; }
        public DbSet<queryBusquedaVictimasJR> qVictimaJR { get; set; }





        public DbSet<queryBusquedaPersonasCarpetas> qBusquedaPersonasCarpetas { get; set; }
        public DbSet<queryBusquedaDatosProtegidos> qBusquedaDatosProtegidos { get; set; }
        public DbSet<UsuarioDis> qLoginConDistrito { get; set; }



        //MODULO CONTROL DE ACCESO
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ControlDistrito> controlDistritos { get; set; }
        public DbSet<PanelUsuario> PanelUsuarios { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<SeccionesRolPanel> SeccionesRolPanels { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<ModuloRol> ModuloRols { get; set; }
        public DbSet<SubModuloRol> SubModuloRols { get; set; }

        public DbSet<TiempoSesion> TiempoSesions { get; set; }
        public DbSet<ClavesAgencia> ClavesAgencias { get; set; }
        public DbSet<BitacoraUsuario> BitacoraUsuarios { get; set; }




        public DbSet<Almacenamiento> Almacenamientos { get; set; }
        //MODULO PANEL DE CONTROL
        public DbSet<PanelControl> PanelControls { get; set; }
        public DbSet<ControlDistrito> ControlDistritos { get; set; }

        //MODULO SERVICIOS PERICIALES
        public DbSet<PeritoAsignado> PeritoAsignados { get; set; }
        public DbSet<PeritosAsignadoForaneas> PeritosAsignadoForaneas { get; set; }
        public DbSet<DocsDiligencia> DocsDiligencias { get; set; }
        public DbSet<DocsDiligenciaForaneas> DocsDiligenciaForaneas { get; set; }
        public DbSet<DiligenciaIndicio> DiligenciaIndicios { get; set; }



        //MODULO JUSTICIA RESTAURATIVA
 
        public DbSet<Expediente> Expedientes { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<SolicitanteRequerido> SolicitanteRequeridos { get; set; }
        public DbSet<SolicitanteRequerido1> SolicitanteRequeridos1 { get; set; }
        public DbSet<DelitoDerivado> DelitosDerivados { get; set; }
        public DbSet<CitatorioRecordatorio> CitatorioRecordatorios { get; set; }
        public DbSet<FacilitadorNotificador> FacilitadorNotificadors { get; set; }
        public DbSet<AsignacionEnvio> AsignacionEnvios { get; set; }        
        public DbSet<Sesion> Sesions { get; set; }
        //public DbSet<Conjunto> Conjuntos { get; set; }
        public DbSet<AcuerdoReparatorio> AcuerdoReparatorios { get; set; }
        public DbSet<SeguimientoCumplimiento> SeguimientoCumplimientos { get; set; }
        public DbSet<ConsultaSolicitanteRequerido> ConsultaSolicitanteRequeridos { get;set; }
        public DbSet<ConsultaExpedientesRezagado> ConsultaExpedientesRezagados { get; set; }
        public DbSet<RegistroConclusion> RegistroConclusions { get; set; }
        public DbSet<ConsultaIdSeguimientoDesdeSesion> ConsultaIdSeguimientoDesdeSesions { get; set; }
        public DbSet<ConsultaRequeridoCat> consultaRequeridoCats { get; set; }
        public DbSet<ConsultaRepresentantesenCat> ConsultaRepresentantesenCats { get; set; }

        public DbSet<RegistroNotificacion> RegistroNotificacions { get; set; }
        public DbSet<Responsablejr> Responsablejrs { get; set; }
        public DbSet<SesionConjunto> SesionConjuntos { get; set; }

        public DbSet<AcuerdosConjunto> AcuerdosConjuntos { get; set; }
        public DbSet<CoordinacionDistrito> CoordinacionDistritos {  get; set; } 
        public DbSet<ACARRaw> ACARRaws { get; set; }
        public DbSet<Registro> Registros { get; set; }


        //MODULO POLICIA iNVESTIGADORA

        public DbSet<Fotos> Fotos { get; set; }
        public DbSet<Detencion> Detencions { get; set; }
        public DbSet<PIPersonaVisita> PIPersonaVisitas { get; set; }
        public DbSet<Direccion> Direccions { get; set; }
        public DbSet<FPersona> FPersonas { get; set; }
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<CMedicoPSR> CMedicoPSRs { get; set; }
        public DbSet<PeritoAsignadoPI> PeritoAsignadoPIs { get; set; }
        public DbSet<CMedicoPR> CMedicoPRs { get; set; }
        public DbSet<Informe> Informes { get; set; }
        public DbSet<SolicitudInteligenciaAsig> SolicitudInteligenciaAsigs { get; set; }
        public DbSet<SolicitudInteligencia> SolicitudInteligencias { get; set; }
        public DbSet<Arresto> Arrestos { get; set; }
        public DbSet<BusquedaDomicilio> BusquedaDomicilios { get; set; }
        public DbSet<ComparecenciasElementos> ComparecenciasElementos { get; set; }
        public DbSet<Exhorto> Exhortos { get; set; }
        public DbSet<OrdenAprension> OrdenAprensions { get; set; }
        public DbSet<PresentacionesYC> PresentacionesYCs { get; set; }
        public DbSet<RequerimientosVarios> RequerimientosVarios { get; set; }
        public DbSet<TrasladosYCustodias> TrasladosYCustodias { get; set; }
        public DbSet<EstatusCustodia> EstatusCustodias { get; set; }
        public DbSet<SubirArchivo> SubirArchivos { get; set; }
        public DbSet<EgresoTemporal> EgresoTemporals { get; set; }
        public DbSet<OAprhensionBitacora> OAprhensionBitacoras { get; set; }
        public DbSet<HistorialDetencion> HistorialDetencions { get; set; }
        public DbSet<ArchivosOAprension> ArchivosOAprensions { get; set; }
        public DbSet<ArchivosComparecencia> ArchivosComparecencias { get; set; }

        //MODULO INVESTIGACION LITIGACION
        public DbSet<Agenda> Agendas  { get; set; }
        public DbSet<Citatorio> Citatorios { get; set; }
        public DbSet<SolicitudInformacion> solicitudInformacions { get; set; }
        public DbSet<PoderJudicial> PoderJudicial { get; set; }

        public DbSet<SolicitudAudiencia> SolicitudAudiencias { get; set; }

        //MODULO FEDC

        public DbSet<NoAcionPenal> NoAcionPenals { get; set; }

        //MODULO DE LOG DE MODIFICACIONES (ADMINISTRACION DE INFORMACION)

        public DbSet<LogAdmon> LogAdmons { get; set; }
        public DbSet<LogTipoMov> LogTipoMovs { get; set; }
        public DbSet<LogAmpDec> LogAmpDecs { get; set; }
        public DbSet<LogCat_RAtencon> LogCatRAtencons { get; set; }
        public DbSet<LogCat_RHecho> LogCatRHechoes { get; set; }
        public DbSet<LogNuc> LogNucs { get; set; }
        public DbSet<LogPersona> LogPersonas { get; set; }
        public DbSet<LogRac> LogRacs { get; set; }
        public DbSet<LogRAP> LogRaps { get; set; }
        public DbSet<LogArchivos> LogArchivos { get; set; }
        public DbSet<LogBitacora> LogBitacoras { get; set; }
        public DbSet<LogC5Formatos> LogC5Formatos { get; set; }
        public DbSet<LogDocsRepresentantes> LogDocsRepresentantes { get; set; }
        public DbSet<LogEnvio> LogEnvios { get; set; }
        public DbSet<LogExpediente> LogExpedientes { get; set; }
        public DbSet<LogHistorialCarpeta> LogHistorialCarpetas { get; set; }
        public DbSet<LogMedidasproteccion> LogMedidasProteccions { get; set; }
        public DbSet<LogOficio> LogOficios { get; set; }
        public DbSet<LogRCitatorioNotificacion> LogRCitatorioNotificacions { get; set; }
        public DbSet<LogRDiligencias> LogDiligencias { get; set; }
        public DbSet<LogRDiligenciasForaneas> LogDiligenciasForaneas { get; set; }
        public DbSet<LogRepresentante> LogRepresentantes { get; set; }
        public DbSet<LogRPersonaDesap> LogPersonaDesaps { get; set; }
        public DbSet<LogVehiculo> LogVehiculos { get; set; }
        public DbSet<LogVehiculoPersonaDesap> LogVehiculoPersonaDesaps { get; set; }
        public DbSet<LogRDH> LogRDHs { get; set; }
        public DbSet<LogRActoInvestigacion> LogRActoInvestigacions { get; set; }
        public DbSet<LogRemisionUI> LogRemisionUIs { get; set; }
        public DbSet<LogDesglose> LogDesgloses { get; set; }


        public DbContextSIIGPP(DbContextOptions<DbContextSIIGPP> options) : base(options)
        {
            
        }
        [DbFunction("RemoveDiacritics", "dbo")]
        public static string RemoveDiacritics(string input)
        {
          
           return (input);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //MODULO CAT
            modelBuilder.ApplyConfiguration(new RacMap());
            modelBuilder.ApplyConfiguration(new NucMap());
            modelBuilder.ApplyConfiguration(new RAtencionMap());
            modelBuilder.ApplyConfiguration(new RAPMap());
            modelBuilder.ApplyConfiguration(new PersonaMap());
            modelBuilder.ApplyConfiguration(new DireccionPersonalMap());
            modelBuilder.ApplyConfiguration(new DireccionEscuchaMap());
            modelBuilder.ApplyConfiguration(new MediaAfiliacionMap());
            modelBuilder.ApplyConfiguration(new MedidasCautelaresMap());
            modelBuilder.ApplyConfiguration(new CondImpProcesoMap());
            modelBuilder.ApplyConfiguration(new TurnoMap());
            modelBuilder.ApplyConfiguration(new AmpoTurnoMap());
            modelBuilder.ApplyConfiguration(new ModuloServicioMap());

            //-----------------------------------------------------------------------------------------------------------------------

            /* modelBuilder.Entity("SIIGPP.Entidades.M_Cat.RDHecho.RDH", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_Cat.Registro.Persona", "Personas")
                    .WithMany()
                    .HasForeignKey("PersonaId")
                    .OnDelete(DeleteBehavior.Cascade);
            }); */


            modelBuilder.ApplyConfiguration(new RDHMap());
            //------------------------------------------------------------------------------------------------------------------------
            modelBuilder.ApplyConfiguration(new RHechoMap());
            modelBuilder.ApplyConfiguration(new RDiligenciasMap());
            modelBuilder.ApplyConfiguration(new RDiligenciasForaneasMap());
            modelBuilder.ApplyConfiguration(new RDDerivacionMap());
            modelBuilder.ApplyConfiguration(new ConjuntoDerivacionMap());
            modelBuilder.ApplyConfiguration(new AmpDecMap());
            modelBuilder.ApplyConfiguration(new IndiciosMap());
            modelBuilder.ApplyConfiguration(new DetalleSeguimientoIndicioMap());
            modelBuilder.ApplyConfiguration(new ArchivosMediaAfiliacionMap());

            modelBuilder.ApplyConfiguration(new DireccionDelitoMap());
            modelBuilder.ApplyConfiguration(new RbitacoraMap());

            modelBuilder.ApplyConfiguration(new IndiciosMap());
            modelBuilder.ApplyConfiguration(new DetalleSeguimientoIndicioMap());
            modelBuilder.ApplyConfiguration(new RCitatorio_NotificacionMap());
            modelBuilder.ApplyConfiguration(new DocumentosPesonaMap());
            modelBuilder.ApplyConfiguration(new VehiculoMap());
            modelBuilder.ApplyConfiguration(new DevolucionvehiculoMap());
            modelBuilder.ApplyConfiguration(new RArmaMap());
            modelBuilder.ApplyConfiguration(new MedidasProteccionMap());
            modelBuilder.ApplyConfiguration(new DeterminacionArchivoMap());
            modelBuilder.ApplyConfiguration(new RIncompetenciaMap());
            modelBuilder.ApplyConfiguration(new RepresentanteMap());
            modelBuilder.ApplyConfiguration(new DocsRepresentantesMap());
            modelBuilder.ApplyConfiguration(new ArchivosMAP());
            modelBuilder.ApplyConfiguration(new DatosRelacionadosMap());
            modelBuilder.ApplyConfiguration(new C5Map());
            modelBuilder.ApplyConfiguration(new DatoProtegidoMap());
            modelBuilder.ApplyConfiguration(new OficioMap());
            modelBuilder.ApplyConfiguration(new NoEjercicioApenalMap());
            modelBuilder.ApplyConfiguration(new HistorialcarpetasMap());
            modelBuilder.ApplyConfiguration(new RegistroTableroIMap());
            modelBuilder.ApplyConfiguration(new DesglosesMap());
            modelBuilder.ApplyConfiguration(new RemisionUIMap());
            modelBuilder.ApplyConfiguration(new SColaboracionMPMap());
            modelBuilder.ApplyConfiguration(new AsignacionColaboracionMap());
            modelBuilder.ApplyConfiguration(new ArchivosVehiculoMap());
            modelBuilder.ApplyConfiguration(new ProcedimientoAbreviadosMap());
            modelBuilder.ApplyConfiguration(new AcumulacionCarpetaMap());
            modelBuilder.ApplyConfiguration(new ContencionesPersonaMap());
            modelBuilder.ApplyConfiguration(new NoMedidasCautelaresMap());
            modelBuilder.ApplyConfiguration(new NoMedidasProteccionMap());
            modelBuilder.ApplyConfiguration(new ResolucionMap());
            modelBuilder.ApplyConfiguration(new TViolenciaMap());
            modelBuilder.ApplyConfiguration(new InstrumentoComisionMap());

            //************* CAT_Captura
            modelBuilder.ApplyConfiguration(new CapturaMap());

            // MODULO DE PRE REGISTRO

            modelBuilder.ApplyConfiguration(new PreRegistroMap());
            modelBuilder.ApplyConfiguration(new PreAtencionMap());
            modelBuilder.Entity("SIIGPP.Entidades.M_Cat.PAtencion.PreAtencion", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_Cat.PRegistro.PreRegistro", "registros")
                    .WithMany()
                    .HasForeignKey("PRegistroId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity("SIIGPP.Entidades.M_Cat.PRegistro.PreRap", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_Cat.PAtencion.PreAtencion", "preAtencion")
                    .WithMany()
                    .HasForeignKey("PAtencionId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.ApplyConfiguration(new PreRapMap());
            modelBuilder.Entity("SIIGPP.Entidades.M_Cat.PCitas.PreCitas", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_Cat.PRegistro.PreRegistro", "registros")
                    .WithMany()
                    .HasForeignKey("PRegistroId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.ApplyConfiguration(new PreCitasMap());
            modelBuilder.ApplyConfiguration(new PreHorarioDisponibleMap());

            //MODULO DE ADMINISTRACION (LOGS de BORRADO)

            modelBuilder.ApplyConfiguration(new LogAdmonMap());
            modelBuilder.ApplyConfiguration(new LogAmpDecMap());
            modelBuilder.ApplyConfiguration(new LogCat_RAtenconMap());
            modelBuilder.ApplyConfiguration(new LogCat_RHechoMap());
            modelBuilder.ApplyConfiguration(new LogNucMap());
            modelBuilder.ApplyConfiguration(new LogPersonaMap());
            modelBuilder.ApplyConfiguration(new LogRacMap());
            modelBuilder.ApplyConfiguration(new LogRAPMap());
            modelBuilder.ApplyConfiguration(new LogTipoMovMap());
            modelBuilder.ApplyConfiguration(new LogArchivosMAP());
            modelBuilder.ApplyConfiguration(new LogBitacoraMap());
            modelBuilder.ApplyConfiguration(new LogC5Map());
            modelBuilder.ApplyConfiguration(new LogDocsRepresentantesMap());
            modelBuilder.ApplyConfiguration(new LogEnvioMap());
            modelBuilder.ApplyConfiguration(new LogExpedienteMap());
            modelBuilder.ApplyConfiguration(new LogHistorialcarpetasMap());
            modelBuilder.ApplyConfiguration(new LogMedidasProteccionMap());
            modelBuilder.ApplyConfiguration(new LogOficioMap());
            modelBuilder.ApplyConfiguration(new LogRCitatorioNotificacionMap());
            modelBuilder.ApplyConfiguration(new LogRDiligenciasMap());
            modelBuilder.ApplyConfiguration(new LogRDiligenciasForaneasMap());
            modelBuilder.ApplyConfiguration(new LogRepresentanteMap());
            modelBuilder.ApplyConfiguration(new LogVehiculoPersonaDesapMap());
            modelBuilder.ApplyConfiguration(new LogVehiculoMap());
            modelBuilder.ApplyConfiguration(new LogVehiculoPersonaDesapMap());
            modelBuilder.ApplyConfiguration(new LogRDHMap());
            modelBuilder.ApplyConfiguration(new LogRActosInvestigacionMap());
            modelBuilder.ApplyConfiguration(new LogRPersonaDesapMap());
            modelBuilder.ApplyConfiguration(new LogRemisionUIMap());
            modelBuilder.ApplyConfiguration(new LogDesgloseMap());


            //  MODULO PERSONAS DESAPARECIDAS
            //
            //modelBuilder.Entity("SIIGPP.Entidades.M_Cat.PersonaDesap.RPersonaDesap", b =>
            //{
            //    b.HasOne("SIIGPP.Entidades.M_Cat.VehiculoDesaparicionPersona.PreRegistro", "registros")
            //        .WithMany()
            //        .HasForeignKey("PersonaDesaparecidaId")
            //        .OnDelete(DeleteBehavior.Cascade);
            //});

            modelBuilder.ApplyConfiguration(new DireccionOcupacionMap());
            modelBuilder.ApplyConfiguration(new RedesSocialesMap());
            modelBuilder.ApplyConfiguration(new RedesSocialesPersonalMap());

            modelBuilder.Entity("SIIGPP.Entidades.M_Cat.PersonaDesap.VehiculoPersonaDesap", b =>
            {
                
                b.HasOne("SIIGPP.Entidades.M_Configuracion.Cat_INEGI.Estado", "Estado")
                    .WithMany()
                    .HasForeignKey("EstadoId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity("SIIGPP.Entidades.M_Cat.PersonaDesap.VehiculoPersonaDesap", b =>
            {

                b.HasOne("SIIGPP.Entidades.M_Cat.PersonaDesap.RPersonaDesap", "personadesap")
                    .WithMany()
                    .HasForeignKey("PersonaDesaparecidaId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.ApplyConfiguration(new VehiculoPersonaDesapMap());
            modelBuilder.ApplyConfiguration(new MarcaTelefonoMap());
            modelBuilder.ApplyConfiguration(new CompaniaTelefonicaMap());

            
            modelBuilder.Entity("SIIGPP.Entidades.M_Cat.PersonaDesap.RPersonaDesap", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_Cat.PersonaDesap.MarcaTelefono", "MarcaTelefono")
                    .WithMany()
                    .HasForeignKey("marcaTelefonoId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            

            modelBuilder.Entity("SIIGPP.Entidades.M_Cat.PersonaDesap.RPersonaDesap", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_Cat.PersonaDesap.CompaniaTelefonica", "CompaniaTelefonica")
                    .WithMany()
                    .HasForeignKey("companiaTelefonicaId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.ApplyConfiguration(new RPersonaDesapMap());




            //MODULO CONFIGURACION
            //************* CAT_Configuracion Global
            modelBuilder.ApplyConfiguration(new ConfGlobalMap());
            modelBuilder.ApplyConfiguration(new HipotesisMap());
            //************* CAT_Configuracion
            modelBuilder.ApplyConfiguration(new DocIdentificacionMap());
            modelBuilder.ApplyConfiguration(new MedioNotificacionMap());
            modelBuilder.ApplyConfiguration(new ClasificacionPersonaMap());
            modelBuilder.ApplyConfiguration(new NivelEstudiosMap());
            modelBuilder.ApplyConfiguration(new MultaCitatoriosMap());
            //************* CAT_Estrucutra
            modelBuilder.ApplyConfiguration(new DistritoMap());
            modelBuilder.ApplyConfiguration(new DSPMap());
            modelBuilder.ApplyConfiguration(new AgenciaMap()); 
            modelBuilder.ApplyConfiguration(new DependeciasDerivacionMap());
            modelBuilder.ApplyConfiguration(new ErrorReplicacionMap());
            //************* CAT_Delito
            modelBuilder.ApplyConfiguration(new DelitoMap()); 
            modelBuilder.ApplyConfiguration(new ClasificaOrdenResultMap());
            modelBuilder.ApplyConfiguration(new IntensionDelitoMap());
            modelBuilder.ApplyConfiguration(new ResultadoDelitoMap());
            modelBuilder.ApplyConfiguration(new TipoDeclaracionMap());
            modelBuilder.ApplyConfiguration(new TipoFueroMap());
            modelBuilder.ApplyConfiguration(new TipoMap());
            modelBuilder.ApplyConfiguration(new DelitoEspecificoMap());
            //************* CAT_Generales
            modelBuilder.ApplyConfiguration(new EstadoCivilMap());
            modelBuilder.ApplyConfiguration(new GeneroMap());
            modelBuilder.ApplyConfiguration(new SexoMap());
            modelBuilder.ApplyConfiguration(new RangoEdadMap());
            //************* CAT_INEGI
           modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new LenguaMap());
            modelBuilder.ApplyConfiguration(new LocalidadMap());
            modelBuilder.ApplyConfiguration(new MunicipioMap());
            modelBuilder.ApplyConfiguration(new OcupacionMap());
            modelBuilder.ApplyConfiguration(new ReligionMap());
            modelBuilder.ApplyConfiguration(new NacionalidadMap());
            modelBuilder.ApplyConfiguration(new DiscapacidadMap());
            modelBuilder.ApplyConfiguration(new VialidadMap());
            modelBuilder.ApplyConfiguration(new AsentamientoMap());
            modelBuilder.ApplyConfiguration(new LugarEspecificoMap());
            //************* Servicios periciales 
            modelBuilder.ApplyConfiguration(new ASPMap());
            modelBuilder.ApplyConfiguration(new ServicioPericialMap());
            //************* Status NUC 
            modelBuilder.ApplyConfiguration(new StatusNUCMap());
            modelBuilder.ApplyConfiguration(new EtapaMap());
            //************* CAT_Indicios
            modelBuilder.ApplyConfiguration(new InstitucionMap());
            modelBuilder.ApplyConfiguration(new TipoIndicioMap());
            //************* CAt_Vehiculos
            modelBuilder.ApplyConfiguration(new AnoMap());
            modelBuilder.ApplyConfiguration(new ColorMap());
            modelBuilder.ApplyConfiguration(new MarcaMap());
            modelBuilder.ApplyConfiguration(new ModeloMap());
            modelBuilder.ApplyConfiguration(new TipovMap());
            //************* CAT_Armas
            modelBuilder.ApplyConfiguration(new ClasificacionArmaMap());
            modelBuilder.ApplyConfiguration(new ArmaObjetoMap());
            modelBuilder.ApplyConfiguration(new MarcaArmaMap());
            modelBuilder.ApplyConfiguration(new CalibreMap());
            
            //************** CAT_RActosInvestigacion
            modelBuilder.ApplyConfiguration(new RActosInvestigacionMap());
            modelBuilder.ApplyConfiguration(new ActosInDetalleMap());

            //************* CONTROL DE ACCESO
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PanelUsuarioMap());
            modelBuilder.ApplyConfiguration(new AlmacenamientoMap()); 
            modelBuilder.ApplyConfiguration(new SeccionesMap());
            modelBuilder.ApplyConfiguration(new TiempoSesionMap());
            modelBuilder.ApplyConfiguration(new ClavesAgenciaMap());
            modelBuilder.ApplyConfiguration(new ControlDistritoMap());
            modelBuilder.ApplyConfiguration(new BitacoraUsuarioMap());

            //SECCIONES ROL PANEL
            modelBuilder.Entity("SIIGPP.Entidades.M_ControlAcceso.Menu.SeccionesRolPanel", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_ControlAcceso.Menu.Seccion", "Seccion")
                    .WithMany()
                    .HasForeignKey("SeccionId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity("SIIGPP.Entidades.M_ControlAcceso.Menu.SeccionesRolPanel", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_ControlAcceso.Roles.Rol", "Rol")
                    .WithMany()
                    .HasForeignKey("RolId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity("SIIGPP.Entidades.M_ControlAcceso.Menu.SeccionesRolPanel", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_Panel.M_PanelControl.PanelControl", "PanelControl")
                    .WithMany()
                    .HasForeignKey("PanelControlId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.ApplyConfiguration(new SeccionesRolPanelMap());

            //MODULOS
            modelBuilder.ApplyConfiguration(new ModulosMap());

            // MODULO ROL 
            modelBuilder.Entity("SIIGPP.Entidades.M_ControlAcceso.Menu.ModuloRol", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_ControlAcceso.Menu.Modulo", "Modulo")
                    .WithMany()
                    .HasForeignKey("ModuloId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity("SIIGPP.Entidades.M_ControlAcceso.Menu.ModuloRol", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_ControlAcceso.Menu.SeccionesRolPanel", "SeccionRolPanel")
                    .WithMany()
                    .HasForeignKey("MenuPanelId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.ApplyConfiguration(new ModulosRolMap());

            //SUBMODULO ROL
            modelBuilder.Entity("SIIGPP.Entidades.M_ControlAcceso.Menu.SubModuloRol", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_ControlAcceso.Menu.Modulo", "Modulo")
                    .WithMany()
                    .HasForeignKey("ModuloId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity("SIIGPP.Entidades.M_ControlAcceso.Menu.SubModuloRol", b =>
            {
                b.HasOne("SIIGPP.Entidades.M_ControlAcceso.Menu.ModuloRol", "ModuloRol")
                    .WithMany()
                    .HasForeignKey("ModuloRolId")
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.ApplyConfiguration(new SubModuloRolMap());
            //************** Cat_ActosInvestigacion
            modelBuilder.ApplyConfiguration(new ActoInvestigacionMap());

            //************** Cat_Incompetencia
            modelBuilder.ApplyConfiguration(new IncompetenciaMap());
            //************** Cat_TiposRepresentantes
            modelBuilder.ApplyConfiguration(new TiposRepresentantesMap());
            //************** Cat_FiscaliaOestados
            modelBuilder.ApplyConfiguration(new FiscaliaOestadoMap());
            //************** Cat_RasgosFaciales
            modelBuilder.ApplyConfiguration(new ComplexionMap());
            modelBuilder.ApplyConfiguration(new Cantidad_de_cabelloMap());
            modelBuilder.ApplyConfiguration(new Forma_de_cabelloMap());
            modelBuilder.ApplyConfiguration(new Largo_de_CabelloMap());
            modelBuilder.ApplyConfiguration(new Color_de_CabelloMap());
            modelBuilder.ApplyConfiguration(new Forma_de_CaraMap());
            modelBuilder.ApplyConfiguration(new Tipo_de_FrenteMap());
            modelBuilder.ApplyConfiguration(new CejasMap());
            modelBuilder.ApplyConfiguration(new Tipo_de_CejasMap());
            modelBuilder.ApplyConfiguration(new Color_OjosMap());
            modelBuilder.ApplyConfiguration(new TipoNarizMap());
            modelBuilder.ApplyConfiguration(new Tamaño_de_BocaMap());
            modelBuilder.ApplyConfiguration(new Forma_de_MentonMap());
            modelBuilder.ApplyConfiguration(new Tipo_de_OrejasMap());
            modelBuilder.ApplyConfiguration(new TezMap());
            modelBuilder.ApplyConfiguration(new Grosor_de_labiosMap());
            modelBuilder.ApplyConfiguration(new Tamaño_NarizMap());
            modelBuilder.ApplyConfiguration(new TipoOjosMap());
            modelBuilder.ApplyConfiguration(new Tipo2OjosMap());

            modelBuilder.ApplyConfiguration(new Adherencia_OrejaMap());
            modelBuilder.ApplyConfiguration(new CalvicieMap());
            modelBuilder.ApplyConfiguration(new Forma_de_ojoMap());
            modelBuilder.ApplyConfiguration(new Implantacion_CejaMap());
            modelBuilder.ApplyConfiguration(new Punta_NarizMap());
            modelBuilder.ApplyConfiguration(new Tamano_DentalMap());
            modelBuilder.ApplyConfiguration(new Tipo_de_MentonMap());
            modelBuilder.ApplyConfiguration(new Tipo_DentaduraMap());
            modelBuilder.ApplyConfiguration(new Tratamiento_DentalMap());
            modelBuilder.ApplyConfiguration(new Tratamientos_Quimicos_CabelloMap());
            modelBuilder.ApplyConfiguration(new PomulosMap());

            //************** Cat_JUZGADOSagencias
            modelBuilder.ApplyConfiguration(new JuzgadosAgenciasMap());

            //************** Cat_MedFiliacionLarga
            modelBuilder.ApplyConfiguration(new UnasNoSaludablesMap());
            modelBuilder.ApplyConfiguration(new SituacionDentalMap());
            modelBuilder.ApplyConfiguration(new TipoLesionesMap());
            modelBuilder.ApplyConfiguration(new TipoRetrasoMap());
            modelBuilder.ApplyConfiguration(new MediaFiliacionDesaparecidoMap());

            //************** Cat_Medidas
            modelBuilder.ApplyConfiguration(new MedidasProteccionCMap());
            modelBuilder.ApplyConfiguration(new MedidasCautelaresCMap());
            //************** Cat_LigacionesSPPI
            modelBuilder.ApplyConfiguration(new SPPiligacionesMap());
            //************** Documentacion del sietema
            modelBuilder.ApplyConfiguration(new ActualizacionesMap());

            //MODULO PANEL DE CONTROL
            modelBuilder.ApplyConfiguration(new PanelControlMap());
            modelBuilder.ApplyConfiguration(new ControlDistritoMap());

            //MODULO SERVICIOS PERICIALES
            modelBuilder.ApplyConfiguration(new DocsDiligenciaMap());
            modelBuilder.ApplyConfiguration(new DiligenciaIndicioMap());
            modelBuilder.ApplyConfiguration(new PeritoAsignadoMap());
            modelBuilder.ApplyConfiguration(new PeritoAsignadoForaneasMap());
            modelBuilder.ApplyConfiguration(new DocsDiligenciaForaneasMap());

            //MODULO JUSTICIA RESTAURATIVA 
            modelBuilder.ApplyConfiguration(new ExpedienteMap());
            modelBuilder.ApplyConfiguration(new EnvioMap());
            modelBuilder.ApplyConfiguration(new SolicitanteRequerdioMap());
            modelBuilder.ApplyConfiguration(new DelitoDerivadoMap());
            modelBuilder.ApplyConfiguration(new CitatorioRecordatorioMap());
            modelBuilder.ApplyConfiguration(new FacilitadorNotificadorMap());
            modelBuilder.ApplyConfiguration(new AsignacionEnvioMap());
            modelBuilder.ApplyConfiguration(new SesionMap());
            modelBuilder.ApplyConfiguration(new AcuerdoReparatorioMap());
            modelBuilder.ApplyConfiguration(new SeguimientoCumplimientoMap());
            modelBuilder.ApplyConfiguration(new RegistroNotificacionesMap());
            modelBuilder.ApplyConfiguration(new RegistroConclusionMap());
            modelBuilder.ApplyConfiguration(new ResponsablejrMap());
            modelBuilder.ApplyConfiguration(new SesionConjuntoMap());
            modelBuilder.ApplyConfiguration(new CoordinacionDistritoMap());
            modelBuilder.ApplyConfiguration(new RegistroMap());


            modelBuilder.ApplyConfiguration(new AcuerdosConjuntoMap());

            //MODULO POLICIA INVESTIGADORA
            modelBuilder.ApplyConfiguration(new FotosMap());
            modelBuilder.ApplyConfiguration(new DetencionMap());
            modelBuilder.ApplyConfiguration(new PIPersonaVisitaMap());
            modelBuilder.ApplyConfiguration(new DireccionMap());
            modelBuilder.ApplyConfiguration(new FPersonaMap());
            modelBuilder.ApplyConfiguration(new VisitaMap());
            modelBuilder.ApplyConfiguration(new CmedicosPSRMap());
            modelBuilder.ApplyConfiguration(new PeritoAsignadoPIMap());
            modelBuilder.ApplyConfiguration(new CMedicoPRMap());
            modelBuilder.ApplyConfiguration(new InformeMap());
            modelBuilder.ApplyConfiguration(new SolicitudInteligenciaMap());
            modelBuilder.ApplyConfiguration(new SolicitudInteligenciaAsigMap());
            modelBuilder.ApplyConfiguration(new ArrestoMap());
            modelBuilder.ApplyConfiguration(new BusquedaDomicilioMap());
            modelBuilder.ApplyConfiguration(new ComparecenciasElementosMap());
            modelBuilder.ApplyConfiguration(new ExhortoMap());
            modelBuilder.ApplyConfiguration(new OrdenAprensionMap());
            modelBuilder.ApplyConfiguration(new PresentacionesYCMap());
            modelBuilder.ApplyConfiguration(new RequerimientosVariosMap());
            modelBuilder.ApplyConfiguration(new TrasladosYCustodiasMap());
            modelBuilder.ApplyConfiguration(new EstatusCustodiaMap());
            modelBuilder.ApplyConfiguration(new SubirArchivoMap());
            modelBuilder.ApplyConfiguration(new EgresoTemporalMap());
            modelBuilder.ApplyConfiguration(new OrdenAprensionMap());
            modelBuilder.ApplyConfiguration(new HistorialDetencionMap());
            modelBuilder.ApplyConfiguration(new ArchivosComparecenciaMap());
            modelBuilder.ApplyConfiguration(new ArchivosOAprensionMap());


            //MODULO INVESTIGACION LITIGACION
            modelBuilder.ApplyConfiguration(new AgendaMap());
            modelBuilder.ApplyConfiguration(new CitatorioMap());
            modelBuilder.ApplyConfiguration(new SolicitudInformacionMap());
            modelBuilder.ApplyConfiguration(new PoderJudicialMap());
            modelBuilder.ApplyConfiguration(new SolicitudAudienciaMap());

            //MODULO INVESTIGACION LITIGACION
            modelBuilder.ApplyConfiguration(new NoAcionPenalMap());
        }
    }
}
 
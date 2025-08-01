using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Ampliacion;
using SIIGPP.Entidades.M_Cat.Direcciones;
using SIIGPP.Entidades.M_Cat.GNUC;
using SIIGPP.Entidades.M_Cat.GRAC;
using SIIGPP.Entidades.M_Cat.Orientacion;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Cat.RDHecho;
using SIIGPP.Entidades.M_Cat.Representantes;
using SIIGPP.Entidades.M_Cat.CArchivos;
using SIIGPP.CAT.Models.DenunciaEnLinea;
using Microsoft.Extensions.Configuration;
using SIIGPP.Entidades.M_Cat.Diligencias;
using SIIGPP.Entidades.M_Cat.MedidasProteccion;
using SIIGPP.Entidades.M_Cat.RActosInvestigacion;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Datos.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_IL.Agendas;
using SIIGPP.Entidades.M_PI.CFotos;
using SIIGPP.CAT.Models.Desgloses;
using SIIGPP.Entidades.M_Configuracion.Cat_Estrucutra;

namespace SIIGPP.CAT.Controllers
{
    //Se ha creado un nuevo controlador para la denucia en linea
    [Route("api/[controller]")]
    [ApiController]
    public class DenunciaEnLineaController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public DenunciaEnLineaController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Esta primera api crear las dos primeras tablas, es decir, el rac y la ratencion
        // POST: api/DenunciaEnLinea/CrearRacRatencion
        [Authorize(Roles = "Administrador,Director,Coordinador,AMPO-AMP,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRacRatencion([FromBody] RAtencionRacsDELViewModel model)
        {
            Guid idatencion;

            try
            {
                //En dado caso que el modelo sea invalido retorna un BadRequest
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Insercion en la tabla rac
                Rac InsertarRAC = new Rac
                {
                    Indicador = model.Indicador,
                    DistritoId = model.DistritoId,
                    CveDistrito = model.CveDistrito,
                    DConsecutivo = model.DConsecutivo,
                    AgenciaId = model.AgenciaId,
                    CveAgencia = model.CveAgencia,
                    AConsecutivo = model.AConsecutivo,
                    Año = model.Año,
                    racg = model.racg,
                    Asignado = model.Asignado,
                    Ndenuncia = model.Ndenuncia,
                };

                //Se guarda el dato
                _context.Racs.Add(InsertarRAC);
                await _context.SaveChangesAsync();

                //Se guarda el id de la rac en una variable local
                var idrac = InsertarRAC.idRac;

                //Insercion en la tabla atencion
                RAtencion InsertarRA = new RAtencion
                {
                    FechaHoraRegistro = model.FechaHoraRegistro,
                    FechaHoraAtencion = model.FechaHoraAtencion,
                    FechaHoraCierre = model.FechaHoraCierre,
                    u_Nombre = model.u_Nombre,
                    u_Puesto = model.u_Puesto,
                    u_Modulo = model.u_Modulo,
                    DistritoInicial = model.DistritoInicial,
                    DirSubProcuInicial = model.DirSubProcuInicial,
                    AgenciaInicial = model.AgenciaInicial,
                    StatusAtencion = model.StatusAtencion,
                    StatusRegistro = model.StatusRegistro,
                    MedioDenuncia = model.MedioDenuncia,
                    ContencionVicitma = model.ContencionVicitma,
                    racId = idrac,
                    ModuloServicio = model.ModuloServicio,
                    MedioLlegada = model.MedioLlegada

                };

                //Se guarda el dato
                _context.RAtencions.Add(InsertarRA);
                await _context.SaveChangesAsync();

                //Se guarda el id de la atencion en una variable local
                idatencion = InsertarRA.IdRAtencion;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
            //Una vez guardado retorna el id del registro de ratencion que se haya creador
            return Ok(new { idatencion = idatencion });
        }

        //Esta api registra la persona inicial asi como el registro en la tabla raps para relacionar las personas con el hecho
        // POST: api/DenunciaEnLinea/GuardarPersonasD
        [Authorize(Roles = "Administrador,Director,Coordinador,AMPO-AMP,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarPersonasD([FromBody] PersonasDELViewModel model)
        {

            Guid idpersonar;
            try
            {
                //En dado caso que el modelo sea invalido retorna un BadRequest
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Insercion en la tabla persona
                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = model.StatusAnonimo,
                    TipoPersona = model.TipoPersona,
                    RFC = model.RFC,
                    RazonSocial = model.RazonSocial,
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = model.Alias,
                    StatusAlias = model.StatusAlias,
                    FechaNacimiento = model.FechaNacimiento,
                    EntidadFederativa = model.EntidadFederativa,
                    DocIdentificacion = model.DocIdentificacion,
                    CURP = model.CURP,
                    Sexo = model.Sexo,
                    Genero = model.Genero,
                    EstadoCivil = model.EstadoCivil,
                    Telefono1 = model.Telefono1,
                    Telefono2 = model.Telefono2,
                    Correo = model.Correo,
                    Medionotificacion = model.Medionotificacion,
                    Nacionalidad = model.Nacionalidad,
                    Ocupacion = model.Ocupacion,
                    NivelEstudio = model.NivelEstudio,
                    Lengua = model.Lengua,
                    Religion = model.Religion,
                    Discapacidad = model.Discapacidad,
                    TipoDiscapacidad = model.TipoDiscapacidad,
                    Parentesco = model.Parentesco,
                    DatosProtegidos = model.DatosProtegidos,
                    InstitutoPolicial = model.InstitutoPolicial,
                    Numerornd = model.Numerornd,
                    InformePolicial = model.InformePolicial,
                    Relacion = model.Relacion,
                    Edad = model.Edad,
                    DatosFalsos = model.DatosFalsos,
                    DocPoderNotarial = model.DocPoderNotarial,
                    CumpleRequisitoLey = model.CumpleRequisitoLey,
                    DecretoLibertad = model.DecretoLibertad,
                    DispusoLibertad = model.DispusoLibertad,
                    InicioDetenido = model.InicioDetenido,
                    VerR = model.VerR,
                    VerI = model.VerI,
                    Registro = model.Registro,
                    PoblacionAfro = model.PoblacionAfro,
                    RangoEdad = model.RangoEdad,
                    RangoEdadTF = model.RangoEdadTF,
                    PoliciaDetuvo = model.PoliciaDetuvo

                };

                //Se guarda el dato
                _context.Personas.Add(InsertarPersona);
                await _context.SaveChangesAsync();

                //Se guarda el id de la atencion en una variable local
                var idpersona = InsertarPersona.IdPersona;
                idpersonar = InsertarPersona.IdPersona;

                //Insercion en la tabla rap
                RAP InsertarRAP = new RAP
                {
                    PersonaId = idpersona,
                    RAtencionId = model.RAtencionId,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = model.PInicio

                };
                //Se guarda el dato
                _context.RAPs.Add(InsertarRAP);
                await _context.SaveChangesAsync();

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            //Una vez guardado retorna el id del registro de persona que se haya creado
            return Ok(new { idpersonar = idpersonar });
        }

        //Api para el guardado de documentos que hay subido
        // POST: api/DenunciaEnLinea/GuardarDocumentosPersonasD
        [Authorize(Roles = "Administrador,Director,Coordinador,AMPO-AMP,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarDocumentosPersonasD([FromBody] DocumentosPersonasDELViewModel model)
        {
            try
            {
                //En dado caso que el modelo sea invalido retorna un BadRequest
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Insercion en la tabla documentospersona
                DocumentosPesona InsertarDocPersona = new DocumentosPesona
                {
                    PersonaId = model.PersonaId,
                    TipoDocumento = model.TipoDocumento,
                    NombreDocumento = model.NombreDocumento,
                    Descripcion = model.Descripcion,
                    FechaRegistro = model.FechaRegistro,
                    Ruta = model.Ruta,
                    Distrito = model.Distrito,
                    DirSubProc = model.DirSubProc,
                    Agencia = model.Agencia,
                    Usuario = model.Usuario,
                    Puesto = model.Puesto
                };

                //Se guarda el dato
                _context.DocumentosPesonas.Add(InsertarDocPersona);
                await _context.SaveChangesAsync();

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new { status = true, message = "Los documentos de la persona se guardaron correctamente" });
        }

        //Api para guardar la direccion personal de la persona asi como la direccion escucha que es la misma
        // POST: api/DenunciaEnLinea/GuardarDireccionPersonasD
        [Authorize(Roles = "Administrador,Director,Coordinador,AMPO-AMP,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarDireccionPersonasD([FromBody] DireccionPersonalDELViewModel model)
        {
            Guid iddireccionpersonal;
            try
            {
                //En dado caso que el modelo sea invalido retorna un BadRequest
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Insercion en la tabla direccionpersona
                DireccionPersonal InsertarDP = new DireccionPersonal
                {

                    Calle = model.Calle,
                    NoExt = model.NoExt,
                    NoInt = model.NoInt,
                    EntreCalle1 = model.EntreCalle1,
                    EntreCalle2 = model.EntreCalle2,
                    Referencia = model.Referencia,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    Municipio = model.Municipio,
                    Localidad = model.Localidad,
                    CP = model.CP,
                    lat = model.lat,
                    lng = model.lng,
                    PersonaId = model.idPersona

                };

                _context.DireccionPersonals.Add(InsertarDP);

                //Insercion en la tabla direccionescucha
                DireccionEscucha InsertarDE = new DireccionEscucha
                {
                    RAPId = model.RAPId,
                    Calle = model.Calle,
                    NoExt = model.NoExt,
                    NoInt = model.NoInt,
                    EntreCalle1 = model.EntreCalle1,
                    EntreCalle2 = model.EntreCalle2,
                    Referencia = model.Referencia,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    Municipio = model.Municipio,
                    Localidad = model.Localidad,
                    CP = model.CP,
                    lat = model.lat,
                    lng = model.lng,
                };

                _context.DireccionEscuchas.Add(InsertarDE);
                //********************************************************************** 

                //Se guarda el dato
                await _context.SaveChangesAsync();
                //Se guarda el id de la atencion en una variable local
                iddireccionpersonal = InsertarDP.IdDPersonal;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            //Una vez guardado retorna el id de la direccion de persona que se haya creado
            return Ok(new {iddireccionpersonal = iddireccionpersonal});
        }

        //Api para crear el rhecho, el registro mas importante
        // POST: api/DenunciaEnLinea/CrearRhecho
        [Authorize(Roles = "Administrador,Director,Coordinador,AMPO-AMP,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRhecho([FromBody] NucRHechoDELViewModel model)
        {
            Guid idrhecho;
            try
            {
                //En dado caso que el modelo sea invalido retorna un BadRequest
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Insercion en la tabla hecho
                RHecho InsertarRHecho = new RHecho
                {
                    RAtencionId = model.RAtencionId,
                    ModuloServicioId = model.ModuloServicioId,
                    Agenciaid = model.AgenciaId,
                    FechaReporte = model.FechaReporte,
                    Status = false,
                    RBreve = model.RBreve,
                    NDenunciaOficio = model.NDenunciaOficio,
                    Texto = model.Texto,
                    FechaHoraSuceso2 = new DateTime(0002, 1, 1, 0, 02, 0),

                };

                //Se guarda el dato
                _context.RHechoes.Add(InsertarRHecho);
                await _context.SaveChangesAsync();

                //Se guarda el id del rhecho en una variable local
                idrhecho = InsertarRHecho.IdRHecho;

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
            //Una vez guardado retorna el id del hecho que se haya creado
            return Ok(new { idrhecho = idrhecho });
        }

        //En caso de ser una carpeta de investigacion se crea el registro de la nuc
        // POST: api/DenunciaEnLinea/CrearNuc
        [Authorize(Roles = "Administrador,Director,Coordinador,AMPO-AMP,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearNuc([FromBody] NucRHechoDELViewModel model)
        {
            Guid idnuc;
            var ExisteRhecho = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            //Se corrobora que exista el registro del rhecho para no insertar nada en nuc
            if (ExisteRhecho == null)
            {
                return NotFound("No hay registro de ese ID del Hecho");
            }
            else
            {
                try
                {
                    //En dado caso que el modelo sea invalido retorna un BadRequest
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    //Insercion en la tabla nuc
                    Nuc InsertarNUC = new Nuc
                    {
                        Indicador = model.Indicador,
                        DistritoId = model.DistritoId,
                        DConsecutivo = model.DConsecutivo,
                        AgenciaId = model.AgenciaId,
                        AConsecutivo = model.AConsecutivo,
                        Año = model.Año,
                        CveAgencia = model.CveAgencia,
                        CveDistrito = model.CveDistrito,
                        nucg = model.nucg,
                        Etapanuc = "Tramite",
                        StatusNUC = "INVESTIGACIÓN INICIAL",

                    };

                    //Se guarda el dato
                    _context.Nucs.Add(InsertarNUC);
                    await _context.SaveChangesAsync();

                    //Se guarda el id de la nuc en una variable local
                    idnuc = InsertarNUC.idNuc;

                    //Se actualiza registros en la tabla de rhecho para poder vincular el numero de nuic asi como los valores como fecha de elevacion
                    var elevaNUC = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

                    if (elevaNUC == null)
                    {
                        return NotFound();
                    }

                    elevaNUC.NucId = idnuc;
                    elevaNUC.Status = true;
                    elevaNUC.FechaElevaNuc = model.FechaElevaNuc;
                    elevaNUC.FechaElevaNuc2 = model.FechaElevaNuc2;

                    // Actualizamos el registro de ratencion
                    // El Campo statusRegistro simpre  se registra en TRUE hasta que se eleva a NUC o se  queda como RAC
                    var bajaRAC = await _context.RAtencions.FirstOrDefaultAsync(a => a.IdRAtencion == model.RAtencionId);

                    
                    if (bajaRAC == null)
                    {
                        return NotFound();
                    }
                    bajaRAC.StatusRegistro = true;

                    //Se guarda el dato
                    await _context.SaveChangesAsync();
                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    return BadRequest();
                }

                //Una vez guardado retorna el id del hecho que se haya creado
                return Ok(new { idnuc = idnuc });
            }


        }

        //Api para actualizar la fecha y hora de los sucesos
        // PUT: api/DenunciaEnLinea/ActualizarFHS
        [Authorize(Roles = "Administrador,Director,Coordinador,AMPO-AMP,AMPO-AMP Mixto")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarFHS([FromBody] ActualizarFHSDELViewModel model)
        {
            //En dado caso que el modelo sea invalido retorna un BadRequest
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actualizamosFHS = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            if (actualizamosFHS == null)
            {
                return NotFound();
            }

            //Actualizamos la fecha y hora del suceso en rhecho
            actualizamosFHS.FechaHoraSuceso = model.fechaHoraSuceso;
            actualizamosFHS.FechaHoraSuceso2 = model.fechaHoraSuceso;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(new { status = true, message = "La direccionn del delito fue guardada exitosamente" });
        }

        //Api para registrar el lugar de los sucesos
        // POST: api/DenunciaEnLinea/GuardarDireccionDelito
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarDireccionDelito(DireccionesDelitoDELViewModel model)
        {
            Guid iddirecciondelito;

            //En dado caso que el modelo sea invalido retorna un BadRequest
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Insercion en la tabla direcciondelito
                DireccionDelito InsertarDD = new DireccionDelito
                {
                    RHechoId = model.IdRHecho,
                    LugarEspecifico = model.LugarEspecifico,
                    Calle = model.Calle,
                    NoExt = model.NoExt,
                    NoInt = model.NoInt,
                    EntreCalle1 = model.EntreCalle1,
                    EntreCalle2 = model.EntreCalle2,
                    Referencia = model.Referencia,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    Municipio = model.Municipio,
                    Localidad = model.Localidad,
                    CP = model.CP,
                    lat = model.lat,
                    lng = model.lng
                };

                //Se guarda el dato
                _context.DireccionDelitos.Add(InsertarDD);
                await _context.SaveChangesAsync();

                //Se guarda el id de la direccion del delito en una variable local
                iddirecciondelito = InsertarDD.IdDDelito;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            //Una vez guardado retorna el id de la direccion que se haya creado
            return Ok(new {iddirecciondelito = iddirecciondelito});


        }

        //Api para guardar la entrevista inicial
        // POST: api/DenunciaEnLinea/GuardarEntrevista
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarEntrevista(EntrevistaDELViewModel model)

        {
            Guid idampliacion;

            //En dado caso que el modelo sea invalido retorna un BadRequest
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Se abtrae la hora del sistema
            DateTime fecha = System.DateTime.Now;

            //Insercion en la tabla ampdec
            AmpDec InsertarAmpDec = new AmpDec
            {

                HechoId = model.HechoId,
                PersonaId = model.PersonaId,
                Tipo = model.Tipo,
                Edad = model.Edad,
                TipoEA = model.TipoEA,
                HoraS = model.HoraS,
                FechaS = model.FechaS,
                Representante = model.Representante,
                Iniciales = model.Iniciales,
                Acompañantev = model.Acompañantev,
                ParentezcoV = model.ParentezcoV,
                VNombre = model.VNombre,
                VPuesto = model.VPuesto,
                Tidentificacion = model.Tidentificacion,
                NoIdentificacion = model.NoIdentificacion,
                ClasificacionPersona = model.ClasificacionPersona,
                Manifestacion = model.Manifestacion,
                Hechos = model.Hechos,
                TRepresentantes = model.TRepresentantes,
                Fechasys = System.DateTime.Now,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Numerooficio = model.Numerooficio,
                TipoP = model.TipoP,
                DireccionP = model.DireccionP,
                ClasificacionP = model.ClasificacionP,
                CURPA = model.CURPA,
                EntrevistaInicial = model.EntrevistaInicial
            };


            try
            {
                //Se guarda el dato
                _context.AmpDecs.Add(InsertarAmpDec);
                await _context.SaveChangesAsync();

                //Se guarda el id de la entrevista del delito en una variable local
                idampliacion = InsertarAmpDec.idAmpliacion;

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }

            //Una vez guardado retorna el id de la direccion que se haya creado
            return Ok(new { idampliacion = idampliacion });

        }

        // POST: api/DenunciaEnLinea/GuardarDelito
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarDelito(DelitosDELViewModel model)

        {
            Guid idrdh;
            //En dado caso que el modelo sea invalido retorna un BadRequest
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Insercion en la tabla rdh
            RDH InsertarRDH = new RDH
            {
                RHechoId = model.RHechoId,
                DelitoId = model.DelitoId,
                TipoFuero = model.TipoFuero,
                TipoDeclaracion = model.TipoDeclaracion,
                ResultadoDelito = model.ResultadoDelito,
                GraveNoGrave = model.GraveNoGrave,
                IntensionDelito = model.IntensionDelito,
                ViolenciaSinViolencia = model.ViolenciaSinViolencia,
                Equiparado = model.Equiparado,
                Tipo = model.Tipo,
                Concurso = model.Concurso,
                ClasificaOrdenResult = model.ClasificaOrdenResult,
                ArmaFuego = model.ArmaFuego,
                ArmaBlanca = model.ArmaBlanca,
                ConAlgunaParteCuerpo = model.ConAlgunaParteCuerpo,
                ConotroElemento = model.ConotroElemento,
                TipoRobado = model.TipoRobado,
                MontoRobado = model.MontoRobado,
                Observaciones = model.Observaciones,
                Fechasys = System.DateTime.Now,
                Hipotesis = model.Hipotesis,
                DelitoEspecifico = model.DelitoEspecifico,
                TipoViolencia = model.TipoViolencia,
                SubtipoSexual = model.SubtipoSexual,
                TipoInfoDigital = model.TipoInfoDigital,
                MedioDigital = model.MedioDigital,

            };

            _context.RDHs.Add(InsertarRDH);

            try
            {
                //Se guarda el dato
                await _context.SaveChangesAsync();

                //Se guarda el id del delito del delito en una variable local
                idrdh = InsertarRDH.IdRDH;

                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
            //Una vez guardado retorna el id del delito que se haya creado
            return Ok(new { idrdh = idrdh});
        }

        // POST: api/DenunciaEnLinea/CrearRepresentante
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRepresentante([FromBody] CrearRepresentanteDELViewModel model)
        {
            Guid repreID;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

            
                Representante repre = new Representante
                {
                    RHechoId = model.RHechoId,
                    PersonaId = model.representados,
                    Nombres = model.Nombres,
                    ApellidoPa = model.ApellidoPa,
                    ApellidoMa = model.ApellidoMa,
                    FechaNacimiento = model.FechaNacimiento,
                    Sexo = model.Sexo,
                    EntidadFeNacimiento = model.EntidadFeNacimiento,
                    Curp = model.Curp,
                    MedioNotificacion = model.MedioNotificacion,
                    Telefono = model.Telefono,
                    CorreoElectronico = model.CorreoElectronico,
                    Nacionalidad = model.Nacionalidad,
                    Genero = model.Genero,
                    Tipo1 = model.Tipo1,
                    Tipo2 = model.Tipo2,
                    CedulaProfesional = model.CedulaProfesional,
                    Fecha = model.Fecha,
                    UDistrito = model.UDistrito,
                    USubproc = model.USubproc,
                    UAgencia = model.UAgencia,
                    Usuario = model.Usuario,
                    UPuesto = model.UPuesto,
                    UModulo = model.UModulo,
                    Fechasys = System.DateTime.Now,
                    Calle = model.Calle,
                    NoInt = model.NoInt,
                    NoExt = model.NoExt,
                    EntreCalle1 = model.EntreCalle1,
                    EntreCalle2 = model.EntreCalle2,
                    Referencia = model.Referencia,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    Municipio = model.Municipio,
                    Localidad = model.Localidad,
                    CP = model.CP,
                    lat = model.lat,
                    lng = model.lng,
                    ArticulosPenales = model.ArticulosPenales
                };

                _context.Representantes.Add(repre);
                await _context.SaveChangesAsync();

                repreID = repre.IdRepresentante;

                DocsRepresentantes docrepre = new DocsRepresentantes
                {


                    RepresentanteId = repreID,
                    TipoDocumento = model.TipoDocumento,
                    NombreDocumento = model.NombreDocumento,
                    Ruta = model.Ruta,
                    Fecha = model.Fecha,
                    UDistrito = model.UDistrito,
                    USubproc = model.USubproc,
                    UAgencia = model.UAgencia,
                    Usuario = model.Usuario,
                    UPuesto = model.UPuesto,
                    UModulo = model.UModulo,
                    Fechasys = System.DateTime.Now,


                };

                _context.DocsRepresentantes.Add(docrepre);
                await _context.SaveChangesAsync();

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { idrepresentante = repreID });
        }

        // POST: api/DenunciaEnLinea/CrearArchivo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearArchivo([FromBody] ArchivosDELViewModel model)
        {
            Guid archivoId;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Archivos archivos = new Archivos
                {
                    RHechoId = model.RHechoId,
                    TipoDocumento = model.TipoDocumento,
                    NombreDocumento = model.NombreDocumento,
                    DescripcionDocumento = model.DescripcionDocumento,
                    Ruta = model.Ruta,
                    Fecha = model.Fecha,
                    UDistrito = model.UDistrito,
                    USubproc = model.USubproc,
                    UAgencia = model.UAgencia,
                    Usuario = model.Usuario,
                    UPuesto = model.UPuesto,
                    UModulo = model.UModulo,
                    Fechasys = System.DateTime.Now,
                };

                _context.Archivos.Add(archivos);
                await _context.SaveChangesAsync();
                archivoId = archivos.IdArchivos;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { archivoId = archivoId});
        }

        // POST: api/DenunciaEnLinea/CrearPI
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPI(CrearDiligenciaForaneaDELViewModel model, Guid iddiligenciaforanea)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var EI = await _context.HistorialCarpetas
            .Where(a => a.RHechoId == model.rHechoId)
            .Where(a => a.Detalle == "VINCULACION APROCESO")
            .OrderByDescending(a => a.Fechasys)
            .FirstOrDefaultAsync();

            Boolean eic = false;

            //Boolean enviada = false;

            if (EI != null) eic = true;
            else eic = false;

            DateTime fecha = System.DateTime.Now;

            var noSolicitud = await _context.RDiligenciasForaneas
            .Where(a => a.AgenciaRecibe == model.DistritoRecibe)
            .ToListAsync();

            int noSol = noSolicitud.Count() + 1;


            RDiligenciasForaneas InsertarRD = new RDiligenciasForaneas
            {

                FechaSolicitud = model.FechaSolicitud,
                Dirigidoa = model.Dirigidoa,
                DirSubPro = model.DirSubPro,
                EmitidoPor = model.EmitidoPor,
                uDirSubPro = model.uDirSubPro,
                UPuesto = model.UPuesto,
                StatusRespuesta = model.StatusRespuesta,
                Servicio = model.Servicio,
                Especificaciones = model.Especificaciones,
                rHechoId = model.rHechoId,
                ASPId = model.ASPId,
                Prioridad = model.Prioridad,
                FechaSys = fecha,
                Modulo = model.Modulo,
                Agencia = model.Agencia,
                Respuestas = model.Respuestas,
                NUC = model.NUC,
                Textofinal = model.Textofinal,
                NumeroOficio = model.NumeroOficio,
                NodeSolicitud = (model.NumeroDistritoOrigen + "-" + model.NumeroDistrito + "/" + noSol),
                NumeroDistrito = model.NumeroDistrito,
                Lat = model.Lat,
                Lng = model.Lng,
                Dirigido = model.Dirigido,
                RecibidoF = false,
                FechaRecibidoF = fecha,
                AgenciaEnvia = model.AgenciaEnvia,
                AgenciaRecibe = model.DistritoRecibe,
                EtapaInicial = eic,
                EnvioExitosoTF = true,


            };

            _context.RDiligenciasForaneas.Add(InsertarRD);
            // AGREGAR EN LA BD DEL DISTRITO ORIGINAL
            await _context.SaveChangesAsync();

            iddiligenciaforanea = InsertarRD.IdRDiligenciasForaneas;
            try
            {
                //AGREGAR EN LA BD DEL DISTRITO DESTINO
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.DistritoRecibe.ToString().ToUpper())).Options;
                using (var ctxDestino = new DbContextSIIGPP(options))
                {
                    RDiligenciasForaneas InsertarRDForaneo = InsertarRD;
                    ctxDestino.RDiligenciasForaneas.Add(InsertarRDForaneo);
                    await ctxDestino.SaveChangesAsync();
                }

                return Ok(new { iddili = iddiligenciaforanea, enviada = true });
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {

                var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == iddiligenciaforanea);

                if (rdili == null)
                {
                    return NotFound();
                }

                rdili.EnvioExitosoTF = false;

                await _context.SaveChangesAsync();

                return Ok(new { iddili = iddiligenciaforanea, enviada = false });
            }

        }

        // POST: api/DenunciaEnLinea/CrearPI
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearDiligenciaForanea(CrearDiligenciaForaneaDELViewModel model, Guid iddiligenciaforanea)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            var noSolicitud = await _context.RDiligenciasForaneas
            .Where(a => a.AgenciaRecibe == model.DistritoRecibe)
            .ToListAsync();

            int noSol = noSolicitud.Count() + 1;

            RDiligenciasForaneas InsertarRD = new RDiligenciasForaneas
            {

                FechaSolicitud = model.FechaSolicitud,
                Dirigidoa = model.Dirigidoa,
                DirSubPro = model.DirSubPro,
                EmitidoPor = model.EmitidoPor,
                uDirSubPro = model.uDirSubPro,
                UPuesto = model.UPuesto,
                StatusRespuesta = "Solicitado",
                Servicio = model.Servicio,
                Especificaciones = model.Especificaciones,
                rHechoId = model.rHechoId,
                ASPId = model.ASPId,
                Prioridad = model.Prioridad,
                FechaSys = fecha,
                Modulo = model.Modulo,
                Agencia = model.Agencia,
                Respuestas = " ",
                NUC = model.NUC,
                Textofinal = model.Textofinal,
                NumeroOficio = model.NumeroOficio,
                NodeSolicitud = (model.NumeroDistritoOrigen + "-" + model.NumeroDistrito + "/" + noSol),
                NumeroDistrito = model.NumeroDistrito,
                Lat = model.Lat,
                Lng = model.Lng,
                Dirigido = model.Dirigido,
                RecibidoF = false,
                FechaRecibidoF = fecha,
                AgenciaEnvia = model.AgenciaEnvia,
                AgenciaRecibe = model.DistritoRecibe,
                EtapaInicial = false,
                EnvioExitosoTF = true,


            };

            _context.RDiligenciasForaneas.Add(InsertarRD);
            // AGREGAR EN LA BD DEL DISTRITO ORIGINAL
            await _context.SaveChangesAsync();

            iddiligenciaforanea = InsertarRD.IdRDiligenciasForaneas;
            try
            {
                //AGREGAR EN LA BD DEL DISTRITO DESTINO
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.DistritoRecibe.ToString().ToUpper())).Options;
                using (var ctxDestino = new DbContextSIIGPP(options))
                {
                    RDiligenciasForaneas InsertarRDForaneo = InsertarRD;
                    ctxDestino.RDiligenciasForaneas.Add(InsertarRDForaneo);
                    await ctxDestino.SaveChangesAsync();
                }

                return Ok(new { datos = new[] { new { iddili = iddiligenciaforanea, status = true } } });
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {

                var rdili = await _context.RDiligenciasForaneas.FirstOrDefaultAsync(a => a.IdRDiligenciasForaneas == iddiligenciaforanea);

                if (rdili == null)
                {
                    return NotFound();
                }

                rdili.EnvioExitosoTF = false;

                await _context.SaveChangesAsync();

                return Ok(new { datos = new[] { new { iddili = iddiligenciaforanea, status = false } } });
            }

        }

        // POST: api/DenunciaEnLinea/CrearMedidasProteccion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearMedidasProteccion ([FromBody] CrearMedidasProteccionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            Medidasproteccion mdp = new Medidasproteccion
            {
                RHechoId = model.RHechoId,
                Victima = model.Victima,
                Imputado = model.Imputado,
                Lugar = model.Lugar,
                Fechahora = model.Fechahora,
                Agente = model.Agente,
                Nuc = model.Nuc,
                Delito = model.Delito,
                Narrativa = model.Narrativa,
                Domicilio = "",
                Telefono = "",
                MedidaProtecion = model.MedidaProtecion,
                Duracion = 0,
                Institucionejec = model.Institucionejec,
                Agencia = model.Agencia,
                Nomedidas = model.Nomedidas,
                Destinatarion = "",
                Domicilion ="",
                FInicio = model.FInicio,
                FVencimiento = model.FVencimiento,
                Ampliacion = false,
                FAmpliacion = "na",
                FterminoAm = "na",
                Ratificacion = "na",
                Distrito = model.Distrito,
                Subproc = model.Subproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                Puesto = model.Puesto,
                Fechasys = fecha,
                Textofinal = "",
                Textofinal2 = "",
                Textofinaldetalle = model.Textofinaldetalle,
                Detalleactivo = false,
                Modulo = model.Modulo,
                NumeroOficio = model.NumeroOficio,
                NumeroOficioN = "",
                //NUEVAS COLUMNAS DE FCAT018C
                PetiOfiMPBool = model.PetiOfiMPBool,
                PetiOfiMPVar = model.PetiOfiMPVar,
                MedidasExtraTF = model.MedidasExtraTF,
                MedidasExtra = model.MedidasExtra

            };

            _context.Medidasproteccions.Add(mdp);

            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { idmedidaprotecion = mdp.IdMProteccion });
        }

        // POST: api/DenunciaEnLinea/CrearNomedida
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearNoMedidasP([FromBody] CrearNoMedidasPViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NoMedidasProteccion mdp = new NoMedidasProteccion
            {
                Clave = model.Clave,
                Descripcion = model.Descripcion,
                MedidasproteccionId = model.MedidasproteccionId

            };

            _context.NoMedidasProteccions.Add(mdp);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new { result = "La informacion se guardo correctamente"});
        }

        // POST: api/DenunciaEnLinea/CrearActosInvestigacion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearActosInvestigacion (CrearActoInvestigacionViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var noSolicitud = await _context.RActoInvestigacions
                .Where(a => a.NumeroDistrito == model.NumeroDistrito)
                .ToListAsync();

                int noSol = noSolicitud.Count() + 1;

                RActoInvestigacion InsertarRD = new RActoInvestigacion
                {

                    RHechoId = model.RHechoId,
                    FechaSolicitud = model.FechaSolicitud,
                    Status = "Solicitado",
                    Servicios = model.Servicios,
                    Especificaciones ="",
                    Cdetenido = false,
                    Respuestas = "",
                    NUC = model.NUC,
                    Textofinal = model.Textofinal,
                    FechaSys = System.DateTime.Now,
                    UDirSubPro = model.UDirSubPro,
                    UUsuario = model.UUsuario,
                    UPuesto = model.UPuesto,
                    UModulo = model.UModulo,
                    UAgencia = model.UAgencia,
                    NumeroOficio = model.NumeroOficio,
                    NodeSolicitud = noSol.ToString(),
                    NumeroDistrito = model.NumeroDistrito,
                    EtapaInicial = false,
                    DSPDEstino = model.DSPDEstino,
                    DistritoId = model.DistritoId
                };

                _context.RActoInvestigacions.Add(InsertarRD);
                await _context.SaveChangesAsync();
                Guid idActoI = InsertarRD.IdRActosInvestigacion ;

                ActosInDetalle ActoDetalle = new ActosInDetalle();

                string[] detalleActo = model.ActoDetalleC.Split(",");

                for (int i = 0; i < detalleActo.Length; i++)
                {
                    ActoDetalle = new ActosInDetalle
                    {
                        RActosInvestigacionId = idActoI,
                        Servicio = detalleActo[i],
                        ServicioNM = "",
                        Status = "Solicitado",
                        TextoFinal ="",
                        FechaRecibido = "",
                        FechaAceptado = "",
                        FechaFinalizado = "",
                        FechaEntregado = "",
                        UltmimoStatus = System.DateTime.Now,
                        Respuesta = "",
                        Conclusion = "",
                        FechaSys = System.DateTime.Now,
                        UDirSubPro = "",
                        UUsuario = "",
                        UPuesto = "",
                        UModulo = "",
                        UAgencia = ""

                    };

                    _context.ActosInDetalles.Add(ActoDetalle);

                    await _context.SaveChangesAsync();
                }

                return Ok(new {idActoI = idActoI,respuesta = "Informacion guardada correctamente"});

                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }


        }

        //------------------------------------------------------------INICIAN APIS PARA EL LISTADO DE CATALOGOS ------------------------------------------------

        //*************************************************CATALOGOS PARA LA CREACION DE DILIGENCIAS A SERVICIOS PERICIALES*************************************

        //Lista distritos a excepcion del que se envia en el get
        // GET: api/DenunciaEnLinea/ListarSinPropiodistrito
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IEnumerable<DistritoDELViewModel>> ListarSinPropiodistrito([FromRoute] Guid iddistrito)
        {
            var distrito = await _context.Distritos
                .Where(a => a.IdDistrito != iddistrito)
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            return distrito.Select(a => new DistritoDELViewModel
            {
                IdDistrito = a.IdDistrito,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Clave = a.Clave,
            });

        }

        //Lista distritos a excepcion del que se envia en el get pero retorna arreglo
        // GET: api/DenunciaEnLinea/ListarSinPropiodistritoArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IActionResult> ListarSinPropiodistritoArray([FromRoute] Guid iddistrito)
        {
            var distrito = await _context.Distritos
                .Where(a => a.IdDistrito != iddistrito)
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            var datos = distrito.Select(a => new DistritoDELViewModel
            {
                IdDistrito = a.IdDistrito,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Clave = a.Clave,
            });

            return Ok(new { datos = datos });
        }

        //Lista la subdireccion de Servicios periciales del distrito que envies en el get
        // GET: api/DenunciaEnLinea/ListarDistritoySP
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IActionResult> ListarDistritoySP([FromRoute] Guid distritoId)
        {
            var a = await _context.DSPs
                    .Include(x => x.Distrito)
                    .Where(x => x.DistritoId == distritoId)
                    .Where(x => x.Clave == "DGSP")
                    .FirstOrDefaultAsync();


            if (a == null)
            {
                return Ok(new { idDSP = 0 });

            }
            return Ok(new DSPDELViewModel
            {
                /*********************************************/

                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Clave = a.Clave,
            });

        }

        //Lista la subdireccion de Servicios periciales del distrito que envies en el get pero retorna arreglo
        // GET: api/DenunciaEnLinea/ListarDistritoySPArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{distritoId}")]
        public async Task<IActionResult> ListarDistritoySPArray([FromRoute] Guid distritoId)
        {
            var DistritoSP = await _context.DSPs
                    .Include(x => x.Distrito)
                    .Where(x => x.DistritoId == distritoId)
                    .Where(x => x.Clave == "DGSP")
                    .ToListAsync();


            if (DistritoSP == null)
            {
                return Ok(new { idDSP = 0 });

            }

            var datos = DistritoSP.Select(a => new DSPDELViewModel
            {
                /*********************************************/

                IdDSP = a.IdDSP,
                NombreSubDir = a.NombreSubDir,
                Responsable = a.Responsable,
                Clave = a.Clave,
            });

            return Ok(new { datos = datos });

        }

        //Lista la agencias
        // GET: api/DenunciaEnLinea/ListarporDSP
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{Iddsp}")]
        public async Task<IEnumerable<AgenciaDELViewModel>> ListarporDSP([FromRoute] Guid Iddsp)
        {
            var agencia = await _context
                .Agencias.Include(a => a.DSP)
                .Where(a => a.DSPId == Iddsp)
                .Include(a => a.DSP.Distrito)
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            return agencia.Select(a => new AgenciaDELViewModel
            {
                IdAgencia = a.IdAgencia,
                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Clave = a.Clave,
            });

        }

        //Lista la agencias de servicios periciales pero en arreglo
        // GET: api/DenunciaEnLinea/ListarporDSPArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{Iddsp}")]
        public async Task<IActionResult> ListarporDSPArray([FromRoute] Guid Iddsp)
        {
            var agencia = await _context
                .Agencias.Include(a => a.DSP)
                .Where(a => a.DSPId == Iddsp)
                .Include(a => a.DSP.Distrito)
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            var datos = agencia.Select(a => new AgenciaDELViewModel
            {
                IdAgencia = a.IdAgencia,
                DSPId = a.DSPId,
                NombreDirSub = a.DSP.NombreSubDir,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Clave = a.Clave,
            });

            return Ok(new { datos = datos });


        }

        //Lista los servicios periciales de determinada agencia
        // GET: api/DenunciaEnLinea/ListarporIdagencia
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{Idagencia}")]
        public async Task<IEnumerable<AsignarServicioDELViewModel>> ListarporIdagencia([FromRoute] Guid Idagencia)
        {
            var asp = await _context.ASPs.Include(a => a.Agencia.DSP)
                .Where(a => a.AgenciaId == Idagencia)
                .Include(a => a.Agencia)
                .Include(a => a.ServicioPericial)
                .Include(a => a.Agencia.DSP.Distrito)
                .ToListAsync();

            return asp.Select(a => new AsignarServicioDELViewModel
            {
                IdASP = a.IdASP,
                DSPId = a.Agencia.DSPId,
                AgenciaId = a.AgenciaId,
                NombreAgencia = a.Agencia.Nombre,
                NombreDirSub = a.Agencia.DSP.NombreSubDir,
                ServicioPericialId = a.ServicioPericialId,
                NombreServicio = a.ServicioPericial.Servicio,

            });

        }

        //Lista los servicios periciales de determinada agencia pero en arreglo
        // GET: api/DenunciaEnLinea/ListarporIdagenciaArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{Idagencia}")]
        public async Task<ActionResult> ListarporIdagenciaArray([FromRoute] Guid Idagencia)
        {
            var asp = await _context.ASPs.Include(a => a.Agencia.DSP)
                .Where(a => a.AgenciaId == Idagencia)
                .Include(a => a.Agencia)
                .Include(a => a.ServicioPericial)
                .Include(a => a.Agencia.DSP.Distrito)
                .ToListAsync();

            var datos = asp.Select(a => new AsignarServicioDELViewModel
            {
                IdASP = a.IdASP,
                DSPId = a.Agencia.DSPId,
                AgenciaId = a.AgenciaId,
                NombreAgencia = a.Agencia.Nombre,
                NombreDirSub = a.Agencia.DSP.NombreSubDir,
                ServicioPericialId = a.ServicioPericialId,
                NombreServicio = a.ServicioPericial.Servicio,
            });

            return Ok(new { datos = datos });

        }

        //Lista la informacion del servicio pericial
        // GET: api/DenunciaEnLinea/FiltrarStatusSP
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{servicioPericialId}/{agenciaId}")]
        public async Task<IActionResult> FiltrarStatusSP([FromRoute] Guid servicioPericialId, Guid agenciaId)

        {
            var asp = await _context.ASPs
                                .Include(a => a.Agencia.DSP)
                                .Include(a => a.Agencia)
                                .Include(a => a.ServicioPericial)
                                .Where(a => a.AgenciaId == agenciaId)
                                .Where(x => x.ServicioPericialId == servicioPericialId)
                                .FirstOrDefaultAsync();

            if (asp == null)
            {
                return NotFound();
            }
            return Ok(new ListarPorFiltroSPDELViewModel
            {
                IdASP = asp.IdASP,
                Responsable = asp.Agencia.DSP.Responsable,
                ServicioPericialId = asp.ServicioPericialId,
                NombreServicio = asp.ServicioPericial.Servicio,
                Descripcion = asp.ServicioPericial.Descripcion,
                Requisitos = asp.ServicioPericial.Requisitos,
                Materia = asp.ServicioPericial.EnMateriaDe,
                AtencionVictimas = asp.ServicioPericial.AtencionVictimas,
            });
        }

        //Lista la informacion del servicio pericial pero en arreglo
        // GET: api/DenunciaEnLinea/FiltrarStatusSPArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{servicioPericialId}/{agenciaId}")]
        public async Task<IActionResult> FiltrarStatusSPArray([FromRoute] Guid servicioPericialId, Guid agenciaId)

        {
            var asp = await _context.ASPs
                                .Include(a => a.Agencia.DSP)
                                .Include(a => a.Agencia)
                                .Include(a => a.ServicioPericial)
                                .Where(a => a.AgenciaId == agenciaId)
                                .Where(x => x.ServicioPericialId == servicioPericialId)
                                .ToListAsync();

            if (asp == null)
            {
                return NotFound();
            }

            var datos = asp.Select(a => new ListarPorFiltroSPDELViewModel
            {
                IdASP = a.IdASP,
                Responsable = a.Agencia.DSP.Responsable,
                ServicioPericialId = a.ServicioPericialId,
                NombreServicio = a.ServicioPericial.Servicio,
                Descripcion = a.ServicioPericial.Descripcion,
                Requisitos = a.ServicioPericial.Requisitos,
                Materia = a.ServicioPericial.EnMateriaDe,
                AtencionVictimas = a.ServicioPericial.AtencionVictimas,
            });

            return Ok (new { datos = datos});
        }

        // GET: api/DenunciaEnLinea/ObtenernumeroMaximoporDistrito
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{Ndistrito}/{idDistrito}")]
        public async Task<IActionResult> ObtenernumeroMaximoporDistrito([FromRoute] String Ndistrito, [FromRoute] Guid idDistrito)
        {
            try
            {

                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + idDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var da = await ctx.RDiligenciasForaneas
                    .OrderByDescending(x => Int32.Parse(x.NodeSolicitud))
                    .Where(x => x.NumeroDistrito == Ndistrito).Where(x => x.NodeSolicitud != null)
                    .FirstOrDefaultAsync();
                    if (da == null)
                    {
                        return Ok(new { NumeroMaximo = 0 });
                    }

                    return Ok(new DatosExtrasDELViewModel
                    {
                        NumeroMaximo = da.NodeSolicitud
                    });
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

        }

        //Lista instituciones policiales
        // GET: api/DenunciaEnLinea/ListarInstitucionesP
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarInstitucionesPViewModel>> ListarInstitucionesP()
        {
            var Instituciones = await _context.Instituciones.ToListAsync();

            return Instituciones.Select(a => new ListarInstitucionesPViewModel
            {
                IdInstitucion = a.IdInstitucion,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Contacto = a.Contacto,
                Telefono = a.Telefono
            });

        }

        //Lista instituciones policiales pero en arreglo
        // GET: api/DenunciaEnLinea/ListarInstitucionesPArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]")]
        public async Task<ActionResult<ListarInstitucionesPViewModel>> ListarInstitucionesPArray()
        {
            var Instituciones = await _context.Instituciones.ToListAsync();

            var datos =  Instituciones.Select(a => new ListarInstitucionesPViewModel
            {
                IdInstitucion = a.IdInstitucion,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Contacto = a.Contacto,
                Telefono = a.Telefono
            });

            return Ok(new { datos = datos });

        }


        //Lista medidas de proteccion
        // GET: api/DenunciaEnLinea/ListarMedidasP
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarMedidasPViewModel>> ListarMedidasP ()
        {
            var db = await _context.MedidasProteccionCs.ToListAsync();

            return db.Select(a => new ListarMedidasPViewModel
            {
                IdMedidasProteccionC = a.IdMedidasProteccionC,
                Clasificacion = a.Clasificacion,
                Clave = a.Clave,
                Descripcion = a.Descripcion
            });

        }

        //Lista medidas de proteccion pero en arreglo
        // GET: api/DenunciaEnLinea/ListarMedidasPArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]")]
        public async Task<ActionResult<ListarMedidasPViewModel>> ListarMedidasPArray()
        {
            var db = await _context.MedidasProteccionCs.ToListAsync();

            var datos =  db.Select(a => new ListarMedidasPViewModel
            {
                IdMedidasProteccionC = a.IdMedidasProteccionC,
                Clasificacion = a.Clasificacion,
                Clave = a.Clave,
                Descripcion = a.Descripcion
            });

            return Ok (new { datos = datos });

        }

        //Lista actos de investigacion
        // GET: api/DenunciaEnLinea/ListarActosInvestigacion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{TFAutorizacion}")]
        public async Task<IEnumerable<ActosInvestigacionDELViewModel>> ListarActosInvestigacion([FromRoute] Boolean TFAutorizacion)
        {
            var ai = await _context.ActoInvestigacions
                .Where(a => a.RAutorizacion == TFAutorizacion)
                .OrderBy(a => a.Nomenclatura)
                .ToListAsync();

            return ai.Select(a => new ActosInvestigacionDELViewModel
            {
                IdActonvestigacion = a.IdActonvestigacion,
                Nombre = a.Nombre,
                Nomenclatura = a.Nomenclatura,
                Descripcion = a.Descripcion,
                RAutorizacion = a.RAutorizacion
            });

        }

        //Lista actos de investigacion pero en arreglo
        // GET: api/DenunciaEnLinea/ListarActosInvestigacionArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{TFAutorizacion}")]
        public async Task<ActionResult <ActosInvestigacionDELViewModel>> ListarActosInvestigacionArray([FromRoute] Boolean TFAutorizacion)
        {
            var ai = await _context.ActoInvestigacions
                .Where(a => a.RAutorizacion == TFAutorizacion)
                .OrderBy(a => a.Nomenclatura)
                .ToListAsync();

            var datos = ai.Select(a => new ActosInvestigacionDELViewModel
            {
                IdActonvestigacion = a.IdActonvestigacion,
                Nombre = a.Nombre,
                Nomenclatura = a.Nomenclatura,
                Descripcion = a.Descripcion,
                RAutorizacion = a.RAutorizacion
            });

            return Ok(new { datos = datos });

        }

        //Lista poliasa investigadora
        // GET: api/DenunciaEnLinea/ListarPoliciaInvestigadora
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{idmodulo}")]
        public async Task<IEnumerable<ListarPoliciaInvestigadoraDELViewModel>> ListarPoliciaInvestigadora([FromRoute] Guid idmodulo)
        {
            var sppi = await _context.SPPiligaciones
                .Include(a => a.DSP)
                .Include(a => a.PanelControl)
                .Include(a => a.DSP.Distrito)
                .Where(a => a.Direccion == true || a.PanelControl.Clave == idmodulo)
                .Where(a => a.DSP.Tipo == "PI")
                .ToListAsync();

            return sppi.Select(a => new ListarPoliciaInvestigadoraDELViewModel
            {
                IdSPPiligaciones = a.IdSPPiligaciones,
                PanelControlId = a.PanelControlId,
                DSPId = a.DSPId,
                Direccion = a.Direccion,
                Dspn = a.DSP.NombreSubDir,
                Paneln = a.PanelControl.Nombre,
                DistritoId = a.DSP.DistritoId
            });

        }

        //Lista poliasa investigadora pero en arreglo
        // GET: api/DenunciaEnLinea/ListarPoliciaInvestigadoraArray
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{idmodulo}")]
        public async Task<ActionResult<ListarPoliciaInvestigadoraDELViewModel>> ListarPoliciaInvestigadoraArray([FromRoute] Guid idmodulo)
        {
            var sppi = await _context.SPPiligaciones
                .Include(a => a.DSP)
                .Include(a => a.PanelControl)
                .Include(a => a.DSP.Distrito)
                .Where(a => a.Direccion == true || a.PanelControl.Clave == idmodulo)
                .Where(a => a.DSP.Tipo == "PI")
                .ToListAsync();

            var datos = sppi.Select(a => new ListarPoliciaInvestigadoraDELViewModel
            {
                IdSPPiligaciones = a.IdSPPiligaciones,
                PanelControlId = a.PanelControlId,
                DSPId = a.DSPId,
                Direccion = a.Direccion,
                Dspn = a.DSP.NombreSubDir,
                Paneln = a.PanelControl.Nombre,
                DistritoId = a.DSP.DistritoId
            });

            return Ok (new {datos = datos });

        }

        //**************************************************CATALOGOS DE ES6RUCTURA DE PROCURADURIA**************************************

        // GET: api/DenunciaEnLinea/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]")]
        public async Task<ActionResult<DistritoDELViewModel>> ListarDistritos()
        {
            var distrito = await _context.Distritos
                .OrderBy(a => a.Nombre)
                .ToListAsync();

            var datos = distrito.Select(a => new DistritoDELViewModel
            {
                IdDistrito = a.IdDistrito,
                Nombre = a.Nombre,
                Direccion = a.Direccion,
                Telefono = a.Telefono,
                Clave = a.Clave,

            });

            return Ok(new { datos = datos });

        }

        // GET: api/DenunciaEnLinea/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ListarDSPPorDistrito([FromRoute] Guid id)
        {
            try
            {
                var dsp = await _context.DSPs
                    .Where(x => x.DistritoId == id)
                    .Where(x => x.StatusInicioCarpeta == true)
                    .Where(x => x.StatusDSP == true)
                    .ToListAsync();

                if (dsp.Count == 0)
                {
                    return Ok(new { datos = new[] { new { aviso = "El distrito no cuenta con subdirección que maneje carpetas de investigación", status = false } } });
                }

                var datos = dsp.Select(a => new DSPDELViewModel
                {
                    IdDSP = a.IdDSP,
                    NombreSubDir = a.NombreSubDir,
                    Responsable = a.Responsable,
                    Clave = a.Clave,
                });

                return Ok(new { datos = datos });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/DenunciaEnLinea/ListarcarpetasporDsP
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,AMPO-IL")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ListarAgenciaPorDSP([FromRoute] Guid id)
        {
            try
            {
                var agencia = await _context.ModuloServicios
                    .Include(x => x.Agencia)
                    .Where(x => x.Agencia.Activa == true)
                    .Where(x => x.Agencia.DSPId == id)
                    .Where(x => x.Condicion == true)
                    .Where(x => x.Tipo == "Recepción")
                    .Where(x => x.Agencia.TipoServicio == "Servicio externo con inicio de carpeta")
                    .ToListAsync();

                if (agencia.Count == 0)
                {
                    return Ok(new { datos = new[] { new { aviso = "La subdirección no cuenta con agencia con recepción de carpetas", status = false } } });
                }

                var datos = agencia.Select(a => new AgenciaDELRViewModel
                {
                    IdAgencia = a.Agencia.IdAgencia,
                    Nombre = a.Agencia.Nombre
                });

                return Ok(new { datos = datos });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }

        //------------------------------------------------------------TERMINAN APIS PARA EL LISTADO DE CATALOGOS ------------------------------------------------

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Ampliacion;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.Ampliacion;
using SIIGPP.Entidades.M_Administracion;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_Cat.MedAfiliacion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmpDecsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public AmpDecsController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        //se modifico el tipo de persona
        // GET: api/AmpDecs/Listar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador, AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
         
        public async Task<IEnumerable<AmpDecViewModel>> Listar([FromRoute] Guid rHechoId)
        {
            var ad = await _context.AmpDecs
                            .Include(a => a.Persona)
                            .OrderByDescending(a => a.Fechasys)
                            .Where(x => x.HechoId==  rHechoId).ToListAsync();

           
            return ad.Select(a => new AmpDecViewModel

            {
                /*********************************************/
 
                idAmpliacion = a.idAmpliacion,
                HechoId = a.HechoId,
                PersonaId = a.PersonaId,
                NombrePersona = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Tipo = a.Tipo,
                TRepresentantes = a.TRepresentantes,
                Edad = a.Edad,
                TipoEA = a.TipoEA,
                HoraS = a.HoraS,
                FechaS = a.FechaS,
                Representante = a.Representante,
                Iniciales = a.Iniciales,
                Acompañantev = a.Acompañantev,
                ParentezcoV = a.ParentezcoV,
                VNombre = a.VNombre,
                VPuesto =a.VPuesto,
                Tidentificacion=  a.Tidentificacion,
                NoIdentificacion  = a.NoIdentificacion,
                Fechasys = a.Fechasys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Numerooficio = a.Numerooficio,
                ClasificacionPersona = a.ClasificacionPersona,
                Manifestacion = a.Manifestacion,
                Hechos = a.Hechos,
                Alias = a.Persona.Alias,
                Curp = a.Persona.CURP,
                DocIdentificacion = a.Persona.DocIdentificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Tel = a.Persona.Telefono1,
                Email = a.Persona.Correo,
                Genero = a.Persona.Genero,
                Sexo = a.Persona.Sexo,
                FechaNacimiento = a.Persona.FechaNacimiento,
                Medionotificacion = a.Persona.Medionotificacion,
                Ocupacion = a.Persona.Ocupacion,
                Nivelestudio = a.Persona.NivelEstudio,
                Religion = a.Persona.Religion,
                Lengua = a.Persona.Lengua,
                Estadocivil = a.Persona.EstadoCivil,
                Tipodiscapacidad = a.Persona.TipoDiscapacidad,
                TipoP = a.TipoP,
                DireccionP = a.DireccionP,
                ClasificacionP = a.ClasificacionP,
                CURPA = a.CURPA,
                EntrevistaInicial = a.EntrevistaInicial,
                RazonSocial = a.Persona.RazonSocial,
                RFC = a.Persona.RFC,
                DocPoderNotarial = a.Persona.DocPoderNotarial,
                DatosProtegidos = a.Persona.DatosProtegidos

            });

        }

        // GET: api/AmpDecs/Eliminar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultaEntrevista = await _context.AmpDecs.Where(a => a.idAmpliacion == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaEntrevista == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ninguna entrevista con la información enviada" });
                }
                else
                {
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("LOG")).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {
                        Guid gLog = Guid.NewGuid();
                        DateTime fecha = System.DateTime.Now;
                        LogAdmon laRegistro = new LogAdmon
                        {
                            IdLogAdmon = gLog,
                            UsuarioId = model.usuario,
                            Tabla = model.tabla,
                            FechaMov = fecha,
                            RegistroId = model.infoBorrado.registroId,
                            SolicitanteId = model.solicitante,
                            RazonMov = model.razon,
                            MovimientoId = new Guid("46648A93-5115-490E-88A9-D60ACFE4FF78")
                        };
                    
                    ctx.Add(laRegistro);
                    LogAmpDec ampDec = new LogAmpDec
                        {
                            LogAdmonId=gLog,
                            idAmpliacion=consultaEntrevista.idAmpliacion,
                            HechoId=consultaEntrevista.HechoId,
                            PersonaId=consultaEntrevista.PersonaId,
                            Tipo = consultaEntrevista.Tipo,
                            ClasificacionPersona = consultaEntrevista.ClasificacionPersona,
                            Manifestacion = consultaEntrevista.Manifestacion,
                            Hechos = consultaEntrevista.Hechos,
                            TRepresentantes = consultaEntrevista.TRepresentantes,
                            Edad = consultaEntrevista.Edad,
                            Fechasys = consultaEntrevista.Fechasys,
                            UDistrito = consultaEntrevista.UDistrito,
                            USubproc = consultaEntrevista.USubproc,
                            UAgencia = consultaEntrevista.UAgencia,
                            Usuario = consultaEntrevista.Usuario,
                            UPuesto = consultaEntrevista.UPuesto,
                            UModulo = consultaEntrevista.UModulo,
                            Numerooficio=consultaEntrevista.Numerooficio,
                            TipoEA = consultaEntrevista.TipoEA,
                            HoraS = consultaEntrevista.HoraS,
                            FechaS = consultaEntrevista.FechaS,
                            Representante = consultaEntrevista.Representante,
                            Iniciales = consultaEntrevista.Iniciales,
                            Acompanantev = consultaEntrevista.Acompañantev,
                            ParentezcoV = consultaEntrevista.ParentezcoV,
                            VNombre = consultaEntrevista.VNombre,
                            VPuesto = consultaEntrevista.VPuesto,
                            Tidentificacion = consultaEntrevista.Tidentificacion,
                            NoIdentificacion = consultaEntrevista.NoIdentificacion,
                            ClasificacionP = consultaEntrevista.ClasificacionP,
                            TipoP = consultaEntrevista.TipoP,
                            DireccionP = consultaEntrevista.DireccionP,
                            CURPA = consultaEntrevista.CURPA,
                            EntrevistaInicial = consultaEntrevista.EntrevistaInicial
                    };
                        ctx.Add(ampDec);
                        _context.Remove(consultaEntrevista);

                        //SE APLICAN TODOS LOS CAMBIOS A LA BD
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();
                    }
                }
                
                

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Entrevista eliminada Correctamente" });
        }

        // GET: api/AmpDecs/SetEntrevistaInciakl
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetEntrevistaInicial([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var consultaEntrevistaInicial = await _context.AmpDecs.Where(a => a.idAmpliacion == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();

                if (consultaEntrevistaInicial == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ninguna entrevista con la información enviada" });
                }

                else
                {

                    var consultaEntrevista = await _context.AmpDecs.Where(a => a.HechoId == model.infoBorrado.rHechoId).Where(a => a.EntrevistaInicial == true)
                                              .Take(1).FirstOrDefaultAsync();

                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("LOG")).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {

                        Guid gLog = Guid.NewGuid();
                        DateTime fecha = System.DateTime.Now;
                        LogAdmon laRegistro = new LogAdmon
                        {
                            IdLogAdmon = gLog,
                            UsuarioId = model.usuario,
                            Tabla = model.tabla,
                            FechaMov = fecha,
                            RegistroId = model.infoBorrado.registroId,
                            SolicitanteId = model.solicitante,
                            RazonMov = model.razon,
                            MovimientoId = new Guid("B1109C2F-C96E-4B62-BF6D-6F0D9518CBD5")
                        };

                        ctx.Add(laRegistro);



                        if (consultaEntrevista != null)
                        {

                            LogAmpDec ampDec = new LogAmpDec
                            {
                                LogAdmonId = gLog,
                                idAmpliacion = consultaEntrevista.idAmpliacion,
                                HechoId = consultaEntrevista.HechoId,
                                PersonaId = consultaEntrevista.PersonaId,
                                Tipo = consultaEntrevista.Tipo,
                                ClasificacionPersona = consultaEntrevista.ClasificacionPersona,
                                Manifestacion = consultaEntrevista.Manifestacion,
                                Hechos = consultaEntrevista.Hechos,
                                TRepresentantes = consultaEntrevista.TRepresentantes,
                                Edad = consultaEntrevista.Edad,
                                Fechasys = consultaEntrevista.Fechasys,
                                UDistrito = consultaEntrevista.UDistrito,
                                USubproc = consultaEntrevista.USubproc,
                                UAgencia = consultaEntrevista.UAgencia,
                                Usuario = consultaEntrevista.Usuario,
                                UPuesto = consultaEntrevista.UPuesto,
                                UModulo = consultaEntrevista.UModulo,
                                Numerooficio = consultaEntrevista.Numerooficio,
                                TipoEA = consultaEntrevista.TipoEA,
                                HoraS = consultaEntrevista.HoraS,
                                FechaS = consultaEntrevista.FechaS,
                                Representante = consultaEntrevista.Representante,
                                Iniciales = consultaEntrevista.Iniciales,
                                Acompanantev = consultaEntrevista.Acompañantev,
                                ParentezcoV = consultaEntrevista.ParentezcoV,
                                VNombre = consultaEntrevista.VNombre,
                                VPuesto = consultaEntrevista.VPuesto,
                                Tidentificacion = consultaEntrevista.Tidentificacion,
                                NoIdentificacion = consultaEntrevista.NoIdentificacion,
                                ClasificacionP = consultaEntrevista.ClasificacionP,
                                TipoP = consultaEntrevista.TipoP,
                                DireccionP = consultaEntrevista.DireccionP,
                                CURPA = consultaEntrevista.CURPA,
                                EntrevistaInicial = consultaEntrevista.EntrevistaInicial
                            };
                            ctx.Add(ampDec);
                            consultaEntrevista.EntrevistaInicial = false;
                        }


                        LogAmpDec ampDecInicial = new LogAmpDec
                        {
                            LogAdmonId = gLog,
                            idAmpliacion = consultaEntrevistaInicial.idAmpliacion,
                            HechoId = consultaEntrevistaInicial.HechoId,
                            PersonaId = consultaEntrevistaInicial.PersonaId,
                            Tipo = consultaEntrevistaInicial.Tipo,
                            ClasificacionPersona = consultaEntrevistaInicial.ClasificacionPersona,
                            Manifestacion = consultaEntrevistaInicial.Manifestacion,
                            Hechos = consultaEntrevistaInicial.Hechos,
                            TRepresentantes = consultaEntrevistaInicial.TRepresentantes,
                            Edad = consultaEntrevistaInicial.Edad,
                            Fechasys = consultaEntrevistaInicial.Fechasys,
                            UDistrito = consultaEntrevistaInicial.UDistrito,
                            USubproc = consultaEntrevistaInicial.USubproc,
                            UAgencia = consultaEntrevistaInicial.UAgencia,
                            Usuario = consultaEntrevistaInicial.Usuario,
                            UPuesto = consultaEntrevistaInicial.UPuesto,
                            UModulo = consultaEntrevistaInicial.UModulo,
                            Numerooficio = consultaEntrevistaInicial.Numerooficio,
                            TipoEA = consultaEntrevistaInicial.TipoEA,
                            HoraS = consultaEntrevistaInicial.HoraS,
                            FechaS = consultaEntrevistaInicial.FechaS,
                            Representante = consultaEntrevistaInicial.Representante,
                            Iniciales = consultaEntrevistaInicial.Iniciales,
                            Acompanantev = consultaEntrevistaInicial.Acompañantev,
                            ParentezcoV = consultaEntrevistaInicial.ParentezcoV,
                            VNombre = consultaEntrevistaInicial.VNombre,
                            VPuesto = consultaEntrevistaInicial.VPuesto,
                            Tidentificacion = consultaEntrevistaInicial.Tidentificacion,
                            NoIdentificacion = consultaEntrevistaInicial.NoIdentificacion,
                            ClasificacionP = consultaEntrevistaInicial.ClasificacionP,
                            TipoP = consultaEntrevistaInicial.TipoP,
                            DireccionP = consultaEntrevistaInicial.DireccionP,
                            CURPA = consultaEntrevistaInicial.CURPA,
                            EntrevistaInicial = consultaEntrevistaInicial.EntrevistaInicial
                        };
                        ctx.Add(ampDecInicial);

                        consultaEntrevistaInicial.EntrevistaInicial = true;



                        await _context.SaveChangesAsync();
                        await ctx.SaveChangesAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Entrevista definida como inicial" });
        }

        


        // POST: api/AmpDecs/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear(CrearViewModel model)

        {
            Guid idampliacion;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;
        

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
                _context.AmpDecs.Add(InsertarAmpDec);

                await _context.SaveChangesAsync();
                idampliacion = InsertarAmpDec.idAmpliacion;

                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }
            return Ok(new {idampliacion = idampliacion});


        }

        // GET: api/AmpDecs/PrimeraEntrevista
        [Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Administrador,AMPO-IL")]
        [HttpGet("[action]/{idrhecho}")]
        public async Task<IActionResult> PrimeraEntrevista([FromRoute] Guid idrhecho)
        {
            var p = await _context.AmpDecs
                          .Where(x => x.HechoId == idrhecho)
                          .FirstOrDefaultAsync();

            if (p == null)
            {
                return Ok(new { PrimeraEntrevista = true });
            }

            return Ok(new PrimeraEntrevistaViewModel
            {
                PrimeraEntrevista = false
            });

        }
        // GET: api/AmpDecs/ListarEntrevistaInicial
        //[Authorize(Roles = "AMPO-AMP,Recepción,Administrador")]
        [HttpGet("[action]/{idrhecho}")]
        public async Task<IActionResult> ListarEntrevistaInicial([FromRoute] Guid idrhecho)
        {
            var a = await _context.AmpDecs
                           .Include(x => x.Persona)
                          .Where(x => x.HechoId == idrhecho)
                          .Where(x=> x.EntrevistaInicial == true)
                          .FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound();
            }

            return Ok(new EntrevistaInicialViewModel
            {
                idAmpliacion = a.idAmpliacion,
                HechoId = a.HechoId,
                PersonaId = a.PersonaId,
                NombrePersona = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Tipo = a.Tipo,
                TRepresentantes = a.TRepresentantes,
                Edad = a.Edad,
                TipoEA = a.TipoEA,
                HoraS = a.HoraS,
                FechaS = a.FechaS,
                Representante = a.Representante,
                Iniciales = a.Iniciales,
                Acompañantev = a.Acompañantev,
                ParentezcoV = a.ParentezcoV,
                VNombre = a.VNombre,
                VPuesto = a.VPuesto,
                Tidentificacion = a.Tidentificacion,
                NoIdentificacion = a.NoIdentificacion,
                Fechasys = a.Fechasys,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Numerooficio = a.Numerooficio,
                ClasificacionPersona = a.ClasificacionPersona,
                Manifestacion = a.Manifestacion,
                Hechos = a.Hechos,
                Alias = a.Persona.Alias,
                Curp = a.Persona.CURP,
                DocIdentificacion = a.Persona.DocIdentificacion,
                Nacionalidad = a.Persona.Nacionalidad,
                Tel = a.Persona.Telefono1,
                Email = a.Persona.Correo,
                Genero = a.Persona.Genero,
                Sexo = a.Persona.Sexo,
                FechaNacimiento = a.Persona.FechaNacimiento,
                Medionotificacion = a.Persona.Medionotificacion,
                Ocupacion = a.Persona.Ocupacion,
                Nivelestudio = a.Persona.NivelEstudio,
                Religion = a.Persona.Religion,
                Lengua = a.Persona.Lengua,
                Estadocivil = a.Persona.EstadoCivil,
                Tipodiscapacidad = a.Persona.TipoDiscapacidad,
                TipoP = a.TipoP,
                DireccionP = a.DireccionP,
                ClasificacionP = a.ClasificacionP,
                CURPA = a.CURPA,
                EntrevistaInicial = a.EntrevistaInicial

            });

        }

        // GET: api/AmpDecs/ListarEntrevistaInicial
        [Authorize(Roles = "Director, Administrador, Coordinador, USAR")]
        [HttpGet("[action]/{idrhecho}/{distritoId}")]
        public async Task<IActionResult> ListarEntrevistaInicialXDistrito([FromRoute] Guid idrhecho, Guid distritoId)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                
                    var a = await ctx.AmpDecs
                                   .Include(x => x.Persona)
                                  .Where(x => x.HechoId == idrhecho)
                                  .Where(x => x.EntrevistaInicial == true)
                                  .FirstOrDefaultAsync();

                    if (a == null)
                    {
                        return NotFound();
                    }

                    return Ok(new EntrevistaInicialViewModel
                    {
                        idAmpliacion = a.idAmpliacion,
                        HechoId = a.HechoId,
                        PersonaId = a.PersonaId,
                        NombrePersona = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                        Tipo = a.Tipo,
                        TRepresentantes = a.TRepresentantes,
                        Edad = a.Edad,
                        TipoEA = a.TipoEA,
                        HoraS = a.HoraS,
                        FechaS = a.FechaS,
                        Representante = a.Representante,
                        Iniciales = a.Iniciales,
                        Acompañantev = a.Acompañantev,
                        ParentezcoV = a.ParentezcoV,
                        VNombre = a.VNombre,
                        VPuesto = a.VPuesto,
                        Tidentificacion = a.Tidentificacion,
                        NoIdentificacion = a.NoIdentificacion,
                        Fechasys = a.Fechasys,
                        UDistrito = a.UDistrito,
                        USubproc = a.USubproc,
                        UAgencia = a.UAgencia,
                        Usuario = a.Usuario,
                        UPuesto = a.UPuesto,
                        UModulo = a.UModulo,
                        Numerooficio = a.Numerooficio,
                        ClasificacionPersona = a.ClasificacionPersona,
                        Manifestacion = a.Manifestacion,
                        Hechos = a.Hechos,
                        Alias = a.Persona.Alias,
                        Curp = a.Persona.CURP,
                        DocIdentificacion = a.Persona.DocIdentificacion,
                        Nacionalidad = a.Persona.Nacionalidad,
                        Tel = a.Persona.Telefono1,
                        Email = a.Persona.Correo,
                        Genero = a.Persona.Genero,
                        Sexo = a.Persona.Sexo,
                        FechaNacimiento = a.Persona.FechaNacimiento,
                        Medionotificacion = a.Persona.Medionotificacion,
                        Ocupacion = a.Persona.Ocupacion,
                        Nivelestudio = a.Persona.NivelEstudio,
                        Religion = a.Persona.Religion,
                        Lengua = a.Persona.Lengua,
                        Estadocivil = a.Persona.EstadoCivil,
                        Tipodiscapacidad = a.Persona.TipoDiscapacidad,
                        TipoP = a.TipoP,
                        DireccionP = a.DireccionP,
                        ClasificacionP = a.ClasificacionP,
                        CURPA = a.CURPA,
                        EntrevistaInicial = a.EntrevistaInicial

                    });
                
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        private bool AmpDecExists(Guid id)
        {
            return _context.AmpDecs.Any(e => e.idAmpliacion == id);
        }

        // POST: api/AmpDecs/Clonar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var entrevistasCarpeta = await _context.AmpDecs.Where(x => x.HechoId == model.IdRHecho).ToListAsync();



                if (entrevistasCarpeta == null)
                {
                    return Ok();

                }

                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (AmpDec ampdecActual in entrevistasCarpeta)
                    {

                        var insertarAmpDec = await ctx.AmpDecs.FirstOrDefaultAsync(a => a.idAmpliacion == ampdecActual.idAmpliacion);

                        if (insertarAmpDec == null)
                        {
                            insertarAmpDec = new AmpDec();
                            ctx.AmpDecs.Add(insertarAmpDec);
                        }

                        insertarAmpDec.idAmpliacion = ampdecActual.idAmpliacion;
                        insertarAmpDec.HechoId = ampdecActual.HechoId;
                        insertarAmpDec.PersonaId = ampdecActual.PersonaId;
                        insertarAmpDec.Fechasys = ampdecActual.Fechasys;
                        insertarAmpDec.Tipo = ampdecActual.Tipo;
                        insertarAmpDec.ClasificacionPersona = ampdecActual.ClasificacionPersona;
                        insertarAmpDec.UAgencia = ampdecActual.UAgencia;
                        insertarAmpDec.TRepresentantes = ampdecActual.TRepresentantes;
                        insertarAmpDec.UModulo = ampdecActual.UModulo;
                        insertarAmpDec.UDistrito = ampdecActual.UDistrito;
                        insertarAmpDec.Manifestacion = ampdecActual.Manifestacion;
                        insertarAmpDec.USubproc = ampdecActual.USubproc;
                        insertarAmpDec.Usuario = ampdecActual.Usuario;
                        insertarAmpDec.UPuesto = ampdecActual.UPuesto;
                        insertarAmpDec.Numerooficio = ampdecActual.Numerooficio;
                        insertarAmpDec.Edad = ampdecActual.Edad;
                        insertarAmpDec.Hechos = ampdecActual.Hechos;
                        insertarAmpDec.Acompañantev = ampdecActual.Acompañantev;
                        insertarAmpDec.FechaS = ampdecActual.FechaS;
                        insertarAmpDec.HoraS = ampdecActual.HoraS;
                        insertarAmpDec.Iniciales = ampdecActual.Iniciales;
                        insertarAmpDec.NoIdentificacion = ampdecActual.NoIdentificacion;
                        insertarAmpDec.ParentezcoV = ampdecActual.ParentezcoV;
                        insertarAmpDec.Representante = ampdecActual.Representante;
                        insertarAmpDec.Tidentificacion = ampdecActual.Tidentificacion;
                        insertarAmpDec.TipoEA = ampdecActual.TipoEA;
                        insertarAmpDec.VNombre = ampdecActual.VNombre;
                        insertarAmpDec.VPuesto = ampdecActual.VPuesto;
                        insertarAmpDec.ClasificacionP = ampdecActual.ClasificacionP;
                        insertarAmpDec.DireccionP = ampdecActual.DireccionP;
                        insertarAmpDec.TipoP = ampdecActual.TipoP;
                        insertarAmpDec.CURPA = ampdecActual.CURPA;
                        insertarAmpDec.EntrevistaInicial = ampdecActual.EntrevistaInicial;


                        await ctx.SaveChangesAsync();

                    }
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.PersonaDesap;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaDesapController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public PersonaDesapController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //GET: api/PersonaDesap
        [HttpGet]
        public IEnumerable<RPersonaDesap> PersonaDesaps()
        {
            return _context.PersonaDesaps;
        }

        //GET: api/PersonaDesap/ListarPersonaDesap/PersonaId
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{PersonaId}")]
        public async Task<IActionResult> ListarPersonaDesap([FromRoute] Guid PersonaId)
        {
            var CDP = await _context.PersonaDesaps.Where(a => a.PersonaId == PersonaId).FirstOrDefaultAsync();

            if(CDP != null)
            {
                return Ok(new { status = true });
            }else
            {
                return Ok(new { status = false });
            }

        }

        // GET: api/PersonaDesap/ListarPorRHecho/{RHechoId}
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IActionResult> ListarPorRhecho([FromRoute] Guid RHechoId)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { error = "Información Incompleta" });
            }
            try
            {
                var ma = await _context.PersonaDesaps
                              .Include(a => a.Persona)
                              .Include(a => a.CompaniaTelefonica)
                              .Include(a => a.MarcaTelefono)
                              .Where(a => a.RHechoId == RHechoId)
                              .Where(a => a.Persona.Registro == true)
                              .ToListAsync();


                //var ma2 = _context.PersonaDesaps.FromSqlRaw("SELECT * FROM CAT_PERSONADESAPARECIDA LEFT JOIN CAT_VEHICULO_DESAPARICIONPERSONA ON IdPersonaDesaparecida = PersonaDesaparecidaId ").ToList();

                return Ok(ma.Select(a => new ListaPersonaDesapViewModel
                {
                    IdPersonaDesaparecida = a.IdPersonaDesaparecida,
                    PersonaId = a.PersonaId,
                    EstadoSalud = a.EstadoSalud,
                    Adicciones = a.Adicciones,
                    Padecimiento = a.Padecimiento,
                    Etnia = a.Etnia,
                    PortabaMedioComunicacion = a.PortabaMedioComunicacion,
                    GrupoDelictivo = a.GrupoDelictivo,
                    ProcedenciaGrupoDelictivo = a.ProcedenciaGrupoDelictivo,
                    FechaHoraUltAvistamiento = a.FechaHoraUltAvistamiento,
                    NombrePersonaAcompanaba = a.NombrePersonaAcompanaba,
                    RelacionPersonaAcompanaba = a.RelacionPersonaAcompanaba,
                    LocalizacionPersonaAcompanaba = a.LocalizacionPersonaAcompanaba,
                    VestimentaAccesorios = a.VestimentaAccesorios,
                    PersonaDesap = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                    descripcionHechos = a.descripcionHechos,
                    marcaTelefonoId = a.MarcaTelefono == null ? Guid.Empty : a.MarcaTelefono.IdMarcaTelefono,
                    marcaTelefono = a.MarcaTelefono == null ? String.Empty : a.MarcaTelefono.nombre,
                    fotografiaURL = a.fotografiaURL,
                    companiaTelefonicaId = a.CompaniaTelefonica == null ? Guid.Empty : a.companiaTelefonicaId,
                    companiaTelefonica = a.CompaniaTelefonica == null ? String.Empty : a.CompaniaTelefonica.nombre,
                    RelacionPersonaDenunciante=a.RelacionPersonaDenunciante,
                    //HAY MUCHOS REGISTROS QUE TIENEN ESTE DATO NULL, POR ELLO, TODOS LOS QUE DIGAN NULL SE TOMARAN COMO FALSE Y SE IMPRIMIRA LO CORRESPOBDIENTE EN FRONT (ACOMPAÑABA: NO)
                    AcompanabaDenunciante = (bool)(a.AcompanabaDenunciante == null ? false :a.AcompanabaDenunciante),
                    FechaSys = a.FechaSys,
                    DistritoCaptura = a.DistritoCaptura,
                    AgenciaCaptura = a.AgenciaCaptura,
                    NombreCaptura = a.NombreCaptura,
                    PersonaCondicion = a.PersonaCondicion,
                    OtraMarca = a.OtraMarca,


}));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException== null ? "SIN EXCEPCION INTERNA":ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/PersonaDesap/BuscarPersonaDesap/{IdPersonaDesaparecida}
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Administrador")]
        [HttpGet("[action]/{IdPersonaDesaparecida}")]
        public async Task<IActionResult> BuscarPersonaDesap([FromRoute] Guid IdPersonaDesaparecida)
        {
            var pd = await _context.PersonaDesaps
                          .Where(a => a.IdPersonaDesaparecida == IdPersonaDesaparecida)
                          .FirstOrDefaultAsync();

            if (pd == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new ListaPersonaDesapViewModel
            {
                /*CAT_PERSONADESAPARECIDA*/
                /*************************/
                IdPersonaDesaparecida = pd.IdPersonaDesaparecida,
                PersonaId = pd.PersonaId,
                EstadoSalud = pd.EstadoSalud,
                Adicciones = pd.Adicciones,
                Padecimiento = pd.Padecimiento,
                Etnia = pd.Etnia,
                PortabaMedioComunicacion = pd.PortabaMedioComunicacion,
                GrupoDelictivo = pd.GrupoDelictivo,
                ProcedenciaGrupoDelictivo = pd.ProcedenciaGrupoDelictivo,
                FechaHoraUltAvistamiento = pd.FechaHoraUltAvistamiento,
                NombrePersonaAcompanaba = pd.NombrePersonaAcompanaba,
                RelacionPersonaAcompanaba = pd.RelacionPersonaAcompanaba,
                LocalizacionPersonaAcompanaba = pd.LocalizacionPersonaAcompanaba,
                VestimentaAccesorios = pd.VestimentaAccesorios
            });
        }

        // POST: api/PersonaDesap/Crear
        [HttpPost("[Action]")]
        public async Task<IActionResult>Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RPersonaDesap insertarPersonaDesap = new RPersonaDesap
            {
                PersonaId = model.PersonaId,
                EstadoSalud = model.EstadoSalud,
                Adicciones = model.Adicciones,
                Padecimiento = model.Padecimiento,
                Etnia = model.Etnia,
                PortabaMedioComunicacion = model.PortabaMedioComunicacion,
                GrupoDelictivo = model.GrupoDelictivo,
                ProcedenciaGrupoDelictivo = model.ProcedenciaGrupoDelictivo,
                FechaHoraUltAvistamiento = model.FechaHoraUltAvistamiento,
                NombrePersonaAcompanaba = model.NombrePersonaAcompanaba,
                RelacionPersonaAcompanaba = model.RelacionPersonaAcompanaba,
                LocalizacionPersonaAcompanaba = model.LocalizacionPersonaAcompanaba,
                VestimentaAccesorios = model.VestimentaAccesorios,
                RHechoId = model.RHechoId,
                companiaTelefonicaId = model.companiaTelefonicaId,
                marcaTelefonoId = model.marcaTelefonoId,
                descripcionHechos = model.descripcionHechos,
                fotografiaURL = model.fotografiaURL,
                RelacionPersonaDenunciante = model.RelacionPersonaDenunciante,
                AcompanabaDenunciante = model.AcompanabaDenunciante,
                FechaSys = model.FechaSys,
                DistritoCaptura = model.DistritoCaptura,
                AgenciaCaptura = model.AgenciaCaptura,
                NombreCaptura = model.NombreCaptura,
                PersonaCondicion = model.PersonaCondicion,
                OtraMarca = model.OtraMarca,
            };
            try 
              {
                _context.PersonaDesaps.Add(insertarPersonaDesap);
                await _context.SaveChangesAsync();
              }
              catch (Exception ex)
              {
                 return BadRequest("Error: " + ex);
              }

            return Ok(new {idpersonadesaparecida = insertarPersonaDesap.IdPersonaDesaparecida});
            
        }

        // PUT: api/PersonaDesap/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personadesaparecida = await _context.PersonaDesaps.FirstOrDefaultAsync(a => a.IdPersonaDesaparecida == model.IdPersonaDesaparecida);

            if (personadesaparecida == null)
            {
                return NotFound();
            }
            personadesaparecida.EstadoSalud = model.EstadoSalud;
            personadesaparecida.Adicciones = model.Adicciones;
            personadesaparecida.Padecimiento = model.Padecimiento;
            personadesaparecida.Etnia = model.Etnia;
            personadesaparecida.PortabaMedioComunicacion = model.PortabaMedioComunicacion;
            personadesaparecida.GrupoDelictivo = model.GrupoDelictivo;
            personadesaparecida.ProcedenciaGrupoDelictivo = model.ProcedenciaGrupoDelictivo;
            personadesaparecida.FechaHoraUltAvistamiento = model.FechaHoraUltAvistamiento;
            personadesaparecida.NombrePersonaAcompanaba = model.NombrePersonaAcompanaba;
            personadesaparecida.RelacionPersonaAcompanaba = model.RelacionPersonaAcompanaba;
            personadesaparecida.LocalizacionPersonaAcompanaba = model.LocalizacionPersonaAcompanaba;
            personadesaparecida.VestimentaAccesorios = model.VestimentaAccesorios;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }


        //ELIMINAR UN REGISTRO CON COPIA A LA BD DE LOG
        // GET: api/PersonaDesap/Eliminar
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
                        MovimientoId = new Guid("9da5ecf1-c287-46ab-9443-2b6e9dcf5644")
                    };

                    ctx.Add(laRegistro);
                    var consultaVehiculoDesaparecido = await _context.VehiculoPersonaDesap.Where(a => a.PersonaDesaparecidaId == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();


                    if (consultaVehiculoDesaparecido != null)
                    {
                        LogVehiculoPersonaDesap vehiculo = new LogVehiculoPersonaDesap
                        {
                            LogAdmonId = gLog,
                            IdVehDesaparicionPersona = consultaVehiculoDesaparecido.IdVehDesaparicionPersona,
                            PersonaDesaparecidaId = consultaVehiculoDesaparecido.PersonaDesaparecidaId,
                            TipovId = consultaVehiculoDesaparecido.TipovId,
                            AnoId = consultaVehiculoDesaparecido.AnoId,
                            ColorId = consultaVehiculoDesaparecido.ColorId,
                            ModeloId = consultaVehiculoDesaparecido.ModeloId,
                            MarcaId = consultaVehiculoDesaparecido.MarcaId,
                            Serie = consultaVehiculoDesaparecido.Serie,
                            Placas = consultaVehiculoDesaparecido.Placas,
                            SenasParticulares = consultaVehiculoDesaparecido.SenasParticulares,
                            NoSerieMotor = consultaVehiculoDesaparecido.NoSerieMotor,
                            Propietario = consultaVehiculoDesaparecido.Propietario,
                            Ruta = consultaVehiculoDesaparecido.Ruta,
                            EstadoId = consultaVehiculoDesaparecido.EstadoId,
                            Privado = consultaVehiculoDesaparecido.Privado


                        };
                        ctx.Add(vehiculo);
                        _context.Remove(consultaVehiculoDesaparecido);
                    }

                    var consultaPersonaDesaparecida = await _context.PersonaDesaps.Where(a => a.IdPersonaDesaparecida == model.infoBorrado.registroId)
                                      .Take(1).FirstOrDefaultAsync();
                    if (consultaPersonaDesaparecida == null)
                    {
                        return Ok(new { res = "Error", men = "No se encontró ninguna cédula de persona desaparecida con la información enviada" });
                    }
                    else
                    {
                        LogRPersonaDesap representante = new LogRPersonaDesap
                        {
                            LogAdmonId = gLog,
                            IdPersonaDesaparecida = consultaPersonaDesaparecida.IdPersonaDesaparecida,
                            PersonaId = consultaPersonaDesaparecida.PersonaId,
                            RHechoId = consultaPersonaDesaparecida.RHechoId,
                            EstadoSalud = consultaPersonaDesaparecida.EstadoSalud,
                            Adicciones = consultaPersonaDesaparecida.Adicciones,
                            Padecimiento = consultaPersonaDesaparecida.Padecimiento,
                            Etnia = consultaPersonaDesaparecida.Etnia ,
                            PortabaMedioComunicacion = consultaPersonaDesaparecida.PortabaMedioComunicacion,
                            GrupoDelictivo = consultaPersonaDesaparecida.GrupoDelictivo,
                            ProcedenciaGrupoDelictivo = consultaPersonaDesaparecida.ProcedenciaGrupoDelictivo,
                            FechaHoraUltAvistamiento = consultaPersonaDesaparecida.FechaHoraUltAvistamiento,
                            NombrePersonaAcompanaba = consultaPersonaDesaparecida.NombrePersonaAcompanaba,
                            RelacionPersonaAcompanaba = consultaPersonaDesaparecida.RelacionPersonaAcompanaba,
                            LocalizacionPersonaAcompanaba = consultaPersonaDesaparecida.LocalizacionPersonaAcompanaba,
                            VestimentaAccesorios = consultaPersonaDesaparecida.VestimentaAccesorios,
                            companiaTelefonicaId = consultaPersonaDesaparecida.companiaTelefonicaId,
                            marcaTelefonoId = consultaPersonaDesaparecida.marcaTelefonoId,
                            descripcionHechos = consultaPersonaDesaparecida.descripcionHechos,
                            fotografiaURL = consultaPersonaDesaparecida.fotografiaURL
                        };
                        ctx.Add(representante);

                        _context.Remove(consultaPersonaDesaparecida);

                        //SE APLICAN TODOS LOS CAMBIOS A LA BD
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();
                    }

                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Cédula de persona desaparecida eliminado Correctamente" });
        }




        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/PersonaDesap/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultaPersonaDesap = await _context.PersonaDesaps.Where(a => a.RHechoId == model.IdRHecho).Take(1).FirstOrDefaultAsync();
                if (consultaPersonaDesap == null)
                {
                    return Ok();
                }
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                
                

                using (var ctx = new DbContextSIIGPP(options))
                {
                    
                    var insertarPersonaDesap = await ctx.PersonaDesaps.FirstOrDefaultAsync(a => a.IdPersonaDesaparecida == consultaPersonaDesap.IdPersonaDesaparecida);
                    if (insertarPersonaDesap == null)
                    {
                        insertarPersonaDesap = new RPersonaDesap();
                        ctx.PersonaDesaps.Add(consultaPersonaDesap);
                    }
                    insertarPersonaDesap.IdPersonaDesaparecida = consultaPersonaDesap.IdPersonaDesaparecida;
                    insertarPersonaDesap.PersonaId = consultaPersonaDesap.PersonaId;
                    insertarPersonaDesap.EstadoSalud = consultaPersonaDesap.EstadoSalud;
                    insertarPersonaDesap.Adicciones = consultaPersonaDesap.Adicciones;
                    insertarPersonaDesap.Padecimiento = consultaPersonaDesap.Padecimiento;
                    insertarPersonaDesap.Etnia = consultaPersonaDesap.Etnia;
                    insertarPersonaDesap.PortabaMedioComunicacion = consultaPersonaDesap.PortabaMedioComunicacion;
                    insertarPersonaDesap.GrupoDelictivo = consultaPersonaDesap.GrupoDelictivo;
                    insertarPersonaDesap.ProcedenciaGrupoDelictivo = consultaPersonaDesap.ProcedenciaGrupoDelictivo;
                    insertarPersonaDesap.FechaHoraUltAvistamiento = consultaPersonaDesap.FechaHoraUltAvistamiento;
                    insertarPersonaDesap.NombrePersonaAcompanaba = consultaPersonaDesap.NombrePersonaAcompanaba;
                    insertarPersonaDesap.RelacionPersonaAcompanaba = consultaPersonaDesap.RelacionPersonaAcompanaba;
                    insertarPersonaDesap.LocalizacionPersonaAcompanaba = consultaPersonaDesap.LocalizacionPersonaAcompanaba;
                    insertarPersonaDesap.VestimentaAccesorios = consultaPersonaDesap.VestimentaAccesorios;
                    insertarPersonaDesap.RHechoId = consultaPersonaDesap.RHechoId;
                    insertarPersonaDesap.marcaTelefonoId = consultaPersonaDesap.marcaTelefonoId;
                    insertarPersonaDesap.descripcionHechos = consultaPersonaDesap.descripcionHechos;
                    insertarPersonaDesap.fotografiaURL = consultaPersonaDesap.fotografiaURL;
                    insertarPersonaDesap.companiaTelefonicaId = consultaPersonaDesap.companiaTelefonicaId;
                    await ctx.SaveChangesAsync();

                    return Ok();


                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }
    }
}

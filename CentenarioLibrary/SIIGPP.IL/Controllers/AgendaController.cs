using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.IL.Models.Agenda;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_IL.Agendas;
using SIIGPP.IL.FilterClass;
using System.Text.Json;

namespace SIIGPP.IL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public AgendaController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Agenda/ListarTodos
        [HttpGet("[action]/{fecha}")]
        public async Task<IActionResult> Listar([FromRoute] DateTime fecha)
        {
            var ao = await _context.Agendas
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.FechaCitacion == fecha)
                .ToListAsync();
            try
            {
                return Ok(ao.Select(a => new AgendaViewModel
                {
                    IdAgenda = a.IdAgenda,
                    RHechoId = a.RHechoId,
                    NumeroOficio = a.NumeroOficio,
                    CausaPenal = a.CausaPenal,
                    Nuc = a.Nuc,
                    Victimas = a.Victimas,
                    Imputado = a.Imputado,
                    Delitos = a.Delitos,
                    Texto = a.Texto,
                    FechaCitacion = a.FechaCitacion,
                    Status = a.Status,
                    Tipo = a.Tipo,
                    DirigidoNombre = a.DirigidoNombre,
                    DirigidoPuesto = a.DirigidoPuesto,
                    ReDireccion = a.ReDireccion,
                    ReTelefono = a.ReTelefono,
                    ReCorreo = a.ReCorreo,
                    ArticulosSancion = a.ArticulosSancion,
                    DireccionImputado = a.DireccionImputado,
                    TelefonoImputado = a.TelefonoImputado,
                    CorreoImputado = a.CorreoImputado,
                    DefensorParticularImp = a.DefensorParticularImp,
                    DomicilioDPI = a.DomicilioDPI,
                    TelefonoDPI = a.TelefonoDPI,
                    CorreoDPI = a.CorreoDPI,
                    InformacionVicAseJu = a.InformacionVicAseJu,
                    InformacionImpDeP = a.InformacionImpDeP,
                    InformacionImp = a.InformacionImp,
                    InformacionDelito = a.InformacionDelito,
                    HoraCitacion = a.HoraCitacion,
                    LugarCitacion = a.LugarCitacion,
                    DescripcionCitacion = a.DescripcionCitacion,
                    DireccionHecho = a.DireccionHecho,
                    HechosIII = a.HechosIII,
                    ClasificacionjIII = a.ClasificacionjIII,
                    CorrelacionArtIII = a.CorrelacionArtIII,
                    ArticuloIII = a.ArticuloIII,
                    ModaidadesVI = a.ModaidadesVI,
                    AutoriaV = a.AutoriaV,
                    Autoria2V = a.Autoria2V,
                    PreceptosVI = a.PreceptosVI,
                    TestimonialVII = a.TestimonialVII,
                    PericialVII = a.PericialVII,
                    DocumentalesVII = a.DocumentalesVII,
                    MaterialVII = a.MaterialVII,
                    AnticipadaVII = a.AnticipadaVII,
                    ArticulosVIII = a.ArticulosVIII,
                    MontoVIII = a.MontoVIII,
                    NumeroLetraVIII = a.NumeroLetraVIII,
                    TestimonialVIII = a.TestimonialVII,
                    PericialVIII = a.PericialVIII,
                    DocumentalesVIII = a.DocumentalesVIII,
                    MaterialVIII = a.MaterialVIII,
                    ArticulosIX = a.ArticulosIX,
                    PenaIX = a.PenaIX,
                    TestimonialesX = a.TestimonialesX,
                    TestimonialX = a.TestimonialX,
                    PericialX = a.PericialX,
                    DocumentalesX = a.DocumentalesX,
                    MaterialX = a.MaterialX,
                    DecomisoXI = a.DecomisoXI,
                    PropuestaXII = a.PropuestaXII,
                    TerminacionXIII = a.TerminacionXIII,
                    Resumen = a.Resumen,
                    Viculada = a.Viculada,
                    PlazoInvestigacion = a.PlazoInvestigacion,
                    Prorroga = a.Prorroga,
                    TiempoProrroga = a.TiempoProrroga,
                    PersonaPresentar = a.PersonaPresentar,
                    Tipo2 = a.Tipo2,
                    UDistrito = a.UDistrito,
                    USubproc = a.USubproc,
                    UAgencia = a.UAgencia,
                    Usuario = a.Usuario,
                    UPuesto = a.UPuesto,
                    UModulo = a.UModulo,
                    Fechasys = a.Fechasys,
                    Aux = a.Aux,
                    SexoPersonaCitada = a.SexoPersonaCitada,
                    Status2 = a.Status2,
                    CUPRE = a.CUPRE,
                    Comparece = a.Comparece
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/Agenda/ListarTipoRHecho
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}/{Tipo}")]
        public async Task<IEnumerable<AgendaViewModel>> ListarTipoRHecho([FromRoute]Guid RHechoId, int Tipo)
        {
            var CitNot = await _context.Agendas
                .Where(a => a.RHechoId == RHechoId)
                .Where(a => a.Tipo == Tipo)
                .OrderByDescending (a => a.Fechasys)
                .ToListAsync();
                
            return CitNot.Select(a => new AgendaViewModel
            {
                IdAgenda = a.IdAgenda,
                RHechoId = a.RHechoId,
                NumeroOficio = a.NumeroOficio,
                CausaPenal = a.CausaPenal,
                Nuc = a.Nuc,
                Victimas = a.Victimas,
                Imputado = a.Imputado,
                Delitos = a.Delitos,
                Texto = a.Texto,
                FechaCitacion = a.FechaCitacion,
                Status = a.Status,
                Tipo = a.Tipo,
                DirigidoNombre = a.DirigidoNombre,
                DirigidoPuesto = a.DirigidoPuesto,
                ReDireccion = a.ReDireccion,
                ReTelefono = a.ReTelefono,
                ReCorreo = a.ReCorreo,
                ArticulosSancion = a.ArticulosSancion,
                DireccionImputado = a.DireccionImputado,
                TelefonoImputado = a.TelefonoImputado,
                CorreoImputado = a.CorreoImputado,
                DefensorParticularImp = a.DefensorParticularImp,
                DomicilioDPI = a.DomicilioDPI,
                TelefonoDPI = a.TelefonoDPI,
                CorreoDPI = a.CorreoDPI,
                InformacionVicAseJu = a.InformacionVicAseJu,
                InformacionImpDeP = a.InformacionImpDeP,
                InformacionImp = a.InformacionImp,
                InformacionDelito = a.InformacionDelito,
                HoraCitacion = a.HoraCitacion,
                LugarCitacion = a.LugarCitacion,
                DescripcionCitacion = a.DescripcionCitacion,
                DireccionHecho = a.DireccionHecho,
                HechosIII = a.HechosIII,
                ClasificacionjIII = a.ClasificacionjIII,
                CorrelacionArtIII = a.CorrelacionArtIII,
                ArticuloIII = a.ArticuloIII,
                ModaidadesVI = a.ModaidadesVI,
                AutoriaV = a.AutoriaV,
                Autoria2V = a.Autoria2V,
                PreceptosVI = a.PreceptosVI,
                TestimonialVII = a.TestimonialVII,
                PericialVII = a.PericialVII,
                DocumentalesVII = a.DocumentalesVII,
                MaterialVII = a.MaterialVII,
                AnticipadaVII = a.AnticipadaVII,
                ArticulosVIII = a.ArticulosVIII,
                MontoVIII = a.MontoVIII,
                NumeroLetraVIII = a.NumeroLetraVIII,
                TestimonialVIII = a.TestimonialVII,
                PericialVIII = a.PericialVIII,
                DocumentalesVIII = a.DocumentalesVIII,
                MaterialVIII = a.MaterialVIII,
                ArticulosIX = a.ArticulosIX,
                PenaIX = a.PenaIX,
                TestimonialesX = a.TestimonialesX,
                TestimonialX = a.TestimonialX,
                PericialX = a.PericialX,
                DocumentalesX = a.DocumentalesX,
                MaterialX = a.MaterialX,
                DecomisoXI = a.DecomisoXI,
                PropuestaXII = a.PropuestaXII,
                TerminacionXIII = a.TerminacionXIII,
                Resumen = a.Resumen,
                Viculada = a.Viculada,
                PlazoInvestigacion = a.PlazoInvestigacion,
                Prorroga = a.Prorroga,
                TiempoProrroga = a.TiempoProrroga,
                PersonaPresentar = a.PersonaPresentar,
                Tipo2 = a.Tipo2,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Aux = a.Aux,
                SexoPersonaCitada = a.SexoPersonaCitada,
                Status2 = a.Status2,
                CUPRE = a.CUPRE,
                Comparece = a.Comparece
            });
        }

        // POST: api/Agenda/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Agenda ag = new Agenda
            {
                RHechoId = model.RHechoId,
                NumeroOficio = model.NumeroOficio,
                CausaPenal = model.CausaPenal,
                Nuc = model.Nuc,
                Victimas = model.Victimas,
                Imputado = model.Imputado,
                Delitos = model.Delitos,
                Texto = model.Texto,
                FechaCitacion = model.FechaCitacion,
                Status = model.Status,
                Tipo = model.Tipo,
                DirigidoNombre = model.DirigidoNombre,
                DirigidoPuesto = model.DirigidoPuesto,
                ReDireccion = model.ReDireccion,
                ReTelefono = model.ReTelefono,
                ReCorreo = model.ReCorreo,
                ArticulosSancion = model.ArticulosSancion,
                DireccionImputado = model.DireccionImputado,
                TelefonoImputado = model.TelefonoImputado,
                CorreoImputado = model.CorreoImputado,
                DefensorParticularImp = model.DefensorParticularImp,
                DomicilioDPI = model.DomicilioDPI,
                TelefonoDPI = model.TelefonoDPI,
                CorreoDPI = model.CorreoDPI,
                InformacionVicAseJu = model.InformacionVicAseJu,
                InformacionImpDeP = model.InformacionImpDeP,
                InformacionImp = model.InformacionImp,
                InformacionDelito = model.InformacionDelito,
                HoraCitacion = model.HoraCitacion,
                LugarCitacion = model.LugarCitacion,
                DescripcionCitacion = model.DescripcionCitacion,
                DireccionHecho = model.DireccionHecho,
                HechosIII = model.HechosIII,
                ClasificacionjIII = model.ClasificacionjIII,
                CorrelacionArtIII = model.CorrelacionArtIII,
                ArticuloIII = model.ArticuloIII,
                ModaidadesVI = model.ModaidadesVI,
                AutoriaV = model.AutoriaV,
                Autoria2V = model.Autoria2V,
                PreceptosVI = model.PreceptosVI,
                TestimonialVII = model.TestimonialVII,
                PericialVII = model.PericialVII,
                DocumentalesVII = model.DocumentalesVII,
                MaterialVII = model.MaterialVII,
                AnticipadaVII = model.AnticipadaVII,
                ArticulosVIII = model.ArticulosVIII,
                MontoVIII = model.MontoVIII,
                NumeroLetraVIII = model.NumeroLetraVIII,
                TestimonialVIII = model.TestimonialVII,
                PericialVIII = model.PericialVIII,
                DocumentalesVIII = model.DocumentalesVIII,
                MaterialVIII = model.MaterialVIII,
                ArticulosIX = model.ArticulosIX,
                PenaIX = model.PenaIX,
                TestimonialesX = model.TestimonialesX,
                TestimonialX = model.TestimonialX,
                PericialX = model.PericialX,
                DocumentalesX = model.DocumentalesX,
                MaterialX = model.MaterialX,
                DecomisoXI = model.DecomisoXI,
                PropuestaXII = model.PropuestaXII,
                TerminacionXIII = model.TerminacionXIII,
                Resumen = model.Resumen,
                Viculada = false,
                PlazoInvestigacion = model.PlazoInvestigacion,
                Prorroga = false,
                TiempoProrroga = model.TiempoProrroga,
                PersonaPresentar = model.PersonaPresentar,
                Tipo2 = model.Tipo2,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = System.DateTime.Now,
                Aux = false,
                SexoPersonaCitada = model.SexoPersonaCitada,
                Status2 = false,
                CUPRE = model.CUPRE,
                Comparece = ""
            };
                _context.Agendas.Add(ag);

                await _context.SaveChangesAsync();

                var idAgenda = ag.IdAgenda;

                // INGRESAR SOLICITUD DE AUDIENCIA EN EL SERVIDOR REMOTO
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-SOLAUD")).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    var distritoActual= await _context.Distritos
                                                      .Where(a=>a.StatusAsginacion==true)
                                                      .FirstOrDefaultAsync();

                    if (distritoActual == null)
                    {
                        var result = new ObjectResult(new { statusCode = "402", mensaje = "El Sistema no encuentra el Distrito por defecto" });
                        result.StatusCode = 402;
                        return result;
                    }
                    else
                    {
                        SolicitudAudiencia solicitud = new SolicitudAudiencia
                        {
                            DistritoId =distritoActual.IdDistrito,
                            NUC = model.Nuc,
                            NumOficio = model.NumeroOficio,
                            Partes = JsonSerializer.Serialize(model.jpartes),
                            delitos = JsonSerializer.Serialize(model.jdelitos),
                            Estatus = 1,
                            FechaSolicitud = System.DateTime.Now,
                            solicitante = model.Usuario,
                            AgendaId = idAgenda
                        };

                        ctx.SolicitudAudiencias.Add(solicitud);
                        await ctx.SaveChangesAsync();
                    }
                }                
                return Ok(new {idAgenda = idAgenda});
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(402, new { mensaje = errorMessage, detalle = ex.Message, version = "1.0" });
            }
        }
      
        // PUT: api/Agenda/Actualizar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ao = await _context.Agendas.FirstOrDefaultAsync(a => a.IdAgenda == model.IdAgenda);

            if (ao == null)
            {
                return NotFound();
            }

            ao.FechaCitacion = model.FechaCitacion;
            ao.Status = model.Status;
            ao.HoraCitacion = model.HoraCitacion;
            ao.LugarCitacion = model.LugarCitacion;
            ao.DescripcionCitacion = model.DescripcionCitacion;
            ao.Viculada = model.Viculada;
            ao.PlazoInvestigacion = model.PlazoInvestigacion;
            ao.Prorroga = model.Prorroga;
            ao.TiempoProrroga = model.TiempoProrroga;
            ao.Resumen = model.Resumen;
            ao.Status2 = model.Status2;

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

        // GET: api/Agenda/ListarTipoRHecho2
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}/{Tipo}/{Tipo2}/{Tipo3}")]
        public async Task<IEnumerable<AgendaViewModel>> ListarTipoRHecho2([FromRoute]Guid RHechoId, int Tipo, int Tipo2,int Tipo3)
        {
            var CitNot = await _context.Agendas
                .Where(a => a.RHechoId == RHechoId)
                .Where(a => a.Tipo == Tipo || a.Tipo == Tipo2 || a.Tipo == Tipo3)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return CitNot.Select(a => new AgendaViewModel
            {
                IdAgenda = a.IdAgenda,
                RHechoId = a.RHechoId,
                NumeroOficio = a.NumeroOficio,
                CausaPenal = a.CausaPenal,
                Nuc = a.Nuc,
                Victimas = a.Victimas,
                Imputado = a.Imputado,
                Delitos = a.Delitos,
                Texto = a.Texto,
                FechaCitacion = a.FechaCitacion,
                Status = a.Status,
                Tipo = a.Tipo,
                DirigidoNombre = a.DirigidoNombre,
                DirigidoPuesto = a.DirigidoPuesto,
                ReDireccion = a.ReDireccion,
                ReTelefono = a.ReTelefono,
                ReCorreo = a.ReCorreo,
                ArticulosSancion = a.ArticulosSancion,
                DireccionImputado = a.DireccionImputado,
                TelefonoImputado = a.TelefonoImputado,
                CorreoImputado = a.CorreoImputado,
                DefensorParticularImp = a.DefensorParticularImp,
                DomicilioDPI = a.DomicilioDPI,
                TelefonoDPI = a.TelefonoDPI,
                CorreoDPI = a.CorreoDPI,
                InformacionVicAseJu = a.InformacionVicAseJu,
                InformacionImpDeP = a.InformacionImpDeP,
                InformacionImp = a.InformacionImp,
                InformacionDelito = a.InformacionDelito,
                HoraCitacion = a.HoraCitacion,
                LugarCitacion = a.LugarCitacion,
                DescripcionCitacion = a.DescripcionCitacion,
                DireccionHecho = a.DireccionHecho,
                HechosIII = a.HechosIII,
                ClasificacionjIII = a.ClasificacionjIII,
                CorrelacionArtIII = a.CorrelacionArtIII,
                ArticuloIII = a.ArticuloIII,
                ModaidadesVI = a.ModaidadesVI,
                AutoriaV = a.AutoriaV,
                Autoria2V = a.Autoria2V,
                PreceptosVI = a.PreceptosVI,
                TestimonialVII = a.TestimonialVII,
                PericialVII = a.PericialVII,
                DocumentalesVII = a.DocumentalesVII,
                MaterialVII = a.MaterialVII,
                AnticipadaVII = a.AnticipadaVII,
                ArticulosVIII = a.ArticulosVIII,
                MontoVIII = a.MontoVIII,
                NumeroLetraVIII = a.NumeroLetraVIII,
                TestimonialVIII = a.TestimonialVII,
                PericialVIII = a.PericialVIII,
                DocumentalesVIII = a.DocumentalesVIII,
                MaterialVIII = a.MaterialVIII,
                ArticulosIX = a.ArticulosIX,
                PenaIX = a.PenaIX,
                TestimonialesX = a.TestimonialesX,
                TestimonialX = a.TestimonialX,
                PericialX = a.PericialX,
                DocumentalesX = a.DocumentalesX,
                MaterialX = a.MaterialX,
                DecomisoXI = a.DecomisoXI,
                PropuestaXII = a.PropuestaXII,
                TerminacionXIII = a.TerminacionXIII,
                Resumen = a.Resumen,
                Viculada = a.Viculada,
                PlazoInvestigacion = a.PlazoInvestigacion,
                Prorroga = a.Prorroga,
                TiempoProrroga = a.TiempoProrroga,
                PersonaPresentar = a.PersonaPresentar,
                Tipo2 = a.Tipo2,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Aux = a.Aux,
                SexoPersonaCitada = a.SexoPersonaCitada,
                Status2 = a.Status2,
                CUPRE = a.CUPRE,
                Comparece = a.Comparece
            });
        }

        // GET: api/Agenda/ListarTodospormodulo
        [HttpGet("[action]/{fecha}/{modulo}")]
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador, AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        public async Task<IActionResult> ListarTodospormodulo([FromRoute] DateTime fecha, Guid modulo)
        {
            var ao = await _context.Agendas
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.FechaCitacion != fecha)
                .Where(a => a.RHecho.ModuloServicioId == modulo)
                .OrderBy(a => a.FechaCitacion)
                .ToListAsync();
            try
            {
                return Ok(ao.Select(a => new AgendaViewModel
                {
                    IdAgenda = a.IdAgenda,
                    RHechoId = a.RHechoId,
                    NumeroOficio = a.NumeroOficio,
                    CausaPenal = a.CausaPenal,
                    Nuc = a.Nuc,
                    Victimas = a.Victimas,
                    Imputado = a.Imputado,
                    Delitos = a.Delitos,
                    Texto = a.Texto,
                    FechaCitacion = a.FechaCitacion,
                    Status = a.Status,
                    Tipo = a.Tipo,
                    DirigidoNombre = a.DirigidoNombre,
                    DirigidoPuesto = a.DirigidoPuesto,
                    ReDireccion = a.ReDireccion,
                    ReTelefono = a.ReTelefono,
                    ReCorreo = a.ReCorreo,
                    ArticulosSancion = a.ArticulosSancion,
                    DireccionImputado = a.DireccionImputado,
                    TelefonoImputado = a.TelefonoImputado,
                    CorreoImputado = a.CorreoImputado,
                    DefensorParticularImp = a.DefensorParticularImp,
                    DomicilioDPI = a.DomicilioDPI,
                    TelefonoDPI = a.TelefonoDPI,
                    CorreoDPI = a.CorreoDPI,
                    InformacionVicAseJu = a.InformacionVicAseJu,
                    InformacionImpDeP = a.InformacionImpDeP,
                    InformacionImp = a.InformacionImp,
                    InformacionDelito = a.InformacionDelito,
                    HoraCitacion = a.HoraCitacion,
                    LugarCitacion = a.LugarCitacion,
                    DescripcionCitacion = a.DescripcionCitacion,
                    DireccionHecho = a.DireccionHecho,
                    HechosIII = a.HechosIII,
                    ClasificacionjIII = a.ClasificacionjIII,
                    CorrelacionArtIII = a.CorrelacionArtIII,
                    ArticuloIII = a.ArticuloIII,
                    ModaidadesVI = a.ModaidadesVI,
                    AutoriaV = a.AutoriaV,
                    Autoria2V = a.Autoria2V,
                    PreceptosVI = a.PreceptosVI,
                    TestimonialVII = a.TestimonialVII,
                    PericialVII = a.PericialVII,
                    DocumentalesVII = a.DocumentalesVII,
                    MaterialVII = a.MaterialVII,
                    AnticipadaVII = a.AnticipadaVII,
                    ArticulosVIII = a.ArticulosVIII,
                    MontoVIII = a.MontoVIII,
                    NumeroLetraVIII = a.NumeroLetraVIII,
                    TestimonialVIII = a.TestimonialVII,
                    PericialVIII = a.PericialVIII,
                    DocumentalesVIII = a.DocumentalesVIII,
                    MaterialVIII = a.MaterialVIII,
                    ArticulosIX = a.ArticulosIX,
                    PenaIX = a.PenaIX,
                    TestimonialesX = a.TestimonialesX,
                    TestimonialX = a.TestimonialX,
                    PericialX = a.PericialX,
                    DocumentalesX = a.DocumentalesX,
                    MaterialX = a.MaterialX,
                    DecomisoXI = a.DecomisoXI,
                    PropuestaXII = a.PropuestaXII,
                    TerminacionXIII = a.TerminacionXIII,
                    Resumen = a.Resumen,
                    Viculada = a.Viculada,
                    PlazoInvestigacion = a.PlazoInvestigacion,
                    Prorroga = a.Prorroga,
                    TiempoProrroga = a.TiempoProrroga,
                    PersonaPresentar = a.PersonaPresentar,
                    Tipo2 = a.Tipo2,
                    UDistrito = a.UDistrito,
                    USubproc = a.USubproc,
                    UAgencia = a.UAgencia,
                    Usuario = a.Usuario,
                    UPuesto = a.UPuesto,
                    UModulo = a.UModulo,
                    Fechasys = a.Fechasys,
                    Aux = a.Aux,
                    SexoPersonaCitada = a.SexoPersonaCitada,
                    Status2 = a.Status2,
                    CUPRE = a.CUPRE,
                    Comparece = a.Comparece
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }

        [HttpGet("[action]")]
        
        public async Task<IActionResult> ListarAudiencia()
        {
            try
            {
                var ao = await _context.Agendas
                    .ToListAsync();

                return Ok (ao.Select(a => new AgendaViewModel
                {
                    IdAgenda = a.IdAgenda,
                    RHechoId = a.RHechoId,
                    NumeroOficio = a.NumeroOficio,
                    CausaPenal = a.CausaPenal,
                    Nuc = a.Nuc,
                    Victimas = a.Victimas,
                    Imputado = a.Imputado,
                    Delitos = a.Delitos,
                    Texto = a.Texto,
                    FechaCitacion = a.FechaCitacion,
                    Status = a.Status,
                    Tipo = a.Tipo,
                    DirigidoNombre = a.DirigidoNombre,
                    DirigidoPuesto = a.DirigidoPuesto,
                    ReDireccion = a.ReDireccion,
                    ReTelefono = a.ReTelefono,
                    ReCorreo = a.ReCorreo,
                    ArticulosSancion = a.ArticulosSancion,
                    DireccionImputado = a.DireccionImputado,
                    TelefonoImputado = a.TelefonoImputado,
                    CorreoImputado = a.CorreoImputado,
                    DefensorParticularImp = a.DefensorParticularImp,
                    DomicilioDPI = a.DomicilioDPI,
                    TelefonoDPI = a.TelefonoDPI,
                    CorreoDPI = a.CorreoDPI,
                    InformacionVicAseJu = a.InformacionVicAseJu,
                    InformacionImpDeP = a.InformacionImpDeP,
                    InformacionImp = a.InformacionImp,
                    InformacionDelito = a.InformacionDelito,
                    HoraCitacion = a.HoraCitacion,
                    LugarCitacion = a.LugarCitacion,
                    DescripcionCitacion = a.DescripcionCitacion,
                    DireccionHecho = a.DireccionHecho,
                    HechosIII = a.HechosIII,
                    ClasificacionjIII = a.ClasificacionjIII,
                    CorrelacionArtIII = a.CorrelacionArtIII,
                    ArticuloIII = a.ArticuloIII,
                    ModaidadesVI = a.ModaidadesVI,
                    AutoriaV = a.AutoriaV,
                    Autoria2V = a.Autoria2V,
                    PreceptosVI = a.PreceptosVI,
                    TestimonialVII = a.TestimonialVII,
                    PericialVII = a.PericialVII,
                    DocumentalesVII = a.DocumentalesVII,
                    MaterialVII = a.MaterialVII,
                    AnticipadaVII = a.AnticipadaVII,
                    ArticulosVIII = a.ArticulosVIII,
                    MontoVIII = a.MontoVIII,
                    NumeroLetraVIII = a.NumeroLetraVIII,
                    TestimonialVIII = a.TestimonialVII,
                    PericialVIII = a.PericialVIII,
                    DocumentalesVIII = a.DocumentalesVIII,
                    MaterialVIII = a.MaterialVIII,
                    ArticulosIX = a.ArticulosIX,
                    PenaIX = a.PenaIX,
                    TestimonialesX = a.TestimonialesX,
                    TestimonialX = a.TestimonialX,
                    PericialX = a.PericialX,
                    DocumentalesX = a.DocumentalesX,
                    MaterialX = a.MaterialX,
                    DecomisoXI = a.DecomisoXI,
                    PropuestaXII = a.PropuestaXII,
                    TerminacionXIII = a.TerminacionXIII,
                    Resumen = a.Resumen,
                    Viculada = a.Viculada,
                    PlazoInvestigacion = a.PlazoInvestigacion,
                    Prorroga = a.Prorroga,
                    TiempoProrroga = a.TiempoProrroga,
                    PersonaPresentar = a.PersonaPresentar,
                    Tipo2 = a.Tipo2,
                    UDistrito = a.UDistrito,
                    USubproc = a.USubproc,
                    UAgencia = a.UAgencia,
                    Usuario = a.Usuario,
                    UPuesto = a.UPuesto,
                    UModulo = a.UModulo,
                    Fechasys = a.Fechasys,
                    Aux = a.Aux,
                    SexoPersonaCitada = a.SexoPersonaCitada,
                    Status2 = a.Status2,
                    CUPRE = a.CUPRE,
                    Comparece = a.Comparece
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/Agenda/ListarTodospormodulo2
        [HttpGet("[action]/{modulo}/{Diasagregar}")]
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,Procurador,Recepción,AMPO-IL")]
        public async Task<IActionResult> ListarTodospormodulo2([FromRoute] Guid modulo, int Diasagregar)
        {
            try
            {
                DateTime FechaActual = System.DateTime.Now;

                var ao = await _context.Agendas
                    .OrderByDescending(a => a.Fechasys)
                    .Where(a => a.FechaCitacion.Year == FechaActual.AddDays(Diasagregar).Year)
                    .Where(a => a.FechaCitacion.Month == FechaActual.AddDays(Diasagregar).Month)
                    .Where(a => a.FechaCitacion.Day == FechaActual.AddDays(Diasagregar).Day)
                    .Where(a => a.RHecho.ModuloServicioId == modulo)
                    .ToListAsync();

                string calcularevento(int tipo1, string tipo2)
                {

                    if (tipo1 == 2)
                        return "Audiencia inicial";
                    else if (tipo1 == 3)
                        return "Audiencia inicial con orden de aprehension cumplida";
                    else if (tipo1 == 4)
                        return "Audiencia Orden de Aprehension";
                    else if (tipo1 == 5)
                        return "Audiencia Orden de Comparecencia";
                    else if (tipo1 == 6)
                        return "Formulacion de acusacion";
                    else if (tipo1 == 10)
                        return tipo2;
                    else if (tipo1 == 11)
                        return tipo2;
                    else if (tipo1 == 12)
                        return "Audiencia Orden de Cateo";
                    else
                        return "Error al calcular evento";
                }

                var eventos = ao.Select(a => new EventoHoyMañanaPasadoViewModel
                {
                    Evento = calcularevento(a.Tipo, a.Tipo2),
                    Fecha = a.FechaCitacion
                });

                IEnumerable<EventoHoyMañanaPasadoViewModel> ReadLines()
                {
                    IEnumerable<EventoHoyMañanaPasadoViewModel> item2;

                    item2 = (new[]{new EventoHoyMañanaPasadoViewModel{
                    Evento = "ZKR",
                    Fecha = FechaActual.AddDays(Diasagregar)
                }});

                    return item2;
                }

                eventos = eventos.Concat(ReadLines());

                return Ok(eventos.OrderBy(a => a.Fecha));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }
        }


        // GET: api/Agenda/ListarTodosporrhechoagendados
        [HttpGet("[action]/{fecha}/{rhecho}")]
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        public async Task<IEnumerable<AgendaViewModel>> ListarTodosporrhechoagendados([FromRoute] DateTime fecha, Guid rhecho)
        {
            var ao = await _context.Agendas
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.FechaCitacion != fecha)
                .Where(a => a.RHechoId == rhecho)
                .OrderBy(a => a.FechaCitacion)
                .ToListAsync();

            return ao.Select(a => new AgendaViewModel
            {
                IdAgenda = a.IdAgenda,
                RHechoId = a.RHechoId,
                NumeroOficio = a.NumeroOficio,
                CausaPenal = a.CausaPenal,
                Nuc = a.Nuc,
                Victimas = a.Victimas,
                Imputado = a.Imputado,
                Delitos = a.Delitos,
                Texto = a.Texto,
                FechaCitacion = a.FechaCitacion,
                Status = a.Status,
                Tipo = a.Tipo,
                DirigidoNombre = a.DirigidoNombre,
                DirigidoPuesto = a.DirigidoPuesto,
                ReDireccion = a.ReDireccion,
                ReTelefono = a.ReTelefono,
                ReCorreo = a.ReCorreo,
                ArticulosSancion = a.ArticulosSancion,
                DireccionImputado = a.DireccionImputado,
                TelefonoImputado = a.TelefonoImputado,
                CorreoImputado = a.CorreoImputado,
                DefensorParticularImp = a.DefensorParticularImp,
                DomicilioDPI = a.DomicilioDPI,
                TelefonoDPI = a.TelefonoDPI,
                CorreoDPI = a.CorreoDPI,
                InformacionVicAseJu = a.InformacionVicAseJu,
                InformacionImpDeP = a.InformacionImpDeP,
                InformacionImp = a.InformacionImp,
                InformacionDelito = a.InformacionDelito,
                HoraCitacion = a.HoraCitacion,
                LugarCitacion = a.LugarCitacion,
                DescripcionCitacion = a.DescripcionCitacion,
                DireccionHecho = a.DireccionHecho,
                HechosIII = a.HechosIII,
                ClasificacionjIII = a.ClasificacionjIII,
                CorrelacionArtIII = a.CorrelacionArtIII,
                ArticuloIII = a.ArticuloIII,
                ModaidadesVI = a.ModaidadesVI,
                AutoriaV = a.AutoriaV,
                Autoria2V = a.Autoria2V,
                PreceptosVI = a.PreceptosVI,
                TestimonialVII = a.TestimonialVII,
                PericialVII = a.PericialVII,
                DocumentalesVII = a.DocumentalesVII,
                MaterialVII = a.MaterialVII,
                AnticipadaVII = a.AnticipadaVII,
                ArticulosVIII = a.ArticulosVIII,
                MontoVIII = a.MontoVIII,
                NumeroLetraVIII = a.NumeroLetraVIII,
                TestimonialVIII = a.TestimonialVII,
                PericialVIII = a.PericialVIII,
                DocumentalesVIII = a.DocumentalesVIII,
                MaterialVIII = a.MaterialVIII,
                ArticulosIX = a.ArticulosIX,
                PenaIX = a.PenaIX,
                TestimonialesX = a.TestimonialesX,
                TestimonialX = a.TestimonialX,
                PericialX = a.PericialX,
                DocumentalesX = a.DocumentalesX,
                MaterialX = a.MaterialX,
                DecomisoXI = a.DecomisoXI,
                PropuestaXII = a.PropuestaXII,
                TerminacionXIII = a.TerminacionXIII,
                Resumen = a.Resumen,
                Viculada = a.Viculada,
                PlazoInvestigacion = a.PlazoInvestigacion,
                Prorroga = a.Prorroga,
                TiempoProrroga = a.TiempoProrroga,
                PersonaPresentar = a.PersonaPresentar,
                Tipo2 = a.Tipo2,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Aux = a.Aux,
                SexoPersonaCitada = a.SexoPersonaCitada,
                Status2 = a.Status2,
                CUPRE = a.CUPRE,
                Comparece = a.Comparece
            });
        }

        // GET: api/Agenda/ListarEstadistica
        [HttpGet("[action]/{EventosAgendaEstadistica}")]
        public async Task<IEnumerable<EstadisticaViewModel>> ListarEstadistica([FromQuery] EventosAgendaEstadistica EventosAgendaEstadistica)
        {
            var ao = await _context.Agendas
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.Status != "Iniciado")
                .Where(a => EventosAgendaEstadistica.DatosGenerales.Distritoact ? a.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == EventosAgendaEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => EventosAgendaEstadistica.DatosGenerales.Dspact ? a.RHecho.ModuloServicio.Agencia.DSP.IdDSP == EventosAgendaEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => EventosAgendaEstadistica.DatosGenerales.Agenciaact ? a.RHecho.ModuloServicio.Agencia.IdAgencia == EventosAgendaEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.RHecho.FechaElevaNuc2 >= EventosAgendaEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.RHecho.FechaElevaNuc2 <= EventosAgendaEstadistica.DatosGenerales.Fechahasta)
                .Where(a => EventosAgendaEstadistica.Evento != 0 ? a.Tipo == EventosAgendaEstadistica.Evento : 1 == 1)
                .ToListAsync();

            ao.Select(a => new AgendaViewModel
            {
                IdAgenda = a.IdAgenda,
                RHechoId = a.RHechoId
            });

            IEnumerable<EstadisticaViewModel> items = new EstadisticaViewModel[] { };

            IEnumerable<EstadisticaViewModel> ReadLines(int cantidad, string tipo)
            {
                IEnumerable<EstadisticaViewModel> item2;

                item2 = (new[]{new EstadisticaViewModel{
                                Cantidad = cantidad,
                                Tipo = tipo
                            }});

                return item2;
            }

            string evento(int ev)
            {
                if (ev == 2) return "Audiencias iniciales totales";
                else if(ev == 3) return "Audiencias iniciales con orden de aprehension cumplida totales";
                else if(ev == 4) return "Audiencias Orden de Aprehension totales";
                else if(ev == 5) return "Audiencias Orden de Comparecencia totales";
                else if(ev == 6) return "Formulacion de acusacion totales";
                else if(ev == 10) return "Eventos totales";
                else if(ev == 11) return "Cupres totales";
                else return "Audiencias Orden de Cateo totales";

            }

            items = items.Concat(ReadLines(ao.Count, EventosAgendaEstadistica.Evento != 0 ? evento(EventosAgendaEstadistica.Evento) : "Todos los eventos" ));

            return items;

        }
    }
}
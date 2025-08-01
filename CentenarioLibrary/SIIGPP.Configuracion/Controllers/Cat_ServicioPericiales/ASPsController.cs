using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Configuracion.Models.Cat_ServicioPericiales.ASP;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Configuracion.Cat_ServiciosPericiales;

namespace SIIGPP.Configuracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ASPsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ASPsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/ASPs/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<AsignarServicioViewModel>> Listar()
        {
            var asp = await _context.ASPs.Include(a  => a.Agencia.DSP)
                .Include(a => a.Agencia)
                .Include(a => a.ServicioPericial)
                .Include(a => a.Agencia.DSP.Distrito)
                .ToListAsync();

            return asp.Select(a => new AsignarServicioViewModel
            {
                IdASP = a.IdASP,
                DSPId = a.Agencia.DSPId,
                AgenciaId = a.AgenciaId, 
                NombreAgencia = a.Agencia.Nombre,
                NombreDirSub = a.Agencia.DSP.NombreSubDir, 
                ServicioPericialId = a.ServicioPericialId,  
                NombreServicio = a.ServicioPericial.Servicio,
                NumeroDistrito = a.Agencia.DSP.Distrito.Clave,
                NombreDistrito = a.Agencia.DSP.Distrito.Nombre
            });

        }
        // GET: api/ASPs/ListarStatusSP
        [HttpGet("[action]")]
        public async Task<IEnumerable<ListarPorStatusSPViewModel>> ListarStatusSP()
        {
            var asp = await _context.ASPs.Include(a => a.Agencia.DSP).Include(a => a.Agencia).Include(a => a.ServicioPericial).Where(x => x.ServicioPericial.AtencionVictimas == true).ToListAsync();

            return asp.Select(a => new ListarPorStatusSPViewModel
            {
               
                ServicioPericialId = a.ServicioPericialId, 
                NombreServicio = a.ServicioPericial.Servicio,
               
            });

        }


        // GET: api/ASPs/FiltrarStatusSP
        [HttpGet("[action]/{servicioPericialId}/{agenciaId}")]
        public async Task<IActionResult> FiltrarStatusSP([FromRoute] Guid servicioPericialId, Guid agenciaId)
             
        {
            var asp = await _context.ASPs.Include(a => a.Agencia.DSP).Include(a => a.Agencia).Include(a => a.ServicioPericial).Where(a => a.AgenciaId == agenciaId).Where(x => x.ServicioPericialId == servicioPericialId).FirstOrDefaultAsync();

            if (asp == null)
            {
                return NotFound();
            }
            return Ok( new ListarPorFiltroSPViewModel
            {
                IdASP = asp.IdASP,
                DSPId = asp.Agencia.DSPId,
                AgenciaId = asp.AgenciaId,
                NombreAgencia = asp.Agencia.Nombre,
                NombreDirSub = asp.Agencia.DSP.NombreSubDir,
                Responsable = asp.Agencia.DSP.Responsable,
                ServicioPericialId = asp.ServicioPericialId,
                Codigo = asp.ServicioPericial.Codigo,
                NombreServicio = asp.ServicioPericial.Servicio,
                Descripcion = asp.ServicioPericial.Descripcion,
                Requisitos = asp.ServicioPericial.Requisitos,
                Materia = asp.ServicioPericial.EnMateriaDe,
                AtencionVictimas = asp.ServicioPericial.AtencionVictimas,
            });
        }



        // GET: api/ASPs/ValidarRegistro/1
        [HttpGet("[action]/{agenciaId},{servicioId}")]
        public async Task<IActionResult> ValidarRegistro([FromRoute] Guid agenciaId, Guid servicioId)
        {
           

            var a = await _context.ASPs.Where(x => x.AgenciaId == agenciaId).Where(x => x.ServicioPericialId == servicioId).ToListAsync();

            if (a.Count == 0)
            {
                return Ok("No existe registro");
            }

            return Ok("Existe registro");

        }

        // PUT: api/ASPs/Actualizar
        [Authorize(Roles = " Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var a = await _context.ASPs.FirstOrDefaultAsync(x => x.IdASP == model.IdASP);

            if (a == null)
            {
                return NotFound();
            }


            a.AgenciaId = model.AgenciaId;
            a.ServicioPericialId = model.ServicioPericialId; 


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

        // POST: api/ASPs/Crear
        [Authorize(Roles = " Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ASP a = new ASP
            {
                 
                AgenciaId = model.AgenciaId,
                ServicioPericialId = model.ServicioPericialId,


            };

            _context.ASPs.Add(a);
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

            return Ok();
        }


        // GET: api/ASPs/Idsp
        [HttpGet("[action]{idsp}")]
        public async Task<IActionResult> Idsp([FromRoute] Guid idsp)
        {
            var asp = await _context
                .ASPs.Where(a => a.ServicioPericialId == idsp)
                
                                                        .FirstOrDefaultAsync();

            if (asp == null)
            {
                return BadRequest("No hay registros");

            }
            return Ok(new AsignarServicioViewModel
            {
                AgenciaId = asp.AgenciaId
            });

        }

        // GET: api/ASPs/ListarporIdagencia
        [HttpGet("[action]/{Idagencia}")]
        public async Task<IEnumerable<AsignarServicioViewModel>> ListarporIdagencia([FromRoute]Guid Idagencia)
        {
            var asp = await _context.ASPs.Include(a => a.Agencia.DSP)
                .Where(a => a.AgenciaId == Idagencia)
                .Include(a => a.Agencia)
                .Include(a => a.ServicioPericial)
                .Include(a => a.Agencia.DSP.Distrito)
                .ToListAsync();

            return asp.Select(a => new AsignarServicioViewModel
            {
                IdASP = a.IdASP,
                DSPId = a.Agencia.DSPId,
                AgenciaId = a.AgenciaId,
                NombreAgencia = a.Agencia.Nombre,
                NombreDirSub = a.Agencia.DSP.NombreSubDir,
                ServicioPericialId = a.ServicioPericialId,
                NombreServicio = a.ServicioPericial.Servicio,
                EnMateriaDe = a.ServicioPericial.EnMateriaDe,
                NumeroDistrito = a.Agencia.DSP.Distrito.Clave,
                Descripcion = a.ServicioPericial.Descripcion,
                Requisitos = a.ServicioPericial.Requisitos,


            });

        }

        // GET: api/ASPs/ListarporIdagenciaatencionvictimas
        [HttpGet("[action]/{Idagencia}")]
        public async Task<IEnumerable<AsignarServicioViewModel>> ListarporIdagenciaatencionvictimas([FromRoute] Guid Idagencia)
        {
            var asp = await _context.ASPs.Include(a => a.Agencia.DSP)
                .Where(a => a.AgenciaId == Idagencia)
                .Where(a => a.ServicioPericial.AtencionVictimas)
                .Include(a => a.Agencia)
                .Include(a => a.ServicioPericial)
                .Include(a => a.Agencia.DSP.Distrito)
                .ToListAsync();

            return asp.Select(a => new AsignarServicioViewModel
            {
                IdASP = a.IdASP,
                DSPId = a.Agencia.DSPId,
                AgenciaId = a.AgenciaId,
                NombreAgencia = a.Agencia.Nombre,
                NombreDirSub = a.Agencia.DSP.NombreSubDir,
                ServicioPericialId = a.ServicioPericialId,
                NombreServicio = a.ServicioPericial.Servicio,
                NumeroDistrito = a.Agencia.DSP.Distrito.Clave

            });

        }

        private bool ASPExists(Guid id)
        {
            return _context.ASPs.Any(e => e.IdASP == id);
        }


        // GET: api/ASPs/BuscaAgenciaPericial
        [HttpGet("[action]/{ASPId}")]
        public async Task<IActionResult> BuscaAgenciaPericial([FromRoute] Guid ASPId)

        {
            var agencia = await _context.ASPs.Include(a => a.Agencia.DSP).Include(a => a.Agencia).Where(a => a.IdASP == ASPId).FirstOrDefaultAsync();
            if (agencia == null)
            {
                return NotFound();
            }
            return Ok(new BuscarAgenciaPericialViewModel
            {
                IdASP = agencia.IdASP,
                NombreAgencia = agencia.Agencia.Nombre,
            });
        }
    }
}
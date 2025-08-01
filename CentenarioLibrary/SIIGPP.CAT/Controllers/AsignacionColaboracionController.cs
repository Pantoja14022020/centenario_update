using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.AsignacionColaboraciones;
using SIIGPP.CAT.Models.AsignacionColaboraciones;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionColaboracionController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public AsignacionColaboracionController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/AsignacionColaboracion/Listar
        [HttpGet("[action]/{Idmoduloservicio}")]
        public async Task<IEnumerable<AsignacionColaboracionViewModel>> Listar([FromRoute]Guid Idmoduloservicio)
        {
            var hi = await _context.AsignacionColaboracions
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.ModuloServicioId == Idmoduloservicio)
                .Include(a => a.SColaboracionMP)
                .ToListAsync();

            return hi.Select(a => new AsignacionColaboracionViewModel
            {
                IdAsignacionColaboraciones = a.IdAsignacionColaboraciones,
                ModuloServicioId = a.ModuloServicioId,
                SColaboracionMPId = a.SColaboracionMPId,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                CIdSColaboracionMP = a.SColaboracionMP.IdSColaboracionMP,
                CRHechoId = a.SColaboracionMP.RHechoId,
                CAgenciaId = a.SColaboracionMP.AgenciaId,
                CTexto = a.SColaboracionMP.Texto,
                CTipoColaboracion = a.SColaboracionMP.TipoColaboracion,
                CNUC = a.SColaboracionMP.NUC,
                CUsuarioSolicita = a.SColaboracionMP.UsuarioSolicita,
                CAgenciaOrigen = a.SColaboracionMP.AgenciaOrigen,
                CAgenciaDestino = a.SColaboracionMP.AgenciaDestino,
                CStatus = a.SColaboracionMP.Status,
                CRespuesta = a.SColaboracionMP.Respuesta,
                CFechaRespuesta = a.SColaboracionMP.FechaRespuesta,
                CFechaRechazo = a.SColaboracionMP.FechaRechazo,
                CUDistrito = a.SColaboracionMP.UDistrito,
                CUSubproc = a.SColaboracionMP.USubproc,
                CUAgencia = a.SColaboracionMP.UAgencia,
                CUsuario = a.SColaboracionMP.Usuario,
                CUPuesto = a.SColaboracionMP.UPuesto,
                CUModulo = a.SColaboracionMP.UModulo,
                CFechasys = a.SColaboracionMP.Fechasys,
            });
        }

        // POST: api/AsignacionColaboracion/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AsignacionColaboracion hi = new AsignacionColaboracion
            {
                ModuloServicioId = model.ModuloServicioId,
                SColaboracionMPId = model.SColaboracionMPId,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,                
                Fechasys = System.DateTime.Now,
            };

            _context.AsignacionColaboracions.Add(hi);
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
            return Ok();
        }

    }
}

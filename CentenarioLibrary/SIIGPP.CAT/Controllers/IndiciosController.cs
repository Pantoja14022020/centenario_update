using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.Indicios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Indicios;


namespace SIIGPP.CAT.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IndiciosController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;

        public IndiciosController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // POST: api/Indicios/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Indicios dato = new Indicios
            {
                RHechoId = model.HechoId,
                TipoIndicio = model.TipoIndicio,
                Descripcion = model.Descripcion,
                Status = model.Status,
                QIniciaCadena = model.QIniciaCadena,
                InstitucionQI = model.InstitucionQI,
                Corporacion = model.Corporacion,
                UltimaUbicacion = model.UltimaUbicacion,
                Distrito = model.Distrito,
                Subproc = model.Subproc,
                Agencia = model.Agencia,
                Puesto = model.Puesto,
                Usuario = model.Usuario,
                Modulo = model.Modulo,
                Matricula = model.Matricula,
                Marca = model.Marca,
                Modelo = model.Modelo,
                Calibre = model.Calibre,
                Etiqueta = model.Etiqueta

            };

            _context.Indicios.Add(dato);
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


        // GET: api/Indicios/Listar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<IndicioViewModel>> Listar([FromRoute]Guid RHechoId)
        {
            var Indicio = await _context.Indicios
                .Where(a=> a.RHechoId == RHechoId)
                .ToListAsync();

            return Indicio.Select(a => new IndicioViewModel
            {
                IdIndicio = a.IdIndicio,
                HechoId = a.RHechoId,
                TipoIndicio = a.TipoIndicio,
                Descripcion = a.Descripcion,
                Status = a.Status ,
                QIniciaCadena = a.QIniciaCadena,
                InstitucionQI = a.InstitucionQI,
                Corporacion = a.Corporacion,
                UltimaUbicacion = a.UltimaUbicacion,
                Distrito = a.Distrito,
                Subproc = a.Subproc,
                Agencia = a.Agencia,
                Puesto = a.Puesto,
                Usuario = a.Usuario,
                Modulo = a.Modulo,
                Matricula = a.Matricula,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Calibre = a.Calibre,
                Etiqueta = a.Etiqueta
              
            });

        }

        // POST: api/Indicios/CrearDetalle
        [HttpPost("[action]")]
        [Authorize(Roles = "Perito,Oficialia de partes,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        public async Task<IActionResult> CrearDetalle([FromBody] DetalleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime fecha = System.DateTime.Now;
            DetalleSeguimientoIndicio detalle = new DetalleSeguimientoIndicio
            {
                IndiciosId = model.IndiciosId,
                FechaHora = fecha,
                Fechasys = model.Fechasys,
                OrigenLugar = model.OrigenLugar,
                DestinoLugar = model.DestinoLugar,
                QuienEntrega = model.QuienEntrega,
                QuienRecibe = model.QuienRecibe


            };

            _context.DetalleSeguimientoIndicios.Add(detalle);
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


        // GET: api/Indicios/ListarD
        [HttpGet("[action]/{idindicio}")]
        [Authorize(Roles = "Perito,Oficialia de partes,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        public async Task<IEnumerable<DetalleViewModel>> ListarD([FromRoute] Guid idindicio)
        {
            var detalle= await _context.DetalleSeguimientoIndicios
                .Where(a=> a.IndiciosId == idindicio)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return detalle.Select(a => new DetalleViewModel
            {
                IdDetalles = a.IdDetalles,
                IndiciosId = a.IndiciosId,
                FechaHora = a.FechaHora,
                Fechasys = a.Fechasys,
                OrigenLugar = a.OrigenLugar,
                DestinoLugar = a.DestinoLugar,
                QuienEntrega = a.QuienEntrega,
                QuienRecibe = a.QuienRecibe

            });

        }



    }
}

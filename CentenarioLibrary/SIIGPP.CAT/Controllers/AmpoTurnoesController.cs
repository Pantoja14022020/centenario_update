using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.AmpoTurno;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Turnador;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmpoTurnoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public AmpoTurnoesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/AmpoTurnoes
        [HttpGet]
        public IEnumerable<AmpoTurno> GetAmpoTurnos()
        {
            return _context.AmpoTurnos;
        }

        // POST: api/AmpoTurnoes/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear(CrearViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AmpoTurno InsertarAT = new AmpoTurno
            {
              ModuloServicioId = model.ModuloServicioId,
              TurnoId = model.TurnoId,
            };

            _context.AmpoTurnos.Add(InsertarAT);

            try
            {
               await _context.SaveChangesAsync();

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

            return Ok();
        }
        private bool AmpoTurnoExists(Guid id)
        {
            return _context.AmpoTurnos.Any(e => e.IdAmpoTurno == id);
        }
    }
}
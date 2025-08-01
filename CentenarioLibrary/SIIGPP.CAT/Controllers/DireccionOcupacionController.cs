using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.PersonaDesap;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionOcupacionController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public DireccionOcupacionController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/DireccionOcupacion
        [HttpGet]
        public IEnumerable<DireccionOcupacion> DireccionOcupacion()
        {
            //return new string[] { "value1", "value2" };
            return _context.DireccionOcupacion;
        }

        // GET: api/DireccionOcupacion/BuscarDireccionOcupacion/{PersonaId}
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Administrador")]
        [HttpGet("[action]/{PersonaId}")]
        public async Task<IActionResult> BuscarDireccionOcupacion([FromRoute] Guid PersonaId)
        {
            try
            {
                var dirocup = await _context.DireccionOcupacion
                              .Where(a => a.PersonaId == PersonaId)
                              .FirstOrDefaultAsync();

                

                if (dirocup == null)
                {
                    return Ok();
                }


                return Ok(new ListaDireccionOcupacionViewModel
                {
                    /*CAT_DIRECCION_OCUPACION*/
                    /*************************/
                    IdDOcupacion = dirocup.IdDOcupacion,
                    PersonaId = dirocup.PersonaId,
                    Calle = dirocup.Calle,
                    NoInt = dirocup.NoInt,
                    NoExt = dirocup.NoExt,
                    EntreCalle1 = dirocup.EntreCalle1,
                    EntreCalle2 = dirocup.EntreCalle2,
                    Referencia = dirocup.Referencia,
                    Pais = dirocup.Pais,
                    Estado = dirocup.Estado,
                    Municipio = dirocup.Municipio,
                    Localidad = dirocup.Localidad,
                    CP = dirocup.CP,
                    TipoVialidad = dirocup.TipoVialidad,
                    TipoAsentamiento = dirocup.TipoAsentamiento,
                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }


        // POST: api/DireccionOcpacion/CrearDireccionOcupacion
        [HttpPost("[Action]")]
        public async Task<IActionResult> CrearDireccionOcupacion([FromBody] CrearDireccionOcupacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DireccionOcupacion insertarDireccionOcupacion = new DireccionOcupacion
            {
                PersonaId = model.PersonaId,
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
                TipoVialidad = model.TipoVialidad,
                TipoAsentamiento = model.TipoAsentamiento,
            };

            try
            {
                _context.DireccionOcupacion.Add(insertarDireccionOcupacion);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex);
            }

            return Ok(new { IdDOcupacion = insertarDireccionOcupacion.IdDOcupacion });

        }


        // PUT: api/DireccionOcupacion/ActualizarDireccionOcupacion
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarDireccionOcupacion([FromBody] ActualizarDireccionOcupacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vdireccionocupacion = await _context.DireccionOcupacion.FirstOrDefaultAsync(a => a.IdDOcupacion == model.IdDOcupacion);

            if (vdireccionocupacion == null)
            {
                return NotFound();
            }
            vdireccionocupacion.Calle = model.Calle;
            vdireccionocupacion.NoInt = model.NoInt;
            vdireccionocupacion.NoExt = model.NoExt;
            vdireccionocupacion.EntreCalle1 = model.EntreCalle1;
            vdireccionocupacion.EntreCalle2 = model.EntreCalle2;
            vdireccionocupacion.Referencia = model.Referencia;
            vdireccionocupacion.Pais = model.Pais;
            vdireccionocupacion.Estado = model.Estado;
            vdireccionocupacion.Municipio = model.Municipio;
            vdireccionocupacion.Localidad = model.Localidad;
            vdireccionocupacion.CP = model.CP;
            vdireccionocupacion.TipoVialidad = model.TipoVialidad;
            vdireccionocupacion.TipoAsentamiento = model.TipoAsentamiento;
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

        // DELETE api/<DireccionOcupacionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

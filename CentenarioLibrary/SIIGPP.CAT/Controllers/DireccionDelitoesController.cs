using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.DireccionSuceso;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Direcciones;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionDelitoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;


        public DireccionDelitoesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // GET: api/DireccionDelitoes/ListarPoridHecho
        //[Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador,Perito")]
        [HttpGet("[action]/{idhecho}")]
        public async Task<IActionResult> ListarPoridHecho([FromRoute] Guid idhecho)
        {
            var dp = await _context.DireccionDelitos
                            .Where(x => x.RHechoId == idhecho)
                            .FirstOrDefaultAsync();

            if (dp == null)
            {
                //var result = new ObjectResult(new { statusCode = "404", message = "No se ha registrado dirección del delito"});
                var result = new ObjectResult(new
                {
                    status = "warning",
                    message = "No se ha registrado dirección del delito"
                });
                result.StatusCode = 200;
                return result;
            }

            return Ok(new DireccionDelitoViewModel
            {
                /*********************************************/
                /*CAT_DIRECCIONPERSONA*/
                IdDDelito = dp.IdDDelito,
                IdRHecho = dp.RHechoId,
                LugarEspecifico = dp.LugarEspecifico,
                calle = dp.Calle,
                noint = dp.NoInt,
                noext = dp.NoExt,
                entrecalle1 = dp.EntreCalle1,
                entrecalle2 = dp.EntreCalle2,
                pais = dp.Pais,
                referencia = dp.Referencia,
                estado = dp.Estado,
                municipio = dp.Municipio,
                localidad = dp.Localidad,
                cp = dp.CP,
                lat = dp.lat,
                lng = dp.lng,
                tipoVialidad = dp.TipoVialidad,
                tipoAsentamiento = dp.TipoAsentamiento,
            });

        }


        // POST: api/DireccionDelitoes/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear(CrearViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

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
                    lng = model.lng,
                    TipoVialidad = model.tipoVialidad,
                    TipoAsentamiento = model.tipoAsentamiento,
                };

                _context.DireccionDelitos.Add(InsertarDD); 
               
                //********************************************************************** 

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


        // POST: api/DireccionDelitoes/Actualizar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var direccio = await _context.DireccionDelitos.FirstOrDefaultAsync(a => a.IdDDelito == model.IdDDelito);
            if (direccio == null)
            {
                return NotFound();
            }
            direccio.LugarEspecifico = model.LugarEspecifico;
            direccio.Calle = model.calle;
            direccio.NoExt = model.noext;
            direccio.NoInt = model.noint;
            direccio.EntreCalle1 = model.entrecalle1;
            direccio.EntreCalle2 = model.entrecalle2;
            direccio.Referencia = model.referencia;
            direccio.Pais = model.pais;
            direccio.Estado = model.estado;
            direccio.Municipio = model.municipio;
            direccio.Localidad = model.localidad;
            direccio.CP = model.cp;
            direccio.lat = model.lat;
            direccio.lng = model.lng;
            direccio.TipoVialidad = model.tipoVialidad;
            direccio.TipoAsentamiento = model.tipoAsentamiento;
            

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

        // GET: api/DireccionDelitoes/EstadisticaMLC
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{fechai}/{fechaf}/{distrito}/{status}/{municipio}/{localidad}/{colonia}/{fechhrinicio}/{fechhrfin}")]
        public async Task<IEnumerable<EstadisticaViewModel>> EstadisticaMLC([FromRoute] DateTime fechai, DateTime fechaf, string distrito, string status, string municipio, string localidad, string colonia, DateTime fechhrinicio, DateTime fechhrfin)
        {
            var delitos = await _context.DireccionDelitos
                          .Include(a => a.rHecho)
                          .Where(a => a.rHecho.FechaElevaNuc2 >= fechai)
                          .Where(a => a.rHecho.FechaElevaNuc2 <= fechaf)
                          .Where(a => (distrito != "ZKR" ? a.rHecho.RAtencion.DistritoInicial == distrito : a.rHecho.RAtencion.DistritoInicial != distrito))
                          .Where(a => (status != "ZKR" ? a.rHecho.NUCs.StatusNUC == status : a.rHecho.NUCs.StatusNUC != status))
                          .Where(a => (municipio != "ZKR" ? a.Municipio == municipio : a.Municipio != municipio))
                          .Where(a => (localidad != "ZKR" ? a.Localidad == localidad : a.Localidad != localidad))
                          .Where(a => (colonia != "ZKR" ? a.Calle == colonia : a.Calle != colonia))
                          .Where(a => a.rHecho.FechaHoraSuceso2.TimeOfDay >= fechhrinicio.TimeOfDay && a.rHecho.FechaHoraSuceso2.TimeOfDay <= fechhrfin.TimeOfDay)
                          .Where(a => a.rHecho.FechaHoraSuceso2.Date >= fechhrinicio.Date && a.rHecho.FechaHoraSuceso2.Date <= fechhrfin.Date)
                          .ToListAsync();

            return delitos.Select(a => new EstadisticaViewModel
            {
                Municipio = a.Municipio,
                Localidad = a.Localidad,
                Colonia = a.Calle,
                rHechoId = a.RHechoId

            });

        }


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/DireccionDelitoes/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultadDDelito = await _context.DireccionDelitos
                                   .Where(x => x.RHechoId == model.IdRHecho)
                                   .Take(1)
                                   .FirstOrDefaultAsync();




                if (consultadDDelito == null)
                {
                    return Ok();

                }
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var InsertarDD = await ctx.DireccionDelitos.FirstOrDefaultAsync(a => a.RHechoId == consultadDDelito.RHechoId);

                    if (InsertarDD == null)
                    {
                        InsertarDD = new DireccionDelito();
                        ctx.DireccionDelitos.Add(InsertarDD);
                    }


                    InsertarDD.IdDDelito = consultadDDelito.IdDDelito;
                    InsertarDD.RHechoId = consultadDDelito.RHechoId;
                    InsertarDD.LugarEspecifico = consultadDDelito.LugarEspecifico;
                    InsertarDD.Calle = consultadDDelito.Calle;
                    InsertarDD.NoInt = consultadDDelito.NoInt;
                    InsertarDD.NoExt = consultadDDelito.NoExt;
                    InsertarDD.EntreCalle1 = consultadDDelito.EntreCalle1;
                    InsertarDD.EntreCalle2 = consultadDDelito.EntreCalle2;
                    InsertarDD.Referencia = consultadDDelito.Referencia;
                    InsertarDD.Pais = consultadDDelito.Pais;
                    InsertarDD.Estado = consultadDDelito.Estado;
                    InsertarDD.Municipio = consultadDDelito.Municipio;
                    InsertarDD.Localidad = consultadDDelito.Localidad;
                    InsertarDD.CP = consultadDDelito.CP;
                    InsertarDD.lat = consultadDDelito.lat;
                    InsertarDD.lng = consultadDDelito.lng;
                    InsertarDD.TipoVialidad = consultadDDelito.TipoVialidad;
                    InsertarDD.TipoAsentamiento = consultadDDelito.TipoAsentamiento;

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

        private bool DireccionDelitoExists(Guid id)
        {
            return _context.DireccionDelitos.Any(e => e.IdDDelito == id);
        }

    }
}
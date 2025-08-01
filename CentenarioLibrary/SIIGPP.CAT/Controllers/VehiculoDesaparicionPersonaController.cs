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
using SIIGPP.Entidades.M_Cat.PersonaDesap;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoDesaparicionPersonaController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public VehiculoDesaparicionPersonaController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //GET: api/VehiculoDesaparicionPersona
        [HttpGet]
        public IEnumerable<VehiculoPersonaDesap> VehiculoPersonaDesap()
        {
            return _context.VehiculoPersonaDesap;
        }

        // GET: api/VehiculoDesaparicionPersona/BuscarVehiculos/{PersonaDesaparecidaId}
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Administrador")]
        [HttpGet("[action]/{PersonaDesaparecidaId}")]
        public async Task<IActionResult> BuscarVehiculos([FromRoute] Guid PersonaDesaparecidaId)
        {
            try
            {
                var datosveh = await _context.VehiculoPersonaDesap
                          .Include(a => a.tipov)
                          .Include(a => a.ano)
                          .Include(a => a.color)
                          .Include(a => a.modelo)
                          .Include(a => a.marca)
                          .Include(a => a.Estado)
                          .Where(a => a.PersonaDesaparecidaId == PersonaDesaparecidaId)
                          .ToListAsync();
            //
            

                return Ok(datosveh.Select(a => new ListarDatosVehiculoDesaparicionPersonaViewModel
                {
                    IdVehDesaparicionPersona = a.IdVehDesaparicionPersona,
                    PersonaDesaparecidaId = a.PersonaDesaparecidaId,
                    TipovId = a.TipovId,
                    Tipov = a.tipov.Dato,
                    AnoId = a.AnoId,
                    Ano = a.ano.Dato,
                    ColorId = a.ColorId,
                    Color = a.color.Dato,
                    ModeloId = a.ModeloId,
                    Modelo = a.modelo.Dato,
                    MarcaId = a.MarcaId,
                    Marca = a.marca.Dato,
                    Serie = a.Serie,
                    Placas = a.Placas,
                    SenasParticulares = a.SenasParticulares,
                    NoSerieMotor = a.NoSerieMotor,
                    Propietario = a.Propietario,
                    Ruta = a.Ruta,
                    iEstado = a.EstadoId,
                    sEstado = a.Estado.Nombre
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/VehiculoDesaparicionPersona/Crear
        //[Authorize(Roles = "Administrador, AMPO-AMP, Director, Coordinador, AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearVehiculoInvolucradoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehiculoPersonaDesap insertarvehiculoDesap = new VehiculoPersonaDesap
            {
                PersonaDesaparecidaId = model.PersonaDesaparecidaId,
                TipovId = model.TipovId,
                AnoId = model.AnoId,
                ColorId = model.ColorId,
                ModeloId = model.ModeloId,
                MarcaId = model.MarcaId,
                Serie = model.Serie,
                Placas = model.Placas,
                SenasParticulares = model.SenasParticulares,
                NoSerieMotor = model.NoSerieMotor,
                Propietario = model.Propietario,
                Ruta = model.Ruta,
                EstadoId = model.EstadoId,
                Privado = model.Privado
            };

            _context.VehiculoPersonaDesap.Add(insertarvehiculoDesap);

            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { idvehdesaparicionpersona = insertarvehiculoDesap.IdVehDesaparicionPersona});
        }
        // POST: api/VehiculoDesaparicionPersona/Clonar
        //[Authorize(Roles = "Administrador, AMPO-AMP, Director, Coordinador, AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            try
            {
                var consultaVehiculosInvolucrados = await _context.VehiculoPersonaDesap
                    .Include(a => a.personadesap)
                    .Where(x => x.personadesap.RHechoId == model.IdRHecho).ToListAsync();


                if (consultaVehiculosInvolucrados != null)
                {
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {
                        foreach (VehiculoPersonaDesap vehiculoPersonaDesapActual in consultaVehiculosInvolucrados)
                        {
                            var insertarVehiculoInvolucrado = await ctx.VehiculoPersonaDesap.FirstOrDefaultAsync(a => a.IdVehDesaparicionPersona == vehiculoPersonaDesapActual.IdVehDesaparicionPersona);
                            if (insertarVehiculoInvolucrado == null)
                            {
                                insertarVehiculoInvolucrado = new VehiculoPersonaDesap();
                                ctx.VehiculoPersonaDesap.Add(insertarVehiculoInvolucrado);
                            }
                            insertarVehiculoInvolucrado.IdVehDesaparicionPersona = vehiculoPersonaDesapActual.IdVehDesaparicionPersona;
                            insertarVehiculoInvolucrado.TipovId = vehiculoPersonaDesapActual.TipovId;
                            insertarVehiculoInvolucrado.AnoId = vehiculoPersonaDesapActual.AnoId;
                            insertarVehiculoInvolucrado.ColorId = vehiculoPersonaDesapActual.ColorId;
                            insertarVehiculoInvolucrado.ModeloId = vehiculoPersonaDesapActual.ModeloId;
                            insertarVehiculoInvolucrado.MarcaId = vehiculoPersonaDesapActual.MarcaId;
                            insertarVehiculoInvolucrado.PersonaDesaparecidaId = vehiculoPersonaDesapActual.PersonaDesaparecidaId;
                            insertarVehiculoInvolucrado.Serie = vehiculoPersonaDesapActual.Serie;
                            insertarVehiculoInvolucrado.Placas = vehiculoPersonaDesapActual.Placas;
                            insertarVehiculoInvolucrado.SenasParticulares = vehiculoPersonaDesapActual.SenasParticulares;
                            insertarVehiculoInvolucrado.NoSerieMotor = vehiculoPersonaDesapActual.NoSerieMotor;
                            insertarVehiculoInvolucrado.Propietario = vehiculoPersonaDesapActual.Propietario;
                            insertarVehiculoInvolucrado.Ruta = vehiculoPersonaDesapActual.Ruta;
                            insertarVehiculoInvolucrado.EstadoId = vehiculoPersonaDesapActual.EstadoId;
                            insertarVehiculoInvolucrado.Privado = vehiculoPersonaDesapActual.Privado;
                            await ctx.SaveChangesAsync();
                        }

                    }
                }
                    return Ok();
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

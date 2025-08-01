using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.DDerivacion;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.DDerivacion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDDerivacionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public RDDerivacionsController(DbContextSIIGPP context)
        {
            _context = context;
        }


        // GET: api/RDDerivacions/Listar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{rHechoId}")]

        public async Task<IEnumerable<DDerivacionViewModel>> Listar([FromRoute] Guid rHechoId)
        {
            var dd = await _context.RDDerivaciones
                            .Include(a => a.DependeciasDerivacion)
                            .Include(a => a.RHecho)
                            .Where(x => x.rHechoId == rHechoId).ToListAsync();


            return dd.Select(a => new DDerivacionViewModel

            {
                /*********************************************/

                idRDDerivacion = a.idRDDerivacion,
                rHechoId = a.rHechoId,
                DDerivacionId = a.idDDerivacion,
                Espesificaciones = a.Espesificaciones,
                FechaDerivacion = a.FechaDerivacion,
                FechaSys = a.FechaSys,
                NombreDDerivacion = a.DependeciasDerivacion.Nombre,
                DireccionDDerivacion = a.DependeciasDerivacion.Direccion,
                Telefono = a.DependeciasDerivacion.Telefono,
                Contacto = a.DependeciasDerivacion.Contacto,
                uDistrito = a.uDistrito,
                uDirSubPro = a.uDirSubPro,
                uAgencia = a.uAgencia,
                uNombre = a.uNombre,
                UPuesto = a.UPuesto,

            });

        }

        // POST: api/RDDerivacions/CrearDD
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearDD(CrearViewModel model)

        {
            Guid idderivacion;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            RDDerivacion InsertarRDD = new RDDerivacion
            {
                rHechoId = model.rHechoId,
                idDDerivacion = model.DDerivacionId,
                Espesificaciones = model.Espesificaciones,
                FechaDerivacion = model.FechaDerivacion,
                FechaSys = fecha,
                uDistrito = model.uDistrito,
                uDirSubPro = model.uDirSubPro,
                uAgencia = model.uAgencia,
                uNombre = model.uNombre,
                UPuesto = model.UPuesto,

            };

            _context.RDDerivaciones.Add(InsertarRDD);

            try
            {
                await _context.SaveChangesAsync();
                idderivacion = InsertarRDD.idDDerivacion;

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

            return Ok(new { idderivacion = idderivacion});
            }


            private bool RDDerivacionExists(Guid id)
        {
            return _context.RDDerivaciones.Any(e => e.idRDDerivacion == id);
        }
    }
}
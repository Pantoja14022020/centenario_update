using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.DatosProtegidos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.DatosProtegidos;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosProtegidoController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public DatosProtegidoController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: api/DatosProtegido/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RatencionId}")]
        public async Task<ActionResult>Listar([FromRoute]Guid RatencionId)


        {
            /*var per = await _context.DatoProtegidos
                .OrderByDescending(a => a.Fechasys)
                .Where(a => a.RAP.RAtencionId==RatencionId)
                .Include(a => a.RAP.Persona)
                .ToListAsync();*/

            String busquedaRepresentantes = @"select a.IdDatosProtegidos,
                                                    a.RAPId
                                                    ,p.Alias,
                                                    a.Nombre,
                                                    a.APaterno,
                                                    a.AMaterno,
                                                    a.FechaNacimiento,
                                                    a.CURP,
                                                    a.RFC,
                                                    a.Rutadocumento,
                                                    a.UDistrito,
                                                    a.USubproc,
                                                    a.UAgencia,
                                                    a.Usuario,
                                                    a.UPuesto,
                                                    a.UModulo,
                                                    a.Fechasys,
                                                    p.Telefono1,
                                                    p.Telefono2,
                                                    CONCAT(
                                                      CASE WHEN v.Nombre IS NOT NULL THEN v.Nombre ELSE '' END,
                                                      CASE WHEN LTRIM(RTRIM(COALESCE(d.Calle, ''))) <> '' THEN CASE WHEN v.Nombre IS NOT NULL THEN ' ' + d.Calle ELSE d.Calle END ELSE '' END,
                                                      CASE WHEN LTRIM(RTRIM(COALESCE(d.NoExt, ''))) <> '' THEN CASE WHEN v.Nombre IS NOT NULL OR LTRIM(RTRIM(COALESCE(d.Calle, ''))) <> '' THEN ', ' + d.NoExt ELSE d.NoExt END ELSE '' END,
                                                      CASE WHEN LTRIM(RTRIM(COALESCE(d.Localidad, ''))) <> '' THEN ', ' + d.Localidad ELSE '' END,
                                                      CASE WHEN d.CP IS NOT NULL THEN ', ' + CAST(d.CP AS VARCHAR) ELSE '' END,
                                                      CASE WHEN LTRIM(RTRIM(COALESCE(d.Municipio, ''))) <> '' THEN ', ' + d.Municipio ELSE '' END,
                                                      CASE WHEN LTRIM(RTRIM(COALESCE(d.Estado, ''))) <> '' THEN ', ' + d.Estado ELSE '' END,
                                                      CASE WHEN LTRIM(RTRIM(COALESCE(d.Pais, ''))) <> '' THEN ', ' + d.Pais ELSE '' END
                                                    ) AS direc
                                                    from CAT_RAP  as r
                                                    left join CAT_PERSONA as p on r.PersonaId = p.IdPersona
                                                    left join CAT_DIRECCION_PERSONAL as d on d.PersonaId=p.IdPersona
                                                    left join CAT_DATO_PROTEGIDO as a on a.RAPId=r.IdRAP
                                                    left join C_TIPO_VIALIDAD as v on d.TipoVialidad = v.Clave
                                                    where
                                                    1=1
                                                    AND p.DatosProtegidos=1
                                                    AND r.RAtencionId=@RatencionId
                                                    ";


            List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
            filtrosBusqueda.Add(new SqlParameter("@RatencionId", RatencionId));
            var per = await _context.qBusquedaDatosProtegidos.FromSqlRaw(busquedaRepresentantes, filtrosBusqueda.ToArray()).ToListAsync();


            return Ok(per.Select(a => new DatosProtegidoViewModel
            {
                IdDatosProtegidos = a.IdDatosProtegidos,
                RAPId = a.RAPId,
                Alias= a.Alias,
                Nombre = a.Nombre,
                APaterno = a.APaterno,
                AMaterno = a.AMaterno,
                FechaNacimiento = a.FechaNacimiento,
                CURP = a.CURP,
                RFC = a.RFC,
                Rutadocumento=a.Rutadocumento, 
                UDistrito = a.UDistrito,
                USubproc =a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                //--------------------------------------------
                Telefono1 = a.Telefono1,
                Telefono2 = a.Telefono2,
                direc=a.direc


            }));

        }

        // POST: api/DatosProtegido/Crear ddddd
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var consultaDato = await _context.DatoProtegidos.Where(x => x.RAPId == model.RAPId).Take(1).FirstOrDefaultAsync();

                var consultaRAP = await _context.RAPs.Where(x => x.IdRAP == model.RAPId).Take(1).FirstOrDefaultAsync();

                if (consultaRAP != null && consultaDato == null)
                {

                    DatoProtegido Dato = new DatoProtegido
                    {
                        RAPId = model.RAPId,
                        Nombre = model.Nombre,
                        APaterno = model.APaterno,
                        AMaterno = model.AMaterno,
                        FechaNacimiento = model.FechaNacimiento,
                        CURP = model.CURP,
                        RFC = model.RFC,
                        Rutadocumento = model.Rutadocumento,
                        UDistrito = model.UDistrito,
                        USubproc = model.USubproc,
                        UAgencia = model.UAgencia,
                        Usuario = model.Usuario,
                        UPuesto = model.UPuesto,
                        UModulo = model.UModulo,
                        Fechasys = System.DateTime.Now,
                    };
                    _context.DatoProtegidos.Add(Dato);
                }

                else
                {

                    consultaDato.RAPId = model.RAPId;
                    consultaDato.Nombre = model.Nombre;
                    consultaDato.APaterno = model.APaterno;
                    consultaDato.AMaterno = model.AMaterno;
                    consultaDato.FechaNacimiento = model.FechaNacimiento;
                    consultaDato.CURP = model.CURP;
                    consultaDato.RFC = model.RFC;
                    consultaDato.Rutadocumento = model.Rutadocumento;
                    consultaDato.UDistrito = model.UDistrito;
                    consultaDato.USubproc = model.USubproc;
                    consultaDato.UAgencia = model.UAgencia;
                    consultaDato.Usuario = model.Usuario;
                    consultaDato.UPuesto = model.UPuesto;
                    consultaDato.UModulo = model.UModulo;
                    consultaDato.Fechasys = System.DateTime.Now;



                }
                await _context.SaveChangesAsync();
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


        //Nueva API de actualizar para caso donde se requiere modificar datos de un dato protegido pero no se quiere volver a ingresar nombre y apellidos de la persona
        //Nombre y apellidos los deja intactos y actualiza los otros datos
        //Usada en m_cat/src/components/Victimaidti/linea 4247 aprox
        // POST: api/DatosProtegido/actualizar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> actualizar ([FromBody] CrearViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var consultaDato = await _context.DatoProtegidos.Where(x => x.RAPId == model.RAPId).Take(1).FirstOrDefaultAsync();

                    consultaDato.RAPId = model.RAPId;
                    consultaDato.FechaNacimiento = model.FechaNacimiento;
                    consultaDato.CURP = model.CURP;
                    consultaDato.RFC = model.RFC;
                    consultaDato.Rutadocumento = model.Rutadocumento;
                    consultaDato.UDistrito = model.UDistrito;
                    consultaDato.USubproc = model.USubproc;
                    consultaDato.UAgencia = model.UAgencia;
                    consultaDato.Usuario = model.Usuario;
                    consultaDato.UPuesto = model.UPuesto;
                    consultaDato.UModulo = model.UModulo;
                    consultaDato.Fechasys = System.DateTime.Now;

                await _context.SaveChangesAsync();
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

        // POST: api/DatosProtegido/Clonar
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var consultaDatoProtegido = await _context.DatoProtegidos
                                            .Include(x => x.RAP)
                                            .Where(x => x.RAP.RAtencion.IdRAtencion == model.IdRAtencion)
                                            .Take(1)
                                            .FirstOrDefaultAsync();


                if (consultaDatoProtegido == null)
                {
                    return Ok();

                }
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var InsertarDP = await ctx.DatoProtegidos.FirstOrDefaultAsync(a => a.RAPId == consultaDatoProtegido.RAPId);

                    if (InsertarDP == null)
                    {
                        InsertarDP = new DatoProtegido();
                        ctx.DatoProtegidos.Add(InsertarDP);
                    }


                    InsertarDP.IdDatosProtegidos = consultaDatoProtegido.IdDatosProtegidos;
                    InsertarDP.RAPId = consultaDatoProtegido.RAPId;
                    InsertarDP.Nombre = consultaDatoProtegido.Nombre;
                    InsertarDP.APaterno = consultaDatoProtegido.APaterno;
                    InsertarDP.AMaterno = consultaDatoProtegido.AMaterno;
                    InsertarDP.FechaNacimiento = consultaDatoProtegido.FechaNacimiento;
                    InsertarDP.CURP = consultaDatoProtegido.CURP;
                    InsertarDP.RFC = consultaDatoProtegido.RFC;
                    InsertarDP.Rutadocumento = consultaDatoProtegido.Rutadocumento;
                    InsertarDP.UDistrito = consultaDatoProtegido.UDistrito;
                    InsertarDP.USubproc = consultaDatoProtegido.USubproc;
                    InsertarDP.UAgencia = consultaDatoProtegido.UAgencia;
                    InsertarDP.Usuario = consultaDatoProtegido.Usuario;
                    InsertarDP.UPuesto = consultaDatoProtegido.UPuesto;
                    InsertarDP.UModulo = consultaDatoProtegido.UModulo;
                    InsertarDP.Fechasys = consultaDatoProtegido.Fechasys;

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

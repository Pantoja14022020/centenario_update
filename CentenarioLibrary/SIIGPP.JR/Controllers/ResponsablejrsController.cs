using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RResponsable;
using SIIGPP.JR.Models.RRespresentatne;
using SIIGPP.JR.Models.RSolicitanteRequerido;
//using MoreLinq;
using SIIGPP.Entidades.M_JR.REnvio;
using SIIGPP.Entidades.M_JR.RAsignacionEnvios;
using Microsoft.Data.SqlClient;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsablejrsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public ResponsablejrsController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Responsablejrs/ResponsaableXPersona  
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idPersona}")]
        public async Task<IActionResult> ResponsaableXPersona([FromRoute] Guid idPersona)
        {
            var Tablajr = await _context.Responsablejrs
                                .Where(a => a.PersonaId == idPersona)
                                .FirstOrDefaultAsync();

            if (Tablajr == null)
            {

                return Ok(new { Responsable = false });

            }

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            return Ok(new GET_RepresentenateVM1
            {
                IdResponsable = Tablajr.IdResponsable,
                NombreCompleto = Tablajr.Nombre + " " + Tablajr.ApellidoPa + " " + Tablajr.ApellidoMa,
                Edad = Tablajr.Edad,
                FechaNacimiento = Tablajr.FechaNacimiento,
                Nacionalidad = Tablajr.Nacionalidad,
                CURP = Tablajr.CURP,
                Telefono = Tablajr.Telefono,
                Correo = Tablajr.Correo,
                Direccion = $"{(Tablajr.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == Tablajr.TipoVialidad)?.Nombre + " " : "")}" +
                             $"{Tablajr.Calle} {(Tablajr.NoInt != null || Tablajr.NoInt != "" ? Tablajr.NoInt : "S/N")} {(Tablajr.NoExt != null || Tablajr.NoExt != "" ? Tablajr.NoExt : "")} {Tablajr.Localidad} {Tablajr.Municipio} {Tablajr.Estado} {Tablajr.Pais}",
                Responsable = true,

            });
        }

        // GET: api/Responsablejrs/SelectIdPersona  
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idPersona}")]
        public async Task<IActionResult> MostrarPersonaJR([FromRoute] Guid idPersona)
        {
            var Tabla = await _context.Responsablejrs
                                .Where(a => a.PersonaId == idPersona)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {

                return Ok(new { ner = 1 });
            }
            else
            {
                return Ok(new GET_RepresentenateVM
                {
                    IdResponsable = Tabla.IdResponsable,
                    PersonaId = Tabla.PersonaId,
                    Nombre = Tabla.Nombre,
                    ApellidoPa = Tabla.ApellidoPa,
                    ApellidoMa = Tabla.ApellidoMa,
                    Edad = Tabla.Edad,
                    FechaNacimiento = Tabla.FechaNacimiento,
                    Nacionalidad = Tabla.Nacionalidad,
                    CURP = Tabla.CURP,
                    Telefono = Tabla.Telefono,
                    Correo = Tabla.Correo,
                    Calle = Tabla.Calle,
                    NoExt = Tabla.NoExt,
                    NoInt = Tabla.NoInt,
                    EntreCalle1 = Tabla.EntreCalle1,
                    EntreCalle2 = Tabla.EntreCalle2,
                    Referencia = Tabla.Referencia,
                    Pais = Tabla.Pais,
                    Estado = Tabla.Estado,
                    Municipio = Tabla.Municipio,
                    Localidad = Tabla.Localidad,
                    CodigoPostal = Tabla.CodigoPostal,
                    TipoVialidad = Tabla.TipoVialidad,
                    TipoAsentamiento = Tabla.TipoAsentamiento,
                });
            }
        }

        // PUT: api/Responsablejrs/Actualizar
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] PUT_RepresentatneVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var tabla = await _context.Responsablejrs.FirstOrDefaultAsync(a => a.IdResponsable == model.IdResponsable);

            if (tabla == null)
            {
                return NotFound();
            }
            tabla.PersonaId = model.PersonaId;
            tabla.Nombre = model.Nombre;
            tabla.ApellidoPa=model.ApellidoPa;
            tabla.ApellidoMa = model.ApellidoMa;
            tabla.Nacionalidad = model.Nacionalidad;
            tabla.Edad = model.Edad;
            tabla.FechaNacimiento = model.FechaNacimiento;  
            tabla.CURP = model.CURP;
            tabla.Telefono = model.Telefono;
            tabla.Correo = model.Correo;
            tabla.Calle = model.Calle;
            tabla.NoExt = model.NoExt;
            tabla.NoInt = model.NoInt;
            tabla.EntreCalle1 = model.EntreCalle1;
            tabla.EntreCalle2 = model.EntreCalle2;
            tabla.Referencia = model.Referencia;
            tabla.Pais = model.Pais;
            tabla.Estado = model.Estado;
            tabla.Municipio = model.Municipio;
            tabla.Localidad = model.Localidad;
            tabla.CodigoPostal = model.CodigoPostal;
            tabla.TipoVialidad = model.TipoVialidad;
            tabla.TipoAsentamiento = model.TipoAsentamiento;
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

        // PUT: api/Responsablejrs/Actualizar
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Reasignar([FromBody] PUT_ReasignarExpediente model)
        {
           
            var tabla = await _context.AsignacionEnvios.FirstOrDefaultAsync(a => a.IdAsingacionEnvio == model.asignacionEnvioId);

            if (tabla == null)
            {
                return NotFound("No se encontraron registros en tabla");
            }
            tabla.ModuloServicioId = model.ModuloServicioId;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Guardar Excepción
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }

        // POST: api/Responsablejrs/Crear
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] POST_RepresentatneVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Responsablejr responsablejr = new  Responsablejr
            {
                PersonaId = model.PersonaId,
                Nombre = model.Nombre,
                ApellidoPa = model.ApellidoPa,
                ApellidoMa = model.ApellidoMa,
                Nacionalidad = model.Nacionalidad,
                Edad = model.Edad,
                FechaNacimiento = model.FechaNacimiento,
                CURP = model.CURP,
                Telefono = model.Telefono,
                Correo = model.Correo,
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
                CodigoPostal = model.CodigoPostal,
                TipoVialidad = model.TipoVialidad,
                TipoAsentamiento = model.TipoAsentamiento,
        };

            _context.Responsablejrs.Add(responsablejr);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }

        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{idPersona}")]
        public async Task<IActionResult> RepresentanteAllActive([FromRoute] Guid idPersona)
        {
            var Tabla = await _context.Responsablejrs
                                .Where(a => a.PersonaId == idPersona)
                                .FirstOrDefaultAsync();

            if (Tabla == null)
            {                
                return Ok(new { exists = false });               
            }


            return Ok(new GET_RepresentenateVM
            {
                IdResponsable = Tabla.IdResponsable,
                PersonaId = Tabla.PersonaId,
                Nombre = Tabla.Nombre,
                ApellidoPa = Tabla.ApellidoPa,
                ApellidoMa = Tabla.ApellidoMa,
                Nacionalidad = Tabla.Nacionalidad,
                Edad = Tabla.Edad,
                FechaNacimiento = Tabla.FechaNacimiento,
                CURP = Tabla.CURP,
                Telefono = Tabla.Telefono,
                Correo = Tabla.Correo,
                Calle = Tabla.Calle,
                NoExt = Tabla.NoExt,
                NoInt = Tabla.NoInt,
                EntreCalle1 = Tabla.EntreCalle1,
                EntreCalle2 = Tabla.EntreCalle2,
                Referencia = Tabla.Referencia,
                Pais = Tabla.Pais,
                Estado = Tabla.Estado,
                Municipio = Tabla.Municipio,
                Localidad = Tabla.Localidad,
                CodigoPostal = Tabla.CodigoPostal,
                TipoVialidad = Tabla.TipoVialidad,
                TipoAsentamiento = Tabla.TipoAsentamiento,
            });
        }

        // GET: api/SolicitanteRequeridoes/ListarTodos
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto, Notificador")]
       [HttpGet("[action]/{EnvioId}/{idRepre}")]
        public async Task<IActionResult> SaveautoRepresentantes([FromRoute] Guid EnvioId, [FromRoute] Guid idRepre)
        {
            try
            {
                String tabla = @"SELECT 
                                           r.IdRepresentante,
                                           p.IdPersona, 
                                           r.Nombres, 
                                           r.ApellidoPa, 
                                           r.ApellidoMa, 
                                           r.Nacionalidad, 
                                           r.FechaNacimiento, 
                                           r.CURP, 
                                           r.Telefono, 
                                           r.CorreoElectronico, 
                                           r.Calle, 
                                           r.NoExt, 
                                           r.NoInt, 
                                           r.EntreCalle1, 
                                           r.EntreCalle2, 
                                           r.Referencia, 
                                           r.Pais, 
                                           r.Estado, 
                                           r.Municipio, 
                                           r.Localidad, 
                                           r.CP,
                                           r.TipoVialidad,
                                           r.TipoAsentamiento,
                                           CASE WHEN re.IdResponsable is not null THEN 1 ELSE 0 END as HayRepreJR,
                                            CASE WHEN r.IdRepresentante is not null THEN 1 ELSE 0 END as HayRepreCAT
                                    FROM CAT_PERSONA p
                                           LEFT JOIN JR_SOLICITANTEREQUERIDO s ON p.IdPersona = s.PersonaId 
                                           LEFT JOIN CAT_REPRESENTANTES as r on r.PersonaId = p.IdPersona
                                           LEFT JOIN JR_RESPONSABLE as re on re.PersonaId = p.IdPersona
                                    WHERE s.EnvioId = @envioid 
                                    AND r.IdRepresentante = @idrepre";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>
                {
                    new SqlParameter("@envioid", EnvioId),
                    new SqlParameter("@idrepre", idRepre)
                };

                var representante = await _context.ConsultaRepresentantesenCats.FromSqlRaw(tabla, filtrosBusqueda.ToArray()).ToListAsync();

                var personasUnicas = representante.DistinctBy(a => a.IdPersona);

                if (representante.Count == 0)
                {

                    return Ok(new { NoHayAr = 1 });

                }
                else
                {
                    return Ok(personasUnicas.Select(a => new GET_RepresentenateCatparaJRVM
                    {
                        IdRepresentante = a.IdRepresentante ?? Guid.Empty,
                        PersonaId = a.IdPersona,                 
                        Nombre = a.Nombres,
                        ApellidoPa = a.ApellidoPa,
                        ApellidoMa = a.ApellidoMa,
                        Nacionalidad = a.Nacionalidad,
                        Edad = 0,
                        FechaNacimiento = a.FechaNacimiento,
                        CURP = a.CURP,
                        Telefono = a.Telefono,
                        CorreoElectronico = a.CorreoElectronico,
                        Calle = a.Calle,
                        NoExt = a.NoExt,
                        NoInt = a.NoInt,
                        EntreCalle1 = a.EntreCalle1,
                        EntreCalle2 = a.EntreCalle2,
                        Referencia = a.Referencia,
                        Pais = a.Pais,
                        Estado = a.Estado,
                        Municipio = a.Municipio,
                        Localidad = a.Localidad,
                        CodigoPostal = a.CP,
                        HayRepreJR = a.HayRepreJR,
                        HayRepreCAT = a.HayRepreCAT,
                        TipoVialidad = a.TipoVialidad,
                        TipoAsentamiento = a.TipoAsentamiento,
                    }));
                }


            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

        }


    }
}
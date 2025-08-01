using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.Persona;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Cat.Consultas;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        
        private readonly IWebHostEnvironment _environment;

        public PersonasController(DbContextSIIGPP context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        //GET: api/Personas/ListarPorId
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Administrador")]
        [HttpGet("[action]/{idPersona}")]
        public async Task<IActionResult> ListarPorId([FromRoute] Guid idPersona)
        {
            var p = await _context.RAPs
                          .Include(x => x.Persona)
                          .Where(x => x.Persona.IdPersona == idPersona)
                          .FirstOrDefaultAsync();
            if (p == null)
            {
                return NotFound();
            }

            return Ok(new BuscarPersona1ViewModel
            {
                /*CAT_PERSONA*/
                /*********************************************/
                PersonaId = p.Persona.IdPersona,
                Clasificacion = p.ClasificacionPersona,
                Medionotificacion = p.Persona.Medionotificacion,
                Telefono1 = p.Persona.Telefono1,
                Telefono2 = p.Persona.Telefono2,
                Correo = p.Persona.Correo,
                Nacionalidad = p.Persona.Nacionalidad,
                EstadoCivil = p.Persona.EstadoCivil,
                Genero = p.Persona.Genero,
                Registro = p.Persona.Registro.GetValueOrDefault(false),
                VerR = p.Persona.VerR.GetValueOrDefault(false),
                VerI = p.Persona.VerI.GetValueOrDefault(false),
                Ocupacion = p.Persona.Ocupacion,
                NivelEstudio = p.Persona.NivelEstudio,
                Lengua = p.Persona.Lengua,
                Religion = p.Persona.Religion,
                Discapacidad = p.Persona.Discapacidad,
                TipoDiscapacidad = p.Persona.TipoDiscapacidad,
                Numerornd = p.Persona.Numerornd,
                InstitutoPolicial = p.Persona.InstitutoPolicial,
            });

        }

        // PERMITE VALIDAT QUE NO EXISTA LA MISMA PERSONA
        // GET: api/Personas/{CURP}
        [Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Administrador,Recepción")]
        [HttpGet("[action]/{CURP}")]
        public async Task<IActionResult> BuscarPersona([FromRoute] string CURP)
        {
            var p= await _context.Personas
                          .Where(x => x.CURP == CURP)
                          .FirstOrDefaultAsync();
            if (p == null)
            {
                return NotFound();
            }

            return Ok(new BuscarPersonaViewModel
            {
                /*CAT_PERSONA*/
                /*********************************************/
                PersonaId = p.IdPersona, 
                Medionotificacion = p.Medionotificacion,
                Telefono1 = p.Telefono1,
                Telefono2 = p.Telefono2,
                Correo = p.Correo,
                Nacionalidad = p.Nacionalidad,
                EstadoCivil = p.EstadoCivil,
                Genero = p.Genero,
                Registro = p.Registro.GetValueOrDefault(false),
                VerR = p.VerR.GetValueOrDefault(false),
                VerI = p.VerI.GetValueOrDefault(false),
                Ocupacion = p.Ocupacion,
                NivelEstudio = p.NivelEstudio,
                Lengua = p.Lengua,
                Religion = p.Religion,
                Discapacidad = p.Discapacidad,
                TipoDiscapacidad = p.TipoDiscapacidad,
                Numerornd = p.Numerornd,
                InstitutoPolicial = p.InstitutoPolicial,
            });

        }

      
        //PERMITE ACTUALIZAR LOS DATOS DE LA PERSONA SE UTILIZA EN EL REGISTRO
        // PUT: api/Personas/Actualizar
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador, AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var persona = await _context.Personas.FirstOrDefaultAsync(a => a.IdPersona == model.personaId);

                if (persona == null)
                {
                    return NotFound();
                }
                persona.StatusAnonimo = model.statusAnonimo;
                persona.TipoPersona = model.tipoPersona;
                persona.RFC = model.rfc;
                persona.RazonSocial = model.razonSocial;
                persona.Nombre = model.nombre;
                persona.ApellidoPaterno = model.apellidoPaterno;
                persona.ApellidoMaterno = model.apellidoMaterno;
                persona.Alias = model.alias;
                persona.StatusAlias = model.statusAlias;
                persona.FechaNacimiento = model.fechaNacimiento;
                persona.RangoEdad = model.RangoEdad;
                persona.RangoEdadTF = model.RangoEdadTF;
                persona.EntidadFederativa = model.entidadFederativa;
                persona.DocIdentificacion = model.docIdentificacion;
                persona.CURP = model.curp;
                persona.PoblacionAfro = model.PoblacionAfro;
                persona.Sexo = model.sexo;
                persona.Genero = model.genero;
                persona.Registro = model.registro;
                persona.VerR = model.verR;
                persona.VerI = model.verI;
                persona.EstadoCivil = model.estadoCivil;
                persona.Telefono1 = model.telefono1;
                persona.Telefono2 = model.telefono2;
                persona.Correo = model.correo;
                persona.Medionotificacion = model.medioNotificacion;
                persona.Nacionalidad = model.nacionalidad;
                persona.Ocupacion = model.ocupacion;
                persona.NivelEstudio = model.nivelEstudio;
                persona.Lengua = model.lengua;
                persona.Religion = model.religion;
                persona.Discapacidad = model.discapacidad;
                persona.TipoDiscapacidad = model.tipoDiscapacidad;
                persona.Edad = model.edad;
                persona.Relacion = model.relacion;
                persona.Parentesco = model.parentesco;
                persona.Edad = model.edad;
                persona.DatosProtegidos = model.datosProtegidos;

                var dirpersonal = await _context.DireccionPersonals.FirstOrDefaultAsync(a => a.PersonaId == model.personaId);

                if (dirpersonal == null)
                {
                    return NotFound();
                }
                dirpersonal.Calle = model.calle;
                dirpersonal.NoExt = model.noExt;
                dirpersonal.NoInt = model.noInt;
                dirpersonal.EntreCalle1 = model.entreCalle1;
                dirpersonal.EntreCalle2 = model.entreCalle2;
                dirpersonal.Referencia = model.referencia;
                dirpersonal.Pais = model.pais;
                dirpersonal.Estado = model.estado;
                dirpersonal.Municipio = model.municipio;
                dirpersonal.Localidad = model.localidad;
                dirpersonal.CP = model.cp;
                dirpersonal.lat = model.lat;
                dirpersonal.lng = model.lng;
                dirpersonal.TipoVialidad = model.tipoVialidad;
                dirpersonal.TipoAsentamiento = model.tipoAsentamiento;

                var rapPersona = await _context.RAPs.FirstOrDefaultAsync(a => a.IdRAP == model.rapId);
                if (rapPersona == null)
                {
                    return NotFound();
                }

                rapPersona.ClasificacionPersona = model.clasificacionPersona;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
}

        // POST: api/Personas/Crear
        [Authorize(Roles = "Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             Persona InsertarPersona = new Persona
                {
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = model.Alias,
                    FechaNacimiento = model.FechaNacimiento,
                    EntidadFederativa = model.EntidadFederativa,
                    Sexo = model.Sexo,
                    InstitutoPolicial = model.InstitutoPolicial,
                    Numerornd = model.Numerornd,
                    Edad = model.Edad,
                    PoliciaDetuvo = model.PoliciaDetuvo
                };

                _context.Personas.Add(InsertarPersona);
           
            try
            {
                 var idP = InsertarPersona.IdPersona;

                RAP InsertarRAP = new RAP
                {
                    RAtencionId = model.RAtencionId,
                    PersonaId = idP,
                    ClasificacionPersona = "Familiar",
                    PInicio = false,
                };

                _context.RAPs.Add(InsertarRAP);

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

        // GET: api/Personas/BuscarPersonaNombreApellidoPa
        [Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> BuscarPersonaNombreApellidoPa([FromBody] Models.Persona.BuscarCarpetasPreviasPersona model)
        {
            //return Ok();
            try
            {
                String busquedaPersona = @"SELECT 
                                                     RH.IdRhecho
                                                    ,CONCAT(P.Nombre,' ',P.ApellidoPaterno,' ',P.ApellidoMaterno) as 'personaRelacionada'
                                                    ,P.IdPersona   
                                                    ,RAP.ClasificacionPersona
                                                    ,RAC.racg
                                                    ,NUC.nucg
                                                    ,RH.FechaElevaNuc as 'FechaCarpeta'
                                                    ,RA.u_Nombre AS 'genero'
                                                    ,NUC.StatusNUC as 'status'
                                                    ,D.Nombre AS Delito
                                                    ,RDH.DelitoEspecifico
                                                    ,CONCAT(DIS.Nombre,',' ,AG.Nombre,', ',MS.Nombre) as 'moduloactual'
                                                    FROM CAT_PERSONA P
                                                    LEFT JOIN CAT_RAP RAP ON RAP.PersonaId = P.IdPersona
                                                    LEFT JOIN CAT_RATENCON RA ON RA.IdRAtencion = RAP.RAtencionId
                                                    LEFT JOIN RAC ON RAC.idRac = RA.racId
                                                    LEFT JOIN CAT_RHECHO RH ON RH.RAtencionId = RA.IdRAtencion
                                                    LEFT JOIN NUC ON NUC.idNuc = RH.NucId
                                                    LEFT JOIN CAT_RDH RDH ON RDH.RHechoId = RH.IdRHecho
                                                    LEFT JOIN C_DELITO D ON RDH.DelitoId = D.IdDelito
                                                    LEFT JOIN C_MODULOSERVICIO MS ON Rh.ModuloServicioId = MS.IdModuloServicio
                                                    LEFT JOIN C_AGENCIA AG  ON MS.AgenciaId = AG.IdAgencia
                                                    LEFT JOIN C_DSP DSP ON DSP.IdDSP = AG.DSPId
                                                    LEFT JOIN C_DISTRITO DIS ON DSP.DistritoId = DIS.IdDistrito
                                                    WHERE 1=1 
                                                    AND P.Nombre LIKE @nombre COLLATE Latin1_general_CI_AI
                                                    AND P.ApellidoPaterno LIKE @apellidopa COLLATE Latin1_general_CI_AI
                                                    AND P.ApellidoMaterno LIKE @apellidoma COLLATE Latin1_general_CI_AI
                                                    ORDER BY  P.Nombre ASC
                                                    ";
                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@nombre", "%" + model.nombre + "%"));
                filtrosBusqueda.Add(new SqlParameter("@apellidopa", "%" + model.paterno + "%"));
                filtrosBusqueda.Add(new SqlParameter("@apellidoma", "%" + model.materno + "%"));
                filtrosBusqueda.Add(new SqlParameter("@curp", "%" + model.curp + "%"));
                var repre = await _context.qBusquedaPersonasCarpetas.FromSqlRaw(busquedaPersona, filtrosBusqueda.ToArray()).ToListAsync();




                return Ok(repre.Select(a => new queryBusquedaPersonasCarpetas
                {

                    //IdRHecho= a.IdRHecho,
                    personaRelacionada = a.personaRelacionada,
                    IdPersona = a.IdPersona,
                    ClasificacionPersona = a.ClasificacionPersona,
                    racg = a.racg,
                    nucg = a.nucg,
                    FechaCarpeta = a.FechaCarpeta,
                    genero = a.genero,
                    moduloactual = a.moduloactual,
                    status = a.status,
                    Delito = a.Delito,
                    DelitoEspecifico = a.DelitoEspecifico

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }


        }

        // PUT: api/Personas/ActualizarInfoAdicionalDetenido
        [Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador, AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarInfoAdicionalDetenido([FromBody] ActualizarInfoAdicionalDetenidoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var persona = await _context.Personas.FirstOrDefaultAsync(a => a.IdPersona == model.PersonaId);

            if (persona == null)
            {
                return NotFound();
            }

            persona.CumpleRequisitoLey = model.CumpleRequisitoLey;
            persona.DecretoLibertad = model.DecretoLibertad;
            persona.DispusoLibertad = model.DispusoLibertad;

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


        private bool PersonaExists(Guid id)
        {
            return _context.Personas.Any(e => e.IdPersona == id);
        }
    }
}
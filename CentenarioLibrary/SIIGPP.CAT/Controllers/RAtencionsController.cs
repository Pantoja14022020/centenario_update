using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Registro;
using SIIGPP.CAT.Models.Turnador;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Direcciones;
using SIIGPP.Entidades.M_Cat.Registro;
using SIIGPP.Entidades.M_Cat.Turnador;
using SIIGPP.CAT.FilterClass;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RAtencionsController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;
        private IConfiguration _configuration;

        public RAtencionsController(DbContextSIIGPP context, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }
        // GET: api/RAtencions/Listar
        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Recepción")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<RegistroViewModel>> Listar()
        {
            var ra = await _context.RAtencions
                    .Include(a => a.RACs)
                    .OrderByDescending(a => a.FechaHoraRegistro)
                    .ToListAsync();


            return ra.Select(a => new RegistroViewModel
            {
                 

               IdRAtencion = a.IdRAtencion,
               rac = a.RACs.racg,
               racId = a.racId,
               FechaHoraRegistro = a.FechaHoraRegistro,
               FechaHoraAtencion = a.FechaHoraAtencion, 
               FechaHoraCierre = a.FechaHoraCierre,
               u_Nombre = a.u_Nombre,
               u_Puesto = a.u_Puesto,
               u_Modulo = a.u_Modulo,
               DistritoInicial = a.DistritoInicial,
               DirSubProcuInicial = a.DirSubProcuInicial,
               AgenciaInicial = a.AgenciaInicial,
               StatusAtencion = a.StatusAtencion,
               StatusRegistro = a.StatusRegistro,
               MedioDenuncia = a.MedioDenuncia,
               ContencionVicitma = a.ContencionVicitma, 
               Numerooficio = a.ModuloServicio

            });
            

        }

        //ACTUALIZA LA HORA DE ATENCION
        // PUT: api/RAtencions/AHAtencion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> AHAtencion([FromBody] AHAtencionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

     
            DateTime fecha = System.DateTime.Now;
            var ra = await _context.RAtencions.FirstOrDefaultAsync(a => a.IdRAtencion == model.idRatencion);

            if (ra == null)
            {
                return NotFound();
            }

            ra.FechaHoraAtencion = fecha;
            ra.StatusAtencion = true;
            ra.u_Nombre = model.u_Nombre;
            ra.u_Puesto = model.u_Puesto;
            ra.u_Modulo = model.u_Modulo;

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
        //ACTUALIZA LA HORA DE CIERRE
        // PUT: api/RAtencions/AHCierre
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> AHCierre([FromBody] AHAtencionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       
            DateTime fecha = System.DateTime.Now;
            var ra = await _context.RAtencions.FirstOrDefaultAsync(a => a.IdRAtencion == model.idRatencion);

            if (ra == null)
            {
                return NotFound();
            }

            ra.FechaHoraCierre = fecha;
             

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


        // POST: api/RAtencions/Crear
        //[Authorize(Roles = "Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;
            string serie = "A";
            int noturno = 1;
            Guid idR ;
            Guid idpersona ;
            Guid idratencion ;

            RAtencion InsertarRA = new RAtencion
            { 
                DistritoInicial = model.DistritoInicial,  
                AgenciaInicial = model.AgenciaInicial,
                DirSubProcuInicial = model.DirSubProcuInicial,
                FechaHoraRegistro = fecha,
                StatusAtencion = false,
                StatusRegistro = false,
                racId = model.racid,
                MedioDenuncia = "Denuncia",
                ContencionVicitma = false,
                ModuloServicio = model.Numerooficio,
                u_Nombre = model.Usuario,
                u_Puesto = model.Puesto,
                u_Modulo = model.Modulo,
                
            };

           
            try
            {
                _context.RAtencions.Add(InsertarRA);




                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = model.StatusAnonimo,
                    TipoPersona = model.TipoPersona,
                    RFC = model.RFC,
                    RazonSocial = model.RazonSocial, 
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = model.Alias,
                    StatusAlias = model.StatusAlias,
                    FechaNacimiento = model.FechaNacimiento,
                    EntidadFederativa = model.EntidadFederativa,
                    DocIdentificacion = model.DocIdentificacion, 
                    CURP = model.CURP,
                    Sexo = model.Sexo,
                    EstadoCivil = model.EstadoCivil,
                    Genero = model.Genero,
                    Registro = model.Registro,
                    VerR = model.VerR,
                    VerI = model.VerI,
                    Telefono1 = model.Telefono1,
                    Telefono2 = model.Telefono2,
                    Correo = model.Correo,
                    Medionotificacion = model.Medionotificacion,
                    Nacionalidad = model.Nacionalidad,
                    Ocupacion = model.Ocupacion,
                    NivelEstudio = model.NivelEstudio,
                    Lengua = model.Lengua,
                    Religion = model.Religion,
                    Discapacidad = model.Discapacidad,
                    TipoDiscapacidad = model.TipoDiscapacidad, 
                    DatosProtegidos = model.DatosProtegidos,
                    Parentesco = model.Parentesco,
                    Edad = model.Edad,
                    Relacion = model.Relacion,
                    DocPoderNotarial = model.DocPoderNotarial


                }; 

                _context.Personas.Add(InsertarPersona);
                //********************************************************

                var idRA = InsertarRA.IdRAtencion;
                var idP = InsertarPersona.IdPersona;

                DireccionPersonal InsertarDP = new DireccionPersonal
                {

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
                    PersonaId = idP,
                    lat = model.lat,
                    lng = model.lng
                    

                };

                _context.DireccionPersonals.Add(InsertarDP);
            

                RAP InsertarRAP = new RAP
                {
                    RAtencionId = idRA,
                    PersonaId = idP,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = model.PInicio,
                };

                _context.RAPs.Add(InsertarRAP);


                //**********************************************************************


                Guid idagencia = model.agenciaId; 

                var turno = await _context.Turnos
                                 .Where(a => a.AgenciaId == idagencia)
                                 .Where(a => a.FechaHoraInicio.ToString("dd/MM/yyyy") == fecha.ToString("dd/MM/yyyy")) 
                                 .OrderByDescending(a => a.NoTurno)
                                 .Take(1)
                                 .FirstOrDefaultAsync();

               

                if (turno != null)
                {
                    serie = turno.Serie;
                    noturno = turno.NoTurno + 1;
                }


                Turno InsertarTurno = new Turno
                {
                    Serie = serie,
                    NoTurno = noturno,
                    FechaHoraInicio = fecha,
                    AgenciaId = idagencia,
                    Status = false,
                    StatusReAsignado = false,
                    RAtencionId = idRA,
                    Modulo = model.Modulo,
                };
                _context.Turnos.Add(InsertarTurno);
                //**********************************************************************
                DireccionEscucha InsertarDE = new DireccionEscucha
                {
                    RAPId = InsertarRAP.IdRAP,
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
                    lng = model.lng


                };

                _context.DireccionEscuchas.Add(InsertarDE);

                await _context.SaveChangesAsync();

                idR = InsertarRAP.IdRAP;
                idpersona = InsertarPersona.IdPersona;
                idratencion = InsertarRA.IdRAtencion;
                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new {  notu = noturno, fh = fecha.ToString("dd/MM/yyyy hh:mm:ss"), idrap = idR,personaid = idpersona, idatencion = idratencion, mesa = model.Modulo });

            
        }

        // POST: api/RAtencions/CrearSinTurno
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearSinTurno(CrearViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            Guid idrap ;
            Guid idpersona ;
            Guid idratencion ;

            RAtencion InsertarRA = new RAtencion
            {
                DistritoInicial = model.DistritoInicial,
                AgenciaInicial = model.AgenciaInicial,
                DirSubProcuInicial = model.DirSubProcuInicial,
                FechaHoraRegistro = fecha,
                StatusAtencion = false,
                StatusRegistro = false,
                racId = model.racid,
                MedioDenuncia = model.MedioDenuncia,
                ContencionVicitma = false,
                ModuloServicio = model.Numerooficio,
                u_Nombre = model.Usuario,
                u_Puesto = model.Puesto,
                u_Modulo = model.Modulo,
                MedioLlegada = model.MedioLlegada
            };


            try
            {
                _context.RAtencions.Add(InsertarRA);




                Persona InsertarPersona = new Persona
                {
                    StatusAnonimo = model.StatusAnonimo,
                    TipoPersona = model.TipoPersona,
                    RFC = model.RFC,
                    RazonSocial = model.RazonSocial,
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Alias = model.Alias,
                    StatusAlias = model.StatusAlias,
                    FechaNacimiento = model.FechaNacimiento,
                    EntidadFederativa = model.EntidadFederativa,
                    DocIdentificacion = model.DocIdentificacion,
                    CURP = model.CURP,
                    //AGREGACIONES NUEVAS QUE NO ESTAN EN TODOS LADOS
                    PoblacionAfro = model.PoblacionAfro,
                    RangoEdad = model.RangoEdad,
                    RangoEdadTF = model.RangoEdadTF,
                    PoliciaDetuvo = model.PoliciaDetuvo,
                    //------------------------------------------
                    Sexo = model.Sexo,
                    EstadoCivil = model.EstadoCivil,
                    Genero = model.Genero,
                    Registro = model.Registro,
                    VerR = model.VerR,
                    VerI = model.VerI,
                    Telefono1 = model.Telefono1,
                    Telefono2 = model.Telefono2,
                    Correo = model.Correo,
                    Medionotificacion = model.Medionotificacion,
                    Nacionalidad = model.Nacionalidad,
                    Ocupacion = model.Ocupacion,
                    NivelEstudio = model.NivelEstudio,
                    Lengua = model.Lengua,
                    Religion = model.Religion,
                    Discapacidad = model.Discapacidad,
                    TipoDiscapacidad = model.TipoDiscapacidad,
                    DatosProtegidos = model.DatosProtegidos,
                    Parentesco = model.Parentesco,
                    Edad = model.Edad,
                    Relacion = model.Relacion,
                    DocPoderNotarial = model.DocPoderNotarial,
                    InicioDetenido = model.InicioDetenido

                    


                };

                _context.Personas.Add(InsertarPersona);
                //***********************************************************************

                var idRA = InsertarRA.IdRAtencion;
                var idP = InsertarPersona.IdPersona;

                DireccionPersonal InsertarDP = new DireccionPersonal
                {

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
                    PersonaId = idP,
                    lat = model.lat,
                    lng = model.lng,
                    TipoVialidad = model.tipoVialidad,
                    TipoAsentamiento = model.tipoAsentamiento,
                };

                _context.DireccionPersonals.Add(InsertarDP);


                RAP InsertarRAP = new RAP
                {
                    RAtencionId = idRA,
                    PersonaId = idP,
                    ClasificacionPersona = model.ClasificacionPersona,
                    PInicio = model.PInicio,
                };

                _context.RAPs.Add(InsertarRAP);

                
                //**********************************************************************

                DireccionEscucha InsertarDE = new DireccionEscucha
                {
                    RAPId = InsertarRAP.IdRAP,
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
                    TipoAsentamiento = model.tipoAsentamiento,
                    TipoVialidad = model.tipoVialidad,
                };

                _context.DireccionEscuchas.Add(InsertarDE);

                await _context.SaveChangesAsync();

                idrap = InsertarRAP.IdRAP;
                idpersona = InsertarPersona.IdPersona;
                idratencion = InsertarRA.IdRAtencion;
                //**********************************************************************
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }
            return Ok(new { idRatencion = InsertarRA.IdRAtencion , idrap = idrap,personaid = idpersona, idatencion = idratencion });

        }



        //[HttpPost("Post/{nombre}" )]
        //[Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPost("[action]/{nombreCarpeta}/{nombreArchivo}")]
        public async Task<IActionResult> Post(IFormFile file, [FromRoute] string nombreCarpeta, string nombreArchivo)
        {
            try
            {


              
                //***********************************************************************************
                string patchp = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + nombreCarpeta);
              
                if (!Directory.Exists(patchp))
                        Directory.CreateDirectory(patchp);

                    string extension;

                    extension = Path.GetExtension(file.FileName);

                   
                var filePath = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + nombreCarpeta, nombreArchivo + extension);
                var path = ("https://localhost:44394/Carpetas/" + nombreCarpeta + "/" + nombreArchivo + extension);
                if (file.Length > 0)
                        using (var stream = new FileStream(filePath, FileMode.Create))
                            await file.CopyToAsync(stream);
 
               

                return Ok(new { count = 1, ruta = path });


            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }
        
        // POST: api/RAtencions/CrearRA
        [Authorize(Roles = "Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRA(CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            int noturno = 1;
            Guid idratencion;

            RAtencion InsertarRA = new RAtencion
            {
                DistritoInicial = model.DistritoInicial,
                AgenciaInicial = model.AgenciaInicial,
                DirSubProcuInicial = model.DirSubProcuInicial,
                FechaHoraRegistro = fecha,
                StatusAtencion = false,
                StatusRegistro = false,
                racId = model.racid,
                MedioDenuncia = "Denuncia",
                ContencionVicitma = false,
                ModuloServicio = model.Numerooficio,
                u_Nombre = model.Usuario,
                u_Puesto = model.Puesto,
                u_Modulo = model.Modulo,

            };


            try
            {
                _context.RAtencions.Add(InsertarRA);
                await _context.SaveChangesAsync();

                idratencion = InsertarRA.IdRAtencion;
                //**********************************************************************
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { notu = noturno, fh = fecha.ToString("dd/MM/yyyy hh:mm:ss"), idatencion = idratencion });


        }

        // GET: api/RAtencions/ListarPorrac
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{racid}")]
        public async Task<IActionResult> ListarPorrac([FromRoute] Guid racid)
        {
            var a = await _context.RAtencions
                          .Where(x => x.racId == racid)
                          .FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new RegistroViewModel
            {

                IdRAtencion = a.IdRAtencion,
                FechaHoraRegistro = a.FechaHoraRegistro

            });

        }



        // POST: api/RAtencions/CrearMCaptura
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-AMP,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearMCaptura(CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

#pragma warning disable CS0168 // La variable 'idrap' se ha declarado pero nunca se usa
            Guid idrap;


            RAtencion InsertarRA = new RAtencion
            {
                DistritoInicial = model.DistritoInicial,
                AgenciaInicial = model.AgenciaInicial,
                DirSubProcuInicial = model.DirSubProcuInicial,
                FechaHoraRegistro = model.FechaRegistro,
                StatusAtencion = false,
                StatusRegistro = false,
                racId = model.racid,
                MedioDenuncia = model.MedioDenuncia,
                ContencionVicitma = false,
                ModuloServicio = model.Numerooficio,
                u_Nombre = model.Usuario,
                u_Puesto = model.Puesto,
                u_Modulo = model.Modulo,
                MedioLlegada = model.MedioLlegada
            };


            try
            {
                _context.RAtencions.Add(InsertarRA);
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
            return Ok(new { idRatencion = InsertarRA.IdRAtencion });

        }

        //PATCH: api/RAtencions/modNANDP
        [Authorize(Roles = "Administrador, Recepción,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-AMP")]
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> modNANDP(Guid id, [FromBody] ModPersona model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reg = _context.RAtencions.FirstOrDefault(p => p.IdRAtencion == id);

                if(reg == null)
                {
                    return BadRequest();
                }

                reg.u_Nombre = model.nombre;
                reg.u_Puesto = model.puesto;

                await _context.SaveChangesAsync();

                return Ok(new {status = "actualizado", registro = reg, datos = model});
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }

        // POST: api/RAtencions/modAtendio
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> modAtendio([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //PRIMERO BUSCAR LA ATENCION
                var consultaAtencion = await _context.RAtencions.Where(a => a.IdRAtencion == model.infoBorrado.rAtencionId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaAtencion == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningún RAC con la información enviada" });
                }
                else
                {
                    var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("LOG")).Options;
                    using (var ctx = new DbContextSIIGPP(options))
                    {

                        Guid gLog = Guid.NewGuid();
                        DateTime fecha = System.DateTime.Now;
                        LogAdmon laRegistro = new LogAdmon
                        {
                            IdLogAdmon = gLog,
                            UsuarioId = model.usuario,
                            Tabla = model.tabla,
                            FechaMov = fecha,
                            RegistroId = model.infoBorrado.registroId,
                            SolicitanteId = model.solicitante,
                            RazonMov = model.razon,
                            MovimientoId = new Guid("e2c746cd-3f26-4aca-b059-b1142c9e95ad")
                        };

                        ctx.Add(laRegistro);

                        LogCat_RAtencon atencion = new LogCat_RAtencon
                        {
                            LogAdmonId = gLog,
                            IdRAtencion = consultaAtencion.IdRAtencion,
                            FechaHoraRegistro = consultaAtencion.FechaHoraRegistro,
                            FechaHoraAtencion = consultaAtencion.FechaHoraAtencion,
                            FechaHoraCierre = consultaAtencion.FechaHoraCierre,
                            u_Nombre = consultaAtencion.u_Nombre,
                            u_Modulo = consultaAtencion.u_Modulo,
                            u_Puesto = consultaAtencion.u_Puesto,
                            DistritoInicial = consultaAtencion.DistritoInicial,
                            DirSubProcuInicial = consultaAtencion.DirSubProcuInicial,
                            AgenciaInicial = consultaAtencion.AgenciaInicial,
                            StatusAtencion = consultaAtencion.StatusAtencion,
                            StatusRegistro = consultaAtencion.StatusRegistro,
                            MedioDenuncia = consultaAtencion.MedioDenuncia
                        };
                        ctx.Add(atencion);

                        consultaAtencion.u_Nombre = model.infoBorrado.textoMod;
                        // AQUI AGREGAR EL MANEJO DE CONSECUTIVOS
                        consultaAtencion.u_Puesto = model.infoBorrado.textoMod2;
                      
                        //SE APLICAN TODOS LOS CAMBIOS A LA BD
                        await ctx.SaveChangesAsync();
                        await _context.SaveChangesAsync();


                    }
                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Perosona que atendió carpeta modificada correctamente" });
        }




        private bool RAtencionExists(Guid id)
        {
            return _context.RAtencions.Any(e => e.IdRAtencion == id);
        }


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/RAtencions/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            { 

                var consultaAtencion = await _context.RAtencions
                                   .Where(x => x.IdRAtencion == model.IdRAtencion)
                                   .Take(1)
                                   .FirstOrDefaultAsync();


                

                if (consultaAtencion == null)
                {
                    return BadRequest(ModelState);

                }
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var InsertarRA = await ctx.RAtencions.FirstOrDefaultAsync(a => a.IdRAtencion == consultaAtencion.IdRAtencion);

                    if (InsertarRA == null)
                    {
                        InsertarRA = new RAtencion();
                        ctx.RAtencions.Add(InsertarRA);
                    }


                    InsertarRA.IdRAtencion = consultaAtencion.IdRAtencion;
                    InsertarRA.DistritoInicial = consultaAtencion.DistritoInicial;
                    InsertarRA.AgenciaInicial = consultaAtencion.AgenciaInicial;
                    InsertarRA.DirSubProcuInicial = consultaAtencion.DirSubProcuInicial;
                    InsertarRA.FechaHoraRegistro = consultaAtencion.FechaHoraRegistro;
                    InsertarRA.StatusAtencion = consultaAtencion.StatusAtencion;
                    InsertarRA.StatusRegistro = consultaAtencion.StatusRegistro;
                    InsertarRA.FechaHoraCierre = consultaAtencion.FechaHoraCierre;
                    InsertarRA.racId = consultaAtencion.racId;
                    InsertarRA.MedioDenuncia = consultaAtencion.MedioDenuncia;
                    InsertarRA.ContencionVicitma = consultaAtencion.ContencionVicitma;
                    InsertarRA.ModuloServicio = consultaAtencion.ModuloServicio;
                    InsertarRA.u_Nombre = consultaAtencion.u_Nombre;
                    InsertarRA.u_Puesto = consultaAtencion.u_Puesto;
                    InsertarRA.u_Modulo = consultaAtencion.u_Modulo;
                    InsertarRA.MedioLlegada = consultaAtencion.MedioLlegada;


                    

                    await ctx.SaveChangesAsync();


                    return Ok();
                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message });
                result.StatusCode = 402;
                return result;
            }

        }


    }
}
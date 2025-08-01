using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Representantes;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.Representantes;
using SIIGPP.Entidades.M_Administracion;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentanteController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IWebHostEnvironment _environment;
        private IConfiguration _configuration;

        public RepresentanteController(DbContextSIIGPP context, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }

        // GET: api/Representante/Representanteslistarporid
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IActionResult> Representanteslistarporid([FromRoute] Guid RHechoId)
        {
            try
            {
                String busquedaRepresentantes = @"SELECT
                 R.IdRepresentante
                ,R.RHechoId
                ,R.PersonaId
                ,CONCAT(P.Nombre,' ',ApellidoPaterno,' ',ApellidoMaterno) as PersonaR
                ,R.Nombres
                ,R.ApellidoPa
                ,R.ApellidoMa
                ,R.FechaNacimiento
                ,R.Sexo
                ,R.EntidadFeNacimiento
                ,R.Curp
                ,R.MedioNotificacion
                ,R.Telefono
                ,R.CorreoElectronico
                ,R.Nacionalidad
                ,R.Genero
                ,R.Tipo1
                ,R.Tipo2
                ,(CASE WHEN TR.Tipo IS NULL THEN 'NINGUNO' ELSE TR.Tipo END) as TipoRep1
                ,(CASE WHEN TR2.Tipo IS NULL THEN 'NINGUNO' ELSE TR2.Tipo END) as TipoRep2
                ,R.CedulaProfesional
                ,R.Fecha
                ,R.Calle
                ,R.UDistrito
                ,R.USubproc
                ,R.UAgencia
                ,R.Usuario
                ,R.UPuesto
                ,R.UModulo
                ,R.Fechasys
                ,R.NoInt
                ,R.NoExt
                ,R.EntreCalle1
                ,R.EntreCalle2
                ,R.Referencia
                ,R.Pais
                ,R.Estado
                ,R.Municipio
                ,R.Localidad
                ,R.CP
                ,R.lat
                ,R.lng
                ,R.ArticulosPenales
                ,R.TipoVialidad
                ,R.TipoAsentamiento
                ,CASE WHEN (DR.TipoDocumento IS NULL) THEN 'NINGUNO' ELSE DR.TipoDocumento END AS TipoDocumento
                FROM CAT_REPRESENTANTES R
                LEFT JOIN CAT_PERSONA P ON R.PersonaId=P.IdPersona
                LEFT JOIN CAT_DOCSREPRESENTANTES  DR ON R.IdRepresentante=DR.RepresentanteId
                LEFT JOIN C_TIPOSREPRESENTANTES TR ON R.Tipo1=TR.Valor
                LEFT JOIN C_TIPOSREPRESENTANTES TR2 ON R.Tipo2=TR2.Valor
                WHERE R.RHechoId=@hechoid
                ORDER BY R.Fechasys DESC";
                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@hechoid", RHechoId));
                var repre = await _context.qBusquedaRepresentantes.FromSqlRaw(busquedaRepresentantes, filtrosBusqueda.ToArray()).ToListAsync();



                return Ok(repre.Select(a => new RepresentanteViewModel
                {
                    IdRepresentante = a.IdRepresentante,
                    RHechoId = a.RHechoId,
                    PersonaId = a.PersonaId,
                    PersonaR = a.PersonaR,
                    Nombres = a.Nombres,
                    ApellidoPa = a.ApellidoPa,
                    ApellidoMa = a.ApellidoMa,
                    FechaNacimiento = a.FechaNacimiento,
                    Sexo = a.Sexo,
                    EntidadFeNacimiento = a.EntidadFeNacimiento,
                    Curp = a.Curp,
                    MedioNotificacion = a.MedioNotificacion,
                    Telefono = a.Telefono,
                    CorreoElectronico = a.CorreoElectronico,
                    Nacionalidad = a.Nacionalidad,
                    Genero = a.Genero,
                    Tipo1 = a.Tipo1,
                    Tipo2 = a.Tipo2,
                    CedulaProfesional = a.CedulaProfesional,
                    Fecha = a.Fecha,
                    UDistrito=a.UDistrito,
                    USubproc = a.USubproc,
                    UAgencia = a.UAgencia,
                    Usuario = a.Usuario,
                    UPuesto = a.UPuesto,
                    UModulo = a.UModulo,
                    Fechasys = a.Fechasys,
                    TipoRep1 = a.TipoRep1,
                    TipoRep2 = a.TipoRep2,
                    Calle = a.Calle,
                    NoInt = a.NoInt,
                    NoExt = a.NoExt,
                    EntreCalle1 = a.EntreCalle1,
                    EntreCalle2 = a.EntreCalle2,
                    Referencia = a.Referencia,
                    Pais = a.Pais,
                    Estado = a.Estado,
                    Municipio = a.Municipio,
                    Localidad = a.Localidad,
                    CP = a.CP,
                    lat = a.lat,
                    lng = a.lng,
                    ArticulosPenales = a.ArticulosPenales,
                    TipoDocumento = a.TipoDocumento,
                    TipoVialidad = a.TipoVialidad,
                    TipoAsentamiento = a.TipoAsentamiento,
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message , version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }



        // POST: api/Representante/CrearRepresentante
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción, AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRepresentante([FromBody] CrearRepresentanteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Representante repre = new Representante();
            DateTime fecha = System.DateTime.Now;
            try
            {
                string[] personal = model.representados.Split("; ");
                for (int i=0; i<personal.Length; i++) {
                    repre = new Representante
                    {
                        RHechoId = model.RHechoId,
                        PersonaId = Guid.Parse(personal[i]),
                        Nombres = model.Nombres,
                        ApellidoPa = model.ApellidoPa,
                        ApellidoMa = model.ApellidoMa,
                        FechaNacimiento = model.FechaNacimiento,
                        Sexo = model.Sexo,
                        EntidadFeNacimiento = model.EntidadFeNacimiento,
                        Curp = model.Curp,
                        MedioNotificacion = model.MedioNotificacion,
                        Telefono = model.Telefono,
                        CorreoElectronico = model.CorreoElectronico,
                        Nacionalidad = model.Nacionalidad,
                        Genero = model.Genero,
                        Tipo1 = model.Tipo1,
                        Tipo2 = model.Tipo2,
                        CedulaProfesional = model.CedulaProfesional,
                        Fecha = model.Fecha,
                        UDistrito = model.UDistrito,
                        USubproc = model.USubproc,
                        UAgencia = model.UAgencia,
                        Usuario = model.Usuario,
                        UPuesto = model.UPuesto,
                        UModulo = model.UModulo,
                        Fechasys = System.DateTime.Now,
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
                        lat = model.lat,
                        lng = model.lng,
                        ArticulosPenales = model.ArticulosPenales,
                        TipoVialidad = model.TipoVialidad,
                        TipoAsentamiento = model.TipoAsentamiento,
                    };

                    _context.Representantes.Add(repre);

                    await _context.SaveChangesAsync();
                }
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { idrepresentante = repre.IdRepresentante });
        }

        // POST: api/Representante/ActualizarRepresentante
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ActualizarRepresentante([FromBody] ActualizarRepresentanteExistente model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;
                
            string[] personal = model.representados.Split("; ");

            for (int i = 0; i < personal.Length; i++)
            {   
                //Busca si existe los representantes 
                var rep = await _context.Representantes.Where(a => a.IdRepresentante == model.representanteId).Take(1).FirstOrDefaultAsync();

                //Busca si existe la persona a representar para en caso de no crear un nuevo representante y en caso que si editarlo
                var per = await _context.Representantes.Where(a => a.PersonaId == Guid.Parse(personal[i])).Take(1).FirstOrDefaultAsync();

                //Busca en la tabla de documentos persona para editar su documento de identificacion
                var doc = await _context.DocsRepresentantes.Where(a => a.RepresentanteId == model.representanteId).Take(1).FirstOrDefaultAsync();


                if (rep != null && per == null && personal.Length >= 2)
                {

                    Representante repre = new Representante();
                    repre = new Representante
                    {
                        RHechoId = model.RHechoId,
                        PersonaId = Guid.Parse(personal[i]),
                        Nombres = model.Nombres,
                        ApellidoPa = model.ApellidoPa,
                        ApellidoMa = model.ApellidoMa,
                        FechaNacimiento = model.FechaNacimiento,
                        Sexo = model.Sexo,
                        EntidadFeNacimiento = model.EntidadFeNacimiento,
                        Curp = model.Curp,
                        MedioNotificacion = model.MedioNotificacion,
                        Telefono = model.Telefono,
                        CorreoElectronico = model.CorreoElectronico,
                        Nacionalidad = model.Nacionalidad,
                        Genero = model.Genero,
                        Tipo1 = model.Tipo1,
                        Tipo2 = model.Tipo2,
                        CedulaProfesional = model.CedulaProfesional,
                        Fecha = model.Fecha,
                        UDistrito = model.UDistrito,
                        USubproc = model.USubproc,
                        UAgencia = model.UAgencia,
                        Usuario = model.Usuario,
                        UPuesto = model.UPuesto,
                        UModulo = model.UModulo,
                        Fechasys = System.DateTime.Now,
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
                        lat = model.lat,
                        lng = model.lng,
                        ArticulosPenales = model.ArticulosPenales,
                        TipoVialidad = model.TipoVialidad,
                        TipoAsentamiento = model.TipoAsentamiento,
                    };

                    _context.Representantes.Add(repre);

                }
 
               
                else
                {

                    rep.PersonaId = Guid.Parse(personal[i]);
                    rep.Nombres = model.Nombres;
                    rep.ApellidoPa = model.ApellidoPa;
                    rep.ApellidoMa = model.ApellidoMa;
                    rep.FechaNacimiento = model.FechaNacimiento;
                    rep.Sexo = model.Sexo;
                    rep.EntidadFeNacimiento = model.EntidadFeNacimiento;
                    rep.Curp = model.Curp;
                    rep.MedioNotificacion = model.MedioNotificacion;
                    rep.Telefono = model.Telefono;
                    rep.CorreoElectronico = model.CorreoElectronico;
                    rep.Nacionalidad = model.Nacionalidad;
                    rep.Genero = model.Genero;
                    rep.Tipo1 = model.Tipo1;
                    rep.Tipo2 = model.Tipo2;
                    rep.CedulaProfesional = model.CedulaProfesional;
                    rep.Fecha = model.Fecha;
                    rep.UDistrito = model.UDistrito;
                    rep.USubproc = model.USubproc;
                    rep.UAgencia = model.UAgencia;
                    rep.Usuario = model.Usuario;
                    rep.UPuesto = model.UPuesto;
                    rep.UModulo = model.UModulo;
                    rep.Fechasys = System.DateTime.Now;
                    rep.Calle = model.Calle;
                    rep.NoInt = model.NoInt;
                    rep.NoExt = model.NoExt;
                    rep.EntreCalle1 = model.EntreCalle1;
                    rep.EntreCalle2 = model.EntreCalle2;
                    rep.Referencia = model.Referencia;
                    rep.Pais = model.Pais;
                    rep.Estado = model.Estado;
                    rep.Municipio = model.Municipio;
                    rep.Localidad = model.Localidad;
                    rep.CP = model.CP;
                    rep.lat = model.lat;
                    rep.lng = model.lng;
                    rep.ArticulosPenales = model.ArticulosPenales;
                    rep.TipoVialidad = model.TipoVialidad;
                    rep.TipoAsentamiento = model.TipoAsentamiento;
                }

                //---------------------------------------------------
                //Actualizar documentos

                if (doc != null)
                {
                    doc.TipoDocumento = model.TipoDocumento;
                }
                if (doc == null)
                {

                    DocsRepresentantes docs = new DocsRepresentantes();
                    docs = new DocsRepresentantes
                    {
                        RepresentanteId=model.representanteId,
                        TipoDocumento = model.TipoDocumento,
                        NombreDocumento = model.NombreDocumento,
                        Fecha = model.Fecha,
                        UDistrito = model.UDistrito,
                        USubproc = model.USubproc,
                        UAgencia = model.UAgencia,
                        Usuario = model.Usuario,
                        UPuesto = model.UPuesto,
                        UModulo = model.UModulo,
                        Fechasys = fecha

                    };
                    _context.DocsRepresentantes.Add(docs);


                }

            }
            
            try
            {
                
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


        // PUT: api/Representante/ActualizarStatus
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL, Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarRepresentanteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var Acrepre = await _context.Representantes.FirstOrDefaultAsync(a => a.IdRepresentante == model.IdRepresentante);

            if (Acrepre == null)
            {
                return NotFound();
            }

            Acrepre.Tipo1 = model.Tipo1;
            Acrepre.Tipo2 = model.Tipo2;

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



        // GET: api/Representante/DocRepresentanteslistarporid
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{idrepresentante}")]
        public async Task<IActionResult> DocRepresentanteslistarporid([FromRoute] Guid idrepresentante)
        {
            var repre = await _context.DocsRepresentantes
                .Where(a => a.RepresentanteId == idrepresentante)
                .FirstOrDefaultAsync();

            if (repre == null)
            {
                return NotFound("No hay registros");

            }

            return Ok(new DocRepresentanteViewModel
            {
                IdDocsRepresentantes = repre.IdDocsRepresentantes,
                RepresentanteId = repre.RepresentanteId,
                TipoDocumento = repre.TipoDocumento,
                NombreDocumento = repre.NombreDocumento,
                Ruta = repre.Ruta,
                Fecha = repre.Fecha,
                UDistrito = repre.UDistrito,
                USubproc = repre.USubproc,
                UAgencia = repre.UAgencia,
                Usuario = repre.Usuario,
                UPuesto = repre.UPuesto,
                UModulo = repre.UModulo,
                Fechasys = repre.Fechasys
            });

        }
        // POST: api/Representante/CrearDocRepresentante
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearDocRepresentante([FromBody] CrearDocRepresentanteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            DocsRepresentantes docrepre = new DocsRepresentantes
            {


                RepresentanteId = model.RepresentanteId,
                TipoDocumento = model.TipoDocumento,
                NombreDocumento = model.NombreDocumento,
                Ruta = model.Ruta,
                Fecha = model.Fecha,
                UDistrito = model.UDistrito,
                USubproc = model.USubproc,
                UAgencia = model.UAgencia,
                Usuario = model.Usuario,
                UPuesto = model.UPuesto,
                UModulo = model.UModulo,
                Fechasys = fecha


            };

            _context.DocsRepresentantes.Add(docrepre);
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

        // POST: api/Representante/ActualizarDocRepresentante
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ActualizarDocRepresentante([FromBody] CrearDocRepresentanteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doc = await _context.DocsRepresentantes.Where(a => a.RepresentanteId==model.RepresentanteId).FirstOrDefaultAsync();

            if(doc != null)
            {

                doc.IdDocsRepresentantes = model.IdDocsRepresnetantes;
                doc.TipoDocumento = model.TipoDocumento;
                doc.Ruta = model.Ruta;
                doc.Fecha = model.Fecha;
                doc.UDistrito = model.UDistrito;
                doc.USubproc = model.USubproc;
                doc.UAgencia = model.UAgencia;
                doc.Usuario = model.Usuario;
                doc.UPuesto = model.UPuesto;
                doc.UModulo = model.UModulo;
                doc.Fechasys = model.Fechasys;


            }

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


        //[HttpPost("Post/{nombre}" )]
        [HttpPost("[action]/{nombreCarpeta}/{nombreArchivo}")]
        public async Task<IActionResult> Post(IFormFile file, [FromRoute] string nombreCarpeta, string nombreArchivo)
        {
            try
            {
                var path="";
                if (file != null)
                {
                    //***********************************************************************************
                    string patchp = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + nombreCarpeta);

                    if (!Directory.Exists(patchp))
                        Directory.CreateDirectory(patchp);

                    string extension;

                    extension = Path.GetExtension(file.FileName);


                    var filePath = Path.Combine(_environment.ContentRootPath, "Carpetas\\" + nombreCarpeta, nombreArchivo + extension);
                    path = ("https://" + HttpContext.Request.Host.Value + "/Carpetas/" + nombreCarpeta + "/" + nombreArchivo + extension);
                    if (file.Length > 0)
                        using (var stream = new FileStream(filePath, FileMode.Create))
                            await file.CopyToAsync(stream);

                }
                else
                {
                    path = ("vacio");
                }
                return Ok(new { count = 1, ruta = path });


            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }
        }

        // GET: api/Representante/idHechoyActivos
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}/{personaId}")]
        public async Task<IEnumerable<RepresentanteViewModel>> idHechoyActivos([FromRoute] Guid RHechoId, Guid personaId)
        {
            var repre = await _context.Representantes
                .Where(a => a.RHechoId == RHechoId)
                .Where(a => a.PersonaId == personaId)
                .Where(a => a.Tipo1>0 || a.Tipo2>0)
                .Include(a => a.Persona)
                .ToListAsync();

            return repre.Select(a => new RepresentanteViewModel
            {
                IdRepresentante = a.IdRepresentante,
                RHechoId = a.RHechoId,
                PersonaId = a.PersonaId,
                PersonaR = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombres = a.Nombres,
                ApellidoPa = a.ApellidoPa,
                ApellidoMa = a.ApellidoMa,
                FechaNacimiento = a.FechaNacimiento,
                Sexo = a.Sexo,
                EntidadFeNacimiento = a.EntidadFeNacimiento,
                Curp = a.Curp,
                MedioNotificacion = a.MedioNotificacion,
                Telefono = a.Telefono,
                CorreoElectronico = a.CorreoElectronico,
                Nacionalidad = a.Nacionalidad,
                Genero = a.Genero,
                Tipo1 = a.Tipo1,
                Tipo2 = a.Tipo2,
                CedulaProfesional = a.CedulaProfesional,
                Fecha = a.Fecha,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys

            });

        }


        // GET: api/Representante/RepresentanteslistarporPersonaLegal
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{IdPersona}")]
        public async Task<IActionResult> RepresentanteslistarporPersonaLegal([FromRoute] Guid IdPersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == IdPersona )
                .Where(a => a.Tipo1 == 1 || a.Tipo2 == 1)
                .FirstOrDefaultAsync();

            if (repre == null)
            {
                return Ok(new { PersonaR = "", Telefono="", CorreoElectronico = "", PersonaId = IdPersona });

            }

            return Ok(new RepresentanteViewModel
            {
                IdRepresentante = repre.IdRepresentante,
                RHechoId = repre.RHechoId,
                PersonaId = repre.PersonaId,
                PersonaR = repre.Nombres + " " + repre.ApellidoPa + " " + repre.ApellidoMa,
                Nombres = repre.Nombres,
                ApellidoPa = repre.ApellidoPa,
                ApellidoMa = repre.ApellidoMa,
                FechaNacimiento = repre.FechaNacimiento,
                Sexo = repre.Sexo,
                EntidadFeNacimiento = repre.EntidadFeNacimiento,
                Curp = repre.Curp,
                MedioNotificacion = repre.MedioNotificacion,
                Telefono = repre.Telefono,
                CorreoElectronico = repre.CorreoElectronico,
                Nacionalidad = repre.Nacionalidad,
                Genero = repre.Genero,
                Tipo1 = repre.Tipo1,
                Tipo2 = repre.Tipo2,
                CedulaProfesional = repre.CedulaProfesional,
                Fecha = repre.Fecha,
                UDistrito = repre.UDistrito,
                USubproc = repre.USubproc,
                UAgencia = repre.UAgencia,
                Usuario = repre.Usuario,
                UPuesto = repre.UPuesto,
                UModulo = repre.UModulo,
                Fechasys = repre.Fechasys
            });

        }


        // GET: api/Representante/RepresentanteslistarporPersonaJuridico
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{IdPersona}")]
        public async Task<IActionResult> RepresentanteslistarporPersonaJuridico([FromRoute] Guid IdPersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == IdPersona)
                .Where(a => a.Tipo1 == 4 || a.Tipo2 == 4)
                .FirstOrDefaultAsync();

            if (repre == null)
            {
                return Ok(new { PersonaR = "", Telefono = "", CorreoElectronico = "", PersonaId = IdPersona });

            }

            return Ok(new RepresentanteViewModel
            {
                IdRepresentante = repre.IdRepresentante,
                RHechoId = repre.RHechoId,
                PersonaId = repre.PersonaId,
                PersonaR = repre.Nombres + " " + repre.ApellidoPa + " " + repre.ApellidoMa,
                Nombres = repre.Nombres,
                ApellidoPa = repre.ApellidoPa,
                ApellidoMa = repre.ApellidoMa,
                FechaNacimiento = repre.FechaNacimiento,
                Sexo = repre.Sexo,
                EntidadFeNacimiento = repre.EntidadFeNacimiento,
                Curp = repre.Curp,
                MedioNotificacion = repre.MedioNotificacion,
                Telefono = repre.Telefono,
                CorreoElectronico = repre.CorreoElectronico,
                Nacionalidad = repre.Nacionalidad,
                Genero = repre.Genero,
                Tipo1 = repre.Tipo1,
                Tipo2 = repre.Tipo2,
                CedulaProfesional = repre.CedulaProfesional,
                Fecha = repre.Fecha,
                UDistrito = repre.UDistrito,
                USubproc = repre.USubproc,
                UAgencia = repre.UAgencia,
                Usuario = repre.Usuario,
                UPuesto = repre.UPuesto,
                UModulo = repre.UModulo,
                Fechasys = repre.Fechasys
            });

        }


        // GET: api/Representante/RepresentanteslistarporPersonaParticular
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{IdPersona}")]
        public async Task<IActionResult> RepresentanteslistarporPersonaParticular([FromRoute] Guid IdPersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == IdPersona)
                .Where(a => a.Tipo1 == 3 || a.Tipo2 == 3 )
                .FirstOrDefaultAsync();

            if (repre == null)
            {
                return Ok(new { PersonaR = "", Telefono = "", CorreoElectronico = "", PersonaId = IdPersona, Direccion = "" });

            }

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            return Ok(new RepresentanteViewModel
            {
                IdRepresentante = repre.IdRepresentante,
                RHechoId = repre.RHechoId,
                PersonaId = repre.PersonaId,
                PersonaR = repre.Nombres + " " + repre.ApellidoPa + " " + repre.ApellidoMa,
                Nombres = repre.Nombres,
                ApellidoPa = repre.ApellidoPa,
                ApellidoMa = repre.ApellidoMa,
                FechaNacimiento = repre.FechaNacimiento,
                Sexo = repre.Sexo,
                EntidadFeNacimiento = repre.EntidadFeNacimiento,
                Curp = repre.Curp,
                MedioNotificacion = repre.MedioNotificacion,
                Telefono = repre.Telefono,
                CorreoElectronico = repre.CorreoElectronico,
                Nacionalidad = repre.Nacionalidad,
                Genero = repre.Genero,
                Tipo1 = repre.Tipo1,
                Tipo2 = repre.Tipo2,
                CedulaProfesional = repre.CedulaProfesional,
                Fecha = repre.Fecha,
                UDistrito = repre.UDistrito,
                USubproc = repre.USubproc,
                UAgencia = repre.UAgencia,
                Usuario = repre.Usuario,
                UPuesto = repre.UPuesto,
                UModulo = repre.UModulo,
                Fechasys = repre.Fechasys,
                Direccion = $"{(repre.TipoVialidad.HasValue ? tiposVialidades.FirstOrDefault(v => v.Clave == repre.TipoVialidad)?.Nombre + " " : "")}" +
                             $"{repre.Calle} {repre.NoExt} {repre.NoInt} {repre.Localidad} {repre.CP} {repre.Municipio} {repre.Estado}",
            });

        }

        // GET: api/Representante/RepresentantesActivos
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{idpersona}")]
        public async Task<IActionResult> RepresentantesActivos([FromRoute] Guid idpersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == idpersona)
                .Where(a => a.Tipo1 > 0 || a.Tipo2 > 0)
                .FirstOrDefaultAsync();


            if (repre == null)
            {
                return Ok(new { respuesta=false ,idpersona = idpersona });
            }

            return Ok(new { respuesta = true, idpersona = idpersona });


        }


        // GET: api/Representante/Representanteslistarporpersona
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{IdPersona}")]
        public async Task<IEnumerable<RepresentanteViewModel>> Representanteslistarporpersona([FromRoute] Guid IdPersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == IdPersona)
                .Include(a => a.Persona)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return repre.Select(a => new RepresentanteViewModel
            {
                IdRepresentante = a.IdRepresentante,
                RHechoId = a.RHechoId,
                PersonaId = a.PersonaId,
                PersonaR = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                Nombres = a.Nombres,
                ApellidoPa = a.ApellidoPa,
                ApellidoMa = a.ApellidoMa,
                FechaNacimiento = a.FechaNacimiento,
                Sexo = a.Sexo,
                EntidadFeNacimiento = a.EntidadFeNacimiento,
                Curp = a.Curp,
                MedioNotificacion = a.MedioNotificacion,
                Telefono = a.Telefono,
                CorreoElectronico = a.CorreoElectronico,
                Nacionalidad = a.Nacionalidad,
                Genero = a.Genero,
                Tipo1 = a.Tipo1,
                Tipo2 = a.Tipo2,
                CedulaProfesional = a.CedulaProfesional,
                Fecha = a.Fecha,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Calle = a.Calle,
                NoInt = a.NoInt,
                NoExt = a.NoExt,
                EntreCalle1 = a.EntreCalle1,
                EntreCalle2 = a.EntreCalle2,
                Referencia = a.Referencia,
                Pais = a.Pais,
                Estado = a.Estado,
                Municipio = a.Municipio,
                Localidad = a.Localidad,
                CP = a.CP,
                lat = a.lat,
                lng = a.lng,
                TipoVialidad = a.TipoVialidad,
                TipoAsentamiento = a.TipoAsentamiento,
            });



        }


        // GET: api/Representante/Representanteprimero
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{IdPersona}")]
        public async Task<IActionResult> Representanteprimero([FromRoute] Guid IdPersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == IdPersona)
                .FirstOrDefaultAsync();

            if (repre == null)
            {
                return Ok(new { PersonaR = "", Telefono = "", CorreoElectronico = "", PersonaId = IdPersona });

            }

            return Ok(new RepresentanteViewModel
            {
                IdRepresentante = repre.IdRepresentante,
                RHechoId = repre.RHechoId,
                PersonaId = repre.PersonaId,
                PersonaR = repre.Nombres + " " + repre.ApellidoPa + " " + repre.ApellidoMa,
                Nombres = repre.Nombres,
                ApellidoPa = repre.ApellidoPa,
                ApellidoMa = repre.ApellidoMa,
                FechaNacimiento = repre.FechaNacimiento,
                Sexo = repre.Sexo,
                EntidadFeNacimiento = repre.EntidadFeNacimiento,
                Curp = repre.Curp,
                MedioNotificacion = repre.MedioNotificacion,
                Telefono = repre.Telefono,
                CorreoElectronico = repre.CorreoElectronico,
                Nacionalidad = repre.Nacionalidad,
                Genero = repre.Genero,
                Tipo1 = repre.Tipo1,
                Tipo2 = repre.Tipo2,
                CedulaProfesional = repre.CedulaProfesional,
                Fecha = repre.Fecha,
                UDistrito = repre.UDistrito,
                USubproc = repre.USubproc,
                UAgencia = repre.UAgencia,
                Usuario = repre.Usuario,
                UPuesto = repre.UPuesto,
                UModulo = repre.UModulo,
                Fechasys = repre.Fechasys
            });

        }

        // GET: api/Representante/RepresentantesActivoPublico
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{idpersona}")]
        public async Task<IActionResult> RepresentantesActivoPublico([FromRoute] Guid idpersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == idpersona)
                .Where(a => a.Tipo1 == 2 || a.Tipo2 == 2)
                .FirstOrDefaultAsync();


            if (repre == null)
            {
                return Ok(new { respuesta = false, idpersona = idpersona });
            }

            return Ok(new { respuesta = true, idpersona = idpersona });


        }

        // GET: api/Representante/RepresentantesActivoParticular
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{idpersona}")]
        public async Task<IActionResult> RepresentantesActivoParticular([FromRoute] Guid idpersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == idpersona)
                .Where(a => a.Tipo1 == 3 || a.Tipo2 == 3)
                .FirstOrDefaultAsync();


            if (repre == null)
            {
                return Ok(new { respuesta = false, idpersona = idpersona });
            }

            return Ok(new { respuesta = true, idpersona = idpersona });


        }


        // GET: api/Representante/RepresentanteslistarporPersonaCoadyuvante
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{IdPersona}")]
        public async Task<IActionResult> RepresentanteslistarporPersonaCoadyuvante([FromRoute] Guid IdPersona)
        {
            var repre = await _context.Representantes
                .Where(a => a.PersonaId == IdPersona)
                .Where(a => a.Tipo1 == 6 || a.Tipo2 == 6)
                .FirstOrDefaultAsync();

            if (repre == null)
            {
                return Ok(new { PersonaR = "SIN REGISTRO DE REPRESENTANTE COADYUVANTE", Telefono = "", CorreoElectronico = "", PersonaId = IdPersona });

            }

            return Ok(new RepresentanteViewModel
            {
                IdRepresentante = repre.IdRepresentante,
                RHechoId = repre.RHechoId,
                PersonaId = repre.PersonaId,
                PersonaR = repre.Nombres + " " + repre.ApellidoPa + " " + repre.ApellidoMa,
                Nombres = repre.Nombres,
                ApellidoPa = repre.ApellidoPa,
                ApellidoMa = repre.ApellidoMa,
                FechaNacimiento = repre.FechaNacimiento,
                Sexo = repre.Sexo,
                EntidadFeNacimiento = repre.EntidadFeNacimiento,
                Curp = repre.Curp,
                MedioNotificacion = repre.MedioNotificacion,
                Telefono = repre.Telefono,
                CorreoElectronico = repre.CorreoElectronico,
                Nacionalidad = repre.Nacionalidad,
                Genero = repre.Genero,
                Tipo1 = repre.Tipo1,
                Tipo2 = repre.Tipo2,
                CedulaProfesional = repre.CedulaProfesional,
                Fecha = repre.Fecha,
                UDistrito = repre.UDistrito,
                USubproc = repre.USubproc,
                UAgencia = repre.UAgencia,
                Usuario = repre.Usuario,
                UPuesto = repre.UPuesto,
                UModulo = repre.UModulo,
                Fechasys = repre.Fechasys
            });

        }
        //ELIMINAR UN REGISTRO CON COPIA A LA BD DE LOG
        // GET: api/Representante/Eliminar
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Eliminar([FromBody] EliminarNuc model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
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
                        MovimientoId = new Guid("5909f304-4550-44b4-b5e6-f7ae04676ca6")
                    };

                    ctx.Add(laRegistro);
                    var consultadocrep = await _context.DocsRepresentantes.Where(a => a.RepresentanteId == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();


                    if (consultadocrep != null)
                    {
                            LogDocsRepresentantes documento = new LogDocsRepresentantes
                            {
                                LogAdmonId = gLog,
                                IdDocsRepresentantes = consultadocrep.IdDocsRepresentantes,
                                RepresentanteId = consultadocrep.RepresentanteId,
                                TipoDocumento = consultadocrep.TipoDocumento,
                                NombreDocumento = consultadocrep.NombreDocumento,
                                DescripcionDocumento = consultadocrep.DescripcionDocumento,
                                Ruta = consultadocrep.Ruta,
                                Fecha = consultadocrep.Fecha,
                                UDistrito = consultadocrep.UDistrito,
                                USubproc = consultadocrep.USubproc,
                                UAgencia = consultadocrep.UAgencia,
                                Usuario = consultadocrep.Usuario,
                                UPuesto = consultadocrep.UPuesto,
                                UModulo = consultadocrep.UModulo,
                                Fechasys = consultadocrep.Fechasys

                            };
                            ctx.Add(documento);
                            _context.Remove(consultadocrep);
                        }
                
                    
                       

                            var consultaRepresentante = await _context.Representantes.Where(a => a.IdRepresentante == model.infoBorrado.registroId)
                                              .Take(1).FirstOrDefaultAsync();
                            if (consultaRepresentante == null)
                            {
                                return Ok(new { res = "Error", men = "No se encontró ninguna persona con la información enviada" });
                            }
                            else
                            {

                                LogRepresentante representante = new LogRepresentante
                                {
                                    LogAdmonId = gLog,
                                    IdRepresentante = consultaRepresentante.IdRepresentante,
                                    RHechoId = consultaRepresentante.RHechoId,
                                    PersonaId = consultaRepresentante.PersonaId,
                                    Nombres = consultaRepresentante.Nombres,
                                    ApellidoPa = consultaRepresentante.ApellidoPa,
                                    ApellidoMa = consultaRepresentante.ApellidoMa,
                                    FechaNacimiento = consultaRepresentante.FechaNacimiento,
                                    EntidadFeNacimiento = consultaRepresentante.EntidadFeNacimiento,
                                    Curp = consultaRepresentante.Curp,
                                    MedioNotificacion = consultaRepresentante.MedioNotificacion,
                                    Telefono = consultaRepresentante.Telefono,
                                    CorreoElectronico = consultaRepresentante.CorreoElectronico,
                                    Nacionalidad = consultaRepresentante.Nacionalidad,
                                    Genero = consultaRepresentante.Genero,
                                    Fecha = consultaRepresentante.Fecha,
                                    Tipo1 = consultaRepresentante.Tipo1,
                                    Tipo2 = consultaRepresentante.Tipo2,
                                    CedulaProfesional = consultaRepresentante.CedulaProfesional,
                                    UDistrito = consultaRepresentante.UDistrito,
                                    USubproc = consultaRepresentante.USubproc,
                                    UAgencia = consultaRepresentante.UAgencia,
                                    Usuario = consultaRepresentante.Usuario,
                                    UPuesto = consultaRepresentante.UPuesto,
                                    UModulo = consultaRepresentante.UModulo,
                                    Fechasys = consultaRepresentante.Fechasys,
                                    Calle = consultaRepresentante.Calle,
                                    NoInt = consultaRepresentante.NoInt,
                                    NoExt = consultaRepresentante.NoExt,
                                    EntreCalle1 = consultaRepresentante.EntreCalle1,
                                    EntreCalle2 = consultaRepresentante.EntreCalle2,
                                    Referencia = consultaRepresentante.Referencia,
                                    Pais = consultaRepresentante.Pais,
                                    Estado = consultaRepresentante.Estado,
                                    Municipio = consultaRepresentante.Municipio,
                                    Localidad = consultaRepresentante.Localidad,
                                    CP = consultaRepresentante.CP,
                                    lat = consultaRepresentante.lat,
                                    lng = consultaRepresentante.lng,
                                    ArticulosPenales = consultaRepresentante.ArticulosPenales
                                };
                                ctx.Add(representante);
                            
                                _context.Remove(consultaRepresentante);

                                //SE APLICAN TODOS LOS CAMBIOS A LA BD
                                await ctx.SaveChangesAsync();
                                await _context.SaveChangesAsync();
                            }
                   
                }

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
            // FIN DEL PROCESO
            return Ok(new { res = "success", men = "Representante eliminado Correctamente" });
        }


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/Representante/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            var listaRepresentantes = await _context.Representantes
                            .Include(a => a.DocsRepresentantes)
                            .Where(x => x.RHechoId == model.IdRHecho)
                            .ToListAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (listaRepresentantes == null)
            {
                return Ok();
            }
            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (Representante repActual in listaRepresentantes)
                    {
                        var insertarRep = await ctx.Representantes
                            .Include(a=>a.DocsRepresentantes)
                            .FirstOrDefaultAsync(a => a.IdRepresentante == repActual.IdRepresentante);

                        if (insertarRep == null)
                        {
                            insertarRep = new Representante();
                            ctx.Representantes.Add(insertarRep);
                        }
                        insertarRep.IdRepresentante = repActual.IdRepresentante;
                        insertarRep.RHechoId = repActual.RHechoId;
                        insertarRep.PersonaId = repActual.PersonaId;
                        insertarRep.Nombres = repActual.Nombres;
                        insertarRep.ApellidoPa = repActual.ApellidoPa;
                        insertarRep.ApellidoMa = repActual.ApellidoMa;
                        insertarRep.FechaNacimiento = repActual.FechaNacimiento;
                        insertarRep.Sexo = repActual.Sexo;
                        insertarRep.EntidadFeNacimiento = repActual.EntidadFeNacimiento;
                        insertarRep.Curp = repActual.Curp;
                        insertarRep.MedioNotificacion = repActual.MedioNotificacion;
                        insertarRep.Telefono = repActual.Telefono;
                        insertarRep.CorreoElectronico = repActual.CorreoElectronico;
                        insertarRep.Nacionalidad = repActual.Nacionalidad;
                        insertarRep.Genero = repActual.Genero;
                        insertarRep.Fecha = repActual.Fecha;
                        insertarRep.Tipo1 = repActual.Tipo1;
                        insertarRep.Tipo2 = repActual.Tipo2;
                        insertarRep.CedulaProfesional = repActual.CedulaProfesional;
                        insertarRep.UDistrito = repActual.UDistrito;
                        insertarRep.USubproc = repActual.USubproc;
                        insertarRep.UAgencia = repActual.UAgencia;
                        insertarRep.Usuario = repActual.Usuario;
                        insertarRep.UPuesto = repActual.UPuesto;
                        insertarRep.UModulo = repActual.UModulo;
                        insertarRep.Fechasys = repActual.Fechasys;
                        insertarRep.Calle = repActual.Calle;
                        insertarRep.NoInt = repActual.NoInt;
                        insertarRep.NoExt = repActual.NoExt;
                        insertarRep.EntreCalle1 = repActual.EntreCalle1;
                        insertarRep.EntreCalle2 = repActual.EntreCalle2;
                        insertarRep.Referencia = repActual.Referencia;
                        insertarRep.Pais = repActual.Pais;
                        insertarRep.Estado = repActual.Estado;
                        insertarRep.Municipio = repActual.Municipio;
                        insertarRep.Localidad = repActual.Localidad;
                        insertarRep.CP = repActual.CP;
                        insertarRep.lat = repActual.lat;
                        insertarRep.lng = repActual.lng;
                        insertarRep.ArticulosPenales = repActual.ArticulosPenales;
                        insertarRep.TipoVialidad = repActual.TipoVialidad;
                        insertarRep.TipoAsentamiento = repActual.TipoAsentamiento;

                        //SOLO COPIA EL PRIMER DOC DEL REPRESENTANTE, HASTA DONDE SE OFICIALMENTE NO HAY MAS DE UNO
                        var docRepActual = await _context.DocsRepresentantes
                            .Where(x => x.RepresentanteId == repActual.IdRepresentante)
                            .FirstOrDefaultAsync();
                        if (docRepActual != null)
                        {//BUSCA SI EN EL SERVIDOR DESTINO YA EXISTE EL REGISTRO
                            var insertarDocRep = await ctx.DocsRepresentantes.FirstOrDefaultAsync(a => a.IdDocsRepresentantes == docRepActual.IdDocsRepresentantes);

                            if (insertarDocRep == null)
                            {
                                insertarDocRep = new DocsRepresentantes();
                                ctx.DocsRepresentantes.Add(insertarDocRep);
                            }

                            insertarDocRep.IdDocsRepresentantes = repActual.DocsRepresentantes.IdDocsRepresentantes;
                            insertarDocRep.RepresentanteId = repActual.DocsRepresentantes.RepresentanteId;
                            insertarDocRep.TipoDocumento = repActual.DocsRepresentantes.TipoDocumento;
                            insertarDocRep.NombreDocumento = repActual.DocsRepresentantes.NombreDocumento;
                            insertarDocRep.DescripcionDocumento = repActual.DocsRepresentantes.DescripcionDocumento;
                            insertarDocRep.Ruta = repActual.DocsRepresentantes.Ruta;
                            insertarDocRep.Fecha = repActual.DocsRepresentantes.Fecha;
                            insertarDocRep.UDistrito = repActual.DocsRepresentantes.UDistrito;
                            insertarDocRep.USubproc = repActual.DocsRepresentantes.USubproc;
                            insertarDocRep.UAgencia = repActual.DocsRepresentantes.UAgencia;
                            insertarDocRep.Usuario = repActual.DocsRepresentantes.Usuario;
                            insertarDocRep.UPuesto = repActual.DocsRepresentantes.UPuesto;
                            insertarDocRep.UModulo = repActual.DocsRepresentantes.UModulo;
                            insertarDocRep.Fechasys = repActual.DocsRepresentantes.Fechasys;
                        }
                        await ctx.SaveChangesAsync();
                    }
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
        [HttpGet("[action]/{IdPersona}")]
        public async Task<IActionResult> RepresentanteCat([FromRoute] Guid IdPersona)
        {
            var representante = await _context.Representantes
                               .Include(a => a.Persona)
                               .Where(x => x.PersonaId == IdPersona)
                               .FirstOrDefaultAsync();
            if (representante == null) {
                
                return Ok (new {NoHayAr = 1});

            }
            else
            {
                return Ok(new ViewModelMostrarRepresentanteJR
                {
                IdResponsable = representante.IdRepresentante,
                PersonaId = representante.PersonaId,
                Nombre = representante.Nombres,
                ApellidoPa = representante.ApellidoPa,
                ApellidoMa = representante.ApellidoMa,
                Nacionalidad = representante.Nacionalidad,
                FechaNacimiento = representante.FechaNacimiento,
                CURP = representante.Curp,
                Telefono = representante.Telefono,
                Correo = representante.CorreoElectronico,
                Calle = representante.Calle,
                NoExt = representante.NoExt,
                NoInt = representante.NoInt,
                EntreCalle1 = representante.EntreCalle1,
                EntreCalle2 = representante.EntreCalle2,
                Referencia = representante.Referencia,
                Pais = representante.Pais,
                Estado = representante.Estado,
                Municipio = representante.Municipio,
                Localidad = representante.Localidad,
                CodigoPostal = representante.CP,
                TipoVialidad = representante.TipoVialidad,
                TipoAsentamiento = representante.TipoAsentamiento,
                });

            }
        }
        // Trae los representantes activos del tipo 1 y 7
        // GET: api/Representante/ConsultaRepresentante
        // API: VALIDAR SI EXISTE UN REPRESENTANTE
        [Authorize(Roles = "Administrador,Director,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto,Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{personaId}")]
        public async Task<IActionResult> ConsultaRepresentante([FromRoute] Guid personaId)
        {

            var Representante = await _context.Representantes
                                        .Where(a => a.PersonaId == personaId && (a.Tipo1 == 1 || a.Tipo1 == 7 || a.Tipo2 == 1 || a.Tipo2 == 7))
                                        .ToListAsync();

            if (Representante == null || !Representante.Any())
            {
                return NoContent();
            }

            var tiposRepresentantes = await _context.TiposRepresentantes.ToListAsync();

            var tiposVialidades = await _context.Vialidades.ToListAsync();

            var resultado = Representante.Select(r => new GetRepresentantesActivosViewModel
            {
                IdRepresentante = r.IdRepresentante,
                PersonaId = r.PersonaId,
                NombreCompleto = $"{r.Nombres} {r.ApellidoPa} {r.ApellidoMa}",
                Sexo = r.Sexo,
                Curp = r.Curp,
                CorreoElectronico = r.CorreoElectronico,
                Telefono = r.Telefono,
                Tipo1 = r.Tipo1,
                Tipo2 = r.Tipo2,
                CedulaProfesional = r.CedulaProfesional,
                MedioNotificacion = r.MedioNotificacion,
                TipoVialidad = r.TipoVialidad,
                TipoAsentamiento = r.TipoAsentamiento,
                Direccion = $"{(tiposVialidades.Any(v => v.Clave == r.TipoVialidad) ? tiposVialidades.FirstOrDefault(v => v.Clave == r.TipoVialidad)?.Nombre + " " : "")}" +
                            $"{r.Calle} No. {r.NoExt} {(!string.IsNullOrEmpty(r.NoInt) ? $"Int. {r.NoInt}" : "")}, {r.Localidad}, {r.Municipio}, {r.Estado}, {r.Pais}, CP: {r.CP}",
                Tipo1Nombre = r.Tipo1 > 0
                    ? tiposRepresentantes.FirstOrDefault(t => t.Valor == r.Tipo1)?.Tipo
                : "No activo",
                Tipo2Nombre = r.Tipo2 > 0
                    ? tiposRepresentantes.FirstOrDefault(t => t.Valor == r.Tipo2)?.Tipo
                    : "No activo"
            }).ToList();

            return Ok(resultado);
        }
    }


}

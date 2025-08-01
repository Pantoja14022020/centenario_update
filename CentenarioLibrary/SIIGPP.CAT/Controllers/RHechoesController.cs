using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using SIIGPP.CAT.FilterClass;
using SIIGPP.CAT.Models.Orientacion;
using SIIGPP.CAT.Models.RDHechos;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Orientacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RHechoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;



        public RHechoesController(DbContextSIIGPP context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }


        // GET: api/RHechoes/ListarPorId
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> ListarPorId([FromRoute] Guid rHechoId)
        {
            var a = await _context.RHechoes
                          .Include(x => x.RAtencion)
                          .Include(x => x.NUCs)
                          .Where(x => x.IdRHecho == rHechoId)
                          .FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new ListarEntrevistaInicial
            {

                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId, 
                
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                nucId = a.NucId,
                NDenunciaOficio = a.NDenunciaOficio,
                Mediodenuncia = a.RAtencion.MedioDenuncia,
                nuc = a.NUCs.nucg,
                FechaElevaNuc = a.FechaElevaNuc,
                FechaHoraSuceso = a.FechaHoraSuceso,
                RBreve = a.RBreve,
                NarrativaHechos = a.NarrativaHechos,
                Vanabim = a.Vanabim,
                Statuscarpeta = a.NUCs.StatusNUC,
                Etapacarpeta = a.NUCs.Etapanuc


            });

        }

        // GET: api/RHechoes/ListarPorModulo
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{idModuloServicio}")]
        public async Task<IEnumerable<ListarMisCarpetasViewModel>> ListarPorModulo([FromRoute] Guid idModuloServicio)
        {
            var carpetas = await _context.RHechoes 
                          .Include(a => a.RAtencion)
                          .Include(a => a.NUCs)
                          .Where(a => a.NucId != null)
                          .Where(a => a.ModuloServicioId == idModuloServicio)          
                          .OrderByDescending(a => a.FechaElevaNuc2)
                          .ToListAsync();

            return carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                nucId = a.NucId,
                nuc = a.NUCs.nucg,
                FechaElevaNuc = a.FechaElevaNuc,
                NDenunciaOficio = a.NDenunciaOficio,

            });

        }


        // GET: api/RHechoes/ListarTodosRegistros
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarTodosRegistros([FromRoute] Guid rAtencionId)
        {
            var a = await _context.RHechoes
                          .Include(x => x.RAtencion) 
                          .Include(x => x.RAtencion.RACs) 
                          .Where(x => x.RAtencionId == rAtencionId).FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }
            try
            {
                return Ok(new ListarRacsViewModel
                {
                    RHechoId = a.IdRHecho,
                    RAtencionId = a.RAtencionId,
                    RacId = a.RAtencion.RACs.idRac,
                    u_Nombre = a.RAtencion.u_Nombre,
                    u_Puesto = a.RAtencion.u_Puesto,
                    u_Modulo = a.RAtencion.u_Modulo,
                    DistritoInicial = a.RAtencion.DistritoInicial,
                    DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                    Agenciaid = a.Agenciaid,
                    AgenciaInicial = a.RAtencion.AgenciaInicial,
                    FechaHoraRegistro = a.RAtencion.FechaHoraRegistro,
                    Rac = a.RAtencion.RACs.racg,
                    rBreve = a.RBreve,
                    FechaElevaNuc = a.FechaElevaNuc,
                    FechaHoraSuceso = a.FechaHoraSuceso,
                    NDenunciaOficio = a.NDenunciaOficio,
                    Numerooficio = a.RAtencion.ModuloServicio

                });
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }



        }
        // GET: api/RHechoes/ListarPorrAtencionId
        //[Authorize(Roles = " Recepción,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarPorrAtencionId([FromRoute] Guid rAtencionId)
        {
            var a = await _context.RHechoes 
                          .Include(x => x.RAtencion) 
                          .Include(x => x.NUCs) 
                          .Include(x => x.RAtencion.RACs)
                          .Where(x => x.RAtencionId == rAtencionId).FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }
            try
            {
                return Ok(new ListarCarpetasPersonaViewModel
                {
                    RHechoId = a.IdRHecho,
                    RAtencionId = a.RAtencionId,
                    u_Nombre = a.RAtencion.u_Nombre,
                    u_Puesto = a.RAtencion.u_Puesto,
                    u_Modulo = a.RAtencion.u_Modulo,
                    DistritoInicial = a.RAtencion.DistritoInicial,
                    DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                    Agenciaid = a.Agenciaid,
                    AgenciaInicial = a.RAtencion.AgenciaInicial,
                    FechaHoraRegistro = a.RAtencion.FechaHoraRegistro,
                    Rac = a.RAtencion.RACs.racg, 
                    nucId = a.NUCs.idNuc,
                    nuc = a.NUCs.nucg,
                    FechaElevaNuc = a.FechaElevaNuc,
                    FechaHoraSuceso = a.FechaHoraSuceso, 
                    NDenunciaOficio = a.NDenunciaOficio,
                    Texto = a.Texto

                });
            }
            catch (DbUpdateConcurrencyException)
            {
                
                return BadRequest();
            }

          

        }

        // GET: api/RHechoes/ComprobarIN
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}/{rAtencionId}")]
            public async Task<IActionResult> ComprobarIN([FromRoute] Guid RHechoId, Guid rAtencionId)
        {
            try
                {
                    String comprobarDatos = @"select 
                                                    r.IdRAP,
                                                    h.IdRHecho, 
                                                    h.FechaHoraSuceso, 
                                                    d.IdDDelito,
                                                    r.ClasificacionPersona,
                                                    n.StatusNUC,
                                                    n.Etapanuc,
                                                    p.IdPersona
                                                    from CAT_RAP as r
                                                    LEFT JOIN CAT_RATENCON as a on a.IdRAtencion = r.RAtencionId
                                                    LEFT JOIN CAT_PERSONA as p on p.IdPersona = r.PersonaId
                                                    LEFT JOIN CAT_RHECHO as h on h.RAtencionId = r.RAtencionId
                                                    LEFT JOIN CAT_DIRECCION_DELITO as d on d.RHechoId = h.IdRHecho
                                                    LEFT JOIN NUC as n on n.idNuc = h.NucId
                                                    where h.IdRHecho = @hechoid and a.IdRAtencion = @ratencionid";

                    List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                    filtrosBusqueda.Add(new SqlParameter("@hechoid", RHechoId));
                    filtrosBusqueda.Add(new SqlParameter("@ratencionid", rAtencionId));

                var compro = await _context.qComprobarDatos.FromSqlRaw(comprobarDatos, filtrosBusqueda.ToArray()).ToListAsync();



                    return Ok(compro.Select(a => new ComprobarViewModel
                    {
                        IdRAP=a.IdRAP,
                        IdRHecho =a.IdRHecho,
                        FechaHoraSuceso=a.FechaHoraSuceso,
                        IdDDelito=a.IdDDelito,
                        ClasificacionPersona=a.ClasificacionPersona,
                        StatusNUC=a.StatusNUC,
                        Etapanuc=a.Etapanuc,
                        IdPersona=a.IdPersona                       

                    }));
                }
                catch (Exception ex)
                {
                    var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message , version = "version 1.4.1" });
                    result.StatusCode = 402;
                    return result;
                }
             



        }
        // GET: api/RHechoes/ComprobarINX
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}/{rAtencionId}")]
        public async Task<IActionResult> ComprobarINX([FromRoute] Guid RHechoId, Guid rAtencionId)
        {
            try
            {
                String comprobarDatos = @"select 
                                                    r.IdRAP,
                                                    h.IdRHecho, 
                                                    h.FechaHoraSuceso, 
                                                    d.IdDDelito,
                                                    r.ClasificacionPersona,
                                                    n.StatusNUC,
                                                    n.Etapanuc,
                                                    p.IdPersona,
                                                    e.IdRDH,
                                                    m.idAmpliacion
                                                    from CAT_RAP as r
                                                    LEFT JOIN CAT_RATENCON as a on a.IdRAtencion = r.RAtencionId
                                                    LEFT JOIN CAT_PERSONA as p on p.IdPersona = r.PersonaId
                                                    LEFT JOIN CAT_RHECHO as h on h.RAtencionId = r.RAtencionId
                                                    LEFT JOIN CAT_DIRECCION_DELITO as d on d.RHechoId = h.IdRHecho
                                                    LEFT JOIN NUC as n on n.idNuc = h.NucId
                                                    LEFT JOIN CAT_RDH AS e on e.RHechoId = h.IdRHecho
                                                    LEFT JOIN CAT_AMPDEC as m on m.HechoId = h.IdRHecho
                                                    where h.IdRHecho = @hechoid and a.IdRAtencion = @ratencionid";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@hechoid", RHechoId));
                filtrosBusqueda.Add(new SqlParameter("@ratencionid", rAtencionId));

                var compro = await _context.qComprobarDatosR.FromSqlRaw(comprobarDatos, filtrosBusqueda.ToArray()).ToListAsync();



                return Ok(compro.Select(a => new ComprobarViewModelR
                {
                    IdRAP = a.IdRAP,
                    IdRHecho = a.IdRHecho,
                    FechaHoraSuceso = a.FechaHoraSuceso,
                    IdDDelito = a.IdDDelito,
                    ClasificacionPersona = a.ClasificacionPersona,
                    StatusNUC = a.StatusNUC,
                    Etapanuc = a.Etapanuc,
                    IdPersona = a.IdPersona,
                    IdRDH=a.IdRDH,
                    idAmpliacion=a.idAmpliacion


                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

        }

        // GET: api/RHechoes/ComprobarINX
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> ComprobarNuc([FromRoute] Guid RHechoId)
        {
            try
            {
                String comprobarDatos = @"SELECT 
                                                    H.IdRHecho,
                                                    CAST(MAX(CASE WHEN H.FechaHoraSuceso IS NOT NULL THEN 1 ELSE 0 END) AS BIT) AS FechaSuceso,
                                                    CAST(MAX(CASE WHEN D.IdDDelito IS NOT NULL THEN 1 ELSE 0 END) AS BIT) AS DireccionDelito,
                                                    CAST(MAX(CASE WHEN R.ClasificacionPersona = 'Imputado' THEN 1 ELSE 0 END) AS BIT) AS Imputado,
                                                    CAST(MAX(CASE WHEN R.ClasificacionPersona LIKE '%Victima%' THEN 1 ELSE 0 END) AS BIT) AS Victima,
                                                    CAST(MAX(CASE WHEN N.Etapanuc = 'Inicial' THEN 0 ELSE 1 END) AS BIT) AS Estatus,
                                                    CAST(MAX(CASE WHEN E.IdRDH IS NOT NULL THEN 1 ELSE 0 END) AS BIT) AS Delito,
                                                    CAST(MAX(CASE WHEN M.idAmpliacion IS NOT NULL THEN 1 ELSE 0 END) AS BIT) AS Entrevista
                                                FROM 
                                                    CAT_RHECHO AS H
                                                LEFT JOIN CAT_RAP AS R ON H.RAtencionId = R.RAtencionId
                                                LEFT JOIN CAT_DIRECCION_DELITO AS D ON D.RHechoId = H.IdRHecho
                                                LEFT JOIN NUC AS N ON N.idNuc = H.NucId
                                                LEFT JOIN CAT_RDH AS E ON E.RHechoId = H.IdRHecho
                                                LEFT JOIN CAT_AMPDEC AS M ON M.HechoId = H.IdRHecho
                                                WHERE 
                                                    H.IdRHecho = @hechoid 
                                                GROUP BY 
                                                    H.IdRHecho";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@hechoid", RHechoId));

                var compro = await _context.qComprobarDatosNuc.FromSqlRaw(comprobarDatos, filtrosBusqueda.First()).FirstOrDefaultAsync();



                return Ok( new ComprobarNucViewModel
                {
                    IdRHecho = compro.IdRHecho,
                    FechaSuceso = compro.FechaSuceso,
                    DireccionDelito = compro.DireccionDelito,
                    Imputado = compro.Imputado,
                    Victima = compro.Victima,
                    Estatus = compro.Estatus,
                    Delito = compro.Delito,
                    Entrevista = compro.Entrevista,



                });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

        }



        // GET: api/RHechoes/ComprobarRemision
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> ComprobarRemision([FromRoute] Guid rHechoId)
        {
            var a = await _context.RHechoes
                          .Where(x => x.IdRHecho == rHechoId)
                          .FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new ComprobarRemision
            {

                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                Status = a.Status,
                nucId = a.NucId,
                FechaElevaNuc = a.FechaElevaNuc,
                FechaHoraSuceso = a.FechaHoraSuceso,
                RBreve = a.RBreve,
                ModuloServicioId=a.ModuloServicioId,




            });

        }


        // POST: api/RHechoes/CrearPI
        //[Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearPI(CrearViewModelH model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;

            

            RHecho InsertarRH = new RHecho
            {
                RAtencionId = model.rAtencionId,
                ModuloServicioId = model.moduloServicioId,
                Agenciaid = model.agenciaId,
                Status = model.status,
                RBreve = model.rbreve, 
                FechaReporte = model.FechaReporte,
                NDenunciaOficio = model.NDenunciaOficio,
                Texto = model.Texto,
                Observaciones = model.Observaciones,
                FechaHoraSuceso2 = new DateTime(0002, 1, 1, 0, 02, 0)

            };



            _context.RHechoes.Add(InsertarRH);


         

            try
            {
                await _context.SaveChangesAsync();

                //**********************************************************************
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            //return Ok(new { idRAH = InsertarRAH.IdRAH, idRH = InsertarRH.IdRHecho });
            return Ok(new { idRH = InsertarRH.IdRHecho });
        }

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        // PUT: api/RHechoes/ActualizarResenaBreve
        [Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ActualizarResenaBreve([FromBody] ActualizarResenaBreveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //ELEVAMOS EL NUC EN LA TABLA DE RHECHOS
            // El campo status se registra en FALSE hasta que se eleva a NUC
            //******************************************************************************************
            var resenaBreve = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            if (resenaBreve == null)
            {
                return NotFound();
            }

            resenaBreve.RBreve = model.RBreve; 
            
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

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/




        // PUT: api/RHechoes/ActualizarNUC
        [Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarNUC([FromBody] ActualizarNUCViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //ELEVAMOS EL NUC EN LA TABLA DE RHECHOS
            // El campo status se registra en FALSE hasta que se eleva a NUC
            //******************************************************************************************
            var elevaNUC = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            if (elevaNUC == null)
            {
                return NotFound();
            }
            elevaNUC.NucId = model.nucId;
            elevaNUC.Status = true;
            elevaNUC.FechaElevaNuc  = System.DateTime.Now;
            elevaNUC.FechaElevaNuc2 = System.DateTime.Now;
            //******************************************************************************************
            // ACTUALIZAMOS EL REGISTRO   DE ATENCION 
            // El Campo statusRegistro simpre  se registra en TRUE hasta que se eleva a NUC o se  queda como RAC
            var bajaRAC = await _context.RAtencions.FirstOrDefaultAsync(a => a.IdRAtencion == model.ratencionId);
            if (bajaRAC == null)
            {
                return NotFound();
            }
            bajaRAC.StatusRegistro = true;
            //******************************************************************************************


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

        // PUT: api/RHechoes/ActualizarFHS
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarFHS([FromBody] ActualizarFHSViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

 
            //actualizamos la fecha y hora del suceso 
            //******************************************************************************************
            var actualizamosFHS = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            if (actualizamosFHS == null)
            {
                return NotFound();
            }

            actualizamosFHS.FechaHoraSuceso = model.fechaHoraSuceso;
            actualizamosFHS.FechaHoraSuceso2 = model.fechaHoraSuceso;
            //******************************************************************************************
          

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
        // PUT: api/RHechoes/ActualizarNarrativa
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarNarrativa([FromBody] ActualizarNarrativaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //actualizamos la fecha y hora del suceso 
            //******************************************************************************************
            var actualizamosN = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            if (actualizamosN == null)
            {
                return NotFound();
            }

            actualizamosN.NarrativaHechos = model.narrativaHechos;
            //******************************************************************************************


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


        // GET: api/RHechoes/ListarporIdNUC
        [Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador,AMPO-IL,Recepción")]
        [HttpGet("[action]/{nucid}")]
        public async Task<IActionResult> ListarporIdNUc([FromRoute] Guid nucid)
        {
            var rhec = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.ModuloServicio)
                          .Include(a => a.ModuloServicio.Agencia.DSP)
                          .Where(a => a.NucId == nucid).FirstOrDefaultAsync();

            if (rhec == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new ListarMPMesaViewModel
            {
                IdRHecho = rhec.IdRHecho,
                Mp = rhec.RAtencion.AgenciaInicial,
                Modulo = rhec.ModuloServicio.Nombre,
                RAtencionId = rhec.RAtencionId,
                RBreve = rhec.RBreve,
                FechaHoraSuceso = rhec.FechaHoraSuceso,
                NDenunciaOficio = rhec.NDenunciaOficio,
                DistritoId = rhec.ModuloServicio.Agencia.DSP.DistritoId,
                IdAgenciaFusion = rhec.Agenciaid,
                IdModuloFusion = rhec.ModuloServicioId
            });

        }

        // PUT: api/RHechoes/ActualizarVanabim
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarVanabim([FromBody] ActualizarVanabimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var rhecho = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            if (rhecho == null)
            {
                return NotFound();
            }

            rhecho.Vanabim = model.Vanabim;

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

        // GET: api/RHechoes/InformacionCompleta/fechai/fechaf
        [Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador,Director,Coordinador,Recepción")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<ListarMisCarpetasViewModel>> InformacionCompleta([FromRoute] Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var carpetas = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.NUCs)
                          .Include(a => a.ModuloServicio)
                          .OrderByDescending(a => a.IdRHecho)
                          .Where(a => a.FechaElevaNuc >= fechai)
                          .Where(a => a.FechaElevaNuc <= fechaf)
                          .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                          .Where(a => a.NucId != null)
                          .ToListAsync();

            return carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                nucId = a.NucId,
                nuc = a.NUCs.nucg,
                FechaElevaNuc = a.FechaElevaNuc,
                Modulo = a.ModuloServicio.Nombre,
              
               

            });

        }


        //GET: api/RHechoes/ContarCarpetasiniciadas/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]

        [Authorize(Roles = "Administrador,Director,Coordinador,")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasModuloViewModel>> ContarCarpetasiniciadas([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.RHechoes
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.NucId != null)
                                      .Where(a => a.FechaElevaNuc >= fechai)
                                      .Where(a => a.FechaElevaNuc <= fechaf)
                                      .Include(a => a.ModuloServicio)
                                      .GroupBy(v => v.ModuloServicio.Nombre)
                                      .Select(x => new {etiqueta = x.Key
                                      ,valor1 = x.Count(v => v.ModuloServicio.Nombre == x.Key)})
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasModuloViewModel
            {
                Modulo = v.etiqueta.ToString(),
                Ciniciadas = v.valor1
            }

            );
        }

        //GET: api/RHechoes/ContarCarpetasiniciadasFechaMes/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]

        [Authorize(Roles = "Administrador,Director,Coordinador,")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> ContarCarpetasiniciadasFechaMes([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.RHechoes
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.NucId != null)
                                      .Where(a => a.FechaElevaNuc >= fechai)
                                      .Where(a => a.FechaElevaNuc <= fechaf)
                                      .GroupBy(v => v.FechaElevaNuc2.Day)
                                      .Select(x => new {etiqueta = x.Key
                                      ,valor1 = x.Count(v => v.FechaElevaNuc2.Day == x.Key)})
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fecha = v.etiqueta,
                Ciniciadas = v.valor1
            }

            );
        }
        public string mes(int a)
        {
            if (a == 1) return "Enero";
            if (a == 2) return "Febrero";
            if (a == 3) return "Marzo";
            if (a == 4) return "Abril";
            if (a == 5) return "Mayo";
            if (a == 6) return "Junio";
            if (a == 7) return "Julio";
            if (a == 8) return "Agosto";
            if (a == 9) return "Septiembre";
            if (a == 10) return "Octubre";
            if (a == 11) return "Noviembre";
            else return "Diciembre";

        }

        //GET: api/RHechoes/ContarCarpetasiniciadasFechaAño/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]

        [Authorize(Roles = "Administrador,Director,Coordinador,")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> ContarCarpetasiniciadasFechaAño([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.RHechoes
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.NucId != null)
                                      .Where(a => a.FechaElevaNuc >= fechai)
                                      .Where(a => a.FechaElevaNuc <= fechaf)
                                      .GroupBy(v => v.FechaElevaNuc2.Month)
                                      .Select(x => new {etiqueta = x.Key
                                      ,valor1 = x.Count(v => v.FechaElevaNuc2.Month == x.Key)})
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fechas = mes(v.etiqueta),
                Ciniciadas = v.valor1
            }

            );
        }

        //GET: api/RHechoes/ContarCarpetasiniciadasFechaAños/fechai/fechaf
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]

        [Authorize(Roles = "Administrador,Director,Coordinador,")]
        [HttpGet("[action]/{iddsp}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasFechaViewModel>> ContarCarpetasiniciadasFechaAños([FromRoute]Guid iddsp, DateTime fechai, DateTime fechaf)
        {
            var Tabla = await _context.RHechoes
                                      .Where(a => a.ModuloServicio.Agencia.DSPId == iddsp)
                                      .Where(a => a.NucId != null)
                                      .Where(a => a.FechaElevaNuc >= fechai)
                                      .Where(a => a.FechaElevaNuc <= fechaf)
                                      .GroupBy(v => v.FechaElevaNuc2.Year)
                                      .Select(x => new {etiqueta = x.Key,valor1 = x.Count(v => v.FechaElevaNuc2.Year == x.Key)})
                                      .ToListAsync();

            return Tabla.Select(v => new EstadisticasFechaViewModel
            {
                Fecha = v.etiqueta,
                Ciniciadas = v.valor1
            }

            );
        }


        // PUT: api/RHechoes/ActualizarModuloyAgencia
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarModuloyAgencia([FromBody] ActualizarAgenciayModulo model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var rhecho = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            if (rhecho == null)
            {
                return NotFound();
            }

            rhecho.ModuloServicioId = model.moduloServicioId;
            rhecho.Agenciaid = model.agenciaId;
            

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

        //Api sencilla para obtener el distrito de origen de una carpeta
        // GET: api/RHechoes/obtenerDistrtitoOrigen
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL,Recepción")]
        [HttpGet("[action]/{agenciaenvio}")]
        public async Task<IActionResult> ObtenerDistritoOrigen([FromRoute] Guid agenciaenvio)
        {
            var disOrigen = await _context.Agencias
                          .Include(a => a.DSP.Distrito)
                          .Where(a => a.IdAgencia == agenciaenvio).FirstOrDefaultAsync();

            //Es una api muy sencilla que solo obtiene un valor
            if (disOrigen == null)
            {
                return NotFound("No de encontro el distrito origen");
            }

            //No se creo otro archivo para este viewModel, se creo otra clase en el mismo archivo
            return Ok(new ListarDistritoOrigen
            {
                //Solo se requiere el id del distrito de origen
                DistritoId = disOrigen.DSP.DistritoId,

            });

        }

        //Api sencilla para obtener el distrito de origen de una carpeta
        // GET: api/RHechoes/obtenerDistrtitoOrigen
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL,Recepción")]
        [HttpGet("[action]/{moduloServicioId}")]
        public async Task<IActionResult> ObtenerDistritoAgenciaOrigen([FromRoute] Guid moduloServicioId)
        {
            var disOrigen = await _context.ModuloServicios
                          .Include(a => a.Agencia)
                          .Include(a => a.Agencia.DSP)
                          .Include(a => a.Agencia.DSP.Distrito)
                          .Where(a => a.IdModuloServicio == moduloServicioId).FirstOrDefaultAsync();

            //Es una api muy sencilla que solo obtiene un valor
            if (disOrigen == null)
            {
                return NotFound("No de encontro el distrito origen");
            }

            //No se creo otro archivo para este viewModel, se creo otra clase en el mismo archivo
            return Ok(new ListarDistritoOrigenRenvio
            {
                //Solo se requiere el id del distrito de origen
                DistritoId = disOrigen.Agencia.DSP.Distrito.IdDistrito,
                AgenciaId = disOrigen.Agencia.IdAgencia,

            });

        }
        // PUT: api/RHechoes/ActualizarModuloyAgenciaRechazo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarModuloyAgenciaRechazo([FromBody] ActualizarAgenciayModuloEntreDistritos model)
        {
            //Try para que me muestre en reponse la razon de un posible error
            try
            {
                //Conexion a base de datos
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    //Actualizado de informacion entre distritos
                    var rhecho = await ctx.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

                    if (rhecho == null)
                    {
                        return NotFound();
                    }

                    rhecho.ModuloServicioId = model.moduloServicioId;
                    rhecho.Agenciaid = model.agenciaId;

                    await ctx.SaveChangesAsync();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

        }


        // GET: api/RHechoes/ListarPorModuloRACS
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{idModuloServicio}")]
        public async Task<IEnumerable<ListarMisCarpetasViewModel>> ListarPorModuloRACS([FromRoute] Guid idModuloServicio)
        {
            var carpetas = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.ModuloServicio)
                          .Include(a => a.RAtencion.RACs)
                          .Where(a => a.NucId == null)
                          .Where(a => a.ModuloServicioId == idModuloServicio)
                          .ToListAsync();

            return carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                NDenunciaOficio = a.NDenunciaOficio,
                RAC = a.RAtencion.RACs.racg,
                Modulos = a.ModuloServicio.Nombre


            });

        }

        // GET: api/RHechoes/ListarPorIdRACS
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> ListarPorIdRACS([FromRoute] Guid rHechoId)
        {
            var a = await _context.RHechoes
                          .Include(x => x.RAtencion)
                          .Include(x => x.ModuloServicio)
                          .Where(x => x.IdRHecho == rHechoId).FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new ListarEntrevistaInicial
            {

                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                NDenunciaOficio = a.NDenunciaOficio,
                Mediodenuncia = a.RAtencion.MedioDenuncia,
                FechaHoraSuceso = a.FechaHoraSuceso,
                RBreve = a.RBreve,
                NarrativaHechos = a.NarrativaHechos,
                Vanabim = a.Vanabim,
                Modulos = a.ModuloServicio.Nombre

            });

        }

        // GET: api/RHechoes/ListarPorAgenciaRACS
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{agenciaid}")]
        public async Task<IEnumerable<ListarMisCarpetasViewModel>> ListarPorAgenciaRACS([FromRoute] Guid agenciaid)
        {
            var carpetas = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.ModuloServicio)
                          .Include(a => a.RAtencion.RACs)
                          .OrderByDescending(a => a.IdRHecho)
                          .Where(a => a.NucId == null)
                          .Where(a => a.Agenciaid == agenciaid).ToListAsync();

            return carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                NDenunciaOficio = a.NDenunciaOficio,
                RAC = a.RAtencion.RACs.racg,
                Modulos = a.ModuloServicio.Nombre


            });

        }


        // GET: api/RHechoes/ListarPorrAtencionId2
        [Authorize(Roles = " Recepción,Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{rAtencionId}")]
        public async Task<IActionResult> ListarPorrAtencionId2([FromRoute] Guid rAtencionId)
        {
            var a = await _context.RHechoes
                          .Include(x => x.RAtencion)
                          .Include(x => x.RAtencion.RACs)
                          .Where(x => x.RAtencionId == rAtencionId).FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }
            try
            {
                return Ok(new ListarCarpetasPersonaViewModel
                {
                    RHechoId = a.IdRHecho,
                    RAtencionId = a.RAtencionId,
                    u_Nombre = a.RAtencion.u_Nombre,
                    u_Puesto = a.RAtencion.u_Puesto,
                    u_Modulo = a.RAtencion.u_Modulo,
                    DistritoInicial = a.RAtencion.DistritoInicial,
                    DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                    Agenciaid = a.Agenciaid,
                    AgenciaInicial = a.RAtencion.AgenciaInicial,
                    FechaHoraRegistro = a.RAtencion.FechaHoraRegistro,
                    Rac = a.RAtencion.RACs.racg,
                    FechaElevaNuc = a.FechaElevaNuc,
                    FechaHoraSuceso = a.FechaHoraSuceso,
                    NDenunciaOficio = a.NDenunciaOficio,
                    Texto = a.Texto

                });
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }



        }



        // GET: api/RHechoes/ListarPorFechayDistritoyStatus
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{fechai}/{fechaf}/{distrito}/{status}/{fechhrinicio}/{fechhrfin}/{etapa}")]
        public async Task<IEnumerable<EstadisticaViewModelH>> ListarPorFechayDistritoyStatus([FromRoute] DateTime fechai, DateTime fechaf, string distrito,string status, DateTime fechhrinicio, DateTime fechhrfin,string etapa)
        {

            
            var carpetas = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.RAtencion.RACs)
                          .Include(a => a.NUCs)
                          .Where(a =>  a.FechaElevaNuc2 >= fechai)
                          .Where(a =>  a.FechaElevaNuc2 <= fechaf)
                          .Where(a => (distrito != "ZKR" ? a.RAtencion.DistritoInicial == distrito : a.RAtencion.DistritoInicial != distrito))
                          .Where(a => (status != "ZKR" ? a.NUCs.Etapanuc == etapa : a.NUCs.Etapanuc != etapa))
                          .Where(a => (status != "ZKR" ?  a.NUCs.StatusNUC == status : a.NUCs.StatusNUC != status))
                          .Where(a => a.FechaHoraSuceso2.TimeOfDay >= fechhrinicio.TimeOfDay && a.FechaHoraSuceso2.TimeOfDay <= fechhrfin.TimeOfDay)
                          .Where(a => a.FechaHoraSuceso2.Date >= fechhrinicio.Date && a.FechaHoraSuceso2.Date <= fechhrfin.Date)
                          .Where(a => a.Status == true)
                          .OrderBy(a=> a.NUCs.nucg)
                          .ToListAsync();

            return carpetas.Select(a => new EstadisticaViewModelH
            {
                RHechoId = a.IdRHecho,
                nuc = a.NUCs.nucg,
                FechaElevaNuc = a.FechaElevaNuc,
                DistritoInicial = a.RAtencion.DistritoInicial,
                StatusCarpeta = a.NUCs.StatusNUC,
                RAtencionId = a.RAtencionId,
                Fechah = a.FechaHoraSuceso2
            });

        }
        // GET: api/RHechoes/CarpetasAgenciaProceso
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{nostatus}/{status1}/{status2}/{status3}/{status4}/{status5}/{status6}/{status7}/{status8}/{status9}/{status10}/{status11}/{status12}/{status13}/{status14}/{status15}")]
        public async Task<IActionResult> CarpetasAgenciaProceso([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, int nostatus, string status1, string status2, string status3, string status4, string status5, string status6, string status7, string status8, string status9, string status10, string status11, string status12, string status13, string status14, string status15)
        {

            var delitos = await _context.RHechoes
                .Where(a => a.NucId != null)
                .Where(a => a.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => nostatus == 1 ? a.NUCs.StatusNUC == status1 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 2 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 3 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 4 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 5 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 6 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 7 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 8 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 9 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 10 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 11 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 12 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 || a.NUCs.StatusNUC == status12 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 13 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 || a.NUCs.StatusNUC == status12 || a.NUCs.StatusNUC == status13 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 14 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 || a.NUCs.StatusNUC == status12 || a.NUCs.StatusNUC == status13 || a.NUCs.StatusNUC == status14 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 15 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 || a.NUCs.StatusNUC == status12 || a.NUCs.StatusNUC == status13 || a.NUCs.StatusNUC == status14 || a.NUCs.StatusNUC == status15 : a.NUCs.StatusNUC != "")
                .GroupBy(v => v.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new EstadisticasILProcesoViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {

                if (registro.mes == 01) enero++;
                else if (registro.mes == 02) febrero++;
                else if (registro.mes == 03) marzo++;
                else if (registro.mes == 04) abril++;
                else if (registro.mes == 05) mayo++;
                else if (registro.mes == 06) junio++;
                else if (registro.mes == 07) julio++;
                else if (registro.mes == 08) agosto++;
                else if (registro.mes == 09) septiembre++;
                else if (registro.mes == 10) octubre++;
                else if (registro.mes == 11) noviembre++;
                else if (registro.mes == 12) diciembre++;

                total++;


            }

            return Ok(new { delito = "Total de carpetas de investigación vinculadas a proceso", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RHechoes/CarpetasAgenciaProcesoMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{nostatus}/{status1}/{status2}/{status3}/{status4}/{status5}/{status6}/{status7}/{status8}/{status9}/{status10}/{status11}/{status12}/{status13}/{status14}/{status15}")]
        public async Task<IActionResult> CarpetasAgenciaProcesoMujeres([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, int nostatus, string status1, string status2, string status3, string status4, string status5, string status6, string status7, string status8, string status9, string status10, string status11, string status12, string status13, string status14, string status15)
        {

            var delitos = await _context.RHechoes
                .Where(a => a.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.NucId != null)
                .Where(a => nostatus == 1 ? a.NUCs.StatusNUC == status1 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 2 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 3 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 4 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 5 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 6 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 7 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 8 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 9 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 10 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 11 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 12 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 || a.NUCs.StatusNUC == status12 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 13 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 || a.NUCs.StatusNUC == status12 || a.NUCs.StatusNUC == status13 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 14 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 || a.NUCs.StatusNUC == status12 || a.NUCs.StatusNUC == status13 || a.NUCs.StatusNUC == status14 : a.NUCs.StatusNUC != "")
                .Where(a => nostatus == 15 ? a.NUCs.StatusNUC == status1 || a.NUCs.StatusNUC == status2 || a.NUCs.StatusNUC == status3 || a.NUCs.StatusNUC == status4 || a.NUCs.StatusNUC == status5 || a.NUCs.StatusNUC == status6 || a.NUCs.StatusNUC == status7 || a.NUCs.StatusNUC == status8 || a.NUCs.StatusNUC == status9 || a.NUCs.StatusNUC == status10 || a.NUCs.StatusNUC == status11 || a.NUCs.StatusNUC == status12 || a.NUCs.StatusNUC == status13 || a.NUCs.StatusNUC == status14 || a.NUCs.StatusNUC == status15 : a.NUCs.StatusNUC != "")
                .GroupBy(v => v.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new EstadisticasILProcesoViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Total de carpetas de investigación vinculadas a proceso(Mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }


        // GET: api/RHechoes/CarpetasAgenciaStatusIL
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{eventoIl}/{statusil}")]
        public async Task<IActionResult> CarpetasAgenciaStatusIL([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, string eventoIl, string statusil)
        {

            var delitos = await _context.RHechoes
                 .Where(a => a.FechaHoraSuceso2.Year >= fechai.Year)
                 .Where(a => a.FechaHoraSuceso2.Year <= fechaf.Year)
                 .Where(a => distritoactivo ? a.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                 .Where(a => dspactivo ? a.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                 .Where(a => agenciaactivo ? a.ModuloServicio.AgenciaId == agencia : 1 == 1)
                 .Where(a => a.NucId != null)
                 .GroupBy(v => v.IdRHecho)
                .Select(x => new {
                    rhechoid = x.Key,
                    mes = x.Select(v => v.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new EstadisticasILProcesoViewModel
            {
                Rhechoid = a.rhechoid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var eventos = await _context.Agendas
                    .Where(a => a.RHechoId == registro.Rhechoid)
                    .Where(a => eventoIl != "ZKR" ?  a.Tipo == 10 && a.Tipo2 == eventoIl : 1 == 1)
                    .Where(a => statusil!= "ZKR" ? a.Tipo == 10 && a.Status == statusil : 1 == 1)
                    .ToListAsync();

                foreach (var evento in eventos)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;

                }

            }

            return Ok(new { delito = "Total de Sentencias Condenatorias", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RHechoes/CarpetasInvestigadasIniciadasDSP
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> CarpetasInvestigadasIniciadasDSP([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RHechoes
                .Where(a => a.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.NucId != null)
                .GroupBy(v => v.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new EstadisticasILProcesoViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {

                if (registro.mes == 01) enero++;
                else if (registro.mes == 02) febrero++;
                else if (registro.mes == 03) marzo++;
                else if (registro.mes == 04) abril++;
                else if (registro.mes == 05) mayo++;
                else if (registro.mes == 06) junio++;
                else if (registro.mes == 07) julio++;
                else if (registro.mes == 08) agosto++;
                else if (registro.mes == 09) septiembre++;
                else if (registro.mes == 10) octubre++;
                else if (registro.mes == 11) noviembre++;
                else if (registro.mes == 12) diciembre++;

                total++;


            }

            return Ok(new { delito = "Total de carpetas de investigación iniciadas", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });

        }

        // GET: api/RHechoes/CarpetasInvestigadasIniciadasDSPMujeres
        [Authorize(Roles = "Administrador, AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IActionResult> CarpetasInvestigadasIniciadasDSPMujeres([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {

            var delitos = await _context.RHechoes
                .Where(a => a.FechaHoraSuceso2.Year >= fechai.Year)
                .Where(a => a.FechaHoraSuceso2.Year <= fechaf.Year)
                .Where(a => distritoactivo ? a.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => a.NucId != null)
                .GroupBy(v => v.RAtencionId)
                .Select(x => new {
                    ratencionid = x.Key,
                    mes = x.Select(v => v.FechaHoraSuceso2.Month),
                })
                .ToListAsync();

            var tx = delitos.Select(a => new EstadisticasILProcesoViewModel
            {
                ratencionid = a.ratencionid,
                mes = a.mes.First()
            });


            int enero = 0, febrero = 0, marzo = 0, abril = 0, mayo = 0, junio = 0, julio = 0, agosto = 0, septiembre = 0, octubre = 0, noviembre = 0, diciembre = 0, total = 0;

            foreach (var registro in tx)
            {
                var novictimas = await _context.RAPs
                    .Where(a => a.RAtencionId == registro.ratencionid)
                    .Include(a => a.Persona)
                    .Where(a => a.Persona.Sexo == "M")
                    .Where(a => a.ClasificacionPersona == "Victima directa" || a.ClasificacionPersona == "Victima indirecta")
                    .ToListAsync();

                foreach (var victima in novictimas)
                {
                    if (registro.mes == 01) enero++;
                    else if (registro.mes == 02) febrero++;
                    else if (registro.mes == 03) marzo++;
                    else if (registro.mes == 04) abril++;
                    else if (registro.mes == 05) mayo++;
                    else if (registro.mes == 06) junio++;
                    else if (registro.mes == 07) julio++;
                    else if (registro.mes == 08) agosto++;
                    else if (registro.mes == 09) septiembre++;
                    else if (registro.mes == 10) octubre++;
                    else if (registro.mes == 11) noviembre++;
                    else if (registro.mes == 12) diciembre++;

                    total++;
                }

            }

            return Ok(new { delito = "Total de carpetas de investigación iniciadas (mujeres)", enero = enero, febrero = febrero, marzo = marzo, abril = abril, mayo = mayo, junio = junio, julio = julio, agosto = agosto, septiembre = septiembre, octubre = octubre, noviembre = noviembre, diciembre = diciembre, total = total });
        }


        // GET: api/RHechoes/ListarporDspyStatus
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}")]
        public async Task<IEnumerable<EstadisticasStatusDspViewModel>> ListarporDspyStatus([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf)
        {
            var carpetas = await _context.RHechoes
                          .Where(a => a.FechaHoraSuceso2 >= fechai)
                          .Where(a => a.FechaHoraSuceso2 <= fechaf)
                          .Where(a => distritoactivo ? a.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                          .Where(a => dspactivo ? a.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                          .Where(a => agenciaactivo ? a.ModuloServicio.AgenciaId == agencia : 1 == 1)
                          .GroupBy(z =>z.NUCs.StatusNUC)
                          .Select(x => new {
                              etiqueta = x.Key,
                              total = x.Count(),
                              
                          })
                          .ToListAsync(); 

            return carpetas.Select(z => new EstadisticasStatusDspViewModel
            {
                status = z.etiqueta,
                Total = z.total
            });

        }


        // GET: api/RHechoes/ListarPorModuloAdminDirector
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{Indicadoragencia}/{idAgencia}/{IndicadorModulo}/{idModuloServicio}/{IndicadorFechaI}/{FechaI}/{IndicadorFechaF}/{FechaF}/{IddspU}/{IdAgenciaU}/{IdModuloU}/{rol}/{nombre}/{apellidop}/{apellidom}/{nuc}")]
        public async Task<IEnumerable<ListarMisCarpetasViewModel>> ListarPorModuloAdminDirector([FromRoute] Boolean Indicadoragencia, Guid idAgencia, Boolean IndicadorModulo, Guid idModuloServicio, Boolean IndicadorFechaI, DateTime FechaI, Boolean IndicadorFechaF, DateTime FechaF, Guid IddspU, Guid IdAgenciaU, Guid IdModuloU, string rol, string nombre, string apellidop, string apellidom,string nuc)
        {
            var carpetas = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.NUCs)
                          .Include(a => a.ModuloServicio)
                          .Where(a => nuc != "ZKR" ? a.NUCs.nucg == nuc : 1 == 1)
                          .Where(a => a.NucId != null)
                          .Where(a => Indicadoragencia ? a.Agenciaid == idAgencia  : rol == "AD" ? a.Agencia.DSPId == IddspU : 1 == 1)
                          .Where(a => IndicadorModulo ? a.ModuloServicioId == idModuloServicio : rol == "C" ? a.ModuloServicio.AgenciaId == IdAgenciaU: 1 == 1)
                          .Where(a => rol == "A" ? a.ModuloServicioId == IdModuloU : 1 == 1)
                          .Where(a => IndicadorFechaI ? a.FechaElevaNuc2 >= FechaI : 1 == 1)
                          .Where(a => IndicadorFechaF ? a.FechaElevaNuc2 <= FechaF : 1 == 1)
                          .ToListAsync();

            var carpetasf = carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,  
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                nucId = a.NucId,
                nuc = a.NUCs.nucg,
                FechaElevaNuc = a.FechaElevaNuc,
                NDenunciaOficio = a.NDenunciaOficio,
                Modulo = a.ModuloServicio.Nombre
            });

            IEnumerable<ListarMisCarpetasViewModel> items = new ListarMisCarpetasViewModel[] { };

            foreach (var carpetaf in carpetasf)
            {
                var victima = await _context.RAPs
                    .Where(a => a.RAtencionId == carpetaf.RAtencionId)
                    .Where(a => a.PInicio)
                    .Include(a => a.Persona)
                    .Where(a => nombre != "ZKR" ? a.Persona.Nombre == nombre : 1 == 1)
                    .Where(a => apellidop != "ZKR" ? a.Persona.ApellidoPaterno == apellidop : 1 == 1)
                    .Where(a => apellidom != "ZKR" ? a.Persona.ApellidoMaterno == apellidom : 1 == 1)                
                    .FirstOrDefaultAsync();


                    IEnumerable<ListarMisCarpetasViewModel> ReadLines()
                    {
                        IEnumerable<ListarMisCarpetasViewModel> item2;

                        item2 = (new[]{new ListarMisCarpetasViewModel{
                        RHechoId = carpetaf.RHechoId,
                        Agenciaid = carpetaf.Agenciaid,
                        RAtencionId = carpetaf.RAtencionId,
                        u_Nombre = carpetaf.u_Nombre,
                        u_Puesto = carpetaf.u_Puesto,
                        u_Modulo = carpetaf.u_Modulo,
                        DistritoInicial = carpetaf.DistritoInicial,
                        DirSubProcuInicial = carpetaf.DirSubProcuInicial,
                        AgenciaInicial = carpetaf.AgenciaInicial,
                        Status = carpetaf.Status,
                        nucId = carpetaf.nucId,
                        nuc = carpetaf.nuc,
                        FechaElevaNuc = carpetaf.FechaElevaNuc,
                        NDenunciaOficio = carpetaf.NDenunciaOficio,
                        Modulo = carpetaf.Modulo,
                        Victima =  victima != null ? victima.Persona.Nombre + " " + victima.Persona.ApellidoPaterno + " " + victima.Persona.ApellidoMaterno : "Sin registrar V/I"
                    }});

                        return item2;
                    }

                    items = items.Concat(ReadLines());


            }

            return items;

        }
        public Tuple<string, List <SqlParameter>> getFiltros(Models.Nuc.BuscarCarpetasDistritoFiltro model,Boolean isRac)
        {
            string Filtros = "";
            List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
            filtrosBusqueda.Add(new SqlParameter("@distrito", model.idDistrito));

            if (model.nucg != "")
            {
                if (isRac)
                   Filtros += " AND RAC.racg LIKE @nucg";
                else
                    Filtros += " AND NUC.nucg LIKE @nucg";
                filtrosBusqueda.Add(new SqlParameter("@nucg", "%" + model.nucg + "%"));
            }
            if (model.nombre != "")
            {
                Filtros += " AND CP.Nombre LIKE @nombre";
                filtrosBusqueda.Add(new SqlParameter("@nombre", "%" + model.nombre + "%"));
            }
            if (model.apellidoPaterno != "")
            {
                Filtros += " AND CP.ApellidoPaterno LIKE @apellidop";
                filtrosBusqueda.Add(new SqlParameter("@apellidop", "%" + model.apellidoPaterno + "%"));
            }

            if (model.apellidoMaterno != "")
            {
                Filtros += " AND CP.ApellidoMaterno LIKE @apellidom";
                filtrosBusqueda.Add(new SqlParameter("@apellidom", "%" + model.apellidoMaterno + "%"));
            }
            if (model.idAgencia.ToString() != "00000000-0000-0000-0000-000000000000" && model.idModulo.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                Filtros += " AND A.IdAgencia = @idagencia";
                filtrosBusqueda.Add(new SqlParameter("@idagencia", model.idAgencia));
            }
            if (model.idModulo.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                Filtros += " AND MS.IdModuloServicio = @servicio";
                filtrosBusqueda.Add(new SqlParameter("@servicio", model.idModulo));
            }
            if (model.fechaDesde != null && model.fechaHasta == null)
            {
                Filtros += " AND  RA.FechaHoraRegistro >= @fechadesde";
                filtrosBusqueda.Add(new SqlParameter("@fechadesde", model.fechaDesde));
            }
            if (model.fechaDesde == null && model.fechaHasta != null)
            {
                Filtros += " AND  RA.FechaHoraRegistro <= @fechahasta";
                filtrosBusqueda.Add(new SqlParameter("@fechahasta", model.fechaHasta));
            }
            if (model.fechaDesde != null && model.fechaHasta != null)
            {
                Filtros += " AND  RA.FechaHoraRegistro BETWEEN @fechadesde AND @fechahasta";
                filtrosBusqueda.Add(new SqlParameter("@fechadesde", model.fechaDesde));
                filtrosBusqueda.Add(new SqlParameter("@fechahasta", model.fechaHasta));
            }

            return new Tuple <string, List < SqlParameter > >(Filtros, filtrosBusqueda);
        }
        // POST: api/RHechoes/BuscarBuscarCarpetasPorDistrito
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> BuscarCarpetasEnServidor([FromBody] Models.Nuc.BuscarCarpetasDistritoFiltro model)
        {
            try
            {
                var tFiltros = getFiltros(model,false);                

                String sqlBusqueda = @"SELECT
                                                D.Nombre AS distritoactual,
                                                DSP.NombreSubDir AS dspactual,
                                                RA.FechaHoraRegistro,
                                                RH.IdRHecho,
                                                A.IdAgencia,
                                                A.Nombre AS agenciaactual,
                                                RAC.racg as RAC,
                                                RAC.IdRac as RacId,
                                                RA.IdRAtencion,
                                                RA.u_Nombre,
                                                RA.u_Puesto,
                                                RA.u_Modulo,
                                                RA.DistritoInicial,
                                                RA.DirSubProcuInicial,
                                                RA.AgenciaInicial,
                                                RH.Status,
                                                RH.NucId,
                                                MS.IdModuloServicio,
                                                MS.Nombre as Modulo,
                                                MS.Nombre AS moduloactual,
                                                NUC.nucg as nuc,
                                                RH.FechaElevaNuc,
                                                RH.NDenunciaOficio,
                                                CASE WHEN (CP.IdPersona = NULL) THEN 'Sin registrar V/I' ELSE CONCAT(CP.Nombre,' ',CP.ApellidoPaterno,' ',CP.ApellidoMaterno) END AS Victima 
                                                FROM CAT_RHECHO RH 
                                                LEFT JOIN NUC ON NUC.idNuc = RH.NucId 
                                                LEFT JOIN CAT_RATENCON RA ON RA.IdRAtencion = RH.RAtencionId 
                                                LEFT JOIN RAC ON RAC.idRac = RA.racId 
                                                LEFT JOIN C_MODULOSERVICIO MS ON MS.IdModuloServicio = rH.ModuloServicioId 
                                                LEFT JOIN C_AGENCIA A ON A.IdAgencia = MS.AgenciaId 
                                                LEFT JOIN C_DSP DSP ON DSP.IdDSP = A.DSPId 
                                                LEFT JOIN C_DISTRITO D ON D.IdDistrito = DSP.DistritoId 
                                                LEFT JOIN CAT_RAP CR ON CR.RAtencionId = RA.IdRAtencion AND CR.PInicio = 1 
                                                LEFT JOIN CAT_PERSONA CP ON CP.IdPersona = CR.PersonaId 
                                                WHERE 1=1" + tFiltros.Item1 + " ORDER BY NUC.nucg ASC";
                ;
                var carpetas = await _context.qBusquedaCarpetas.FromSqlRaw(sqlBusqueda, tFiltros.Item2.ToArray()).ToListAsync();

                return Ok(carpetas.Select(a => new ListarMisCarpetasViewModel
                {
                    RHechoId = a.IdRHecho,
                    Agenciaid = a.IdAgencia,
                    IdModuloServicio = a.IdModuloServicio,
                    RAtencionId = a.IdRAtencion,
                    u_Nombre = a.u_Nombre,
                    u_Puesto = a.u_Puesto,
                    u_Modulo = a.u_Modulo,
                    DistritoInicial = a.DistritoInicial,
                    DirSubProcuInicial = a.DirSubProcuInicial,
                    AgenciaInicial = a.AgenciaInicial,
                    Status = a.Status,
                    nucId = a.nucId,
                    nuc = a.nuc,
                    RacId = a.RacId,
                    FechaElevaNuc = a.FechaElevaNuc,
                    NDenunciaOficio = a.NDenunciaOficio,
                    Modulo = a.Modulo,
                    Modulos = "",
                    Victima = a.Victima,
                    distritoactual = a.distritoactual,
                    dspactual = a.dspactual,
                    agenciaactual = a.agenciaactual,
                    moduloactual = a.moduloactual,
                    
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message + " cadena:" + model.fechaDesde.ToString(), version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }
        // POST: api/RHechoes/BuscarBuscarCarpetasPorDistrito
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> BuscarCarpetasPorDistrito([FromBody] Models.Nuc.BuscarCarpetasDistritoFiltro model)
        {
            try
            {
                var tFiltros = getFiltros(model,false);
                String sqlBusqueda = @"SELECT 
                                                D.Nombre AS distritoactual,
                                                DSP.NombreSubDir AS dspactual,
                                                A.Nombre AS agenciaactual,
                                                MS.Nombre AS moduloactual,
                                                RA.FechaHoraRegistro,
                                                RH.IdRHecho,A.IdAgencia,
                                                RAC.racg as RAC,
                                                RAC.IdRac as RacId,
                                                RA.IdRAtencion,
                                                RA.u_Nombre,
                                                RA.u_Puesto,
                                                RA.u_Modulo,
                                                RA.DistritoInicial,
                                                RA.DirSubProcuInicial,
                                                RA.AgenciaInicial,
                                                RH.Status,
                                                RH.NucId,
                                                MS.IdModuloServicio,
                                                MS.Nombre as Modulo,
                                                NUC.nucg as nuc,
                                                RH.FechaElevaNuc,
                                                RH.NDenunciaOficio,
                                                CASE WHEN (CP.IdPersona = NULL) THEN 'Sin registrar V/I' ELSE CONCAT(CP.Nombre,' ',CP.ApellidoPaterno,' ',CP.ApellidoMaterno) END AS Victima 
                                                FROM CAT_RHECHO RH 
                                                LEFT JOIN NUC ON NUC.idNuc = RH.NucId 
                                                LEFT JOIN CAT_RATENCON RA ON RA.IdRAtencion = RH.RAtencionId 
                                                LEFT JOIN RAC ON RAC.idRac = RA.racId 
                                                LEFT JOIN C_MODULOSERVICIO MS ON MS.IdModuloServicio = rH.ModuloServicioId 
                                                LEFT JOIN C_AGENCIA A ON A.IdAgencia = MS.AgenciaId 
                                                LEFT JOIN C_DSP DSP ON DSP.IdDSP = A.DSPId 
                                                LEFT JOIN C_DISTRITO D ON D.IdDistrito = DSP.DistritoId 
                                                LEFT JOIN CAT_RAP CR ON CR.RAtencionId = RA.IdRAtencion AND CR.PInicio = 1 
                                                LEFT JOIN CAT_PERSONA CP ON CP.IdPersona = CR.PersonaId 
                                                WHERE D.IdDistrito =@distrito " + tFiltros.Item1 + " ORDER BY NUC.nucg ASC";
;
                var carpetas = await _context.qBusquedaCarpetas.FromSqlRaw(sqlBusqueda, tFiltros.Item2.ToArray()).ToListAsync();
               
                return Ok( carpetas.Select(a => new ListarMisCarpetasViewModel
                {
                    RHechoId = a.IdRHecho,
                    Agenciaid = a.IdAgencia,
                    RAtencionId = a.IdRAtencion,
                    u_Nombre = a.u_Nombre,
                    u_Puesto = a.u_Puesto,
                    u_Modulo = a.u_Modulo,
                    DistritoInicial = a.DistritoInicial,
                    DirSubProcuInicial = a.DirSubProcuInicial,
                    AgenciaInicial = a.AgenciaInicial,
                    agenciaactual=a.agenciaactual,
                    distritoactual=a.distritoactual,
                    moduloactual=a.moduloactual,
                    Status =a.Status,
                    nucId = a.nucId,
                    nuc = a.nuc,
                    RacId = a.RacId,
                    FechaElevaNuc = a.FechaElevaNuc,
                    NDenunciaOficio = a.NDenunciaOficio,
                    Modulo = a.Modulo,
                    Modulos= "",
                    Victima = a.Victima,
                    dspactual = a.dspactual,
                }));
           }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message +" cadena:"+ model.fechaDesde.ToString(), version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }


        // POST: api/RHechoes/BuscarRACPorDistrito
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> BuscarRACPorDistrito([FromBody] Models.Nuc.BuscarCarpetasDistritoFiltro model)
        {
            try
            {
                var tFiltros = getFiltros(model,true);

                String sqlBusqueda = "SELECT RH.IdRHecho,RA.FechaHoraRegistro,A.IdAgencia,RAC.racg as RAC,RAC.IdRac as RacId,RA.IdRAtencion,RA.u_Nombre,RA.u_Puesto,RA.u_Modulo,RA.DistritoInicial,RA.DirSubProcuInicial,RA.AgenciaInicial,D.Nombre AS distritoactual,A.Nombre AS agenciaactual,MS.Nombre AS moduloactual,RH.Status,RH.NucId,MS.IdModuloServicio,MS.Nombre as Modulo,NUC.nucg as nuc,RH.FechaElevaNuc,RH.NDenunciaOficio,CASE WHEN (CP.IdPersona = NULL) THEN 'Sin registrar V/I' ELSE CONCAT(CP.Nombre,' ',CP.ApellidoPaterno,' ',CP.ApellidoMaterno) END AS Victima FROM CAT_RHECHO RH LEFT JOIN NUC ON NUC.idNuc = RH.NucId LEFT JOIN CAT_RATENCON RA ON RA.IdRAtencion = RH.RAtencionId LEFT JOIN RAC ON RAC.idRac = RA.racId LEFT JOIN C_MODULOSERVICIO MS ON MS.IdModuloServicio = RH.ModuloServicioId LEFT JOIN C_AGENCIA A ON A.IdAgencia = MS.AgenciaId LEFT JOIN C_DSP DSP ON DSP.IdDSP = A.DSPId LEFT JOIN C_DISTRITO D ON D.IdDistrito = DSP.DistritoId LEFT JOIN CAT_RAP CR ON CR.RAtencionId = RA.IdRAtencion LEFT JOIN CAT_PERSONA CP ON CP.IdPersona = CR.PersonaId WHERE D.IdDistrito =@distrito AND CR.PInicio = 1 " + tFiltros.Item1 + " ORDER BY RAC.racg ASC";
               
                var carpetas = await _context.qBusquedaCarpetas.FromSqlRaw(sqlBusqueda, tFiltros.Item2.ToArray()).ToListAsync();

                return Ok(carpetas.Select(a => new ListarMisCarpetasViewModel
                {
                    RHechoId = a.IdRHecho,
                    Agenciaid = a.IdAgencia,
                    RAtencionId = a.IdRAtencion,
                    u_Nombre = a.u_Nombre,
                    u_Puesto = a.u_Puesto,
                    u_Modulo = a.u_Modulo,
                    RAC = a.RAC,
                    RacId = a.RacId,
                    agenciaactual=a.agenciaactual,
                    distritoactual=a.distritoactual,
                    moduloactual=a.moduloactual,
                    DistritoInicial = a.DistritoInicial,
                    DirSubProcuInicial = a.DirSubProcuInicial,
                    AgenciaInicial = a.AgenciaInicial,
                    Status = a.Status,
                    nucId = a.nucId,
                    nuc = a.nuc,
                    FechaElevaNuc = a.FechaHoraRegistro,
                    NDenunciaOficio = a.NDenunciaOficio,
                    Modulo = a.Modulo,
                    Modulos = "",
                    Victima = a.Victima
                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }





        // GET: api/RHechoes/ListarPorModuloCarpetas
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{idModuloServicio}")]    
        public async Task<IActionResult> ListarPorModuloCarpetas([FromRoute] Guid idModuloServicio)
        {
            try
            {
                var carpetas = await _context.RHechoes
                              .Include(a => a.RAtencion)
                              .Include(a => a.NUCs)

                              .Where(a => a.NucId != null)
                              .Where(a => a.ModuloServicioId == idModuloServicio)
                              .OrderByDescending(a => a.FechaElevaNuc2)
                              //.Take(20)
                              .ToListAsync();

                var carpetasf = carpetas.Select(a => new ListarMisCarpetasViewModel
                {
                    RHechoId = a.IdRHecho,
                    Agenciaid = a.Agenciaid,
                    RAtencionId = a.RAtencionId,
                    u_Nombre = a.RAtencion.u_Nombre,
                    u_Puesto = a.RAtencion.u_Puesto,
                    u_Modulo = a.RAtencion.u_Modulo,
                    DistritoInicial = a.RAtencion.DistritoInicial,
                    DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                    AgenciaInicial = a.RAtencion.AgenciaInicial,
                    Status = a.Status,
                    nucId = a.NucId,
                    nuc = a.NUCs.nucg,
                    FechaElevaNuc = a.FechaElevaNuc,
                    NDenunciaOficio = a.NDenunciaOficio,

                });

                IEnumerable<ListarMisCarpetasViewModel> items = new ListarMisCarpetasViewModel[] { };

                foreach (var carpetaf in carpetasf)
                {
                    var victima = await _context.RAPs
                        .Where(a => a.RAtencionId == carpetaf.RAtencionId)
                        .Where(a => a.PInicio)
                        .Include(a => a.Persona)
                        .FirstOrDefaultAsync();

                    //victima.Persona.Registro.GetValueOrDefault(false);
                    IEnumerable<ListarMisCarpetasViewModel> ReadLines()
                    {
                        IEnumerable<ListarMisCarpetasViewModel> item2;

                        item2 = (new[]{new ListarMisCarpetasViewModel{
                        RHechoId = carpetaf.RHechoId,
                        Agenciaid = carpetaf.Agenciaid,
                        RAtencionId = carpetaf.RAtencionId,
                        u_Nombre = carpetaf.u_Nombre,
                        u_Puesto = carpetaf.u_Puesto,
                        u_Modulo = carpetaf.u_Modulo,
                        DistritoInicial = carpetaf.DistritoInicial,
                        DirSubProcuInicial = carpetaf.DirSubProcuInicial,
                        AgenciaInicial = carpetaf.AgenciaInicial,
                        Status = carpetaf.Status,
                        nucId = carpetaf.nucId,
                        nuc = carpetaf.nuc,
                        FechaElevaNuc = carpetaf.FechaElevaNuc,
                        NDenunciaOficio = carpetaf.NDenunciaOficio,
                        Modulo = carpetaf.Modulo,
                        Victima =  victima != null ? victima.Persona.Nombre + " " + victima.Persona.ApellidoPaterno + " " + victima.Persona.ApellidoMaterno : "Sin registrar V/I"
                    }});

                        return item2;
                    }
                    items = items.Concat(ReadLines());
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }

        }


        // GET: api/RHechoes/ListarPorModuloRACSAdminDirector
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{Indicadoragencia}/{idAgencia}/{IndicadorModulo}/{idModuloServicio}/{IndicadorFechaI}/{FechaI}/{IndicadorFechaF}/{FechaF}/{IddspU}/{IdAgenciaU}/{IdModuloU}/{rol}")]
        public async Task<IEnumerable<ListarMisCarpetasViewModel>> ListarPorModuloRACSAdminDirector([FromRoute] Boolean Indicadoragencia, Guid idAgencia, Boolean IndicadorModulo, Guid idModuloServicio, Boolean IndicadorFechaI, DateTime FechaI, Boolean IndicadorFechaF, DateTime FechaF, Guid IddspU, Guid IdAgenciaU, Guid IdModuloU, string rol)
        {
            var carpetas = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.NUCs)
                          .Include(a => a.RAtencion.RACs)
                          .Include(a => a.ModuloServicio)
                          .Where(a => a.NucId == null)
                          .Where(a => Indicadoragencia ? a.Agenciaid == idAgencia : rol == "AD" ? a.Agencia.DSPId == IddspU : 1 == 1)
                          .Where(a => IndicadorModulo ? a.ModuloServicioId == idModuloServicio : rol == "C" ? a.ModuloServicio.AgenciaId == IdAgenciaU : 1 == 1)
                          .Where(a => rol == "A" ? a.ModuloServicioId == IdModuloU : 1 == 1)
                          .Where(a => IndicadorFechaI ? a.FechaElevaNuc2 > FechaI : 1 == 1)
                          .Where(a => IndicadorFechaF ? a.FechaElevaNuc2 < FechaF : 1 == 1)
                          .ToListAsync();

            return carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                NDenunciaOficio = a.NDenunciaOficio,
                RAC = a.RAtencion.RACs.racg,
                Modulos = a.ModuloServicio.Nombre


            });

        }

        // GET: api/RHechoes/ListarPorAgenciaNUCS
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{agenciaid}")]
        public async Task<IEnumerable<ListarMisCarpetasViewModel>> ListarPorAgenciaNUCS([FromRoute] Guid agenciaid)
        {
            var carpetas = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.ModuloServicio)
                          .Include(a => a.RAtencion.RACs)
                          .Include(a => a.NUCs)
                          .OrderByDescending(a => a.IdRHecho)
                          .Where(a => a.NucId != null)
                          .Where(a => a.Agenciaid == agenciaid).ToListAsync();

            return carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                NDenunciaOficio = a.NDenunciaOficio,
                RAC = a.RAtencion.RACs.racg,
                Modulos = a.ModuloServicio.Nombre,
                nuc = a.NUCs.nucg


            });

        }

        // PUT: api/RHechoes/ActualizarNUCMCaptura
        [Authorize(Roles = " AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador, Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarNUCMCaptura([FromBody] ActualizarNUCMCapturaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //ELEVAMOS EL NUC EN LA TABLA DE RHECHOS
            // El campo status se registra en FALSE hasta que se eleva a NUC
            //******************************************************************************************
            var elevaNUC = await _context.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == model.IdRHecho);

            if (elevaNUC == null)
            {
                return NotFound();
            }
            elevaNUC.NucId = model.nucId;
            elevaNUC.Status = true;
            elevaNUC.FechaElevaNuc = model.FechaElevacion;
            elevaNUC.FechaElevaNuc2 = model.FechaElevacion;
            //******************************************************************************************
            // ACTUALIZAMOS EL REGISTRO   DE ATENCION 
            // El Campo statusRegistro simpre  se registra en TRUE hasta que se eleva a NUC o se  queda como RAC
            var bajaRAC = await _context.RAtencions.FirstOrDefaultAsync(a => a.IdRAtencion == model.ratencionId);
            if (bajaRAC == null)
            {
                return NotFound();
            }
            bajaRAC.StatusRegistro = true;
            //******************************************************************************************


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


        // GET: api/RHechoes/Estadisticaporcarpetaf
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{CarpetasTotalesEstadistica}")]
        public async Task<IEnumerable<TotalCarpetasEstadistica>> Estadisticaporcarpetaf([FromQuery] CarpetasTotalesEstadistica CarpetasTotalesEstadistica)
        {
            var carpetas = await _context.RHechoes
                          .Where(a => a.NucId != null)
                          .Where(a => CarpetasTotalesEstadistica.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == CarpetasTotalesEstadistica.DatosGenerales.Distrito : 1 == 1)
                          .Where(a => CarpetasTotalesEstadistica.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == CarpetasTotalesEstadistica.DatosGenerales.Dsp : 1 == 1)
                          .Where(a => CarpetasTotalesEstadistica.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == CarpetasTotalesEstadistica.DatosGenerales.Agencia : 1 == 1)
                          .Where(a => a.FechaElevaNuc2 > CarpetasTotalesEstadistica.DatosGenerales.Fechadesde)
                          .Where(a => a.FechaElevaNuc2 < CarpetasTotalesEstadistica.DatosGenerales.Fechahasta)
                          .Where(a => CarpetasTotalesEstadistica.EstatusEtapaCarpeta.EtapaActual != "null" ? a.NUCs.Etapanuc == CarpetasTotalesEstadistica.EstatusEtapaCarpeta.EtapaActual : 1 == 1)
                          .Where(a => CarpetasTotalesEstadistica.EstatusEtapaCarpeta.StatusActual != "null" ? a.NUCs.StatusNUC == CarpetasTotalesEstadistica.EstatusEtapaCarpeta.StatusActual : 1 == 1)
                          .Where(a => CarpetasTotalesEstadistica.Tipoinicio != "null" ? a.RAtencion.MedioDenuncia == CarpetasTotalesEstadistica.Tipoinicio : 1 == 1)
                          .Where(a => CarpetasTotalesEstadistica.Tipoinicio == "Noticia de hechos" && CarpetasTotalesEstadistica.Mediollegada != "null" ? a.RAtencion.MedioLlegada == CarpetasTotalesEstadistica.Mediollegada : 1 == 1)
                          .ToListAsync();

            var carpetaf = carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,
                RAtencionId = a.RAtencionId,

            });
            
            IEnumerable<ListarMisCarpetasViewModel> items = new ListarMisCarpetasViewModel[] { };

            if (CarpetasTotalesEstadistica.DatosDetenidoEstadistica.FiltroconDetenido)
            {
                foreach (var carpeta in carpetaf)
                {
                    var persona = await _context.RAPs
                        .Where(a => a.RAtencionId == carpeta.RAtencionId)
                        .Where(a => a.Persona.InicioDetenido)
                        .Where(a => CarpetasTotalesEstadistica.DatosDetenidoEstadistica.CumpleRequisitos != "null" ? a.Persona.CumpleRequisitoLey == CarpetasTotalesEstadistica.DatosDetenidoEstadistica.CumpleRequisitos : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.DatosDetenidoEstadistica.CumpleRequisitos == "Si" && CarpetasTotalesEstadistica.DatosDetenidoEstadistica.DecretoLibertad != "null" ? a.Persona.DecretoLibertad == CarpetasTotalesEstadistica.DatosDetenidoEstadistica.DecretoLibertad : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.DatosDetenidoEstadistica.CumpleRequisitos == "No" && CarpetasTotalesEstadistica.DatosDetenidoEstadistica.DecretoLibertad != "null" ? a.Persona.DispusoLibertad == CarpetasTotalesEstadistica.DatosDetenidoEstadistica.DispusoLibertad : 1 == 1)
                        .FirstOrDefaultAsync();

                    if (persona != null)
                    {

                        IEnumerable<ListarMisCarpetasViewModel> ReadLines()
                        {
                            IEnumerable<ListarMisCarpetasViewModel> item2;

                            item2 = (new[]{new ListarMisCarpetasViewModel{
                                RHechoId = carpeta.RHechoId,
                                RAtencionId = carpeta.RAtencionId,
                            }});

                            return item2;
                        }

                        items = items.Concat(ReadLines());

                    }

                }
                carpetaf = items;
            }

            IEnumerable<ListarMisCarpetasViewModel> items2 = new ListarMisCarpetasViewModel[] { };

            if (CarpetasTotalesEstadistica.EstatusEtapaCarpeta.FiltroActivoStatusHistorico)
            {
                foreach (var carpeta in carpetaf)
                {
                    var historial = await _context.HistorialCarpetas
                        .Where(a => a.RHechoId == carpeta.RHechoId)
                        .Where(a => CarpetasTotalesEstadistica.EstatusEtapaCarpeta.EtapaHistorico != "null" ? a.DetalleEtapa == CarpetasTotalesEstadistica.EstatusEtapaCarpeta.EtapaHistorico : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.EstatusEtapaCarpeta.StatusHistorico != "null" ? a.Detalle == CarpetasTotalesEstadistica.EstatusEtapaCarpeta.StatusHistorico : 1 == 1)
                        .FirstOrDefaultAsync();

                    if (historial != null)
                    {

                        IEnumerable<ListarMisCarpetasViewModel> ReadLines()
                        {
                            IEnumerable<ListarMisCarpetasViewModel> item2;

                            item2 = (new[]{new ListarMisCarpetasViewModel{
                                RHechoId = carpeta.RHechoId,
                                RAtencionId = carpeta.RAtencionId,
                            }});

                            return item2;
                        }

                        items2 = items2.Concat(ReadLines());

                    }

                }
                carpetaf = items2;
            }

            IEnumerable<ListarMisCarpetasViewModel> items3 = new ListarMisCarpetasViewModel[] { };
            
            if (CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.FiltroActivoDelito)
            {
                foreach (var carpeta in carpetaf)
                {
                    var delitof = await _context.RDHs
                        .Where(a => a.RHechoId == carpeta.RHechoId)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Delitoactivo ? a.DelitoId == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.DelitoNombre : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Delitoactivo && CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Delitoespecifico != "null" ? a.DelitoEspecifico == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Delitoespecifico : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Tipofuero != "null" ? a.TipoFuero == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Tipofuero : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Requisitoprocedibilidad != "null" ? a.TipoDeclaracion == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Requisitoprocedibilidad : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Gradoejecucion != "null" ? a.ResultadoDelito == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Gradoejecucion : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Prisionpreventiva != "null" ? a.GraveNoGrave == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Prisionpreventiva : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Formacomision != "null" ? a.IntensionDelito == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Formacomision : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia != "null" ? a.ViolenciaSinViolencia == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Modalidaddelito != "null" ? a.Tipo == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Modalidaddelito : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ClasificaOrdenResult != "null" ? a.ClasificaOrdenResult == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ClasificaOrdenResult : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Montorobado > 0 ? a.MontoRobado == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Montorobado : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Descripcionrobado != "null" ? a.TipoRobado == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Descripcionrobado : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Armablanca == "Si" ? a.ArmaBlanca : CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Armablanca == "No" ? !a.ArmaBlanca : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Armafuego == "Si" ? a.ArmaFuego : CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.Armafuego == "No" ? !a.ArmaFuego : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ConOtroElemento != "null" ? a.ConotroElemento == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ConOtroElemento : 1 == 1)
                        .Where(a => CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ViolenciaSinViolencia == "Violencia física" && CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ConAlgunaParteCuerpo != "null" ?  a.ConAlgunaParteCuerpo == CarpetasTotalesEstadistica.CaracteristicasDelitoEstadistica.ConAlgunaParteCuerpo : 1 == 1)
                        .FirstOrDefaultAsync();

                    if (delitof != null)
                    {

                        IEnumerable<ListarMisCarpetasViewModel> ReadLines()
                        {
                            IEnumerable<ListarMisCarpetasViewModel> item2;

                            item2 = (new[]{new ListarMisCarpetasViewModel{
                                RHechoId = carpeta.RHechoId,
                                RAtencionId = carpeta.RAtencionId,
                            }});

                            return item2;
                        }

                        items3 = items3.Concat(ReadLines());

                    }

                }
                carpetaf = items3;
            }

            IEnumerable<TotalCarpetasEstadistica> datos = new TotalCarpetasEstadistica[] { };

            IEnumerable<TotalCarpetasEstadistica> ReadLines2( int cantidad, string tipo)
            {
                IEnumerable<TotalCarpetasEstadistica> item2;

                item2 = (new[]{new TotalCarpetasEstadistica{
                                Tipo = tipo,
                                Cantidad = cantidad,
                            }});

                return item2;
            }

            datos = datos.Concat(ReadLines2(carpetaf.Count(), "Total de carpetas"));

            return datos;
        }

        // GET: api/RHechoes/InfodelRhecho
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> InfodelRhecho([FromRoute] Guid rHechoId)
        {
            var a = await _context.RHechoes
                          .Include(x => x.ModuloServicio.Agencia.DSP)
                          .Include(x => x.ModuloServicio.Agencia.DSP)
                          .Include(x => x.ModuloServicio.Agencia.DSP)
                          .Include(x => x.ModuloServicio.Agencia.DSP.Distrito)
                          .Where(x => x.IdRHecho == rHechoId).FirstOrDefaultAsync();

            if (a == null)
            {
                return NotFound("No hay registros");
            }

            return Ok(new ListarEntrevistaInicial
            {

                DistritoInicial = a.ModuloServicio.Agencia.DSP.Distrito.Nombre,
                DirSubProcuInicial = a.ModuloServicio.Agencia.DSP.NombreSub,
                AgenciaInicial = a.ModuloServicio.Agencia.Nombre,
                u_Modulo = a.ModuloServicio.Nombre


            });

        }


        // GET: api/RHechoes/NUCSRACSEstadisticaef
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido, Recepción")]
        [HttpGet("[action]/{NucsRacsEstadistica}")]
        public async Task<IEnumerable<NucsRacsEstadisticaViewModel>> NUCSRACSEstadisticaef([FromQuery] NucsRacsEstadistica NucsRacsEstadistica)
        {
            var nucs = await _context.RHechoes
                .Where(a => a.NucId != null)
                .Where(a => NucsRacsEstadistica.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == NucsRacsEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => NucsRacsEstadistica.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == NucsRacsEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => NucsRacsEstadistica.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == NucsRacsEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.FechaElevaNuc2 >= NucsRacsEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.FechaElevaNuc2 <= NucsRacsEstadistica.DatosGenerales.Fechahasta)
                .ToListAsync();


            var racs = await _context.RHechoes
                .Where(a => !a.Status)
                .Where(a => NucsRacsEstadistica.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == NucsRacsEstadistica.DatosGenerales.Distrito : 1 == 1)
                .Where(a => NucsRacsEstadistica.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == NucsRacsEstadistica.DatosGenerales.Dsp : 1 == 1)
                .Where(a => NucsRacsEstadistica.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == NucsRacsEstadistica.DatosGenerales.Agencia : 1 == 1)
                .Where(a => a.RAtencion.FechaHoraRegistro >= NucsRacsEstadistica.DatosGenerales.Fechadesde)
                .Where(a => a.RAtencion.FechaHoraRegistro <= NucsRacsEstadistica.DatosGenerales.Fechahasta)
                .ToListAsync();

            IEnumerable<NucsRacsEstadisticaViewModel> items = new NucsRacsEstadisticaViewModel[] { };

            IEnumerable<NucsRacsEstadisticaViewModel> ReadLines(int cantidad, string tipo)
            {
                IEnumerable<NucsRacsEstadisticaViewModel> item2;

                item2 = (new[]{new NucsRacsEstadisticaViewModel{
                                Cantidad = cantidad,
                                Tipo = tipo
                            }});

                return item2;
            }

            items = items.Concat(ReadLines(nucs.Count, "Total de Nucs"));
            items = items.Concat(ReadLines(racs.Count, "Total de Racs"));


            return items;

        }

        // GET: api/RHechoes/ListarPorNucMC
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{NUC}")]
        public async Task<IEnumerable<ListarMisCarpetasViewModel>> ListarPorNucMC([FromRoute] string NUC)
        {
            var carpetas = await _context.RHechoes
                          .Include(a => a.RAtencion)
                          .Include(a => a.NUCs)
                          .Where(a => a.NucId != null)
                          .Where(a => a.NUCs.nucg == NUC)
                          .ToListAsync();

            var carpetasf = carpetas.Select(a => new ListarMisCarpetasViewModel
            {
                RHechoId = a.IdRHecho,
                Agenciaid = a.Agenciaid,
                RAtencionId = a.RAtencionId,
                u_Nombre = a.RAtencion.u_Nombre,
                u_Puesto = a.RAtencion.u_Puesto,
                u_Modulo = a.RAtencion.u_Modulo,
                DistritoInicial = a.RAtencion.DistritoInicial,
                DirSubProcuInicial = a.RAtencion.DirSubProcuInicial,
                AgenciaInicial = a.RAtencion.AgenciaInicial,
                Status = a.Status,
                nucId = a.NucId,
                nuc = a.NUCs.nucg,
                FechaElevaNuc = a.FechaElevaNuc,
                NDenunciaOficio = a.NDenunciaOficio,

            });

            IEnumerable<ListarMisCarpetasViewModel> items = new ListarMisCarpetasViewModel[] { };

            foreach (var carpetaf in carpetasf)
            {
                var victima = await _context.RAPs
                    .Where(a => a.RAtencionId == carpetaf.RAtencionId)
                    .Where(a => a.PInicio)
                    .Include(a => a.Persona)
                    .FirstOrDefaultAsync();

                    IEnumerable<ListarMisCarpetasViewModel> ReadLines()
                    {
                        IEnumerable<ListarMisCarpetasViewModel> item2;

                        item2 = (new[]{new ListarMisCarpetasViewModel{
                        RHechoId = carpetaf.RHechoId,
                        Agenciaid = carpetaf.Agenciaid,
                        RAtencionId = carpetaf.RAtencionId,
                        u_Nombre = carpetaf.u_Nombre,
                        u_Puesto = carpetaf.u_Puesto,
                        u_Modulo = carpetaf.u_Modulo,
                        DistritoInicial = carpetaf.DistritoInicial,
                        DirSubProcuInicial = carpetaf.DirSubProcuInicial,
                        AgenciaInicial = carpetaf.AgenciaInicial,
                        Status = carpetaf.Status,
                        nucId = carpetaf.nucId,
                        nuc = carpetaf.nuc,
                        FechaElevaNuc = carpetaf.FechaElevaNuc,
                        NDenunciaOficio = carpetaf.NDenunciaOficio,
                        Modulo = carpetaf.Modulo,
                        Victima =  victima != null ? victima.Persona.Nombre + " " + victima.Persona.ApellidoPaterno + " " + victima.Persona.ApellidoMaterno : "Sin registrar V/I"
                    }});

                        return item2;
                    }

                    items = items.Concat(ReadLines());

            }

            return items;

        }

        // GET: api/RHechoes/ValidarNuc
        [Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador, Recepción")]
        [HttpGet("[action]/{nuc}/{distritoId}")]
        public async Task<IActionResult> ValidarNuc([FromRoute] string nuc, [FromRoute] Guid distritoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + distritoId.ToString().ToUpper())).Options;

                using (var ctx = new DbContextSIIGPP(options))
                {
                    bool nucActivo = await ctx.RHechoes
                                            .AsNoTracking()
                                            .AnyAsync(a => a.NUCs.nucg == nuc);

                    return Ok(new ValidarNucViewModel
                    {
                        NucActivo = nucActivo
                    });

                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }


        // GET: api/RHechoes/EstadisticaListaCarpetas
        //[Authorize(Roles = "AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Administrador")]
        [HttpGet("[action]/{ListaNucsEstadistica}")]
        public async Task<IEnumerable<EstadisticaListaCarpetasViewModel>> EstadisticaListaCarpetas([FromQuery] ListaNucsEstadistica ListaNucsEstadistica)
        {


            var carpetas = await _context.RDHs
                          .Include(a => a.RHecho.ModuloServicio.Agencia.DSP.Distrito)
                          .Include(a => a.RHecho.ModuloServicio.Agencia.DSP)
                          .Include(a => a.RHecho.ModuloServicio.Agencia)
                          .Include(a => a.RHecho.RAtencion)
                          .Include(a => a.RHecho.RAtencion.RACs)
                          .Include(a => a.RHecho.NUCs)
                          .Include(a => a.Delito)
                          .Include(a => a.RHecho)
                          .Where(a => a.RHecho.FechaElevaNuc2 >= ListaNucsEstadistica.DatosGenerales.Fechadesde)
                          .Where(a => a.RHecho.FechaElevaNuc2 <= ListaNucsEstadistica.DatosGenerales.Fechahasta)
                          .Where(a => ListaNucsEstadistica.DatosGenerales.Distritoact ? a.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == ListaNucsEstadistica.DatosGenerales.Distrito : 1 == 1)
                          .Where(a => ListaNucsEstadistica.DatosGenerales.Dspact ? a.RHecho.ModuloServicio.Agencia.DSP.IdDSP == ListaNucsEstadistica.DatosGenerales.Dsp : 1 == 1)
                          .Where(a => ListaNucsEstadistica.DatosGenerales.Agenciaact ? a.RHecho.ModuloServicio.Agencia.IdAgencia == ListaNucsEstadistica.DatosGenerales.Agencia : 1 == 1)
                          .Where(a => (ListaNucsEstadistica.EstatusEtapaCarpeta.EtapaActual != "ZKR" ? a.RHecho.NUCs.Etapanuc == ListaNucsEstadistica.EstatusEtapaCarpeta.EtapaActual : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.EstatusEtapaCarpeta.StatusActual != "ZKR" ? a.RHecho.NUCs.StatusNUC == ListaNucsEstadistica.EstatusEtapaCarpeta.StatusActual : 1 == 1))
                          .Where(a => a.RHecho.FechaHoraSuceso2.TimeOfDay >= ListaNucsEstadistica.HoraLugarSuceso.Horainicio.TimeOfDay && a.RHecho.FechaHoraSuceso2.TimeOfDay <= ListaNucsEstadistica.HoraLugarSuceso.HoraFin.TimeOfDay)
                          .Where(a => a.RHecho.FechaHoraSuceso2.Date >= ListaNucsEstadistica.HoraLugarSuceso.Fechainicio.Date && a.RHecho.FechaHoraSuceso2.Date <= ListaNucsEstadistica.HoraLugarSuceso.FechaFin.Date)
                          .Where(a => a.RHecho.Status == true)
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.Delitoactivo ? a.DelitoId == ListaNucsEstadistica.CaracteristicasDelito.DelitoNombre : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.Delitoespecifico != "ZKR" ? a.DelitoEspecifico == ListaNucsEstadistica.CaracteristicasDelito.Delitoespecifico : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.Modalidaddelito != "ZKR" ? a.Tipo == ListaNucsEstadistica.CaracteristicasDelito.Modalidaddelito : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.Gradoejecucion != "ZKR" ? a.ResultadoDelito == ListaNucsEstadistica.CaracteristicasDelito.Gradoejecucion : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.Descripcionrobado != "ZKR" ? a.TipoRobado == ListaNucsEstadistica.CaracteristicasDelito.Descripcionrobado : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.ViolenciaSinViolencia != "ZKR" ? a.ViolenciaSinViolencia == ListaNucsEstadistica.CaracteristicasDelito.ViolenciaSinViolencia : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.ConAlgunaParteCuerpo != "ZKR" ? a.ConAlgunaParteCuerpo == ListaNucsEstadistica.CaracteristicasDelito.ConAlgunaParteCuerpo : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.ConOtroElemento != "ZKR" ? a.ConotroElemento == ListaNucsEstadistica.CaracteristicasDelito.ConOtroElemento : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.Armafuego != "ZKR" ? ListaNucsEstadistica.CaracteristicasDelito.Armafuego == "Si" ?   a.ArmaFuego  : !a.ArmaFuego : 1 == 1))
                          .Where(a => (ListaNucsEstadistica.CaracteristicasDelito.Armablanca != "ZKR" ? ListaNucsEstadistica.CaracteristicasDelito.Armablanca == "Si" ? a.ArmaBlanca : !a.ArmaBlanca : 1 == 1))
                          .OrderBy(a => a.RHecho.NUCs.nucg)
                          .ToListAsync();

            var carpetasf =  carpetas.Select(a => new EstadisticaListaCarpetasViewModel
            {
                RHechoId = a.RHechoId,             
                nuc = a.RHecho.NUCs.nucg,
                FechaElevaNuc = a.RHecho.FechaElevaNuc,
                DistritoInicial = a.RHecho.ModuloServicio.Agencia.DSP.Distrito.Nombre,
                DspInicial = a.RHecho.ModuloServicio.Agencia.DSP.NombreSub,
                AgenciaInicial = a.RHecho.ModuloServicio.Agencia.Nombre,
                EtapaCarpeta = a.RHecho.NUCs.Etapanuc,
                StatusCarpeta = a.RHecho.NUCs.StatusNUC,
                RAtencionId = a.RHecho.RAtencionId,
                Fechah = a.RHecho.FechaHoraSuceso2,
                Delito = a.Delito.Nombre,
                DelitoEspecifico = a.DelitoEspecifico,
                ModalidadesDelito = a.Tipo,
                GradoEjecucion = a.ReclasificacionDelito,
                TipoRobado = a.TipoRobado,
                ConAlgunaParteCuerpo = a.ConAlgunaParteCuerpo,
                ViolenciaSinViolencia = a.ViolenciaSinViolencia,
                ConotroElemento = a.ConotroElemento,
                ArmaBlanca =  (a.ArmaBlanca ? "Si" : "No"),
                ArmaFuego = (a.ArmaFuego ? "Si" : "No")
            });

            IEnumerable<EstadisticaListaCarpetasViewModel> items = new EstadisticaListaCarpetasViewModel[] { };

            foreach (var carpeta in carpetasf)
            {
                var direccion = await _context.DireccionDelitos
                    .Where(a => (ListaNucsEstadistica.HoraLugarSuceso.Municipio != "ZKR" ? a.Municipio == ListaNucsEstadistica.HoraLugarSuceso.Municipio : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.HoraLugarSuceso.Localidad != "ZKR" ? a.Localidad == ListaNucsEstadistica.HoraLugarSuceso.Localidad : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.HoraLugarSuceso.Calle != "ZKR" ? a.Calle == ListaNucsEstadistica.HoraLugarSuceso.Calle : 1 == 1))
                    .FirstOrDefaultAsync();

                if(direccion != null)
                {
                    IEnumerable<EstadisticaListaCarpetasViewModel> ReadLines()
                    {
                        IEnumerable<EstadisticaListaCarpetasViewModel> item2;

                        item2 = (new[]{new EstadisticaListaCarpetasViewModel{
                        RHechoId = carpeta.RHechoId,
                        nuc = carpeta.nuc,
                        FechaElevaNuc = carpeta.FechaElevaNuc,
                        DistritoInicial = carpeta.DistritoInicial,
                        DspInicial = carpeta.DspInicial,
                        AgenciaInicial = carpeta.AgenciaInicial,
                        EtapaCarpeta = carpeta.EtapaCarpeta,
                        StatusCarpeta = carpeta.StatusCarpeta,
                        RAtencionId = carpeta.RAtencionId,
                        Fechah = carpeta.Fechah,
                        Delito = carpeta.Delito,
                        DelitoEspecifico = carpeta.DelitoEspecifico,
                        ModalidadesDelito = carpeta.ModalidadesDelito,
                        GradoEjecucion = carpeta.GradoEjecucion,
                        TipoRobado = carpeta.TipoRobado,
                        ConAlgunaParteCuerpo = carpeta.ConAlgunaParteCuerpo,
                        ViolenciaSinViolencia = carpeta.ViolenciaSinViolencia,
                        ConotroElemento = carpeta.ConotroElemento,
                        ArmaBlanca = carpeta.ArmaBlanca,
                        ArmaFuego = carpeta.ArmaFuego,
                        Municipio = direccion.Municipio,
                        Localidad = direccion.Localidad,
                        Colonia = direccion.Calle
                    }});

                        return item2;

                    }

                    items = items.Concat(ReadLines());

                }              
                    
            }

            carpetasf = items;

            items = new EstadisticaListaCarpetasViewModel[] { };

            foreach (var carpeta in carpetasf)
            {
                var victima = await _context.RAPs
                    .Include(a => a.Persona)
                    .Where(x => x.RAtencionId == carpeta.RAtencionId)
                    .Where(x => x.ClasificacionPersona == "Victima directa" || x.ClasificacionPersona == "Victima indirecta")
                    .Where(a => (ListaNucsEstadistica.VictimaDatos.Nombre != "ZKR" ? a.Persona.Nombre == ListaNucsEstadistica.VictimaDatos.Nombre : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.VictimaDatos.Apellidopa != "ZKR" ? a.Persona.ApellidoPaterno == ListaNucsEstadistica.VictimaDatos.Apellidopa : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.VictimaDatos.Apellidoma != "ZKR" ? a.Persona.ApellidoMaterno == ListaNucsEstadistica.VictimaDatos.Apellidoma : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.VictimaDatos.Ocupacion != "ZKR" ? a.Persona.Ocupacion == ListaNucsEstadistica.VictimaDatos.Ocupacion : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.VictimaDatos.Sexo != "ZKR" ? a.Persona.Sexo == ListaNucsEstadistica.VictimaDatos.Sexo : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.VictimaDatos.Nacionalidad != "ZKR" ? a.Persona.Nacionalidad == ListaNucsEstadistica.VictimaDatos.Nacionalidad : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.VictimaDatos.Tipoedad == "Si(númerica)" ? a.Persona.Edad == ListaNucsEstadistica.VictimaDatos.Edad : ListaNucsEstadistica.VictimaDatos.Tipoedad == "Rango de edad" ? a.Persona.Edad >= ListaNucsEstadistica.VictimaDatos.Edadinicial && a.Persona.Edad <= ListaNucsEstadistica.VictimaDatos.EdadFinal : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.VictimaDatos.RelacionImputado == "Si" && ListaNucsEstadistica.VictimaDatos.TipoRelacion == "ZKR" ? a.Persona.Relacion == true : ListaNucsEstadistica.VictimaDatos.RelacionImputado == "Si" && ListaNucsEstadistica.VictimaDatos.TipoRelacion != "ZKR" ? a.Persona.Relacion == true && a.Persona.Parentesco == ListaNucsEstadistica.VictimaDatos.TipoRelacion : ListaNucsEstadistica.VictimaDatos.RelacionImputado == "No" ? a.Persona.Relacion == false : 1 == 1))
                    .ToListAsync();

                    if(victima.Count > 0)
                    {
                        IEnumerable<EstadisticaListaCarpetasViewModel> ReadLines()
                        {
                            IEnumerable<EstadisticaListaCarpetasViewModel> item2;

                            item2 = (new[]{new EstadisticaListaCarpetasViewModel{
                            RHechoId = carpeta.RHechoId,
                            nuc = carpeta.nuc,
                            FechaElevaNuc = carpeta.FechaElevaNuc,
                            DistritoInicial = carpeta.DistritoInicial,
                            DspInicial = carpeta.DspInicial,
                            AgenciaInicial = carpeta.AgenciaInicial,
                            EtapaCarpeta = carpeta.EtapaCarpeta,
                            StatusCarpeta = carpeta.StatusCarpeta,
                            RAtencionId = carpeta.RAtencionId,
                            Fechah = carpeta.Fechah,
                            Delito = carpeta.Delito,
                            DelitoEspecifico = carpeta.DelitoEspecifico,
                            ModalidadesDelito = carpeta.ModalidadesDelito,
                            GradoEjecucion = carpeta.GradoEjecucion,
                            TipoRobado = carpeta.TipoRobado,
                            ConAlgunaParteCuerpo = carpeta.ConAlgunaParteCuerpo,
                            ViolenciaSinViolencia = carpeta.ViolenciaSinViolencia,
                            ConotroElemento = carpeta.ConotroElemento,
                            ArmaBlanca = carpeta.ArmaBlanca,
                            ArmaFuego = carpeta.ArmaFuego,
                            Municipio = carpeta.Municipio,
                            Localidad = carpeta.Localidad,
                            Colonia = carpeta.Colonia,
                            Victimas = victima.Count
                        }});

                            return item2;

                        }

                        items = items.Concat(ReadLines());

                    }
                    

            }

            carpetasf = items;

            items = new EstadisticaListaCarpetasViewModel[] { };

            foreach (var carpeta in carpetasf)
            {
                var imputado = await _context.RAPs
                    .Include(a => a.Persona)
                    .Where(x => x.RAtencionId == carpeta.RAtencionId)
                    .Where(x => x.ClasificacionPersona == "Imputado")
                    .Where(a => (ListaNucsEstadistica.ImputadoDatos.Nombre != "ZKR" ? a.Persona.Nombre == ListaNucsEstadistica.ImputadoDatos.Nombre : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.ImputadoDatos.Apellidopa != "ZKR" ? a.Persona.ApellidoPaterno == ListaNucsEstadistica.ImputadoDatos.Apellidopa : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.ImputadoDatos.Apellidoma != "ZKR" ? a.Persona.ApellidoMaterno == ListaNucsEstadistica.ImputadoDatos.Apellidoma : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.ImputadoDatos.Ocupacion != "ZKR" ? a.Persona.Ocupacion == ListaNucsEstadistica.ImputadoDatos.Ocupacion : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.ImputadoDatos.Sexo != "ZKR" ? a.Persona.Sexo == ListaNucsEstadistica.ImputadoDatos.Sexo : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.ImputadoDatos.Nacionalidad != "ZKR" ? a.Persona.Nacionalidad == ListaNucsEstadistica.ImputadoDatos.Nacionalidad : 1 == 1))
                    .Where(a => (ListaNucsEstadistica.ImputadoDatos.Tipoedad == "Si(númerica)" ? a.Persona.Edad == ListaNucsEstadistica.ImputadoDatos.Edad : ListaNucsEstadistica.ImputadoDatos.Tipoedad == "Rango de edad" ? a.Persona.Edad >= ListaNucsEstadistica.ImputadoDatos.Edadinicial && a.Persona.Edad <= ListaNucsEstadistica.ImputadoDatos.EdadFinal : 1 == 1))                   
                    .ToListAsync();


                if (imputado.Count > 0)
                {
                    IEnumerable<EstadisticaListaCarpetasViewModel> ReadLines()
                    {
                        IEnumerable<EstadisticaListaCarpetasViewModel> item2;

                        item2 = (new[]{new EstadisticaListaCarpetasViewModel{
                        RHechoId = carpeta.RHechoId,
                        nuc = carpeta.nuc,
                        FechaElevaNuc = carpeta.FechaElevaNuc,
                        DistritoInicial = carpeta.DistritoInicial,
                        DspInicial = carpeta.DspInicial,
                        AgenciaInicial = carpeta.AgenciaInicial,
                        EtapaCarpeta = carpeta.EtapaCarpeta,
                        StatusCarpeta = carpeta.StatusCarpeta,
                        RAtencionId = carpeta.RAtencionId,
                        Fechah = carpeta.Fechah,
                        Delito = carpeta.Delito,
                        DelitoEspecifico = carpeta.DelitoEspecifico,
                        ModalidadesDelito = carpeta.ModalidadesDelito,
                        GradoEjecucion = carpeta.GradoEjecucion,
                        TipoRobado = carpeta.TipoRobado,
                        ConAlgunaParteCuerpo = carpeta.ConAlgunaParteCuerpo,
                        ViolenciaSinViolencia = carpeta.ViolenciaSinViolencia,
                        ConotroElemento = carpeta.ConotroElemento,
                        ArmaBlanca = carpeta.ArmaBlanca,
                        ArmaFuego = carpeta.ArmaFuego,
                        Municipio = carpeta.Municipio,
                        Localidad = carpeta.Localidad,
                        Colonia = carpeta.Colonia,
                        Victimas = carpeta.Victimas,
                        Imputados = imputado.Count


                    }});

                        return item2;

                    }

                    items = items.Concat(ReadLines());

                }

            }

            carpetasf = items;

            return carpetasf;

        }

        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/RHechoes/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            { 
            var consultaHecho = await _context.RHechoes
                               .Where(x => x.IdRHecho == model.IdRHecho)
                               .Take(1)
                               .FirstOrDefaultAsync();




            if (consultaHecho == null)
            {
                return BadRequest(ModelState);

            }
            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var InsertarRH = await ctx.RHechoes.FirstOrDefaultAsync(a => a.IdRHecho == consultaHecho.IdRHecho);

                    if (InsertarRH == null)
                    {
                        InsertarRH = new RHecho();
                        ctx.RHechoes.Add(InsertarRH);
                    }


                    InsertarRH.IdRHecho = consultaHecho.IdRHecho;
                    InsertarRH.RAtencionId = consultaHecho.RAtencionId;
                    InsertarRH.ModuloServicioId = consultaHecho.ModuloServicioId;
                    InsertarRH.Agenciaid = consultaHecho.Agenciaid;
                    InsertarRH.FechaReporte = consultaHecho.FechaReporte;
                    InsertarRH.FechaHoraSuceso = consultaHecho.FechaHoraSuceso;
                    InsertarRH.Status = consultaHecho.Status;
                    InsertarRH.RBreve = consultaHecho.RBreve;
                    InsertarRH.NarrativaHechos = consultaHecho.NarrativaHechos;
                    InsertarRH.NucId = consultaHecho.NucId;
                    InsertarRH.FechaElevaNuc = consultaHecho.FechaElevaNuc;
                    InsertarRH.FechaElevaNuc2 = consultaHecho.FechaElevaNuc2;
                    InsertarRH.Vanabim = consultaHecho.Vanabim;
                    InsertarRH.NDenunciaOficio = consultaHecho.NDenunciaOficio;
                    InsertarRH.Texto = consultaHecho.Texto;
                    InsertarRH.Observaciones = consultaHecho.Observaciones;
                    InsertarRH.FechaHoraSuceso2 = consultaHecho.FechaHoraSuceso2;

                   
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.RemisionesUI;
using SIIGPP.CAT.Models.RemisionesUI;
using SIIGPP.CAT.Models.Rac;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SIIGPP.Entidades.M_Administracion;
using SIIGPP.Entidades.M_Cat.Registro;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemisionesUIController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;


        public RemisionesUIController(DbContextSIIGPP context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: api/RemisionesUI/Listar
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IActionResult> Listar([FromRoute] Guid RHechoId)
        {
            try
            {
                var hi = await _context.RemisionUIs
                .Include(a => a.ModuloServicio.Agencia.DSP.Distrito)
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

                return Ok(hi.Select(a => new RemisionUIViewModel
                {
                    IdRemisionUI = a.IdRemisionUI,
                    RHechoId = a.RHechoId,
                    Fecha = a.Fecha,
                    DirigidoA = a.DirigidoA,
                    Status = a.Status,
                    UDistrito = a.UDistrito,
                    USubproc = a.USubproc,
                    UAgencia = a.UAgencia,
                    Usuario = a.Usuario,
                    UPuesto = a.UPuesto,
                    UModulo = a.UModulo,
                    Fechasys = a.Fechasys,
                    Moduloqueenvia = a.Moduloqueenvia,
                    PuestoA = a.PuestoA,
                    Rechazo = a.Rechazo,
                    FechaRechazo = a.FechaRechazo,
                    NumeroOficio = a.NumeroOficio,
                    ModuloServicioId = a.ModuloServicioId,
                    AgenciaQueenvia = a.AgenciaQueenvia,
                    Nuc = a.Nuc,
                    ParaDonde = a.ModuloServicio.Agencia.Nombre + " de " + a.ModuloServicio.Agencia.DSP.Distrito.Nombre,
                    EnvioExitosoTF = a.EnvioExitosoTF,

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

        }
        // GET: api/RemisionesUI/Listar
        [HttpGet("[action]/{idRemisionUI}")]
        public async Task<IActionResult> ListarFallidos([FromRoute] Guid idRemisionUI)
        {
            try
            {
                var hi = await _context.RemisionUIs
                .Where(a => a.IdRemisionUI == idRemisionUI)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

                return Ok(hi.Select(a => new RemisionUIViewModel
                {
                    IdRemisionUI = a.IdRemisionUI,
                    RHechoId = a.RHechoId,
                    Fecha = a.Fecha,
                    DirigidoA = a.DirigidoA,
                    Status = a.Status,
                    UDistrito = a.UDistrito,
                    USubproc = a.USubproc,
                    UAgencia = a.UAgencia,
                    Usuario = a.Usuario,
                    UPuesto = a.UPuesto,
                    UModulo = a.UModulo,
                    Fechasys = a.Fechasys,
                    Moduloqueenvia = a.Moduloqueenvia,
                    PuestoA = a.PuestoA,
                    Rechazo = a.Rechazo,
                    FechaRechazo = a.FechaRechazo,
                    NumeroOficio = a.NumeroOficio,
                    ModuloServicioId = a.ModuloServicioId,
                    AgenciaQueenvia = a.AgenciaQueenvia,
                    Nuc = a.Nuc,
                    EnvioExitosoTF = a.EnvioExitosoTF,

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

        }

        // GET: api/RemisionesUI/Listar
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IActionResult> ListarConOrden([FromRoute] Guid RHechoId)
        {

            try
            {
                string busquedaRemisiones = @"SELECT 
                [IdRemisionUI],
                [RHechoId],
                [Fecha],
                [DirigidoA],
                [UDistrito],
                [USubproc],
                [Status],
                [UAgencia],
                [Usuario],
                [UPuesto],
                [UModulo],
                [Fechasys],
                [Moduloqueenvia],
                [PuestoA],
                [FechaRechazo],
                [Rechazo],
                [NumeroOficio],
                [ModuloServicioId],
                [AgenciaQueenvia],
                [Nuc],
                (ROW_NUMBER() OVER(ORDER BY Fechasys DESC)) as posicion 
                FROM CAT_REMISIONUI 
                WHERE RHechoId=@hechoid 
                ORDER BY Fechasys DESC
                ";
             
                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@hechoid", RHechoId));
                
                var remisiones = await _context.RemisionUIPoss.FromSqlRaw(busquedaRemisiones, filtrosBusqueda.ToArray()).ToListAsync();
                
                
                return Ok(remisiones.Select(a => new RemisionUIViewModelPos
                {
                    IdRemisionUI = a.IdRemisionUI,
                    RHechoId = a.RHechoId,
                    Fecha = a.Fecha,
                    DirigidoA = a.DirigidoA,
                    Status = a.Status,
                    UDistrito = a.UDistrito,
                    USubproc = a.USubproc,
                    UAgencia = a.UAgencia,
                    Usuario = a.Usuario,
                    UPuesto = a.UPuesto,
                    UModulo = a.UModulo,
                    Fechasys = a.Fechasys,
                    Moduloqueenvia = a.Moduloqueenvia,
                    PuestoA = a.PuestoA,
                    Rechazo = a.Rechazo,
                    FechaRechazo = a.FechaRechazo,
                    NumeroOficio = a.NumeroOficio,
                    ModuloServicioId = a.ModuloServicioId,
                    AgenciaQueenvia = a.AgenciaQueenvia,
                    Nuc = a.Nuc,
                    posicion= a.posicion
                }));
                
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
            
        }
        
        // POST: api/RemisionesUI/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                RemisionUI hi = new RemisionUI
                {

                    RHechoId = model.RHechoId,
                    Fecha = model.Fecha,
                    DirigidoA = model.DirigidoA,
                    Status = model.Status,
                    UDistrito = model.UDistrito,
                    USubproc = model.USubproc,
                    UAgencia = model.UAgencia,
                    Usuario = model.Usuario,
                    UPuesto = model.UPuesto,
                    UModulo = model.UModulo,
                    Fechasys = System.DateTime.Now,
                    Moduloqueenvia = model.Moduloqueenvia,
                    PuestoA = model.PuestoA,
                    Rechazo = model.Rechazo,
                    FechaRechazo = model.FechaRechazo,
                    NumeroOficio = model.NumeroOficio,
                    ModuloServicioId = model.ModuloServicioId,
                    AgenciaQueenvia = model.AgenciaQueenvia,
                    Nuc = model.Nuc,
                    EnvioExitosoTF = model.EnvioExitosoTF,
                    

                };

                _context.RemisionUIs.Add(hi);
                
                await _context.SaveChangesAsync();

                //Guardo el id de la remision que se creo para su posterior uso en frontend
                Guid idRemision = hi.IdRemisionUI;

                return Ok(new { idRemision = idRemision });
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

            
        }

        // PUT: api/RemisionesUI/Actualizar
        [Authorize(Roles = "Administrador,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.RemisionUIs.FirstOrDefaultAsync(a => a.IdRemisionUI == model.IdRemisionUI);

            if (ao == null)
            {
                return NotFound();
            }

            ao.Status = model.Status;

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

        // PUT: api/RemisionesUI/ActualizarEnvioExitoso
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarEnvioExitoso([FromBody] ActualizarViewModelReenviar model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ao = await _context.RemisionUIs.FirstOrDefaultAsync(a => a.IdRemisionUI == model.IdRemisionUI);

            if (ao == null)
            {
                return NotFound();
            }

            ao.EnvioExitosoTF = model.EnvioExitosoTF;

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

        //Api para la actualizar el distrtito de donde se manda, este mensaje solo es importante para lña agencia que envia, por eso se edita esta api en lugar de duplicarla
        // PUT: api/RemisionesUI/ActualizarRechazo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarRechazo([FromBody] ActualizarViewModelDistrito model)
        {
            try
            {
                //Conexion a base de datos
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {

                    var ao = await _context.RemisionUIs.FirstOrDefaultAsync(a => a.IdRemisionUI == model.IdRemisionUI);

                    if (ao == null)
                    {
                        return NotFound();
                    }

                    ao.Status = model.Status;
                    ao.FechaRechazo = System.DateTime.Now;
                    ao.Rechazo = model.Rechazo;

                    await _context.SaveChangesAsync();
      
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

        //Nueva api para el 
        // GET: api/RemisionesUI/ListarporModulo
        [HttpGet("[action]/{ModuloId}/{status}")]
        public async Task<IEnumerable<RemisionUIViewModel>> ListarporModulo([FromRoute] Guid ModuloId, string status)
        {
            var br = await _context.RemisionUIs
                .Include(a => a.RHecho)
                .Where(a => a.ModuloServicioId == ModuloId)
                .Where(a => a.RHecho.ModuloServicioId == ModuloId)
                .Where(a => a.Status == status)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            var carpetasUnicas = br.DistinctBy(a => a.RHechoId);

            return carpetasUnicas.Select(a => new RemisionUIViewModel
            {
                IdRemisionUI = a.IdRemisionUI,
                RHechoId = a.RHechoId,
                Fecha = a.Fecha,
                DirigidoA = a.DirigidoA,
                Status = a.Status,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Moduloqueenvia = a.Moduloqueenvia,
                PuestoA = a.PuestoA,
                Rechazo = a.Rechazo,
                FechaRechazo = a.FechaRechazo,
                NumeroOficio = a.NumeroOficio,
                ModuloServicioId = a.ModuloServicioId,
                AgenciaQueenvia = a.AgenciaQueenvia,
                Nuc = a.Nuc

            });

        }

        //Nueva api para el 
        // GET: api/RemisionesUI/ListarporModulo
        [HttpGet("[action]/{ModuloId}")]
        public async Task<IEnumerable<RemisionUIViewModel>> ListarporModuloTodo([FromRoute] Guid ModuloId, string status)
        {
            var br = await _context.RemisionUIs
                .Include(a => a.RHecho)
                .Where(a => a.ModuloServicioId == ModuloId)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return br.Select(a => new RemisionUIViewModel
            {
                IdRemisionUI = a.IdRemisionUI,
                RHechoId = a.RHechoId,
                Fecha = a.Fecha,
                DirigidoA = a.DirigidoA,
                Status = a.Status,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Moduloqueenvia = a.Moduloqueenvia,
                PuestoA = a.PuestoA,
                Rechazo = a.Rechazo,
                FechaRechazo = a.FechaRechazo,
                NumeroOficio = a.NumeroOficio,
                ModuloServicioId = a.ModuloServicioId,
                AgenciaQueenvia = a.AgenciaQueenvia,
                Nuc = a.Nuc

            });

        }


        // GET: api/RemisionesUI/ListarporModuloEnvia
        [HttpGet("[action]/{ModuloId}")]
        public async Task<IEnumerable<RemisionUIViewModel>> ListarporModuloEnvia([FromRoute] Guid ModuloId)
        {
            var hi = await _context.RemisionUIs
                .Where(a => a.Moduloqueenvia == ModuloId)
                .Where(a => a.Status == "Rechazado")
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return hi.Select(a => new RemisionUIViewModel
            {
                IdRemisionUI = a.IdRemisionUI,
                RHechoId = a.RHechoId,
                Fecha = a.Fecha,
                DirigidoA = a.DirigidoA,
                Status = a.Status,
                UDistrito = a.UDistrito,
                USubproc = a.USubproc,
                UAgencia = a.UAgencia,
                Usuario = a.Usuario,
                UPuesto = a.UPuesto,
                UModulo = a.UModulo,
                Fechasys = a.Fechasys,
                Moduloqueenvia = a.Moduloqueenvia,
                PuestoA = a.PuestoA,
                Rechazo = a.Rechazo,
                FechaRechazo = a.FechaRechazo,
                NumeroOficio = a.NumeroOficio,
                ModuloServicioId = a.ModuloServicioId,
                AgenciaQueenvia = a.AgenciaQueenvia,
                Nuc = a.Nuc

            });

        }
        // POST: api/RemisionesUI/Eliminar
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
                //PRIMERO BUSCAR EL HECHO
                var consultaRemision = await _context.RemisionUIs
                    .Where(a => a.IdRemisionUI == model.infoBorrado.registroId)
                    .Include(a=>a.ModuloServicio)
                    .Include(a=>a.ModuloServicio.Agencia)
                    .Take(1).FirstOrDefaultAsync();
                if (consultaRemision == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningúna remisión con la información enviada" });
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
                            MovimientoId = new Guid("19f48c4e-bd64-48ae-a7c6-191303da21c8")
                        };

                        ctx.Add(laRegistro);

                        LogRemisionUI remision = new LogRemisionUI
                        {
                            LogAdmonId = gLog,
                            IdRemisionUI = consultaRemision.IdRemisionUI,
                            RHechoId = consultaRemision.RHechoId,
                            ModuloServicioId = consultaRemision.ModuloServicioId,
                            Fecha = consultaRemision.Fecha,
                            DirigidoA = consultaRemision.DirigidoA,
                            PuestoA = consultaRemision.PuestoA,
                            Moduloqueenvia = consultaRemision.Moduloqueenvia,
                            AgenciaQueenvia = consultaRemision.AgenciaQueenvia,
                            UDistrito = consultaRemision.UDistrito,
                            USubproc = consultaRemision.USubproc,
                            Status = consultaRemision.Status,
                            UAgencia = consultaRemision.UAgencia,
                            Usuario = consultaRemision.Usuario,
                            UPuesto = consultaRemision.UPuesto,
                            UModulo = consultaRemision.UModulo,
                            Fechasys = consultaRemision.Fechasys,
                            Rechazo = consultaRemision.Rechazo,
                            FechaRechazo = consultaRemision.FechaRechazo,
                            NumeroOficio = consultaRemision.NumeroOficio,
                            Nuc = consultaRemision.Nuc

                        };
                        ctx.Add(remision);
                        var consultaHecho = await _context.RHechoes.Where(a => a.IdRHecho == model.infoBorrado.rHechoId)
                                          .Take(1).FirstOrDefaultAsync();
                        if (consultaRemision == null)
                        {
                            return Ok(new { res = "Error", men = "No se encontró hecho con la información enviada" });
                        }
                        else
                        {

                            LogCat_RHecho hecho = new LogCat_RHecho
                            {
                                LogAdmonId = gLog,
                                IdRHecho = consultaHecho.IdRHecho,
                                RAtencionId = consultaHecho.RAtencionId,
                                ModuloServicioId = consultaHecho.ModuloServicioId,
                                Agenciaid = consultaHecho.Agenciaid,
                                FechaReporte = consultaHecho.FechaReporte,
                                FechaHoraSuceso = consultaHecho.FechaHoraSuceso,
                                FechaHoraSuceso2 = consultaHecho.FechaHoraSuceso2,
                                Status = consultaHecho.Status,
                                RBreve = consultaHecho.RBreve,
                                NarrativaHechos = consultaHecho.NarrativaHechos,
                                NucId = consultaHecho.NucId,
                                FechaElevaNuc = consultaHecho.FechaElevaNuc,
                                FechaElevaNuc2 = consultaHecho.FechaElevaNuc2,
                                Vanabim = consultaHecho.Vanabim,
                                NDenunciaOficio = consultaHecho.NDenunciaOficio,
                                Texto = consultaHecho.Texto,
                                Observaciones = consultaHecho.Observaciones
                            };
                            ctx.Add(hecho);
                            
                            //CAMBIAR LA AGENCIA Y MODULO A LOS DE LA AGENCIA QUE ENVIÓ ORIGINALMENTE
                            consultaHecho.Agenciaid =consultaRemision.ModuloServicio.Agencia.IdAgencia;
                            consultaHecho.ModuloServicioId =consultaRemision.ModuloServicioId;
                            
                            var consultaHistorial = await _context.HistorialCarpetas.Where(a => a.RHechoId == model.infoBorrado.rHechoId).OrderByDescending(a => a.Fechasys).Where(a=>a.Detalle== "Remisión UI")
                                          .Take(1).FirstOrDefaultAsync();
                            if (consultaHistorial == null)
                            {
                                return Ok(new { res = "Error", men = "No se encontró historial de carpeta con la información enviada" });
                            }
                            else
                            {
                                LogHistorialCarpeta historial = new LogHistorialCarpeta
                                {
                                    LogAdmonId = gLog,
                                    IdHistorialcarpetas = consultaHistorial.IdHistorialcarpetas,
                                    RHechoId = consultaHistorial.RHechoId,
                                    Detalle = consultaHistorial.Detalle,
                                    DetalleEtapa = consultaHistorial.DetalleEtapa,
                                    Modulo = consultaHistorial.Modulo,
                                    Agencia = consultaHistorial.Agencia,
                                    UDistrito = consultaHistorial.UDistrito,
                                    USubproc = consultaHistorial.USubproc,
                                    UAgencia = consultaHistorial.UAgencia,
                                    Usuario = consultaHistorial.Usuario,
                                    UPuesto = consultaHistorial.UPuesto,
                                    UModulo = consultaHistorial.UModulo,
                                    Fechasys = consultaHistorial.Fechasys

                                };
                                ctx.Add(historial);
                                _context.Remove(consultaHistorial);
                            }
                            }
                        //ELIMINAR LA REMISION DE LA BD
                        _context.Remove(consultaRemision);
                                                                        
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
            return Ok(new { res = "success", men = "Remisión eliminada Correctamente" });
        }


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/RemisionesUI/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var remisionesCarpeta = await _context.RemisionUIs.Where(x => x.RHechoId == model.IdRHecho).ToListAsync();

                if (remisionesCarpeta == null)
                {
                    return Ok();

                }

                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {


                    foreach (RemisionUI remisionActual in remisionesCarpeta)
                    {

                        var insertarRemision = await ctx.RemisionUIs.FirstOrDefaultAsync(a => a.IdRemisionUI == remisionActual.IdRemisionUI);

                        if (insertarRemision == null)
                        {
                            insertarRemision = new RemisionUI();
                            ctx.RemisionUIs.Add(insertarRemision);
                        }

                        insertarRemision.IdRemisionUI = remisionActual.IdRemisionUI;
                        insertarRemision.RHechoId = remisionActual.RHechoId;
                        insertarRemision.Fecha = remisionActual.Fecha;
                        insertarRemision.DirigidoA = remisionActual.DirigidoA;
                        insertarRemision.UDistrito = remisionActual.UDistrito;
                        insertarRemision.USubproc = remisionActual.USubproc;
                        insertarRemision.Status = remisionActual.Status;
                        insertarRemision.UAgencia = remisionActual.UAgencia;
                        insertarRemision.Usuario = remisionActual.Usuario;
                        insertarRemision.UPuesto = remisionActual.UPuesto;
                        insertarRemision.UModulo = remisionActual.UModulo;
                        insertarRemision.Fechasys = remisionActual.Fechasys;
                        insertarRemision.Moduloqueenvia = remisionActual.Moduloqueenvia;
                        insertarRemision.PuestoA = remisionActual.PuestoA;
                        insertarRemision.FechaRechazo = remisionActual.FechaRechazo;
                        insertarRemision.Rechazo = remisionActual.Rechazo;
                        insertarRemision.NumeroOficio = remisionActual.NumeroOficio;
                        insertarRemision.ModuloServicioId = remisionActual.ModuloServicioId;
                        insertarRemision.AgenciaQueenvia = remisionActual.AgenciaQueenvia;
                        insertarRemision.Nuc = remisionActual.Nuc;
                        insertarRemision.EnvioExitosoTF = remisionActual.EnvioExitosoTF;

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


    }
}

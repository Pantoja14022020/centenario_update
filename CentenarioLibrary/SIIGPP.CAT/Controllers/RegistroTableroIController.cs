using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.Datos;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SIIGPP.CAT.Models.RegistrosTableroI;
using SIIGPP.Entidades.M_Cat.RegistrosTableroI;
using Microsoft.Data.SqlClient;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroTableroIController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public RegistroTableroIController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        // POST: api/RegistroTableroI/Crear
        //[Authorize(Roles = "Administrador,Director,AMPO-AMP,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Coordinador, Jurídico, Recepción, Facilitador, Facilitador-Mixto,Notificador,Procurador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModelRTI model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            RegistroTableroI hi = new RegistroTableroI
            {
                RHechoId = model.RHechoId,
                TipoRegistroTableroI = model.TipoRegistroTableroI,
                Distrito = model.Distrito,
                DirSub = model.DirSub,
                Agencia = model.Agencia,
                ModuloServicioId = model.ModuloServicioId,
                Modulo = model.Modulo,
                UsuarioId = model.UsuarioId,
                NombreUsuario = model.NombreUsuario,
                FechaRegistro = System.DateTime.Now,

            };

            _context.RegistrosTableroI.Add(hi);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { id = hi.IdRegistroTableroI });
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result;
            }

        }

        // GET: api/RegistroTableroI/Listar
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IEnumerable<ListarViewModel>> Listar([FromRoute] Guid rHechoId)
        
        {

            var hi = await _context.RegistrosTableroI
                .Where(a => a.RHechoId == rHechoId)
                .OrderByDescending(a => a.FechaRegistro)
                .ToListAsync();


            return hi.Select(a => new ListarViewModel
            {
                IdRegistroTableroI = a.IdRegistroTableroI,
                RHechoId = a.RHechoId,
                TipoRegistroTableroI = a.TipoRegistroTableroI,
                Distrito = a.Distrito,
                DirSub = a.DirSub,
                Agencia = a.Agencia,
                ModuloServicioId = a.ModuloServicioId,
                Modulo = a.Modulo,
                UsuarioId = a.UsuarioId,
                NombreUsuario = a.NombreUsuario,
                FechaRegistro = a.FechaRegistro,

            });




        }

        // GET: api/RegistroTableroI/ListarI
        [HttpGet("[action]/{rHechoId}")]
        public async Task<IActionResult> ListarI([FromRoute] Guid rHechoId)
        {

            string nucm = "00000000-0000-0000-0000-000000000000";

            var hi = await _context.RegistrosTableroI
                .Where(a => a.RHechoId == rHechoId)
                .OrderByDescending(a => a.FechaRegistro)
                .FirstOrDefaultAsync();

            var nu = await _context.RHechoes
                .Include(x => x.NUCs)
                .Where(a => a.IdRHecho == rHechoId)
                .Where(a => a.NUCs.Etapanuc == "Concluida" || a.NUCs.Etapanuc == "Suspendida")
                .FirstOrDefaultAsync();

            //Este mensaje lo cacha como que no se a resuleto la carpeta y tampoco hay registros del tablero
            if (hi == null && nu == null)
            {
                //Este mensaje lo cacha como que no se a resuleto la carpeta y tampoco hay registros
                //return NotFound("No hay registros del tablero ni esta resuelta la carpeta");
                var result = new ObjectResult(new
                {
                    status = "warning",
                    message = "No hay registros del tablero ni esta resuelta la carpeta"
                });
                result.StatusCode = 200;
                return result;
            }
            if (hi == null && nu != null)
            {
                return Ok(new ListarViewModel
                {
                    NucId = nu.NUCs.idNuc

                });
            }
            if (nu == null)
            {
                return Ok(new ListarViewModel
                {
                    IdRegistroTableroI = hi.IdRegistroTableroI,
                    RHechoId = hi.RHechoId,
                    TipoRegistroTableroI = hi.TipoRegistroTableroI,
                    Distrito = hi.Distrito,
                    DirSub = hi.DirSub,
                    Agencia = hi.Agencia,
                    ModuloServicioId = hi.ModuloServicioId,
                    Modulo = hi.Modulo,
                    UsuarioId = hi.UsuarioId,
                    NombreUsuario = hi.NombreUsuario,
                    FechaRegistro = hi.FechaRegistro,
                    NucId = Guid.Parse(nucm)

                });
            }


            return Ok(new ListarViewModel
            {
                IdRegistroTableroI = hi.IdRegistroTableroI,
                RHechoId = hi.RHechoId,
                TipoRegistroTableroI = hi.TipoRegistroTableroI,
                Distrito = hi.Distrito,
                DirSub = hi.DirSub,
                Agencia = hi.Agencia,
                ModuloServicioId = hi.ModuloServicioId,
                Modulo = hi.Modulo,
                UsuarioId = hi.UsuarioId,
                NombreUsuario = hi.NombreUsuario,
                FechaRegistro = hi.FechaRegistro,
                NucId = nu.NUCs.idNuc

            });
            



        }
        // GET: api/RegistroTableroI/ListarI
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador,Recepción")]
        [HttpGet("[action]/{u_idagencia}")]
        public async Task<IActionResult> ListarM([FromRoute] Guid u_idagencia)
        {
            try
            {
                String ListaCarpetas = @"WITH CTE AS (
                                                            SELECT 
                                                                RTI.IdRegistroTableroI,
                                                                RH.IdRHecho,
                                                                RA.IdRAtencion,
                                                                NUC.nucg AS NUC,
                                                                RTI.TipoRegistroTableroI AS Ultimo_Registro,
                                                                MS.Nombre AS Modulo,
                                                                AG.Nombre AS Agencia,
                                                                DSP.nombreSubDir AS Direccion,
                                                                RA.DistritoInicial AS Distrito_Procedencia,
                                                                DIS.Nombre AS Distrito_Actual,
                                                                RH.FechaReporte,
                                                                RTI.FechaRegistro AS Fecha_del_Ultimo_Registro,
                                                                DATEDIFF(day, RTI.FechaRegistro, GETDATE()) AS Dias_Inactiva,
                                                                ROW_NUMBER() OVER (PARTITION BY RH.IdRHecho ORDER BY RTI.FechaRegistro DESC) AS RowNum
                                                            FROM CAT_REGISTROTABLEROI AS RTI
                                                            LEFT JOIN CAT_RHECHO AS RH ON RH.IdRHecho = RTI.RHechoId
                                                            LEFT JOIN CAT_RATENCON AS RA ON RA.IdRAtencion = RH.RAtencionId
                                                            LEFT JOIN NUC AS NUC ON NUC.idNuc = RH.NucId
                                                            LEFT JOIN C_MODULOSERVICIO AS MS ON MS.IdModuloServicio = RH.ModuloServicioId
                                                            LEFT JOIN C_AGENCIA AS AG ON AG.IdAgencia = ms.AgenciaId
                                                            LEFT JOIN C_DSP AS DSP ON DSP.IdDSP = AG.DSPId
                                                            LEFT JOIN C_DISTRITO AS DIS ON DIS.IdDistrito = DSP.DistritoId
                                                            WHERE AG.IdAgencia = @u_idagencia AND NUC.Etapanuc != 'Concluida' COLLATE Latin1_general_CI_AI AND NUC.Etapanuc != 'Suspendida' COLLATE Latin1_general_CI_AI 
                                                        )
                                                        SELECT 
                                                            IdRegistroTableroI,
                                                            IdRHecho,
                                                            IdRAtencion,
                                                            NUC,
                                                            Ultimo_Registro,
                                                            Modulo,
                                                            Agencia,
                                                            Direccion,
                                                            Distrito_Procedencia,
                                                            Distrito_Actual,
                                                            FechaReporte,
                                                            Fecha_del_Ultimo_Registro,
                                                            Dias_Inactiva
                                                        FROM CTE
                                                        WHERE RowNum = 1;";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@u_idagencia", u_idagencia));

                var compro = await _context.qComprobarModulo.FromSqlRaw(ListaCarpetas, filtrosBusqueda.ToArray()).ToListAsync();

                return Ok(compro.Select(a => new ListarModulosViewModel
                {
                    IdRegistroTableroI = a.IdRegistroTableroI,
                    IdRHecho = a.IdRHecho,
                    IdRAtencion = a.IdRAtencion,
                    NUC = a.NUC,
                    Ultimo_Registro = a.Ultimo_Registro,
                    Modulo = a.Modulo,
                    Agencia = a.Agencia,
                    Direccion = a.Direccion,
                    Distrito_Procedencia = a.Distrito_Procedencia,
                    Distrito_Actual = a.Distrito_Actual,
                    FechaReporte = a.FechaReporte,
                    Fecha_del_Ultimo_Registro = a.Fecha_del_Ultimo_Registro,
                    Dias_Inactiva = a.Dias_Inactiva

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
        // GET: api/RegistroTableroI/ListarI
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador")]
        [HttpGet("[action]/{u_idagencia}/{modulou}")]
        public async Task<IActionResult> ListarMEspecifico([FromRoute] Guid u_idagencia, Guid modulou)
        {
            try
            {
                String ListaCarpetas = @"WITH CTE AS (
                                                            SELECT 
                                                                RTI.IdRegistroTableroI,
                                                                RH.IdRHecho,
                                                                RA.IdRAtencion,
                                                                NUC.nucg AS NUC,
                                                                RTI.TipoRegistroTableroI AS Ultimo_Registro,
                                                                MS.Nombre AS Modulo,
                                                                AG.Nombre AS Agencia,
                                                                DSP.nombreSubDir AS Direccion,
                                                                RA.DistritoInicial AS Distrito_Procedencia,
                                                                DIS.Nombre AS Distrito_Actual,
                                                                RH.FechaReporte,
                                                                RTI.FechaRegistro AS Fecha_del_Ultimo_Registro,
                                                                DATEDIFF(day, RTI.FechaRegistro, GETDATE()) AS Dias_Inactiva,
                                                                ROW_NUMBER() OVER (PARTITION BY RH.IdRHecho ORDER BY RTI.FechaRegistro DESC) AS RowNum
                                                            FROM CAT_REGISTROTABLEROI AS RTI
                                                            LEFT JOIN CAT_RHECHO AS RH ON RH.IdRHecho = RTI.RHechoId
                                                            LEFT JOIN CAT_RATENCON AS RA ON RA.IdRAtencion = RH.RAtencionId
                                                            LEFT JOIN NUC AS NUC ON NUC.idNuc = RH.NucId
                                                            LEFT JOIN C_MODULOSERVICIO AS MS ON MS.IdModuloServicio = RH.ModuloServicioId
                                                            LEFT JOIN C_AGENCIA AS AG ON AG.IdAgencia = ms.AgenciaId
                                                            LEFT JOIN C_DSP AS DSP ON DSP.IdDSP = AG.DSPId
                                                            LEFT JOIN C_DISTRITO AS DIS ON DIS.IdDistrito = DSP.DistritoId
                                                            WHERE AG.IdAgencia = @u_idagencia AND MS.IdModuloServicio = @modulou AND NUC.Etapanuc != 'Concluida' COLLATE Latin1_general_CI_AI AND NUC.Etapanuc != 'Suspendida' COLLATE Latin1_general_CI_AI 
                                                        )
                                                        SELECT 
                                                            IdRegistroTableroI,
                                                            IdRHecho,
                                                            IdRAtencion,
                                                            NUC,
                                                            Ultimo_Registro,
                                                            Modulo,
                                                            Agencia,
                                                            Direccion,
                                                            Distrito_Procedencia,
                                                            Distrito_Actual,
                                                            FechaReporte,
                                                            Fecha_del_Ultimo_Registro,
                                                            Dias_Inactiva
                                                        FROM CTE
                                                        WHERE RowNum = 1;";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@u_idagencia", u_idagencia));
                filtrosBusqueda.Add(new SqlParameter("@modulou", modulou));

                var compro = await _context.qComprobarModulo.FromSqlRaw(ListaCarpetas, filtrosBusqueda.ToArray()).ToListAsync();

                return Ok(compro.Select(a => new ListarModulosViewModel
                {
                    IdRegistroTableroI = a.IdRegistroTableroI,
                    IdRHecho = a.IdRHecho,
                    IdRAtencion = a.IdRAtencion,
                    NUC = a.NUC,
                    Ultimo_Registro = a.Ultimo_Registro,
                    Modulo = a.Modulo,
                    Agencia = a.Agencia,
                    Direccion = a.Direccion,
                    Distrito_Procedencia = a.Distrito_Procedencia,
                    Distrito_Actual = a.Distrito_Actual,
                    FechaReporte = a.FechaReporte,
                    Fecha_del_Ultimo_Registro = a.Fecha_del_Ultimo_Registro,
                    Dias_Inactiva = a.Dias_Inactiva

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/RegistroTableroI/ListarI
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador")]
        [HttpGet("[action]/{u_iddsp}")]
        public async Task<IActionResult> ListarA([FromRoute] Guid u_iddsp)
        {
            try
            {
                String ListaCarpetas = @"WITH CTE AS (
                                                            SELECT 
                                                                RTI.IdRegistroTableroI,
                                                                RH.IdRHecho,
                                                                RA.IdRAtencion,
                                                                NUC.nucg AS NUC,
                                                                RTI.TipoRegistroTableroI AS Ultimo_Registro,
                                                                MS.Nombre AS Modulo,
                                                                AG.Nombre AS Agencia,
                                                                DSP.nombreSubDir AS Direccion,
                                                                RA.DistritoInicial AS Distrito_Procedencia,
                                                                DIS.Nombre AS Distrito_Actual,
                                                                RH.FechaReporte,
                                                                RTI.FechaRegistro AS Fecha_del_Ultimo_Registro,
                                                                DATEDIFF(day, RTI.FechaRegistro, GETDATE()) AS Dias_Inactiva,
                                                                ROW_NUMBER() OVER (PARTITION BY RH.IdRHecho ORDER BY RTI.FechaRegistro DESC) AS RowNum
                                                            FROM CAT_REGISTROTABLEROI AS RTI
                                                            LEFT JOIN CAT_RHECHO AS RH ON RH.IdRHecho = RTI.RHechoId
                                                            LEFT JOIN CAT_RATENCON AS RA ON RA.IdRAtencion = RH.RAtencionId
                                                            LEFT JOIN NUC AS NUC ON NUC.idNuc = RH.NucId
                                                            LEFT JOIN C_MODULOSERVICIO AS MS ON MS.IdModuloServicio = RH.ModuloServicioId
                                                            LEFT JOIN C_AGENCIA AS AG ON AG.IdAgencia = ms.AgenciaId
                                                            LEFT JOIN C_DSP AS DSP ON DSP.IdDSP = AG.DSPId
                                                            LEFT JOIN C_DISTRITO AS DIS ON DIS.IdDistrito = DSP.DistritoId
                                                            WHERE DSP.IdDSP = @u_iddsp AND NUC.Etapanuc != 'Concluida' COLLATE Latin1_general_CI_AI AND NUC.Etapanuc != 'Suspendida' COLLATE Latin1_general_CI_AI 
                                                        )
                                                        SELECT 
                                                            IdRegistroTableroI,
                                                            IdRHecho,
                                                            IdRAtencion,
                                                            NUC,
                                                            Ultimo_Registro,
                                                            Modulo,
                                                            Agencia,
                                                            Direccion,
                                                            Distrito_Procedencia,
                                                            Distrito_Actual,
                                                            FechaReporte,
                                                            Fecha_del_Ultimo_Registro,
                                                            Dias_Inactiva
                                                        FROM CTE
                                                        WHERE RowNum = 1;";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@u_iddsp", u_iddsp));

                var compro = await _context.qComprobarModulo.FromSqlRaw(ListaCarpetas, filtrosBusqueda.ToArray()).ToListAsync();

                return Ok(compro.Select(a => new ListarModulosViewModel
                {
                    IdRegistroTableroI = a.IdRegistroTableroI,
                    IdRHecho = a.IdRHecho,
                    IdRAtencion = a.IdRAtencion,
                    NUC = a.NUC,
                    Ultimo_Registro = a.Ultimo_Registro,
                    Modulo = a.Modulo,
                    Agencia = a.Agencia,
                    Direccion = a.Direccion,
                    Distrito_Procedencia = a.Distrito_Procedencia,
                    Distrito_Actual = a.Distrito_Actual,
                    FechaReporte = a.FechaReporte,
                    Fecha_del_Ultimo_Registro = a.Fecha_del_Ultimo_Registro,
                    Dias_Inactiva = a.Dias_Inactiva

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        // GET: api/RegistroTableroI/ListarI
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador")]
        [HttpGet("[action]/{u_iddsp}/{u_idagencia}")]
        public async Task<IActionResult> ListarAEspecifico([FromRoute] Guid u_iddsp, Guid u_idagencia)
        {
            try
            {
                String ListaCarpetas = @"WITH CTE AS (
                                                            SELECT 
                                                                RTI.IdRegistroTableroI,
                                                                RH.IdRHecho,
                                                                RA.IdRAtencion,
                                                                NUC.nucg AS NUC,
                                                                RTI.TipoRegistroTableroI AS Ultimo_Registro,
                                                                MS.Nombre AS Modulo,
                                                                AG.Nombre AS Agencia,
                                                                DSP.nombreSubDir AS Direccion,
                                                                RA.DistritoInicial AS Distrito_Procedencia,
                                                                DIS.Nombre AS Distrito_Actual,
                                                                RH.FechaReporte,
                                                                RTI.FechaRegistro AS Fecha_del_Ultimo_Registro,
                                                                DATEDIFF(day, RTI.FechaRegistro, GETDATE()) AS Dias_Inactiva,
                                                                ROW_NUMBER() OVER (PARTITION BY RH.IdRHecho ORDER BY RTI.FechaRegistro DESC) AS RowNum
                                                            FROM CAT_REGISTROTABLEROI AS RTI
                                                            LEFT JOIN CAT_RHECHO AS RH ON RH.IdRHecho = RTI.RHechoId
                                                            LEFT JOIN CAT_RATENCON AS RA ON RA.IdRAtencion = RH.RAtencionId
                                                            LEFT JOIN NUC AS NUC ON NUC.idNuc = RH.NucId
                                                            LEFT JOIN C_MODULOSERVICIO AS MS ON MS.IdModuloServicio = RH.ModuloServicioId
                                                            LEFT JOIN C_AGENCIA AS AG ON AG.IdAgencia = ms.AgenciaId
                                                            LEFT JOIN C_DSP AS DSP ON DSP.IdDSP = AG.DSPId
                                                            LEFT JOIN C_DISTRITO AS DIS ON DIS.IdDistrito = DSP.DistritoId
                                                            WHERE DSP.IdDSP = @u_iddsp AND AG.IdAgencia =@u_idagencia AND NUC.Etapanuc != 'Concluida' COLLATE Latin1_general_CI_AI AND NUC.Etapanuc != 'Suspendida' COLLATE Latin1_general_CI_AI 
                                                        )
                                                        SELECT 
                                                            IdRegistroTableroI,
                                                            IdRHecho,
                                                            IdRAtencion,
                                                            NUC,
                                                            Ultimo_Registro,
                                                            Modulo,
                                                            Agencia,
                                                            Direccion,
                                                            Distrito_Procedencia,
                                                            Distrito_Actual,
                                                            FechaReporte,
                                                            Fecha_del_Ultimo_Registro,
                                                            Dias_Inactiva
                                                        FROM CTE
                                                        WHERE RowNum = 1;";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@u_iddsp", u_iddsp));
                filtrosBusqueda.Add(new SqlParameter("@u_idagencia", u_idagencia));

                var compro = await _context.qComprobarModulo.FromSqlRaw(ListaCarpetas, filtrosBusqueda.ToArray()).ToListAsync();

                return Ok(compro.Select(a => new ListarModulosViewModel
                {
                    IdRegistroTableroI = a.IdRegistroTableroI,
                    IdRHecho = a.IdRHecho,
                    IdRAtencion = a.IdRAtencion,
                    NUC = a.NUC,
                    Ultimo_Registro = a.Ultimo_Registro,
                    Modulo = a.Modulo,
                    Agencia = a.Agencia,
                    Direccion = a.Direccion,
                    Distrito_Procedencia = a.Distrito_Procedencia,
                    Distrito_Actual = a.Distrito_Actual,
                    FechaReporte = a.FechaReporte,
                    Fecha_del_Ultimo_Registro = a.Fecha_del_Ultimo_Registro,
                    Dias_Inactiva = a.Dias_Inactiva

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
        // GET: api/RegistroTableroI/ListarI
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador")]
        [HttpGet("[action]/{u_iddistrito}")]
        public async Task<IActionResult> ListarD([FromRoute] Guid u_iddistrito)
        {
            try
            {
                String ListaCarpetas = @"WITH CTE AS (
                                                            SELECT 
                                                                RTI.IdRegistroTableroI,
                                                                RH.IdRHecho,
                                                                RA.IdRAtencion,
                                                                NUC.nucg AS NUC,
                                                                RTI.TipoRegistroTableroI AS Ultimo_Registro,
                                                                MS.Nombre AS Modulo,
                                                                AG.Nombre AS Agencia,
                                                                DSP.nombreSubDir AS Direccion,
                                                                RA.DistritoInicial AS Distrito_Procedencia,
                                                                DIS.Nombre AS Distrito_Actual,
                                                                RH.FechaReporte,
                                                                RTI.FechaRegistro AS Fecha_del_Ultimo_Registro,
                                                                DATEDIFF(day, RTI.FechaRegistro, GETDATE()) AS Dias_Inactiva,
                                                                ROW_NUMBER() OVER (PARTITION BY RH.IdRHecho ORDER BY RTI.FechaRegistro DESC) AS RowNum
                                                            FROM CAT_REGISTROTABLEROI AS RTI
                                                            LEFT JOIN CAT_RHECHO AS RH ON RH.IdRHecho = RTI.RHechoId
                                                            LEFT JOIN CAT_RATENCON AS RA ON RA.IdRAtencion = RH.RAtencionId
                                                            LEFT JOIN NUC AS NUC ON NUC.idNuc = RH.NucId
                                                            LEFT JOIN C_MODULOSERVICIO AS MS ON MS.IdModuloServicio = RH.ModuloServicioId
                                                            LEFT JOIN C_AGENCIA AS AG ON AG.IdAgencia = ms.AgenciaId
                                                            LEFT JOIN C_DSP AS DSP ON DSP.IdDSP = AG.DSPId
                                                            LEFT JOIN C_DISTRITO AS DIS ON DIS.IdDistrito = DSP.DistritoId
                                                            WHERE DIS.IdDistrito = @u_iddistrito AND NUC.Etapanuc != 'Concluida' COLLATE Latin1_general_CI_AI AND NUC.Etapanuc != 'Suspendida' COLLATE Latin1_general_CI_AI 
                                                        )
                                                        SELECT 
                                                            IdRegistroTableroI,
                                                            IdRHecho,
                                                            IdRAtencion,
                                                            NUC,
                                                            Ultimo_Registro,
                                                            Modulo,
                                                            Agencia,
                                                            Direccion,
                                                            Distrito_Procedencia,
                                                            Distrito_Actual,
                                                            FechaReporte,
                                                            Fecha_del_Ultimo_Registro,
                                                            Dias_Inactiva
                                                        FROM CTE
                                                        WHERE RowNum = 1;";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@u_iddistrito", u_iddistrito));

                var compro = await _context.qComprobarModulo.FromSqlRaw(ListaCarpetas, filtrosBusqueda.ToArray()).ToListAsync();

                return Ok(compro.Select(a => new ListarModulosViewModel
                {
                    IdRegistroTableroI = a.IdRegistroTableroI,
                    IdRHecho = a.IdRHecho,
                    IdRAtencion = a.IdRAtencion,
                    NUC = a.NUC,
                    Ultimo_Registro = a.Ultimo_Registro,
                    Modulo = a.Modulo,
                    Agencia = a.Agencia,
                    Direccion = a.Direccion,
                    Distrito_Procedencia = a.Distrito_Procedencia,
                    Distrito_Actual = a.Distrito_Actual,
                    FechaReporte = a.FechaReporte,
                    Fecha_del_Ultimo_Registro = a.Fecha_del_Ultimo_Registro,
                    Dias_Inactiva = a.Dias_Inactiva

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
        // GET: api/RegistroTableroI/ListarI
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador")]
        [HttpGet("[action]/{u_iddistrito}/{u_iddsp}")]
        public async Task<IActionResult> ListarDEspecifico([FromRoute] Guid u_iddistrito, Guid u_iddsp)
        {
            try
            {
                String ListaCarpetas = @"WITH CTE AS (
                                                            SELECT 
                                                                RTI.IdRegistroTableroI,
                                                                RH.IdRHecho,
                                                                RA.IdRAtencion,
                                                                NUC.nucg AS NUC,
                                                                RTI.TipoRegistroTableroI AS Ultimo_Registro,
                                                                MS.Nombre AS Modulo,
                                                                AG.Nombre AS Agencia,
                                                                DSP.nombreSubDir AS Direccion,
                                                                RA.DistritoInicial AS Distrito_Procedencia,
                                                                DIS.Nombre AS Distrito_Actual,
                                                                RH.FechaReporte,
                                                                RTI.FechaRegistro AS Fecha_del_Ultimo_Registro,
                                                                DATEDIFF(day, RTI.FechaRegistro, GETDATE()) AS Dias_Inactiva,
                                                                ROW_NUMBER() OVER (PARTITION BY RH.IdRHecho ORDER BY RTI.FechaRegistro DESC) AS RowNum
                                                            FROM CAT_REGISTROTABLEROI AS RTI
                                                            LEFT JOIN CAT_RHECHO AS RH ON RH.IdRHecho = RTI.RHechoId
                                                            LEFT JOIN CAT_RATENCON AS RA ON RA.IdRAtencion = RH.RAtencionId
                                                            LEFT JOIN NUC AS NUC ON NUC.idNuc = RH.NucId
                                                            LEFT JOIN C_MODULOSERVICIO AS MS ON MS.IdModuloServicio = RH.ModuloServicioId
                                                            LEFT JOIN C_AGENCIA AS AG ON AG.IdAgencia = ms.AgenciaId
                                                            LEFT JOIN C_DSP AS DSP ON DSP.IdDSP = AG.DSPId
                                                            LEFT JOIN C_DISTRITO AS DIS ON DIS.IdDistrito = DSP.DistritoId
                                                            WHERE DIS.IdDistrito = @u_iddistrito AND DSP.IdDSP =@u_iddsp AND NUC.Etapanuc != 'Concluida' COLLATE Latin1_general_CI_AI AND NUC.Etapanuc != 'Suspendida' COLLATE Latin1_general_CI_AI 
                                                        )
                                                        SELECT 
                                                            IdRegistroTableroI,
                                                            IdRHecho,
                                                            IdRAtencion,
                                                            NUC,
                                                            Ultimo_Registro,
                                                            Modulo,
                                                            Agencia,
                                                            Direccion,
                                                            Distrito_Procedencia,
                                                            Distrito_Actual,
                                                            FechaReporte,
                                                            Fecha_del_Ultimo_Registro,
                                                            Dias_Inactiva
                                                        FROM CTE
                                                        WHERE RowNum = 1;";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@u_iddistrito", u_iddistrito));
                filtrosBusqueda.Add(new SqlParameter("@u_iddsp", u_iddsp));

                var compro = await _context.qComprobarModulo.FromSqlRaw(ListaCarpetas, filtrosBusqueda.ToArray()).ToListAsync();

                return Ok(compro.Select(a => new ListarModulosViewModel
                {
                    IdRegistroTableroI = a.IdRegistroTableroI,
                    IdRHecho = a.IdRHecho,
                    IdRAtencion = a.IdRAtencion,
                    NUC = a.NUC,
                    Ultimo_Registro = a.Ultimo_Registro,
                    Modulo = a.Modulo,
                    Agencia = a.Agencia,
                    Direccion = a.Direccion,
                    Distrito_Procedencia = a.Distrito_Procedencia,
                    Distrito_Actual = a.Distrito_Actual,
                    FechaReporte = a.FechaReporte,
                    Fecha_del_Ultimo_Registro = a.Fecha_del_Ultimo_Registro,
                    Dias_Inactiva = a.Dias_Inactiva

                }));
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }
        // POST: api/RegistroTableroI/Clonar
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registrostablero = await _context.RegistrosTableroI.Where(x => x.RHechoId == model.IdRHecho).ToListAsync();

            if (registrostablero == null)
            {
                return Ok();
            }

            try
            {
                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;

                using (var ctx = new DbContextSIIGPP(options))
                {
                    foreach (RegistroTableroI registrotActual in registrostablero)
                    {

                        var insertarRegistroTI = await ctx.RegistrosTableroI.FirstOrDefaultAsync(a => a.IdRegistroTableroI == registrotActual.IdRegistroTableroI);

                        if (insertarRegistroTI == null)
                        {
                            insertarRegistroTI = new RegistroTableroI();
                            ctx.RegistrosTableroI.Add(insertarRegistroTI);
                        }

                        insertarRegistroTI.IdRegistroTableroI = registrotActual.IdRegistroTableroI;
                        insertarRegistroTI.RHechoId = registrotActual.RHechoId;
                        insertarRegistroTI.TipoRegistroTableroI = registrotActual.TipoRegistroTableroI;
                        insertarRegistroTI.Distrito = registrotActual.Distrito;
                        insertarRegistroTI.DirSub = registrotActual.DirSub;
                        insertarRegistroTI.Agencia = registrotActual.Agencia;
                        insertarRegistroTI.ModuloServicioId = registrotActual.ModuloServicioId;
                        insertarRegistroTI.Modulo = registrotActual.Modulo;
                        insertarRegistroTI.UsuarioId = registrotActual.UsuarioId;
                        insertarRegistroTI.NombreUsuario = registrotActual.NombreUsuario;
                        insertarRegistroTI.FechaRegistro = registrotActual.FechaRegistro;

                        await ctx.SaveChangesAsync();
                    }
                    return Ok();
                }
            }

            catch (Exception ex)
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.0" });
                result.StatusCode = 402;
                return result; ;
            }
        }
    }
}
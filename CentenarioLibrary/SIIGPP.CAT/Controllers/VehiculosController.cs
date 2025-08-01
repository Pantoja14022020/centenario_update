using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.Vehiculos;
using SIIGPP.Datos;
using SIIGPP.CAT.Models.AdminInfo;
using SIIGPP.Entidades.M_Cat.VehiculoImplicito;
using SIIGPP.Entidades.M_Administracion;
using SIIGPP.Entidades.M_Cat.Oficios;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {

        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;

        public VehiculosController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: api/Vehiculos/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<VehiculoViewModel>> Listar([FromRoute] Guid RHechoId)
        {
            var vehiculo = await _context.Vehiculos
                .Where(a => a.RHechoId == RHechoId)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return vehiculo.Select(a => new VehiculoViewModel
            {
                IdVehiculo = a.IdVehiculo,
                RHechoId = a.RHechoId,
                Estado = a.Estado,
                EstadoRobado = a.EstadoRobado,
                Marca = a.Marca,
                Tipo = a.Tipo,
                Modelo = a.Modelo,
                Serie = a.Serie,
                Placas = a.Placas,
                Color = a.Color,
                Ano = a.Ano,
                Recuperado = a.Recuperado,
                Devuelto = a.Devuelto,
                SenasParticulares = a.SenasParticulares,
                FechaSys = a.FechaSys,
                FechaRegistro = a.FechaRegistro,
                Distrito = a.Distrito,
                Subproc = a.Subproc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                Modulo = a.Modulo,
                NoSerieMotor = a.NoSerieMotor,
                Recepcion = a.Recepcion,
                Lugar = a.Lugar,
                Municipio = a.Municipio,
                Modalidad = a.Modalidad,
                FechaRobo = a.FechaRobo,
                Propietario = a.Propietario,
                NombreDenunciante = a.NombreDenunciante,
                DomicilioDenunciante = a.DomicilioDenunciante,
                NumeroOficio = a.NumeroOficio,
                Lugardeposito = a.Lugardeposito,
                Estadov = a.Estadov,
                Ubicacion = a.Ubicacion,
                StatusVehiculo = a.StatusVehiculo,
                NsChasis = a.NsChasis,
                NsMotor = a.NsMotor,
                TipoServicio = a.TipoServicio
            });

        }


        // PUT: api/Vehiculos/Actualizar
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehiculo = await _context.Vehiculos.FirstOrDefaultAsync(a => a.IdVehiculo == model.IdVehiculo);

            if (vehiculo == null)
            {
                return NotFound();
            }

            vehiculo.Estado = model.Estado;
            vehiculo.EstadoRobado = model.EstadoRobado;
            vehiculo.Marca = model.Marca;
            vehiculo.Tipo = model.Tipo;
            vehiculo.Modelo = model.Modelo;
            vehiculo.Serie = model.Serie;
            vehiculo.Placas = model.Placas;
            vehiculo.Color = model.Color;
            vehiculo.Ano = model.Ano;
            vehiculo.Recuperado = model.Recuperado;
            vehiculo.Devuelto = model.Devuelto;
            vehiculo.SenasParticulares = model.SenasParticulares;
            vehiculo.NoSerieMotor = model.NoSerieMotor;
            vehiculo.Recepcion = model.Recepcion;
            vehiculo.Lugar = model.Lugar;
            vehiculo.Municipio = model.Municipio;
            vehiculo.Modalidad = model.Modalidad;
            vehiculo.FechaRobo = model.FechaRobo;
            vehiculo.Propietario = model.Propietario;
            vehiculo.NombreDenunciante = model.NombreDenunciante;
            vehiculo.DomicilioDenunciante = model.DomicilioDenunciante;
            vehiculo.Lugardeposito = model.Lugardeposito;
            vehiculo.Estadov = model.Estadov;
            vehiculo.TipoServicio = model.TipoServicio;
          

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

        // POST: api/Vehiculos/Crear
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Vehiculo vehiculo = new Vehiculo
            {
                RHechoId = model.RHechoId,
                Estado = model.Estado,
                EstadoRobado = model.EstadoRobado,
                Marca = model.Marca,
                Tipo = model.Tipo,
                Modelo = model.Modelo,
                Serie = model.Serie,
                Placas = model.Placas,
                Color = model.Color,
                Ano = model.Ano,
                Recuperado = model.Recuperado,
                Devuelto = model.Devuelto,
                SenasParticulares = model.SenasParticulares,
                FechaSys = System.DateTime.Now,
                FechaRegistro = model.FechaRegistro,
                Distrito = model.Distrito,
                Subproc = model.Subproc,
                Agencia = model.Agencia,
                Usuario = model.Usuario,
                Puesto = model.Puesto,
                Modulo = model.Modulo,
                NoSerieMotor = model.NoSerieMotor,
                Recepcion = model.Recepcion,
                Lugar = model.Lugar,
                Municipio = model.Municipio,
                Modalidad = model.Modalidad,
                FechaRobo = model.FechaRobo,
                Propietario = model.Propietario,
                NombreDenunciante = model.NombreDenunciante,
                DomicilioDenunciante = model.DomicilioDenunciante,
                NumeroOficio = model.NumeroOficio,
                Lugardeposito = model.Lugardeposito,
                Estadov = model.Estadov,
                Ubicacion = model.Ubicacion,
                StatusVehiculo = model.StatusVehiculo,
                NsChasis = model.NsChasis,
                NsMotor = model.NsMotor,
                TipoServicio = model.TipoServicio
            };

            _context.Vehiculos.Add(vehiculo);
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
        // POST: api/Vehiculos/CrearDevolucion
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearDevolucion([FromBody] CrearDevolucionVehiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid iddevolucionvehiculo;

            DevolucionVehiculo devolucion = new DevolucionVehiculo
            {
                VehiculoId = model.VehiculoId,
                NucRelacionadaForaneaTF  = model.NucRelacionadaForaneaTF,
                NucRelacionadaForanea = model.NucRelacionadaForanea,
                NoOficio = model.NoOficio, 
                DevueltoA = model.DevueltoA,
                CalidadDevueltoA = model.CalidadDevueltoA,
                TipoDevolucion = model.TipoDevolucion,
                NombreCorralon = model.NombreCorralon,
                UbicacionCorralon = model.UbicacionCorralon,
                FechaIngresoCorralon = model.FechaIngresoCorralon,
                DirigidoA = model.DirigidoA,
                Ccp = model.Ccp,
                Usuario = model.Usuario,
                Puesto = model.Puesto,
                Agencia = model.Agencia,
                Subproc = model.Subproc,
                Distrito = model.Distrito,
                UsuarioId = model.UsuarioId,
                FirmasVoBoTF = model.FirmasVoBoTF,
                FirmasVoBo = model.FirmasVoBo,
                TextoDevolucion = model.TextoDevolucion,
                Fechasys = System.DateTime.Now,
            };

            _context.DevolucionVehiculos.Add(devolucion);
            try
            {
                await _context.SaveChangesAsync();
                iddevolucionvehiculo = devolucion.IdDevolucionVehiculo;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new {iddevolucionvehiculo = iddevolucionvehiculo});
        }

        // GET: api/Vehiculos/Listar
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{idVehiculo}")]
        public async Task<IEnumerable<DevolucionVehiculoViewModel>> ListarDevolucion([FromRoute] Guid idVehiculo)
        {
            var Devolucionvehiculo = await _context.DevolucionVehiculos
                .Where(a => a.VehiculoId == idVehiculo)
                .OrderByDescending(a => a.Fechasys)
                .ToListAsync();

            return Devolucionvehiculo.Select(a => new DevolucionVehiculoViewModel
            {
                IdDevolucionVehiculo = a.IdDevolucionVehiculo,
                VehiculoId = a.VehiculoId,
                NucRelacionadaForaneaTF = a.NucRelacionadaForaneaTF,
                NucRelacionadaForanea = a.NucRelacionadaForanea,
                NoOficio = a.NoOficio,
                DevueltoA = a.DevueltoA,
                CalidadDevueltoA = a.CalidadDevueltoA,
                TipoDevolucion = a.TipoDevolucion,
                NombreCorralon = a.NombreCorralon,
                UbicacionCorralon = a.UbicacionCorralon,
                FechaIngresoCorralon = a.FechaIngresoCorralon,
                DirigidoA = a.DirigidoA,
                Ccp = a.Ccp,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                Agencia = a.Agencia,
                Subproc = a.Subproc,
                Distrito = a.Distrito,
                UsuarioId = a.UsuarioId,
                FirmasVoBoTF = a.FirmasVoBoTF,
                FirmasVoBo = a.FirmasVoBo,
                TextoDevolucion = a.TextoDevolucion,
                Fechasys = a.Fechasys,
            });
        }

        // PUT: api/Vehiculos/Actualizar
        //[Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpPut("[action]")]
        public async Task<IActionResult> EditarDevolucion([FromBody] EditarDevolucionVehiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var vehiculoDevolucion = await _context.DevolucionVehiculos.FirstOrDefaultAsync(a => a.IdDevolucionVehiculo == model.IdDevolucionVehiculo);

            if (vehiculoDevolucion == null)
            {
                return NotFound();
            }

            vehiculoDevolucion.NucRelacionadaForaneaTF = model.NucRelacionadaForaneaTF;
            vehiculoDevolucion.NucRelacionadaForanea = model.NucRelacionadaForanea;
            vehiculoDevolucion.NoOficio = model.NoOficio;
            vehiculoDevolucion.DevueltoA = model.DevueltoA;
            vehiculoDevolucion.CalidadDevueltoA = model.CalidadDevueltoA;
            vehiculoDevolucion.TipoDevolucion = model.TipoDevolucion;
            vehiculoDevolucion.NombreCorralon = model.NombreCorralon;
            vehiculoDevolucion.UbicacionCorralon = model.UbicacionCorralon;
            vehiculoDevolucion.FechaIngresoCorralon = model.FechaIngresoCorralon;
            vehiculoDevolucion.DirigidoA = model.DirigidoA;
            vehiculoDevolucion.Ccp = model.Ccp;
            vehiculoDevolucion.FirmasVoBoTF = model.FirmasVoBoTF;
            vehiculoDevolucion.FirmasVoBo = model.FirmasVoBo;
            vehiculoDevolucion.TextoDevolucion = model.TextoDevolucion;


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

        // GET: api/Vehiculos/ListarEstadisticas
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{iddistrito}/{iddsp}/{año}/{mes}/{indicadorano}/{indicadormes}/{estadov}/{estadorobado}/{decomisado}/{devuelto}/{lugardeposito}/{statusvehiculo}/{marca}/{tipo}/{modelo}/{serie}/{placas}/{color}/{ano}/{seriemotor}/{recepcion}/{ubicacion}/{senasparticulares}/{distritoactivo}/{dspactivo}")]
        public async Task<IEnumerable<EstadisticasViewModel>> ListarEstadisticas([FromRoute]Guid iddistrito, Guid iddsp, DateTime año,DateTime mes, Boolean indicadorano, Boolean indicadormes, string estadov,string estadorobado, string decomisado, string devuelto,string lugardeposito,string statusvehiculo,string marca,string tipo, string modelo,string serie,string placas,string color,string ano,string seriemotor,string recepcion,string ubicacion, string senasparticulares, Boolean distritoactivo, Boolean dspactivo)
        {
            var vehiculo = await _context.Vehiculos
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.ModuloServicio)
                .Include(a => a.RHecho.Agencia.DSP)
                .Where(a => distritoactivo ? a.RHecho.Agencia.DSP.DistritoId == iddistrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.Agencia.DSPId == iddsp : 1 == 1)
                .Where(a => !indicadorano ? a.FechaSys.Year == año.Year : 1 == 1)
                .Where(a => !indicadormes ? a.FechaSys.Month == mes.Month : 1 == 1)
                .Where(a => estadov != "ZKR" ? a.Estadov == estadov : 1 == 1 )

                .Where(a => estadov == "Robado" && estadorobado != "ZKR" ? (estadorobado == "true" ?  a.EstadoRobado == true  :  a.EstadoRobado == false) : 1 == 1 )
                .Where(a => estadov == "Robado" && decomisado != "ZKR" ? (decomisado == "true" ?  a.Recuperado == true:  a.Recuperado == false ) : 1 == 1)
                .Where(a => estadov == "Robado" && decomisado == "true"  ? (devuelto == "true" ?  a.Devuelto == true: a.Devuelto == false) : 1 == 1)
                .Where(a => estadov == "Robado" && decomisado == "true" && !(devuelto == "true") && lugardeposito != "ZKR" ?  lugardeposito == a.Lugardeposito : 1 == 1)
                .Where(a => estadov == "Robado" && decomisado == "true" && devuelto == "true" && statusvehiculo != "ZKR" ? statusvehiculo == "true" ? a.StatusVehiculo == true : a.StatusVehiculo == false : 1 == 1)

                .Where(a => marca != "ZKR" ? marca == a.Marca : 1 == 1)
                .Where(a => tipo != "ZKR" ? tipo == a.Tipo : 1 == 1)
                .Where(a => (marca != "ZKR") &&(modelo != "ZKR") ? modelo == a.Modelo : 1 == 1)
                .Where(a => serie != "ZKR" ? serie == a.Serie : 1 == 1) 
                .Where(a => placas != "ZKR" ? placas == a.Placas : 1 == 1)
                .Where(a => color != "ZKR" ? color == a.Color : 1 == 1)
                .Where(a => ano != "ZKR"  ? ano == a.Ano : 1 == 1)
                .Where(a => seriemotor != "ZKR" ? seriemotor == a.NoSerieMotor : 1 == 1)
                .Where(a => recepcion != "ZKR" ? recepcion == a.Recepcion : 1 == 1)
                .Where(a => ubicacion != "ZKR" ? a.Ubicacion.Contains(ubicacion) : 1 == 1)
                .Where(a => senasparticulares != "ZKR" ? a.SenasParticulares.Contains(senasparticulares) : 1 == 1)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return vehiculo.Select(a => new EstadisticasViewModel
            {
                NUC = a.RHecho.NUCs.nucg,
                Agencia = a.RHecho.Agencia.Nombre,
                Modulo = a.RHecho.ModuloServicio.Nombre,
                DSP = a.RHecho.Agencia.DSP.NombreSubDir
            });

        }

        // GET: api/Vehiculos/ListarEstadisticasModulo
        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{distrito}/{dsp}/{agencia}/{distritoactivo}/{dspactivo}/{agenciaactivo}/{fechai}/{fechaf}/{estadov}/{estadorobado}/{decomisado}/{devuelto}/{lugardeposito}/{statusvehiculo}/{marca}/{tipo}/{modelo}/{serie}/{placas}/{color}/{ano}/{seriemotor}/{recepcion}/{ubicacion}/{senasparticulares}")]
        public async Task<IEnumerable<EstadisticasViewModel>> ListarEstadisticasModulo([FromRoute] Guid distrito, Guid dsp, Guid agencia, Boolean distritoactivo, Boolean dspactivo, Boolean agenciaactivo, DateTime fechai, DateTime fechaf, string estadov, string estadorobado, string decomisado, string devuelto, string lugardeposito, string statusvehiculo, string marca, string tipo, string modelo, string serie, string placas, string color, string ano, string seriemotor, string recepcion, string ubicacion, string senasparticulares)
        {
            var vehiculo = await _context.Vehiculos
                .Include(a => a.RHecho.NUCs)
                .Include(a => a.RHecho.ModuloServicio)
                .Include(a => a.RHecho.Agencia.DSP)
                .Where(a => a.RHecho.FechaHoraSuceso2 >= fechai)
                .Where(a => a.RHecho.FechaHoraSuceso2 <= fechaf)
                .Where(a => distritoactivo ? a.RHecho.ModuloServicio.Agencia.DSP.DistritoId == distrito : 1 == 1)
                .Where(a => dspactivo ? a.RHecho.ModuloServicio.Agencia.DSPId == dsp : 1 == 1)
                .Where(a => agenciaactivo ? a.RHecho.ModuloServicio.AgenciaId == agencia : 1 == 1)
                .Where(a => estadov != "ZKR" ? a.Estadov == estadov : 1 == 1)

                .Where(a => estadov == "Robado" && estadorobado != "ZKR" ? (estadorobado == "true" ? a.EstadoRobado == true : a.EstadoRobado == false) : 1 == 1)
                .Where(a => estadov == "Robado" && decomisado != "ZKR" ? (decomisado == "true" ? a.Recuperado == true : a.Recuperado == false) : 1 == 1)
                .Where(a => estadov == "Robado" && decomisado == "true" ? (devuelto == "true" ? a.Devuelto == true : a.Devuelto == false) : 1 == 1)
                .Where(a => estadov == "Robado" && decomisado == "true" && !(devuelto == "true") && lugardeposito != "ZKR" ? lugardeposito == a.Lugardeposito : 1 == 1)
                .Where(a => estadov == "Robado" && decomisado == "true" && devuelto == "true" && statusvehiculo != "ZKR" ? statusvehiculo == "true" ? a.StatusVehiculo == true : a.StatusVehiculo == false : 1 == 1)

                .Where(a => marca != "ZKR" ? marca == a.Marca : 1 == 1)
                .Where(a => tipo != "ZKR" ? tipo == a.Tipo : 1 == 1)
                .Where(a => (marca != "ZKR") && (modelo != "ZKR") ? modelo == a.Modelo : 1 == 1)
                .Where(a => serie != "ZKR" ? serie == a.Serie : 1 == 1)
                .Where(a => placas != "ZKR" ? placas == a.Placas : 1 == 1)
                .Where(a => color != "ZKR" ? color == a.Color : 1 == 1)
                .Where(a => ano != "ZKR" ? ano == a.Ano : 1 == 1)
                .Where(a => seriemotor != "ZKR" ? seriemotor == a.NoSerieMotor : 1 == 1)
                .Where(a => recepcion != "ZKR" ? recepcion == a.Recepcion : 1 == 1)
                .Where(a => ubicacion != "ZKR" ? a.Ubicacion.Contains(ubicacion) : 1 == 1)
                .Where(a => senasparticulares != "ZKR" ? a.SenasParticulares.Contains(senasparticulares) : 1 == 1)
                .OrderByDescending(a => a.FechaSys)
                .ToListAsync();

            return vehiculo.Select(a => new EstadisticasViewModel
            {
                NUC = a.RHecho.NUCs.nucg,
                Agencia = a.RHecho.Agencia.Nombre,
                Modulo = a.RHecho.ModuloServicio.Nombre,
                DSP = a.RHecho.Agencia.DSP.NombreSubDir
            });

        }
        // GET: api/RActosInvestigacion/Eliminar
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
                var consultaVehiculo = await _context.Vehiculos.Where(a => a.IdVehiculo == model.infoBorrado.registroId)
                                          .Take(1).FirstOrDefaultAsync();
                if (consultaVehiculo == null)
                {
                    return Ok(new { res = "Error", men = "No se encontró ningún acto de investigación con la información enviada" });
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
                            MovimientoId = new Guid("931dee42-4cd6-4b97-afff-0367babcdc8a") // ESTE DATO HAY QUE BUSCARLO EN LA TABLA C_TIPO_MOV DE LA BD CENTENARIO_LOG
                        };

                        ctx.Add(laRegistro);
                        LogVehiculo vehiculo = new LogVehiculo
                        {
                            LogAdmonId = gLog,
                            IdVehiculo = consultaVehiculo.IdVehiculo,
                            RHechoId = consultaVehiculo.RHechoId,
                            Estado = consultaVehiculo.Estado,
                            EstadoRobado = consultaVehiculo.EstadoRobado,
                            Marca = consultaVehiculo.Marca,
                            Tipo = consultaVehiculo.Tipo,
                            Modelo = consultaVehiculo.Modelo,
                            Serie = consultaVehiculo.Serie,
                            Placas = consultaVehiculo.Placas,
                            Color = consultaVehiculo.Color,
                            Ano = consultaVehiculo.Ano,
                            Recuperado = consultaVehiculo.Recuperado,
                            Devuelto = consultaVehiculo.Devuelto,
                            SenasParticulares = consultaVehiculo.SenasParticulares,
                            FechaSys = consultaVehiculo.FechaSys,
                            FechaRegistro = consultaVehiculo.FechaRegistro,
                            Distrito = consultaVehiculo.Distrito,
                            Subproc = consultaVehiculo.Subproc,
                            Agencia = consultaVehiculo.Agencia,
                            Usuario = consultaVehiculo.Usuario,
                            Puesto = consultaVehiculo.Puesto,
                            Modulo = consultaVehiculo.Modulo,
                            NoSerieMotor = consultaVehiculo.NoSerieMotor,
                            Recepcion = consultaVehiculo.Recepcion,
                            Lugar = consultaVehiculo.Lugar,
                            Municipio = consultaVehiculo.Municipio,
                            Modalidad = consultaVehiculo.Modalidad,
                            FechaRobo = consultaVehiculo.FechaRobo,
                            Propietario = consultaVehiculo.Propietario,
                            NombreDenunciante = consultaVehiculo.NombreDenunciante,
                            DomicilioDenunciante = consultaVehiculo.DomicilioDenunciante,
                            NumeroOficio = consultaVehiculo.NumeroOficio,
                            Lugardeposito = consultaVehiculo.Lugardeposito,
                            Estadov = consultaVehiculo.Estadov,
                            Ubicacion = consultaVehiculo.Ubicacion,
                            StatusVehiculo = consultaVehiculo.StatusVehiculo,
                            NsChasis = consultaVehiculo.NsChasis,
                            NsMotor = consultaVehiculo.NsMotor,
                            TipoServicio = consultaVehiculo.TipoServicio
                        };
                        ctx.Add(vehiculo);
                        _context.Remove(consultaVehiculo);

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
            return Ok(new { res = "success", men = "Sábana de Vehículo eliminada Correctamente" });
        }


        [Authorize(Roles = "Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción,AMPO-IL")]
        [HttpPost("[action]")]
        // POST: api/Vehiculos/Clonar
        public async Task<IActionResult> Clonar([FromBody] Models.Rac.ClonarViewModel model)
        {

           
            var listaVehiculo = await _context.Vehiculos.Where(x => x.RHechoId == model.IdRHecho).ToListAsync();

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (listaVehiculo == null)
                {
                    return Ok();

                }

                var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(options))
                {


                    foreach (Vehiculo vehiculoActual in listaVehiculo)
                    {

                        var insertarVehiculo = await ctx.Vehiculos.FirstOrDefaultAsync(a => a.IdVehiculo == vehiculoActual.IdVehiculo);

                        if (insertarVehiculo == null)
                        {
                            insertarVehiculo = new Vehiculo();
                            ctx.Vehiculos.Add(insertarVehiculo);
                        }


                        insertarVehiculo.IdVehiculo = vehiculoActual.IdVehiculo;
                        insertarVehiculo.RHechoId = vehiculoActual.RHechoId;
                        insertarVehiculo.Estado = vehiculoActual.Estado;
                        insertarVehiculo.EstadoRobado = vehiculoActual.EstadoRobado;
                        insertarVehiculo.Marca = vehiculoActual.Marca;
                        insertarVehiculo.Tipo = vehiculoActual.Tipo;
                        insertarVehiculo.Modelo = vehiculoActual.Modelo;
                        insertarVehiculo.Serie = vehiculoActual.Serie;
                        insertarVehiculo.Placas = vehiculoActual.Placas;
                        insertarVehiculo.Color = vehiculoActual.Color;
                        insertarVehiculo.Ano = vehiculoActual.Ano;
                        insertarVehiculo.Recuperado = vehiculoActual.Recuperado;
                        insertarVehiculo.Devuelto = vehiculoActual.Devuelto;
                        insertarVehiculo.SenasParticulares = vehiculoActual.SenasParticulares;
                        insertarVehiculo.FechaSys = vehiculoActual.FechaSys;
                        insertarVehiculo.FechaRegistro = vehiculoActual.FechaRegistro;
                        insertarVehiculo.Distrito = vehiculoActual.Distrito;
                        insertarVehiculo.Subproc = vehiculoActual.Subproc;
                        insertarVehiculo.Agencia = vehiculoActual.Agencia;
                        insertarVehiculo.Usuario = vehiculoActual.Usuario;
                        insertarVehiculo.Puesto = vehiculoActual.Puesto;
                        insertarVehiculo.Modulo = vehiculoActual.Modulo;
                        insertarVehiculo.NoSerieMotor = vehiculoActual.NoSerieMotor;
                        insertarVehiculo.Recepcion = vehiculoActual.Recepcion;
                        insertarVehiculo.Lugar = vehiculoActual.Lugar;
                        insertarVehiculo.Municipio = vehiculoActual.Municipio;
                        insertarVehiculo.Modalidad = vehiculoActual.Modalidad;
                        insertarVehiculo.FechaRobo = vehiculoActual.FechaRobo;
                        insertarVehiculo.Propietario = vehiculoActual.Propietario;
                        insertarVehiculo.NombreDenunciante = vehiculoActual.NombreDenunciante;
                        insertarVehiculo.DomicilioDenunciante = vehiculoActual.DomicilioDenunciante;
                        insertarVehiculo.NumeroOficio = vehiculoActual.NumeroOficio;
                        insertarVehiculo.Lugardeposito = vehiculoActual.Lugardeposito;
                        insertarVehiculo.Estadov = vehiculoActual.Estadov;
                        insertarVehiculo.NsChasis = vehiculoActual.NsChasis;
                        insertarVehiculo.NsMotor = vehiculoActual.NsMotor;
                        insertarVehiculo.StatusVehiculo = vehiculoActual.StatusVehiculo;
                        insertarVehiculo.Ubicacion = vehiculoActual.Ubicacion;
                        insertarVehiculo.TipoServicio = vehiculoActual.TipoServicio;

                        var consultaVehiculoDevolver = await _context.DevolucionVehiculos.Where(a => a.VehiculoId == vehiculoActual.IdVehiculo).ToListAsync();

                        if (consultaVehiculoDevolver != null)
                        {
                            foreach (DevolucionVehiculo vehiculoActualDevolver in consultaVehiculoDevolver)
                            {

                                var insertarVehiculoDevolver = await ctx.DevolucionVehiculos.FirstOrDefaultAsync(a => a.IdDevolucionVehiculo == vehiculoActualDevolver.IdDevolucionVehiculo);

                                if (insertarVehiculoDevolver == null)
                                {
                                    insertarVehiculoDevolver = new DevolucionVehiculo();
                                    ctx.DevolucionVehiculos.Add(insertarVehiculoDevolver);
                                }

                                insertarVehiculoDevolver.IdDevolucionVehiculo = vehiculoActualDevolver.IdDevolucionVehiculo;
                                insertarVehiculoDevolver.VehiculoId = vehiculoActualDevolver.VehiculoId;
                                insertarVehiculoDevolver.NucRelacionadaForaneaTF = vehiculoActualDevolver.NucRelacionadaForaneaTF;
                                insertarVehiculoDevolver.NucRelacionadaForanea = vehiculoActualDevolver.NucRelacionadaForanea;
                                insertarVehiculoDevolver.NoOficio = vehiculoActualDevolver.NoOficio;
                                insertarVehiculoDevolver.DevueltoA = vehiculoActualDevolver.DevueltoA;
                                insertarVehiculoDevolver.CalidadDevueltoA = vehiculoActualDevolver.CalidadDevueltoA;
                                insertarVehiculoDevolver.TipoDevolucion = vehiculoActualDevolver.TipoDevolucion;
                                insertarVehiculoDevolver.NombreCorralon = vehiculoActualDevolver.NombreCorralon;
                                insertarVehiculoDevolver.UbicacionCorralon = vehiculoActualDevolver.UbicacionCorralon;
                                insertarVehiculoDevolver.FechaIngresoCorralon = vehiculoActualDevolver.FechaIngresoCorralon;
                                insertarVehiculoDevolver.DirigidoA = vehiculoActualDevolver.DirigidoA;
                                insertarVehiculoDevolver.Ccp = vehiculoActualDevolver.Ccp;
                                insertarVehiculoDevolver.Usuario = vehiculoActualDevolver.Usuario;
                                insertarVehiculoDevolver.Puesto = vehiculoActualDevolver.Puesto;
                                insertarVehiculoDevolver.Agencia = vehiculoActualDevolver.Agencia;
                                insertarVehiculoDevolver.Subproc = vehiculoActualDevolver.Subproc;
                                insertarVehiculoDevolver.Distrito = vehiculoActualDevolver.Distrito;
                                insertarVehiculoDevolver.UsuarioId = vehiculoActualDevolver.UsuarioId;
                                insertarVehiculoDevolver.FirmasVoBoTF = vehiculoActualDevolver.FirmasVoBoTF;
                                insertarVehiculoDevolver.FirmasVoBo = vehiculoActualDevolver.FirmasVoBo;
                                insertarVehiculoDevolver.TextoDevolucion = vehiculoActualDevolver.TextoDevolucion;
                                insertarVehiculoDevolver.Fechasys = vehiculoActualDevolver.Fechasys;

                            }

                        }

                        await ctx.SaveChangesAsync();


                    }
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

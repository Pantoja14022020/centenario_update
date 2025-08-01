using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.CAT.Models.MedidasCautelares;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.MedCautelares;
using SIIGPP.CAT.FilterClass;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidasCautelaresController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public MedidasCautelaresController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/MedidasCautelares
        [HttpGet]
        public IEnumerable<MedidasCautelares> GetMedidasCautelares()
        {
            return _context.MedidasCautelares;
        }


        // GET: api/MedidasCautelares/ListarPorRHecho{RHechoId}
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpGet("[action]/{RHechoId}")]
        public async Task<IEnumerable<MedidasCautelaresViewModel>> ListarPorRhecho([FromRoute] Guid RHechoId)
        {
            var mc = await _context.MedidasCautelares
                          .Include(a => a.Persona)
                          .Include(a => a.Persona.RAPs)
                          .Where(a => a.RHechoId == RHechoId).ToListAsync();

            return mc.Select(a => new MedidasCautelaresViewModel
            {
                IdMedCautelares = a.IdMedCautelares,
                PersonaId = a.PersonaId,
                NombreImputado = a.Persona.Nombre + " " + a.Persona.ApellidoPaterno + " " + a.Persona.ApellidoMaterno,
                RHechoId = a.RHechoId,
                MedidaCautelar = a.MedidaCautelar,
                Tiempo = a.Tiempo,
                FechaInicio = a.FechaInicio,
                FechaTermino = a.FechaTermino,
                FechaSys = a.FechaSys,
                Distrito = a.Distrito,
                DirSubProc = a.DirSubProc,
                Agencia = a.Agencia,
                Usuario = a.Usuario,
                Puesto = a.Puesto,
                 


            });

        }

        // PUT: api/MedidasCautelares/Actualizar
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

      

            var mc = await _context.MedidasCautelares.FirstOrDefaultAsync(a => a.IdMedCautelares == model.IdMedCautelares);

            if (mc == null)
            {
                return NotFound();
            }


            mc.IdMedCautelares = model.IdMedCautelares;
            mc.PersonaId = model.PersonaId;
            mc.RHechoId = model.RHechoId;
            mc.MedidaCautelar = model.MedidaCautelar;
            mc.Tiempo = model.Tiempo;
            mc.FechaInicio = model.FechaInicio;
            mc.FechaTermino = model.FechaTermino; 
            mc.Distrito = model.Distrito;
            mc.DirSubProc = model.DirSubProc;
            mc.Agencia = model.Agencia;
            mc.Usuario = model.Usuario;
            mc.Puesto = model.Puesto;

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

        // POST: api/MedidasCautelares/Crear
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,AMPO-IL,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;
            MedidasCautelares mc = new MedidasCautelares
            {
                PersonaId = model.PersonaId,
                RHechoId = model.RHechoId, 
                MedidaCautelar = model.MedidaCautelar, 
                Tiempo = model.Tiempo,
                FechaInicio  = model.FechaInicio,
                FechaTermino = model.FechaTermino,
                FechaSys  = fecha,
                Distrito  = model.Distrito,
                DirSubProc = model.DirSubProc,
                Agencia  = model.Agencia,
                Usuario  = model.Usuario,
                Puesto  = model.Puesto,

            };

            _context.MedidasCautelares.Add(mc);
            try
            {
                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                var result = new ObjectResult(new { statusCode = "402", mensaje = ex.InnerException.Message, detail = ex.Message, version = "version 1.2" });
                result.StatusCode = 402;
                return result;
            }

            return Ok(new { idmedidacautelar = mc.IdMedCautelares });
        }

        // POST: api/MedidasCautelares/CrearNoMedidas
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearNoMedidas([FromBody] CrearNoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NoMedidasCautelares mc = new NoMedidasCautelares
            {
                Clave = model.Clave,
                Descripcion = model.Descripcion,
                MedidasCautelaresId = model.MedidasCautelaresId

            };

            _context.NoMedidasCautelares.Add(mc);
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


        // GET: api/MedidasCautelares/ListarPorIdMedida
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{MedidaId}")]
        public async Task<IEnumerable<NoMedidasCautelaresViewModel>> ListarPorIdMedida([FromRoute] Guid MedidaId)
        {
            var mc = await _context.NoMedidasCautelares
                          .Where(a => a.MedidasCautelaresId == MedidaId)
                          .ToListAsync();

            return mc.Select(a => new NoMedidasCautelaresViewModel
            {   
                Clave = a.Clave,
                Descripcion = a.Descripcion,
                IdNoMedidasCautelares = a.IdNoMedidasCautelares,
                MedidasCautelaresId = a.MedidasCautelaresId

            });

        }



        // DELETE: api/MedidasCautelares/Eliminar
        [Authorize(Roles = " Administrador")]
        [HttpDelete("[action]/{medidacautelarId}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid medidacautelarId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var rpu = await _context.NoMedidasCautelares
                           .Where(a => a.MedidasCautelaresId == medidacautelarId)
                           .ToListAsync();



            rpu.Select(u => new NoMedidasCautelaresViewModel
            {
                IdNoMedidasCautelares = u.IdNoMedidasCautelares,
                Clave = u.Clave,
                Descripcion = u.Descripcion,
                MedidasCautelaresId = u.MedidasCautelaresId
            });

            foreach (var encuentra in rpu)
            {
                var up = await _context.NoMedidasCautelares.FindAsync(encuentra.IdNoMedidasCautelares);

                if (up == null)
                {
                    return NotFound();
                }

                _context.NoMedidasCautelares.Remove(up);

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

            }

            return Ok();
        }

        // GET: api/MedidasCautelares/TotalEstadisticaImputadosConSinMe
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{MedidaCautelar}")]
        public async Task<IEnumerable<EstadisticaMedidasCViewModel>> TotalEstadisticaImputadosConSinMe([FromQuery] TotalImputadosMedidasCautelares MedidaCautelar)
        {

            var rhechos = await _context.RHechoes
                    .Where(a => a.NucId != null)
                    .Where(a => MedidaCautelar.DatosGenerales.Distritoact ? a.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == MedidaCautelar.DatosGenerales.Distrito : 1 == 1)
                    .Where(a => MedidaCautelar.DatosGenerales.Dspact ? a.ModuloServicio.Agencia.DSP.IdDSP == MedidaCautelar.DatosGenerales.Dsp : 1 == 1)
                    .Where(a => MedidaCautelar.DatosGenerales.Agenciaact ? a.ModuloServicio.Agencia.IdAgencia == MedidaCautelar.DatosGenerales.Agencia : 1 == 1)
                    .Where(a => a.FechaElevaNuc2 >= MedidaCautelar.DatosGenerales.Fechadesde)
                    .Where(a => a.FechaElevaNuc2 <= MedidaCautelar.DatosGenerales.Fechahasta)
                    .ToListAsync();

            IEnumerable<CSImputadoEstadisticaViewModel> itemsr = new CSImputadoEstadisticaViewModel[] { };

            var hechos = rhechos.Select(a => new CSImputadoEstadisticaViewModel
            {
               RhechoId = a.IdRHecho,
               Ratencion = a.RAtencionId
            });


            foreach(var rhecho in hechos)
            {
                var personas = await _context.RAPs
                    .Where(a => a.ClasificacionPersona == "Imputado")
                    .Where(a => a.RAtencionId == rhecho.Ratencion)
                    .ToListAsync();

                foreach(var persona in personas)
                {
                    IEnumerable<CSImputadoEstadisticaViewModel> ReadLines2()
                    {
                        IEnumerable<CSImputadoEstadisticaViewModel> item2;

                        item2 = (new[]{new CSImputadoEstadisticaViewModel{
                                RhechoId = rhecho.RhechoId,
                                Ratencion = rhecho.Ratencion,
                                PersonaId = persona.PersonaId
                            }});

                        return item2;
                    }

                    itemsr = itemsr.Concat(ReadLines2());

                }


            }

            IEnumerable<CSImputadoEstadisticaViewModel> itemsr2 = new CSImputadoEstadisticaViewModel[] { };

            foreach (var itemr in itemsr)
            {
                var medidacautelar = await _context.MedidasCautelares
                    .Where(a => a.PersonaId == itemr.PersonaId)
                    .ToListAsync();
                    
                if(medidacautelar.Count > 0)
                {

                    foreach (var medida in medidacautelar)
                    {

                        IEnumerable<CSImputadoEstadisticaViewModel> ReadLines2()
                        {
                            IEnumerable<CSImputadoEstadisticaViewModel> item2;

                            item2 = (new[]{new CSImputadoEstadisticaViewModel{
                                RhechoId = itemr.RhechoId,
                                Ratencion = itemr.Ratencion,
                                PersonaId = itemr.PersonaId,
                                ConMedida =true,
                                MedidaCautelar = medida.MedidaCautelar == "I-La presentación periódica ante el juez o ante autoridad distinta que aquél designe" ||
                                medida.MedidaCautelar == "II-La exhibición de una garantía económica" ||
                                medida.MedidaCautelar == "III-El embargo de bienes" ||
                                medida.MedidaCautelar == "IV-La inmovilización de cuentas y demás valores que se encuentren dentro del sistema financiero" ||
                                medida.MedidaCautelar == "V-La prohibición de salir sin autorización del país, de la localidad en la cual reside o del ámbito territorial que fije el juez" ||
                                medida.MedidaCautelar == "VI-El sometimiento al cuidado o vigilancia de una persona o institución determinada o internamiento a institución determinada" ||
                                medida.MedidaCautelar == "VII-La prohibición de concurrir a determinadas reuniones o acercarse o ciertos lugares" ||
                                medida.MedidaCautelar == "VIII-La prohibición de convivir, acercarse o comunicarse con determinadas personas, con las víctimas u ofendidos o testigos, siempre que no se afecte el derecho de defensa" ||
                                medida.MedidaCautelar == "IX-La separación inmediata del domicilio" ||
                                medida.MedidaCautelar == "X-La suspensión temporal en el ejercicio del cargo cuando se le atribuye un delito cometido por servidores públicos" ||
                                medida.MedidaCautelar == "XI-La suspensión temporal en el ejercicio de una determinada actividad profesional o laboral" ||
                                medida.MedidaCautelar == "XII-La colocación de localizadores electrónicos" ||
                                medida.MedidaCautelar == "XIII-El resguardo en su propio domicilio con las modalidades que el juez disponga" ||
                                medida.MedidaCautelar == "INEGI-La prisión preventiva oficiosa" ||
                                medida.MedidaCautelar == "INEGI-La prisión preventiva justificada" ?
                                medida.MedidaCautelar: "Otra" ,
                            }});

                            return item2;
                        }

                        itemsr2 = itemsr2.Concat(ReadLines2());

                    }

                }
                else
                {

                    IEnumerable<CSImputadoEstadisticaViewModel> ReadLines2()
                    {
                        IEnumerable<CSImputadoEstadisticaViewModel> item2;

                        item2 = (new[]{new CSImputadoEstadisticaViewModel{
                                RhechoId = itemr.RhechoId,
                                Ratencion = itemr.Ratencion,
                                PersonaId = itemr.PersonaId,
                                ConMedida = false,
                                MedidaCautelar = "Sin medida"
                            }});

                        return item2;
                    }

                    itemsr2 = itemsr2.Concat(ReadLines2());

                }

            }

            IEnumerable<EstadisticaMedidasCViewModel> itemsr3 = new EstadisticaMedidasCViewModel[] { };

            itemsr3 =  itemsr2.GroupBy(v => v.MedidaCautelar)
                .Select(x => new EstadisticaMedidasCViewModel
                {
                    Tipo = x.Key,
                    Cantidad = x.Count()
                })
                .ToList();

            return itemsr3;
        }

        // GET: api/MedidasCautelares/TotalEstadistica
        [Authorize(Roles = " Administrador,AMPO-AMP,Director,Coordinador,AMPO-AMP Mixto, AMPO-AMP Detenido,Recepción")]
        [HttpGet("[action]/{MedidaCautelar}")]
        public async Task<IEnumerable<EstadisticaMedidasCViewModel>> TotalEstadistica([FromQuery] TotalImputadosMedidasCautelares MedidaCautelar)
        {
            var mc = await _context.MedidasCautelares
                            .Where(a => MedidaCautelar.DatosGenerales.Distritoact ? a.RHecho.ModuloServicio.Agencia.DSP.Distrito.IdDistrito == MedidaCautelar.DatosGenerales.Distrito : 1 == 1)
                            .Where(a => MedidaCautelar.DatosGenerales.Dspact ? a.RHecho.ModuloServicio.Agencia.DSP.IdDSP == MedidaCautelar.DatosGenerales.Dsp : 1 == 1)
                            .Where(a => MedidaCautelar.DatosGenerales.Agenciaact ? a.RHecho.ModuloServicio.Agencia.IdAgencia == MedidaCautelar.DatosGenerales.Agencia : 1 == 1)
                            .Where(a => a.RHecho.FechaElevaNuc2 >= MedidaCautelar.DatosGenerales.Fechadesde)
                            .Where(a => a.RHecho.FechaElevaNuc2 <= MedidaCautelar.DatosGenerales.Fechahasta)
                            .Where(a => MedidaCautelar.Medida != "null" && MedidaCautelar.Medida != "Otra" ? a.MedidaCautelar == MedidaCautelar.Medida : MedidaCautelar.Medida == "Otra" ?
                            a.MedidaCautelar != "I-La presentación periódica ante el juez o ante autoridad distinta que aquél designe" &&
                            a.MedidaCautelar != "II-La exhibición de una garantía económica" &&
                            a.MedidaCautelar != "III-El embargo de bienes" &&
                            a.MedidaCautelar != "IV-La inmovilización de cuentas y demás valores que se encuentren dentro del sistema financiero" &&
                            a.MedidaCautelar != "V-La prohibición de salir sin autorización del país, de la localidad en la cual reside o del ámbito territorial que fije el juez" &&
                            a.MedidaCautelar != "VI-El sometimiento al cuidado o vigilancia de una persona o institución determinada o internamiento a institución determinada" &&
                            a.MedidaCautelar != "VII-La prohibición de concurrir a determinadas reuniones o acercarse o ciertos lugares" &&
                            a.MedidaCautelar != "VIII-La prohibición de convivir, acercarse o comunicarse con determinadas personas, con las víctimas u ofendidos o testigos, siempre que no se afecte el derecho de defensa" &&
                            a.MedidaCautelar != "IX-La separación inmediata del domicilio" &&
                            a.MedidaCautelar != "X-La suspensión temporal en el ejercicio del cargo cuando se le atribuye un delito cometido por servidores públicos" &&
                            a.MedidaCautelar != "XI-La suspensión temporal en el ejercicio de una determinada actividad profesional o laboral" &&
                            a.MedidaCautelar != "XII-La colocación de localizadores electrónicos" &&
                            a.MedidaCautelar != "XIII-El resguardo en su propio domicilio con las modalidades que el juez disponga" &&
                            a.MedidaCautelar != "INEGI-La prisión preventiva oficiosa" &&
                            a.MedidaCautelar != "INEGI-La prisión preventiva justificada" : 1 == 1)
                            .ToListAsync();


            IEnumerable<EstadisticaMedidasCViewModel> items = new EstadisticaMedidasCViewModel[] { };

            IEnumerable<EstadisticaMedidasCViewModel> ReadLines(int cantidad, string tipo)
            {
                IEnumerable<EstadisticaMedidasCViewModel> item2;

                item2 = (new[]{new EstadisticaMedidasCViewModel{
                                Cantidad = cantidad,
                                Tipo = tipo
                            }});

                return item2;
            }

            items = items.Concat(ReadLines(mc.Count, (MedidaCautelar.Medida != "null" ? MedidaCautelar.Medida : "Todas las medidas")));


            return items;
        }



        private bool MedidasCautelaresExists(Guid id)
        {
            return _context.MedidasCautelares.Any(e => e.IdMedCautelares == id);
        }
    }
}
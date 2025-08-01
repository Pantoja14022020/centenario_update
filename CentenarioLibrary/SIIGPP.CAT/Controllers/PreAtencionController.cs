using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIIGPP.CAT.Models.PRegistro;
using SIIGPP.CAT.Models.Registro;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_Cat.Direcciones;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.PAtencion;
using SIIGPP.Entidades.M_Cat.Registro;

namespace SIIGPP.CAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreAtencionesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private IConfiguration _configuration;
        public PreAtencionesController(DbContextSIIGPP context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<PreAtencion> PreRegistros()
        {
            return _context.PreAtenciones;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] PreAtencionCrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fecha = System.DateTime.Now;
           

            var options = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_configuration.GetConnectionString("C-" + model.distritoId.ToString().ToUpper())).Options;
            using (var ctx = new DbContextSIIGPP(options))
            {

                    Persona InsertarPersona;
                    Guid dirPersona,idRAP, idP;
                    PreAtencion InsertarPA = new PreAtencion
                    {
                    FechaHoraRegistro = fecha,
                    StatusAtencion = false,
                    StatusRegistro = false,
                    PRegistroId = model.PRegistroId,
                    MedioDenuncia = "Registro Internet",
                    ContencionVicitma = false
                    };


                try
                {
                    ctx.PreAtenciones.Add(InsertarPA);




                    InsertarPersona = new Persona
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
                        
                        CURP = model.CURP,
                        Sexo = model.Sexo,
                        EstadoCivil = model.EstadoCivil,
                        Genero = model.Genero,
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
                        Relacion = model.Relacion


                    };

                    ctx.Personas.Add(InsertarPersona);
                    //********************************************************

                   

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
                        PersonaId = InsertarPersona.IdPersona,
                        lat = model.lat,
                        lng = model.lng
                    };

                    ctx.DireccionPersonals.Add(InsertarDP);
                    
                    

                    PreRap InsertarRAP = new PreRap
                    {
                        PAtencionId = InsertarPA.IdPAtencion,
                        PersonaId = InsertarPersona.IdPersona,
                        ClasificacionPersona = model.ClasificacionPersona,
                        PInicio = model.PInicio,
                    };

                    ctx.PreRaps.Add(InsertarRAP);
                   

                    await ctx.SaveChangesAsync();
                    var idPA = InsertarPA.IdPAtencion;
                    idP = InsertarPersona.IdPersona;
                    dirPersona = InsertarDP.IdDPersonal;
                    idRAP = InsertarRAP.IdPRap;

                }

                catch (Exception ex)
                {
                    var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                    result.StatusCode = 402;
                    return result;
                }

                return Ok(new { idpatencion = InsertarPA.IdPAtencion, personaid = idP, direccionPersonal=dirPersona,rap=idRAP });

            }
        }



    }

}

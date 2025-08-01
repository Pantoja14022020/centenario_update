using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_JR.RAsignacionEnvios;
using SIIGPP.Entidades.M_JR.RCitatorioRecordatorio;
using SIIGPP.Entidades.M_JR.RSesion;
using SIIGPP.JR.Models.RSesion;
using SIIGPP.Entidades.M_Cat.DDerivacion;

namespace SIIGPP.JR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionsConjuntoController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;

        public SesionsConjuntoController(DbContextSIIGPP context)
        {
            _context = context;
        }

        //GET: api/SesionsConjunto/ListarSRD
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
         [HttpGet("[action]/{SesionId}")]
          public async Task<IEnumerable<GET_ConjuntosViewModel>> ListarSRD([FromRoute] Guid SesionId)
        {
            var tabla = await _context.SesionConjuntos
                                        .Include(a => a.Conjunto)
                                        .Where(a => a.SesionId == SesionId)
                                        .ToListAsync();

            if (tabla == null)
            {
                return Enumerable.Empty<GET_ConjuntosViewModel>();
            }
            else
            {
                return tabla.Select(a => new GET_ConjuntosViewModel
                {
                    IdConjuntoDerivaciones = a.Conjunto.IdConjuntoDerivaciones,
                    EnvioId = a.Conjunto.EnvioId,
                    SolicitadosC = a.Conjunto.SolicitadosC,
                    RequeridosC = a.Conjunto.RequeridosC,
                    DelitosC = a.Conjunto.DelitosC,
                    NombreS = a.Conjunto.NombreS,
                    DireccionS = a.Conjunto.DireccionS,
                    TelefonoS = a.Conjunto .TelefonoS,
                    ClasificacionS = a.Conjunto.ClasificacionS,
                    NombreR = a.Conjunto.NombreR,
                    DireccionR = a.Conjunto.DireccionR,
                    TelefonoR = a.Conjunto.TelefonoR,
                    ClasificacionR = a.Conjunto.ClasificacionR,
                    NombreD  = a.Conjunto.NombreD,
                    NoOficio = a.Conjunto.NoOficio,
                    ResponsableJR = a.Conjunto.ResponsableJR,
                    NombreSubDirigido = a.Conjunto.NombreSubDirigido,
                });
            }
        }

        //GET: api/SesionsConjunto/ListarSRDConjunto
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]/{ConjuntoId}")]
        public async Task<IEnumerable<GET_ConjuntosViewModel>> ListarSRDConjunto([FromRoute] Guid ConjuntoId)
        {
            var tabla = await _context.ConjuntoDerivaciones
                                        .Where(a => a.IdConjuntoDerivaciones == ConjuntoId)
                                        .ToListAsync();

            if (tabla == null)
            {
                return Enumerable.Empty<GET_ConjuntosViewModel>();
            }
            else
            {
                return tabla.Select(a => new GET_ConjuntosViewModel
                {
                    IdConjuntoDerivaciones = a.IdConjuntoDerivaciones,
                    EnvioId = a.EnvioId,
                    SolicitadosC = a.SolicitadosC,
                    RequeridosC = a.RequeridosC,
                    DelitosC = a.DelitosC,
                    NombreS = a.NombreS,
                    DireccionS = a.DireccionS,
                    TelefonoS = a.TelefonoS,
                    ClasificacionS = a.ClasificacionS,
                    NombreR = a.NombreR,
                    DireccionR = a.DireccionR,
                    TelefonoR = a.TelefonoR,
                    ClasificacionR = a.ClasificacionR,
                    NombreD = a.NombreD,
                    NoOficio = a.NoOficio,
                    ResponsableJR = a.ResponsableJR,
                    NombreSubDirigido = a.NombreSubDirigido,
                });
            }
        }
    }
}
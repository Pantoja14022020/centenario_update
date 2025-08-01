using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIIGPP.ControlAcceso.Models.ControlAcceso.Almacenamineto;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_ControlAcceso.UAlmacenamiento;

namespace SIIGPP.ControlAcceso.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenamientoesController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
         

        public AlmacenamientoesController(DbContextSIIGPP context)
        {
            _context = context;
        }

        // GET: api/Almacenamientoes/Listar
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<AlmacenamientoViewModel>> Listar()
        {
            var db = await _context.Almacenamientos.ToListAsync();

            return db.Select(a => new AlmacenamientoViewModel
            {
                IdAlmacenamiento = a.IdAlmacenamiento, 
                StatusActivo =a.StatusActivo,
                StatusLLeno = a.StatusLLeno,
                EspacioDsiponible = a.EspacioDsiponible,
                EspacioTotal = a.EspacioTotal,
                EspacioUtilizado = a.EspacioUtilizado,
                Porcentaje = a.Porcentaje,
            });

        }
        // GET: api/Almacenamientoes/ListarActivo
       // [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarActivo()
        {
            var db = await _context.Almacenamientos.Where(a=> a.StatusActivo == true).FirstOrDefaultAsync();

            if (db == null)
            {
                return NotFound( new { error = "Por el monmento no existe ninguna unidad activa por favor consullte al administrador" }); 
            }
            return Ok(new AlmacenamientoViewModel
                {
                IdAlmacenamiento = db.IdAlmacenamiento, 
                StatusActivo = db.StatusActivo,
                StatusLLeno = db.StatusLLeno,
                EspacioDsiponible = db.EspacioDsiponible,
                EspacioTotal = db.EspacioTotal,
                EspacioUtilizado = db.EspacioUtilizado,
                Porcentaje = db.Porcentaje,
            });

        }
     
         

         
    }
}
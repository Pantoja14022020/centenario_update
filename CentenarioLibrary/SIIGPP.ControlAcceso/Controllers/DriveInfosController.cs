using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SIIGPP.ControlAcceso.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class DriveInfosController : ControllerBase
    {
        // GET api/DriveInfos/Get
        [HttpGet("{nombreUnidad}")]
        public ActionResult<IEnumerable<string>> Get(string nombreUnidad)
 

        {
            try
            {
                DriveInfo info = new DriveInfo(nombreUnidad);
                var nombre = "";
                var espaciodisponible = "";
                var espaciototal = "";
                nombre = info.Name;
                espaciodisponible = (info.TotalFreeSpace / 1073741824).ToString();
                espaciototal = (info.TotalSize / 1073741824).ToString();

                return Ok(new { nu = nombre, ed = espaciodisponible, et = espaciototal });
            }
            catch  
            {
                // Guardar Excepción
                return BadRequest();
            }
           
 
           
        }
    }
}
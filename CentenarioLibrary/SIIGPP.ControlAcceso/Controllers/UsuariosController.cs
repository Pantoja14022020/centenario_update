using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIIGPP.ControlAcceso.Models.ControlAcceso.Usuarios;
using SIIGPP.Datos;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using SIIGPP.Entidades.M_ControlAcceso.ControlDistrito;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.Options;
using SIIGPP.Entidades.M_Panel.M_PanelControl;
using SIIGPP.Entidades.M_PI.Direcciones;
using SIIGPP.Entidades.M_ControlAcceso.PanelUsuarios;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SIIGPP.ControlAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextSIIGPP _context;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _environment;


        public UsuariosController(DbContextSIIGPP context, IConfiguration config, IWebHostEnvironment environment)
        {
            _context = context;
            _config = config;
            _environment = environment;
        }

        //POyyST: pai/Usuarios/ClonarUsuario
        [HttpPost("[action]")]
        //[Authorize(Roles  = "Administrador")]
        public async Task<IActionResult> ClonarUsuario([FromBody] ClonarUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var consultausuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.IdUsuario);

                if (consultausuario == null)
                {
                    return BadRequest(ModelState);
                }

                if (model.Caso == 2)
                {
                    Console.Write("entra caso 2");
                    var option = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_config.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;

                    using (var ctx = new DbContextSIIGPP(option))
                    {
                        var usuario = await ctx.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.IdUsuario);

                        if (usuario == null)
                        {
                            usuario = new Usuario();
                            ctx.Usuarios.Add(usuario);
                        }

                        usuario.IdUsuario = consultausuario.IdUsuario;
                        usuario.ModuloServicioId = consultausuario.ModuloServicioId;
                        usuario.RolId = consultausuario.RolId;
                        usuario.clave = consultausuario.clave;
                        usuario.usuario = consultausuario.usuario.ToLower();
                        usuario.nombre = consultausuario.nombre;
                        usuario.direccion = consultausuario.direccion;
                        usuario.telefono = consultausuario.telefono;
                        usuario.email = consultausuario.email.ToLower();
                        usuario.puesto = consultausuario.puesto;
                        usuario.password_hash = consultausuario.password_hash;
                        usuario.password_salt = consultausuario.password_salt;
                        usuario.condicion = consultausuario.condicion;
                        usuario.Titular = consultausuario.Titular;
                        usuario.ResponsableCuenta = consultausuario.ResponsableCuenta;

                        await ctx.SaveChangesAsync();
                    }
                    return Ok();
                }
                else if (model.Caso == 3)
                {
                    Console.Write("entra caso 3");
                    for (int i = 0; i <= 1; i++)
                    {
                        Console.Write("entra for");
                        if (i == 0)
                        {
                            var option = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_config.GetConnectionString("C-" + model.IdDistritoO.ToString().ToUpper())).Options;

                            using (var ctx = new DbContextSIIGPP(option))
                            {
                                var usuario = await ctx.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.IdUsuario);

                                if (usuario == null)
                                {
                                    usuario = new Usuario();
                                    ctx.Usuarios.Add(usuario);
                                }

                                usuario.IdUsuario = consultausuario.IdUsuario;
                                usuario.ModuloServicioId = consultausuario.ModuloServicioId;
                                usuario.RolId = consultausuario.RolId;
                                usuario.clave = consultausuario.clave;
                                usuario.usuario = consultausuario.usuario.ToLower();
                                usuario.nombre = consultausuario.nombre;
                                usuario.direccion = consultausuario.direccion;
                                usuario.telefono = consultausuario.telefono;
                                usuario.email = consultausuario.email.ToLower();
                                usuario.puesto = consultausuario.puesto;
                                usuario.password_hash = consultausuario.password_hash;
                                usuario.password_salt = consultausuario.password_salt;
                                usuario.condicion = consultausuario.condicion;
                                usuario.Titular = consultausuario.Titular;
                                usuario.ResponsableCuenta = consultausuario.ResponsableCuenta;

                                await ctx.SaveChangesAsync();
                            }

                        }
                        if (i == 1)
                        {
                            var option = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_config.GetConnectionString("C-" + model.IdDistritoD.ToString().ToUpper())).Options;

                            using (var ctx = new DbContextSIIGPP(option))
                            {
                                var usuario = await ctx.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.IdUsuario);

                                if (usuario == null)
                                {
                                    usuario = new Usuario();
                                    ctx.Usuarios.Add(usuario);
                                }

                                usuario.IdUsuario = consultausuario.IdUsuario;
                                usuario.ModuloServicioId = consultausuario.ModuloServicioId;
                                usuario.RolId = consultausuario.RolId;
                                usuario.clave = consultausuario.clave;
                                usuario.usuario = consultausuario.usuario.ToLower();
                                usuario.nombre = consultausuario.nombre;
                                usuario.direccion = consultausuario.direccion;
                                usuario.telefono = consultausuario.telefono;
                                usuario.email = consultausuario.email.ToLower();
                                usuario.puesto = consultausuario.puesto;
                                usuario.password_hash = consultausuario.password_hash;
                                usuario.password_salt = consultausuario.password_salt;
                                usuario.condicion = consultausuario.condicion;
                                usuario.Titular = consultausuario.Titular;
                                usuario.ResponsableCuenta = consultausuario.ResponsableCuenta;

                                await ctx.SaveChangesAsync();
                            }
                        }
                    }
                    return Ok();
                }


                Console.ReadLine();
                return BadRequest("No se Puede");
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }


        // GET: api/Usuarios/ListarPorDistrito
        [HttpGet("[action]/{idDistrito}")]
        public async Task<IEnumerable<UsuarioViewModel>> ListarPorDistrito([FromRoute] Guid idDistrito)
        {
            var usuario = await _context.Usuarios
                            .Include(u => u.rol)
                            .Include(u => u.ModuloServicio)
                            .Include(u => u.ModuloServicio.Agencia)
                            .Include(u => u.ModuloServicio.Agencia.DSP)
                            .Include(u => u.ModuloServicio.Agencia.DSP.Distrito)
                            .Where(u => u.ModuloServicio.Agencia.DSP.DistritoId == idDistrito)
                            .ToListAsync();

            int contador = usuario.Count;

            return usuario.Select(u => new UsuarioViewModel
            {
                idUsuario = u.IdUsuario,
                rolId = u.RolId,
                nombrerol = u.rol.Nombre,
                distritoId = u.ModuloServicio.Agencia.DSP.DistritoId,
                dspId = u.ModuloServicio.Agencia.DSPId,
                agenciaId = u.ModuloServicio.AgenciaId,
                moduloServicioId = u.ModuloServicioId,
                modulonombre = u.ModuloServicio.Nombre,
                usuario = u.usuario,
                password_hash = u.password_hash,
                nombre = u.nombre,
                direccion = u.direccion,
                telefono = u.telefono,
                email = u.email,
                puesto = u.puesto,
                condicion = u.condicion,
                contador = contador,
                nombreAgencia = u.ModuloServicio.Agencia.Nombre,
                Titular = u.Titular,
                ResponsableCuenta = u.ResponsableCuenta,
                

            });
        }

        // GET: api/Usuarios/ListarPorClaveDSP
        //[Authorize(Roles  = "Administrador")]
        [HttpGet("[action]/{clave}")]
        public async Task<IEnumerable<UsuarioViewModel>> ListarPorClaveDSP(String clave)
        {
            var usuario = await _context.Usuarios
                            .Include(u => u.rol)
                            .Include(u => u.ModuloServicio)
                            .Include(u => u.ModuloServicio.Agencia)
                            .Include(u => u.ModuloServicio.Agencia.DSP)
                            .Include(u => u.ModuloServicio.Agencia.DSP.Distrito)
                            .Where(u => u.ModuloServicio.Agencia.DSP.Clave == clave)
                            .ToListAsync();

            int contador = usuario.Count;

            return usuario.Select(u => new UsuarioViewModel
            {
                idUsuario = u.IdUsuario,
                rolId = u.RolId,
                nombrerol = u.rol.Nombre,
                distritoId = u.ModuloServicio.Agencia.DSP.DistritoId,
                dspId = u.ModuloServicio.Agencia.DSPId,
                agenciaId = u.ModuloServicio.AgenciaId,
                moduloServicioId = u.ModuloServicioId,
                modulonombre = u.ModuloServicio.Nombre,
                usuario = u.usuario,
                password_hash = u.password_hash,
                nombre = u.nombre,
                direccion = u.direccion,
                telefono = u.telefono,
                email = u.email,
                puesto = u.puesto,
                condicion = u.condicion,
                contador = contador,
                nombreAgencia = u.ModuloServicio.Agencia.Nombre,
                Titular = u.Titular,
                ResponsableCuenta = u.ResponsableCuenta,
            });
        }

        // GET: api/Usuarios/Listar
        //[Authorize(Roles  = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios
                            .Include(u => u.rol)
                            .Include(u => u.ModuloServicio)
                            .Include(u => u.ModuloServicio.Agencia)
                            .Include(u => u.ModuloServicio.Agencia.DSP)
                            .Include(u => u.ModuloServicio.Agencia.DSP.Distrito)
                            .ToListAsync();

            int contador = usuario.Count;

            return usuario.Select(u => new UsuarioViewModel
            {
                idUsuario = u.IdUsuario,
                rolId = u.RolId,
                nombrerol = u.rol.Nombre,
                distritoId = u.ModuloServicio.Agencia.DSP.DistritoId,
                dspId = u.ModuloServicio.Agencia.DSPId,
                agenciaId = u.ModuloServicio.AgenciaId,
                moduloServicioId = u.ModuloServicioId,
                modulonombre = u.ModuloServicio.Nombre,
                usuario = u.usuario,
                password_hash = u.password_hash,
                nombre = u.nombre,
                direccion = u.direccion,
                telefono = u.telefono,
                email = u.email,
                puesto = u.puesto,
                condicion = u.condicion,
                contador = contador,
                nombreAgencia = u.ModuloServicio.Agencia.Nombre,
                Titular = u.Titular,
                ResponsableCuenta = u.ResponsableCuenta,
                subdir = u.ModuloServicio.Agencia.DSP.NombreSubDir,
                UrlDistrito = u.ModuloServicio.Agencia.DSP.Distrito.UrlDistrito,


            });



        }
        // GET: api/Usuarios/ListarModulo
        //[Authorize(Roles = "Director, Administrador, Recepción, Facilitador,Facilitador-Mixto Notificador, Coordinador, Jurídico")] 
        [HttpGet("[action]/{moduloServicioId}")]
        public async Task<IActionResult> ListarModulo([FromRoute] Guid moduloServicioId)
        {
            var tabla = await _context.Usuarios
                                .Where(u => u.ModuloServicioId == moduloServicioId)
                                .Include(u => u.rol)
                                .Include(u => u.ModuloServicio)
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return NotFound("No hay registros");
            }
            return Ok(new UsuarioViewModel
            {
                idUsuario = tabla.IdUsuario,
                rolId = tabla.RolId,
                nombrerol = tabla.rol.Nombre,
                moduloServicioId = tabla.ModuloServicioId,
                modulonombre = tabla.ModuloServicio.Nombre,
                usuario = tabla.usuario,
                password_hash = tabla.password_hash,
                nombre = tabla.nombre,
                direccion = tabla.direccion,
                telefono = tabla.telefono,
                email = tabla.email,
                puesto = tabla.puesto,
                condicion = tabla.condicion,
                Titular = tabla.Titular,
                ResponsableCuenta = tabla.ResponsableCuenta
            });
        }


        // GET: api/Usuarios/ListarModulConId
        //[Authorize(Roles = "Director, Administrador, Recepción, Facilitador,Facilitador-Mixto Notificador, Coordinador, Jurídico")] 
        [HttpGet("[action]/{moduloServicioId}/{facinoti}")]
        public async Task<IActionResult> ListarModuloConId([FromRoute] Guid moduloServicioId, Guid facinoti)
        {
            var tabla = await _context.Usuarios
                                .Where(u => u.ModuloServicioId == moduloServicioId)
                                .Where(u => u.IdUsuario == facinoti)
                                .Include(u => u.rol)
                                .Include(u => u.ModuloServicio)
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return NotFound("No hay registros");
            }
            return Ok(new UsuarioViewModel
            {
                idUsuario = tabla.IdUsuario,
                rolId = tabla.RolId,
                nombrerol = tabla.rol.Nombre,
                moduloServicioId = tabla.ModuloServicioId,
                modulonombre = tabla.ModuloServicio.Nombre,
                usuario = tabla.usuario,
                password_hash = tabla.password_hash,
                nombre = tabla.nombre,
                direccion = tabla.direccion,
                telefono = tabla.telefono,
                email = tabla.email,
                puesto = tabla.puesto,
                condicion = tabla.condicion,
                Titular = tabla.Titular,
                ResponsableCuenta = tabla.ResponsableCuenta,
            });
        }

        // POST: api/Usuarios/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid usuarioid;

            var usr = model.usuario.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.usuario == usr))
            {
                return BadRequest("El usuario ya existe");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            Usuario usuario = new Usuario
            {
                RolId = model.rolId,
                clave = Guid.NewGuid(),
                ModuloServicioId = model.moduloServicioId,
                puesto = model.puesto,
                nombre = model.nombre,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email.ToLower(),
                usuario = model.usuario,
                Titular = model.Titular,
                password_hash = passwordHash,
                password_salt = passwordSalt,
                condicion = true,
                ResponsableCuenta = model.ResponsableCuenta,

            };

            _context.Usuarios.Add(usuario);

            try
            {
                await _context.SaveChangesAsync();
                usuarioid = usuario.IdUsuario;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return BadRequest();
            }

            return Ok(new {usuarioid = usuarioid});
        }


        // PUT: api/Articulos/Actualizar
        [Authorize(Roles = "Administrador,Director")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.idusuario);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.RolId = model.rolId;
            usuario.ModuloServicioId = model.moduloServicioId;
            usuario.puesto = model.puesto;
            usuario.nombre = model.nombre;
            usuario.ResponsableCuenta = model.ResponsableCuenta;
            usuario.direccion = model.direccion;
            usuario.telefono = model.telefono;
            usuario.Titular = model.Titular;
            usuario.email = model.email.ToLower();
            usuario.usuario = model.usuario.ToLower();
            usuario.ResponsableCuenta = model.ResponsableCuenta;

            if (model.act_password == true)
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.password_hash = passwordHash;
                usuario.password_salt = passwordSalt;
            }

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
        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        // PUT: api/Usuarios/Desactivar/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] Guid id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.condicion = false;

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

        // PUT: api/Usuarios/Activar/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] Guid id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.condicion = true;

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

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var usr = model.usuario.ToLower();
                //Guarda la misma informacion que lo de arriba
                var ussr = model.usuario.ToLower();

                //Consulta con la nueva tabla C_CONTROLDISTRITOS para saber si pertenece al mismo distrito

                String logiNuevo = @"SELECT 
                                            u.IdUsuario,
                                            u.clave,
                                            u.RolId,
                                            u.ModuloServicioId,
                                            u.usuario,
                                            u.nombre,
                                            u.direccion,
                                            u.telefono,
                                            u.email,
                                            u.puesto,
                                            d.DistritoId,
                                            u.password_hash,
                                            u.password_salt,
                                            u.condicion,
                                            u.Titular,
                                            M.Clave as cla
                                            FROM CA_USUARIO as U
                                            LEFT JOIN CA_ROL AS R ON R.IdRol = U.RolId
                                            LEFT JOIN C_MODULOSERVICIO AS M ON M.IdModuloServicio = U.ModuloServicioId
                                            LEFT JOIN C_AGENCIA AS A ON A.IdAgencia = M.AgenciaId
                                            LEFT JOIN C_DSP AS D ON D.IdDSP = A.DSPId
                                            LEFT JOIN C_DISTRITO AS I ON I.IdDistrito = D.DistritoId
                                            LEFT JOIN C_CONTROLDISTRITOS AS C ON C.DisId =I.IdDistrito
                                            WHERE @usuar = u.usuario AND U.condicion = 1 and c.DisId = d.DistritoId";

                List<SqlParameter> filtrosBusqueda = new List<SqlParameter>();
                filtrosBusqueda.Add(new SqlParameter("@usuar", ussr));

                //En usuario guardo el resultado de la consulta a la que posteriormente contare  sus resultados, si es 0 no pertenece al distrito
                var usuarios = await _context.qLoginConDistrito.FromSqlRaw(logiNuevo, filtrosBusqueda.ToArray()).ToListAsync();

                //Mantengo la misma consulta para comprobar el usuario porque usuario es importante en el resto de codigo
                var usuario = await _context
                             .Usuarios.Where(u => u.condicion == true)
                             .Include(u => u.rol)
                             .Include(u => u.ModuloServicio)
                             .Include(u => u.ModuloServicio.Agencia.DSP)
                             .Include(u => u.ModuloServicio.Agencia.DSP.Distrito)
                             .FirstOrDefaultAsync(u => u.usuario == usr);

                if (usuario == null)
                {
                    return BadRequest("El nombre de usuario que ingreso no existe por favor verifiquelo");
                }

                if (!VerificarPasswordHash(model.password, usuario.password_hash, usuario.password_salt))
                {
                    return NotFound("La contraseña es incorrecta, por favor verifiquela");
                }

                //si no hay registros no pertenece al distrito al que intenta entrar

                if (usuarios.Count == 0)
                {
                    return BadRequest("El usuario no pertenece a este distrito");
                }

                var usuariopanel = await _context
                            .PanelUsuarios
                            .Include(u => u.PanelControl)
                            .Include(u => u.Usuarios)
                            .Where(p => p.UsuarioId == usuario.IdUsuario)
                            .Where(p => p.PanelControl.Clave == model.ClaveP)
                            .FirstOrDefaultAsync();

                if (usuariopanel == null)
                {
                    return NotFound("No esta autorizado para ingresar a este módulo");
                }

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario.rol.Nombre ),
                new Claim("iddistrito", usuario.ModuloServicio.Agencia.DSP.DistritoId.ToString()),
                new Claim("distrito", usuario.ModuloServicio.Agencia.Municipio),
                new Claim("iddirSubProc", usuario.ModuloServicio.Agencia.DSP.IdDSP.ToString()),
                new Claim("dirSubProc", usuario.ModuloServicio.Agencia.DSP.NombreSubDir),
                new Claim("idagencia", usuario.ModuloServicio.AgenciaId.ToString()),
                new Claim("agencia", usuario.ModuloServicio.Agencia.Nombre),
                new Claim("idmoduloservicio", usuario.ModuloServicioId.ToString()),
                new Claim("modulo", usuario.ModuloServicio.Nombre),
                new Claim("tipomodulo", usuario.ModuloServicio.Tipo),
                new Claim("idusuario", usuario.IdUsuario.ToString()),
                new Claim("usuario", usuario.nombre),
                new Claim("clave", usuario.clave.ToString()),
                new Claim("claveMS", usuario.ModuloServicio.Clave),
                new Claim("rol", usuario.rol.Nombre),
                new Claim("rolId", usuario.RolId.ToString()),
                new Claim("puesto", usuario.puesto),
                new Claim("subProc", usuario.ModuloServicio.Agencia.DSP.NombreSub != null || usuario.ModuloServicio.Agencia.DSP.NombreSub != "Sin asignación"  ?  usuario.ModuloServicio.Agencia.DSP.NombreSub : " "),
                new Claim("iddsp",usuario.ModuloServicio.Agencia.DSPId.ToString()),
                new Claim("nodistrito",usuario.ModuloServicio.Agencia.DSP.Distrito.Clave),
                new Claim("ClaveDSP",usuario.ModuloServicio.Agencia.DSP.Clave),
                new Claim("clavedistrito",usuario.ModuloServicio.Agencia.DSP.Distrito.Clave),
                new Claim("municipio",usuario.ModuloServicio.Agencia.Municipio),
                new Claim("clavedistrito",usuario.ModuloServicio.Agencia.DSP.Distrito.Clave),
                new Claim("claveAQDeriva",usuario.ModuloServicio.Agencia.Clave),
                new Claim("responsablecuenta", usuario.ResponsableCuenta),
                };


                return Ok(
                        new { token = GenerarToken(claims) }
                    );
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }
        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(900),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // GET: api/Usuarios/Listar2
        [HttpGet("[action]/{idModuloServicio}")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar2([FromRoute] Guid idModuloServicio)
        {
            var usuario = await _context.Usuarios
                .Where(a => a.ModuloServicioId == idModuloServicio)
                .ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                idUsuario = u.IdUsuario,
                nombre = u.nombre,
                puesto = u.puesto,


            });

        }

        //GET:  api/Usuarios/ListarEnvio
        // API: LISTA LOS EXPEDIENTES  FILTRADOS POR NOTIFICADOR FACILITADOR
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador, AMPO-AMP,AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido")]
        [HttpGet("[action]/{idModuloServicio}")]
        public async Task<IActionResult> ListarEnvio([FromRoute] Guid idModuloServicio)
        {
            var tabla = await _context.Usuarios
                                .Include(a => a.rol)
                                .Include(a => a.ModuloServicio)
                                .Where(a => a.ModuloServicioId == idModuloServicio)
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return NotFound("No hay registros");

            }
            return Ok(new UsuarioViewModel
            {
                idUsuario = tabla.IdUsuario,
                rolId = tabla.RolId,
                nombrerol = tabla.rol.Nombre,
                moduloServicioId = tabla.ModuloServicioId,
                modulonombre = tabla.ModuloServicio.Nombre,
                usuario = tabla.usuario,
                password_hash = tabla.password_hash,
                nombre = tabla.nombre,
                direccion = tabla.direccion,
                telefono = tabla.telefono,
                email = tabla.email,
                puesto = tabla.puesto,
                condicion = tabla.condicion,
                Titular = tabla.Titular,
            });

        }


        // GET: api/Usuarios/DirectorPI
        [Authorize(Roles = "Director, Administrador, Recepción, Facilitador, Notificador, Coordinador, Jurídico, AMPO-AMP, AMPO-AMP Mixto, AMPO-AMP Detenido")]
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IActionResult> DirectorPI([FromRoute] Guid iddistrito)
        {
            var tabla = await _context.Usuarios
                                .Where(u => u.rol.Nombre == "Director")
                                .Where(u => u.ModuloServicio.Agencia.DSP.NombreSubDir == "Policia Investigadora")
                                .Where(u => u.ModuloServicio.Agencia.DSP.DistritoId == iddistrito)
                                .Include(u => u.rol)
                                .Include(u => u.ModuloServicio)
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return NotFound("No hay registros");
            }
            return Ok(new UsuarioViewModel
            {
                idUsuario = tabla.IdUsuario,
                rolId = tabla.RolId,
                nombrerol = tabla.rol.Nombre,
                moduloServicioId = tabla.ModuloServicioId,
                modulonombre = tabla.ModuloServicio.Nombre,
                usuario = tabla.usuario,
                password_hash = tabla.password_hash,
                nombre = tabla.nombre,
                direccion = tabla.direccion,
                telefono = tabla.telefono,
                email = tabla.email,
                puesto = tabla.puesto,
                condicion = tabla.condicion,
                Titular = tabla.Titular,
            });
        }

        // GET: api/Usuarios/DirectorUECS
        [Authorize(Roles = "Director, Administrador, Recepción, Facilitador, Notificador, Coordinador, Jurídico, AMPO-AMP, AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador, AMPO-IL")]
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IActionResult> DirectorUECS([FromRoute] Guid iddistrito)
        {
            var tabla = await _context.Usuarios
                                .Where(u => u.rol.Nombre == "Director")
                                .Where(u => u.ModuloServicio.Agencia.DSP.NombreSubDir == "Unidad Especializada en Combate al Secuestro")
                                .Where(u => u.ModuloServicio.Agencia.DSP.DistritoId == iddistrito)
                                .Include(u => u.rol)
                                .Include(u => u.ModuloServicio)
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return NotFound("No hay registros");
            }
            return Ok(new UsuarioViewModel
            {
                idUsuario = tabla.IdUsuario,
                rolId = tabla.RolId,
                nombrerol = tabla.rol.Nombre,
                moduloServicioId = tabla.ModuloServicioId,
                modulonombre = tabla.ModuloServicio.Nombre,
                usuario = tabla.usuario,
                password_hash = tabla.password_hash,
                nombre = tabla.nombre,
                direccion = tabla.direccion,
                telefono = tabla.telefono,
                email = tabla.email,
                puesto = tabla.puesto,
                condicion = tabla.condicion,
                Titular = tabla.Titular,
            });
        }

        // GET: api/Usuarios/DirectorFEDC
        [Authorize(Roles = "Director, Administrador, Recepción, Facilitador, Notificador, Coordinador, Jurídico, AMPO-AMP")]
        [HttpGet("[action]/{iddistrito}")]
        public async Task<IActionResult> DirectorFEDC([FromRoute] Guid iddistrito)
        {
            var tabla = await _context.Usuarios
                                .Where(u => u.rol.Nombre == "Director")
                                .Where(u => u.ModuloServicio.Agencia.DSP.NombreSubDir == "Fiscalia especializada en delitos de corrupción")
                                .Where(u => u.ModuloServicio.Agencia.DSP.DistritoId == iddistrito)
                                .Include(u => u.rol)
                                .Include(u => u.ModuloServicio)
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return NotFound("No hay registros");
            }
            return Ok(new UsuarioViewModel
            {
                idUsuario = tabla.IdUsuario,
                rolId = tabla.RolId,
                nombrerol = tabla.rol.Nombre,
                moduloServicioId = tabla.ModuloServicioId,
                modulonombre = tabla.ModuloServicio.Nombre,
                usuario = tabla.usuario,
                password_hash = tabla.password_hash,
                nombre = tabla.nombre,
                direccion = tabla.direccion,
                telefono = tabla.telefono,
                email = tabla.email,
                puesto = tabla.puesto,
                condicion = tabla.condicion,
                Titular = tabla.Titular,
            });
        }

        // GET: api/Usuarios/Modulointerno
        [HttpGet("[action]/{idusuario}")]
        public async Task<IActionResult> Modulointerno([FromRoute] Guid idusuario)
        {
            var tabla = await _context.Usuarios
                                .Include(u => u.ModuloServicio)
                                .Where(u => u.IdUsuario == idusuario)
                                .FirstOrDefaultAsync();
            if (tabla == null)
            {
                return Ok(new { status = false, Idmodulo = "000-000-000" });
            }
            return Ok(new ModuloInterno
            {
                status = tabla.ModuloServicio.ServicioInterno,
                Idmodulo = tabla.ModuloServicio.IdModuloServicio,
            });
        }

        // GET: api/Usuarios/ValidarToken
        [HttpGet("[action]")]
        [Authorize]
#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<IActionResult> ValidarToken()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {


            if (ModelState.IsValid)
                return Ok(new { status = true });
            else
                return Ok(new { status = false });
        }

        // GET: api/Usuarios/DirectorUECS
        [Authorize(Roles = "Director, Administrador, Recepción, Facilitador, Notificador, Coordinador, Jurídico, AMPO-AMP, AMPO-AMP Mixto, AMPO-AMP Detenido,Procurador, AMPO-IL")]
        [HttpGet("[action]/{ClaveA}")]
        public async Task<IActionResult> mostrarModuloUsuario([FromRoute] String ClaveA)
        {
            var infoClave = await _context.ClavesAgencias
                                .Where(u => u.NuevaClave == ClaveA)
                                .FirstOrDefaultAsync();
            if (infoClave == null)
            {
                return NotFound("No hay registros");
            }
            return Ok(new ClavesAgenciaViewModel
            {
                IdClave = infoClave.IdClave,
                NuevaClave = infoClave.NuevaClave,
                NombreModulo = infoClave.NombreModulo,
                NombreSubdireccionAgencia = infoClave.NombreSubdireccionAgencia,
                RolAdecuado = infoClave.RolAdecuado,
            });
        }

        // GET: api/Usuarios/DirectorUECS
        [Authorize(Roles = "Director, Administrador, Recepción, Facilitador, Notificador, Coordinador, Jurídico, AMPO-AMP, AMPO-AMP Mixto, AMPO-AMP Detenido, AMPO-AMP Detenido,Procurador, AMPO-IL")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ClavesAgenciaViewModel>> ListarClaves()
        {
            var infoClave = await _context.ClavesAgencias
            .ToListAsync();

            return infoClave.Select(a => new ClavesAgenciaViewModel
            {
                IdClave = a.IdClave,
                NuevaClave = a.NuevaClave,
                NombreModulo = a.NombreModulo,
                NombreSubdireccionAgencia = a.NombreSubdireccionAgencia,
                RolAdecuado = a.RolAdecuado,
            });
        }

        //EP para obtener los datos acerca de la URL del distrito
        [HttpGet("[action]")]
        public async Task<IActionResult> DistrictUser()
        {
            var dataDistrict = await _context.ControlDistritos
                                    .FirstOrDefaultAsync();

            if (dataDistrict == null)
            {
                return Ok(new { status = "NaN"});
            }

            return Ok(new ControlDistritoViewModel
            {
                IdControlDistrito = dataDistrict.IdControlDistrito,
                DisId = dataDistrict.DisId,
                Direccion = dataDistrict.Direccion,
                NombreDistrito = dataDistrict.NombreDistrito
            });
        }


        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }


        // GET: api/Usuarios/TiempoSesion  
        //[Authorize(Roles = "Director, Administrador, Coordinador, Jurídico, Recepción, Facilitador, Notificador")]
        [HttpGet("[action]")]
        public async Task<IActionResult> TiempoCSesion()
        {
            try
            {
            var Tiempo = await _context.TiempoSesions
                                .FirstOrDefaultAsync();

            return Ok(new TiempodeSesionViewModel
            {
                idTiempo = Tiempo.IdTiempo,
                CerrarSesionMinutos = Tiempo.CerrarSesionMinutos,
                AvisoSesionMinutos = Tiempo.AvisoSesionMinutos,
            });
                

            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //Api para listar las clonaciones fallidas
        // GET: api/Usuarios/TiempoSesion  
        [Authorize(Roles = "Administrador,Director")]
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarClonacionesUsuariosFallidos()
        {
            try
            {
                var clonacionesFallidas = await _context.BitacoraUsuarios
                                          .Where(u => u.Status == false)
                                          .OrderBy(u => u.FechaRegistro)
                                          .ToListAsync();

                return Ok(clonacionesFallidas.Select(u => new BitacoraUsuariosViewModel
                {
                    IdBitacoraUsuario = u.IdBitacoraUsuario,
                    UsuarioId = u.UsuarioId,
                    Usuario = u.Usuario,
                    DescripcionMovimiento = u.DescripcionMovimiento,
                    ModuloServicioIdAnterior = u.ModuloServicioIdAnterior,
                    DistritoFallo = u.DistritoFallo,
                    ArregloDistritoFallo = u.ArregloDistritoFallo,
                    Proceso = u.Proceso,
                    Status = u.Status,
                    DistritoHaceMovimiento = u.DistritoHaceMovimiento,
                    DirSubHaceMovimiento = u.DirSubHaceMovimiento,
                    AgenciaHaceMovimiento = u.AgenciaHaceMovimiento,
                    ModuloHaceMovimiento = u.ModuloHaceMovimiento,
                    ModuloServicioIdHaceMovimiento = u.ModuloServicioIdHaceMovimiento,
                    UsuarioIdHaceMovimiento = u.UsuarioIdHaceMovimiento,
                    ResponsableCuentaHaceMovimiento = u.ResponsableCuentaHaceMovimiento,
                    FechaRegistro = u.FechaRegistro,
                }));


            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //Api para listar las clonaciones fallidas
        // GET: api/Usuarios/TiempoSesion  
        [Authorize(Roles = "Administrador,Director")]
        [HttpGet("[action]/{idModulo}")]
        public async Task<IActionResult> ListarClonacionesUsuariosFallidosPorModulo(Guid idModulo)
        {
            try
            {
                var clonacionesFallidas = await _context.BitacoraUsuarios
                                          .Where(u => u.Status == false)
                                          .Where(u => u.ModuloServicioIdHaceMovimiento == idModulo)
                                          .OrderBy(u => u.FechaRegistro)
                                          .ToListAsync();

                return Ok(clonacionesFallidas.Select(u => new BitacoraUsuariosViewModel
                {
                    IdBitacoraUsuario = u.IdBitacoraUsuario,
                    UsuarioId = u.UsuarioId,
                    Usuario = u.Usuario,
                    DescripcionMovimiento = u.DescripcionMovimiento,
                    ModuloServicioIdAnterior = u.ModuloServicioIdAnterior,
                    DistritoFallo = u.DistritoFallo,
                    ArregloDistritoFallo = u.ArregloDistritoFallo,
                    Proceso = u.Proceso,
                    Status = u.Status,
                    DistritoHaceMovimiento = u.DistritoHaceMovimiento,
                    DirSubHaceMovimiento = u.DirSubHaceMovimiento,
                    AgenciaHaceMovimiento = u.AgenciaHaceMovimiento,
                    ModuloHaceMovimiento = u.ModuloHaceMovimiento,
                    ModuloServicioIdHaceMovimiento = u.ModuloServicioIdHaceMovimiento,
                    UsuarioIdHaceMovimiento = u.UsuarioIdHaceMovimiento,
                    ResponsableCuentaHaceMovimiento = u.ResponsableCuentaHaceMovimiento,
                    FechaRegistro = u.FechaRegistro,
                }));


            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //Api para creary editar registros de bitacoras
        // POST: api/Usuarios/Crear
        [Authorize(Roles = "Administrador,Director")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearBitacoraUsuario([FromBody] BitacoraUsuariosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                //Se crea el registro de la bitacora el movimiento
                BitacoraUsuario bitacorausuario = new BitacoraUsuario
                {
                    UsuarioId = model.UsuarioId,
                    Usuario = model.Usuario,
                    DescripcionMovimiento = model.DescripcionMovimiento,
                    ModuloServicioIdAnterior = model.ModuloServicioIdAnterior,
                    DistritoFallo = model.DistritoFallo,
                    ArregloDistritoFallo = model.ArregloDistritoFallo,
                    Proceso = model.Proceso,
                    Status = model.Status,
                    DistritoHaceMovimiento = model.DistritoHaceMovimiento,
                    DirSubHaceMovimiento = model.DirSubHaceMovimiento,
                    AgenciaHaceMovimiento = model.AgenciaHaceMovimiento,
                    ModuloHaceMovimiento = model.ModuloHaceMovimiento,
                    ModuloServicioIdHaceMovimiento = model.ModuloServicioIdHaceMovimiento,
                    UsuarioIdHaceMovimiento = model.UsuarioIdHaceMovimiento,
                    ResponsableCuentaHaceMovimiento = model.ResponsableCuentaHaceMovimiento,
                    FechaRegistro = System.DateTime.Now,
                    RutaResponsiva=model.RutaResponsiva,

                };

                _context.BitacoraUsuarios.Add(bitacorausuario);
                await _context.SaveChangesAsync();

                //Se guarda en una variable local el id de la bitacora
                Guid idbitacora = bitacorausuario.IdBitacoraUsuario;

                //En caso de que la cloncion funciono correctamente en todos distritos buscara si anteriormente ese mismo usuario habia fallado y lo arreglara que ya esta clonado
                if (model.Status == true)
                {
                    var descartarFallas = await _context.BitacoraUsuarios
                                          .Where(u => u.UsuarioId == model.UsuarioId)
                                          .Where(u => u.Status == false)
                                          .FirstOrDefaultAsync();

                    //En caso de encontrar dicho registro solo se cambia el estatus a logrado para que ya no aparezca en lista de los fallados
                    if (descartarFallas != null)
                    {
                        descartarFallas.Status = model.Status;
                    }                   
                }
                //En caso de el registro haya fallado y que tambien haya un registro previo de fallo prevalecera el ultimo fallo y se dara por realizado el anterior
                else
                {
                    var descartarFallas = await _context.BitacoraUsuarios
                                          .Where(u => u.IdBitacoraUsuario != idbitacora)
                                          .Where(u => u.UsuarioId == model.UsuarioId)
                                          .Where(u => u.Status == false)
                                          .FirstOrDefaultAsync();

                    //En caso de encontrar dicho registro solo se cambia el estatus a logrado para que ya no aparezca en lista de los fallados
                    if (descartarFallas != null)
                    {
                        descartarFallas.Status = true;
                    }
                }

                    await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }
        //Api para editar si volvio a fallar o si se logro la clonacion desde el apartado de reintento
        // POST: api/Usuarios/Crear
        [Authorize(Roles = "Administrador,Director")]
        [HttpPut("[action]")]
        public async Task<IActionResult> EditarBitacoraUsuario([FromBody] EditarBitacoraUsuariosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var descartarFallas = await _context.BitacoraUsuarios
                                        .Where(u => u.IdBitacoraUsuario == model.IdBitacoraUsuario)
                                        .FirstOrDefaultAsync();
                if (descartarFallas == null)
                {
                    return NotFound("No hay registros");
                }

                //En caso de que todo se haya clonado correctamente solo dara por exitoso
                if (model.Status == true)
                {
                    descartarFallas.Status = model.Status;
                }
                //En caso de volver a fallar se tiene que remplazar ahora donde fallo para solo hacer la clonacion donde falta y se mantiene el estatus de fallo
                else
                {
                    descartarFallas.DistritoFallo = model.DistritoFallo;
                    descartarFallas.ArregloDistritoFallo = model.ArregloDistritoFallo;
                    descartarFallas.Status = model.Status;
                }

                await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }

            return Ok();
        }

        //Api para listar los movimientos por usuario
        // GET: api/Usuarios/TiempoSesion  
        [Authorize(Roles = "Administrador,Director")]
        [HttpGet("[action]/{idusuario}")]
        public async Task<IActionResult> ListarHistorialUsuario([FromRoute] Guid idusuario)
        {
            try
            {
                var clonacionesFallidas = await _context.BitacoraUsuarios
                                          .Where(u => u.UsuarioId == idusuario)
                                          .OrderBy(u => u.FechaRegistro)
                                          .ToListAsync();

                return Ok(clonacionesFallidas.Select(u => new BitacoraUsuariosViewModel
                {
                    IdBitacoraUsuario = u.IdBitacoraUsuario,
                    UsuarioId = u.UsuarioId,
                    Usuario = u.Usuario,
                    DescripcionMovimiento = u.DescripcionMovimiento,
                    ModuloServicioIdAnterior = u.ModuloServicioIdAnterior,
                    DistritoFallo = u.DistritoFallo,
                    ArregloDistritoFallo = u.ArregloDistritoFallo,
                    Proceso = u.Proceso,
                    Status = u.Status,
                    DistritoHaceMovimiento = u.DistritoHaceMovimiento,
                    DirSubHaceMovimiento = u.DirSubHaceMovimiento,
                    AgenciaHaceMovimiento = u.AgenciaHaceMovimiento,
                    ModuloHaceMovimiento = u.ModuloHaceMovimiento,
                    ModuloServicioIdHaceMovimiento = u.ModuloServicioIdHaceMovimiento,
                    UsuarioIdHaceMovimiento = u.UsuarioIdHaceMovimiento,
                    ResponsableCuentaHaceMovimiento = u.ResponsableCuentaHaceMovimiento,
                    FechaRegistro = u.FechaRegistro,
                    RutaResponsiva = u.RutaResponsiva,
                }));


            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4.1" });
                result.StatusCode = 402;
                return result;
            }
        }

        //Api para clonar los usuarios
        //POST: api/Usuarios/ClonarUsuario
        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador,Director")]
        public async Task<IActionResult> ClonarUsuarioCA([FromBody] ClonarUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Se busca la informacion del usuario
                var consultausuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.IdUsuario);

                //Se establece la conexion con la base de datos foranea
                var option = new DbContextOptionsBuilder<DbContextSIIGPP>().UseSqlServer(_config.GetConnectionString("C-" + model.IdDistrito.ToString().ToUpper())).Options;
                using (var ctx = new DbContextSIIGPP(option))
                {
                    //Una vez establecida la conecion se procede a corroborar la existencia del usuario
                    var usuario = await ctx.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.IdUsuario);

                    //Si no existe se insertara
                    if (usuario == null)
                    {
                        usuario = new Usuario();
                        ctx.Usuarios.Add(usuario);
                    }
                    //Si ya existe e actualizara los datos
                    usuario.IdUsuario = consultausuario.IdUsuario;
                    usuario.ModuloServicioId = consultausuario.ModuloServicioId;
                    usuario.RolId = consultausuario.RolId;
                    usuario.clave = consultausuario.clave;
                    usuario.usuario = consultausuario.usuario.ToLower();
                    usuario.nombre = consultausuario.nombre;
                    usuario.direccion = consultausuario.direccion;
                    usuario.telefono = consultausuario.telefono;
                    usuario.email = consultausuario.email.ToLower();
                    usuario.puesto = consultausuario.puesto;
                    usuario.password_hash = consultausuario.password_hash;
                    usuario.password_salt = consultausuario.password_salt;
                    usuario.condicion = consultausuario.condicion;
                    usuario.Titular = consultausuario.Titular;
                    usuario.ResponsableCuenta = consultausuario.ResponsableCuenta;

                    //Se hace la consulta en local de los paneles que se tienen permisos
                    var consultaPanel = await _context.PanelUsuarios.Where(a => a.UsuarioId == model.IdUsuario).ToListAsync();

                    //Solo se hace las clonacion en caso de haber datos
                    if (consultaPanel != null)
                    {
                        //Se busca en la base de datos foranea los paneles con permisos y se eliminan dichos registro
                        var panelDestino = await ctx.PanelUsuarios.Where(p => p.UsuarioId == model.IdUsuario).ToListAsync();

                        if (panelDestino.Any())
                        {
                            ctx.PanelUsuarios.RemoveRange(panelDestino);
                            await ctx.SaveChangesAsync();
                        }
                        //Se recole el arrelo dependiendo el numero de elementos que existan en local
                        foreach (PanelUsuario panelesUsuarios in consultaPanel)
                        {
                            // Validar que el PanelControlId exista en la base de datos destino
                            bool existePanelControl = await ctx.PanelControls.AnyAsync(pc => pc.Id == panelesUsuarios.PanelControlId);

                            if (!existePanelControl)
                            {
                                // Si no existe el PanelControl, se omite este registro
                                continue;
                            }

                            // Con la búsqueda previa recorres los elementos por id para que sean los mismos
                            var insertarPanelesUsuarios = await ctx.PanelUsuarios.FirstOrDefaultAsync(a => a.IdPanelUsuario == panelesUsuarios.IdPanelUsuario);

                            //En caso de no encontrar nada se insertara el dato, y si, es con first porque como se dijo antes de hace el recorrido uno por uno
                            if (insertarPanelesUsuarios == null)
                            {
                                insertarPanelesUsuarios = new PanelUsuario();
                                ctx.PanelUsuarios.Add(insertarPanelesUsuarios);
                            }
                            //En caso de existir se actualizaran los regitros
                            insertarPanelesUsuarios.IdPanelUsuario = panelesUsuarios.IdPanelUsuario;
                            insertarPanelesUsuarios.UsuarioId = panelesUsuarios.UsuarioId;
                            insertarPanelesUsuarios.PanelControlId = panelesUsuarios.PanelControlId;
                        }
                    }
                    //Se guardan los datos en las bases de datos foraneas
                    await ctx.SaveChangesAsync();
                }
                return Ok();
              
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new { mensaje = ex.Message, detail = ex.InnerException == null ? "SIN EXCEPCION INTERNA" : ex.InnerException.Message, version = "version 1.4" });
                result.StatusCode = 402;
                return result;
            }
        }
        //[HttpPost("Post/{nombre}" )]
        [HttpPost("[action]/{nombreCarpeta}/{nombreArchivo}")]
        public async Task<IActionResult> GuardarRsponsiva(IFormFile file, [FromRoute] string nombreCarpeta, string nombreArchivo)
        {
            try
            {

                //***********************************************************************************
                string patchp = Path.Combine(_environment.ContentRootPath, "Responsivas\\" + nombreCarpeta);

                if (!Directory.Exists(patchp))
                    Directory.CreateDirectory(patchp);

                string extension;

                extension = Path.GetExtension(file.FileName);


                var filePath = Path.Combine(_environment.ContentRootPath, "Responsivas\\" + nombreCarpeta, nombreArchivo + extension);
                var path = ("https://" + HttpContext.Request.Host.Value + "/Responsivas/" + nombreCarpeta + "/" + nombreArchivo + extension);
                if (file.Length > 0)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);


                return Ok(new { count = 1, ruta = path });


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
        //ValidarToken
        public bool EsTokenValido(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.Zero 
                }, out SecurityToken validatedToken);

                // Si llega aquí, el token es válido y no ha expirado
                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                // Token ha expirado
                return false;
            }
            catch (Exception)
            {
                // Token inválido por otra razón
                return false;
            }
        }

        [HttpGet("[action]")]
        public IActionResult ValidarToken2()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (EsTokenValido(token))
                return Ok(new { valido = true });
            else
                return Unauthorized();
        }

    }
}
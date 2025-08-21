using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System.IO;

namespace CentenarioLibrary
{
    public class ResponseCacheMiddleware
    {
        private readonly RequestDelegate _next; //Referencia al siguiente middleware en la tubería (pipeline).
        private readonly IDistributedCache _cache; //Servicio de caché distribuida (en éste caso es Redis).

        //Inyecta el siguiente middleware y el servicio de caché.
        public ResponseCacheMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }


        //Este método se ejecuta en cada request.
        public async Task InvokeAsync(HttpContext context)
        {
            //Crea una llave de caché basada en la URL solicitada (/api/usuarios → CACHE_/api/usuarios).
            var cacheKey = $"CACHE_{context.Request.Path}";

            //Busca si ya existe en caché
            var cachedResponse = await _cache.GetStringAsync(cacheKey);

            //Si la respuesta ya está en caché en Redis, la devuelve directo.
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cachedResponse);
                Console.WriteLine("Datos obtenidos desde caché en memoria");
                return;
            }

            //Capturar la respuesta si no está en caché
            Console.WriteLine("Consultando BD/API porque no está en caché...");
            //Guarda el stream original de la respuesta.
            var originalBodyStream = context.Response.Body;
            //Reemplaza temporalmente el Response.Body con un MemoryStream.
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            //Llama al siguiente middleware/controlador, que genera la respuesta normalmente, pero en lugar de mandarla al cliente, se queda en el memoryStream.
            await _next(context);

            //Guarda la respuesta en Redis
            //Lee el contenido de la respuesta desde memoryStream.
            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(memoryStream).ReadToEnd();
            //Lo guarda en Redis bajo la llave cacheKey.
            await _cache.SetStringAsync(cacheKey, responseBody,
                new DistributedCacheEntryOptions
                {
                    //Establece un tiempo de vida de 5 minutos
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            //Devolver la respuesta al cliente
            //Regresa al inicio del memoryStream.
            memoryStream.Seek(0, SeekOrigin.Begin);
            //Copia la respuesta capturada al Response.Body original.
            await memoryStream.CopyToAsync(originalBodyStream);
        }
    }
}
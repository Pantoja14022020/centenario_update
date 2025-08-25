using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CentenarioLibrary
{
    public class ResponseCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private readonly Dictionary<string, int> _durations;

        public ResponseCacheMiddleware(RequestDelegate next, IDistributedCache cache, IConfiguration configuration)
        {
            _next = next;
            _cache = cache;

            // Cargar CacheDurations de appsettings.json en un diccionario case-insensitive
            _durations = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            configuration.GetSection("CacheDurations").Bind(_durations);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = Normalize(context.Request.Path.Value ?? "/");

            // 🔑 Clave de caché (incluye querystring si lo deseas)
            // var cacheKey = $"CACHE_{path}{context.Request.QueryString}";
            var cacheKey = $"CACHE_{path}";

            // 1) Intentar leer de caché
            var cachedResponse = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cachedResponse);
                Console.WriteLine($"[CACHE HIT] {path}");
                return;
            }

            Console.WriteLine($"[CACHE MISS] {path}");
            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            // 2) Ejecutar pipeline
            await _next(context);

            // 3) Leer respuesta generada
            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            // 4) Resolver TTL desde appsettings
            var seconds = ResolveTtlSeconds(path);
            Console.WriteLine($"[CACHE SET] {path} -> {seconds}s");

            await _cache.SetStringAsync(cacheKey, responseBody,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds)
                });

            // 5) Devolver la respuesta al cliente
            memoryStream.Seek(0, SeekOrigin.Begin);
            context.Response.Body = originalBodyStream;
            await memoryStream.CopyToAsync(originalBodyStream);
        }

        private static string Normalize(string path)
            => (path ?? "/").TrimEnd('/').ToLowerInvariant();

        private int ResolveTtlSeconds(string normalizedPath)
        {
            // 1) Match exacto
            if (_durations.TryGetValue(normalizedPath, out var ttlExact))
                return ttlExact;

            // 2) Match con comodines "/*"
            foreach (var kv in _durations)
            {
                if (kv.Key.EndsWith("/*", StringComparison.Ordinal))
                {
                    var prefix = kv.Key[..^2].TrimEnd('/');
                    if (normalizedPath.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                        return kv.Value;
                }
            }

            // 3) Default
            return 30;
        }
    }
}
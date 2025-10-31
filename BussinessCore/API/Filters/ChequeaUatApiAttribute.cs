using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClickCore.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ChequeaUatApiAttribute : Attribute, IAsyncActionFilter
    {
        private readonly SmartClickContext _context;
        public ChequeaUatApiAttribute(SmartClickContext context)
        {
            _context = context;
        }

        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var bodyStr = "";
            var req = context.HttpContext.Request;
            var jsonBody = new JObject();

            var method = req.Method?.ToUpperInvariant();

            // GET: UAT por header
            if (method == "GET")
            {
                if (!context.HttpContext.Request.Headers.TryGetValue("UAT", out var uat))
                {
                    Log.Error("Consulta de APi sin UAT");
                    context.Result = new BadRequestObjectResult(new RespuestaAPI { Mensaje = "Consulta API sin UAT" });
                    return;
                }

                if (_context.UAT.FirstOrDefault(x => x.Token == uat) == null)
                {
                    Log.Error("Consulta de APi con UAT invalida");
                    context.Result = new BadRequestObjectResult(new RespuestaAPI { Mensaje = "UAT invalida" });
                    return;
                }

                Log.Warning($"Request API: {req.HttpContext.Connection.RemoteIpAddress} - {req.Path} -  (UAT en header)");
                await next();
                return;
            }

            // Para los métodos que pueden llevar body (POST, PUT, PATCH, DELETE, etc.)
            if (method == "POST" || method == "PUT" || method == "PATCH" || method == "DELETE")
            {
                try
                {
                    req.EnableRewind();
                }
                catch
                {
                    // EnableRewind puede lanzar si no está disponible; ignorar para seguir intentando leer
                }

                // Leer body de forma segura
                try
                {
                    req.Body.Position = 0;
                }
                catch { /* ignore if not seekable */ }

                using (var reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                }

                // Resetear la posición para que el model binder pueda leerlo después
                try
                {
                    req.Body.Position = 0;
                }
                catch { /* ignore if not seekable */ }

                if (string.IsNullOrWhiteSpace(bodyStr))
                {
                    Log.Error("Request API sin body");
                    context.Result = new BadRequestObjectResult(new RespuestaAPI { Mensaje = "Request sin body o body vacío" });
                    return;
                }

                // Parsear JSON de forma segura
                try
                {
                    jsonBody = JObject.Parse(bodyStr);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Body JSON inválido");
                    context.Result = new BadRequestObjectResult(new RespuestaAPI { Mensaje = "JSON inválido en el body", Status = 400 });
                    return;
                }

                var uatPostToken = jsonBody.GetValue("UAT") ?? jsonBody.GetValue("uat") ?? "";

                if (uatPostToken == null || string.IsNullOrWhiteSpace(uatPostToken.ToString()))
                {
                    Log.Error("Consulta de APi sin UAT en body");
                    context.Result = new BadRequestObjectResult(new RespuestaAPI { Mensaje = "UAT requerido en body", Status = 403 });
                    return;
                }

                var uatPost = uatPostToken.ToString().Trim();
                if (_context.UAT.FirstOrDefault(x => x.Token == uatPost) == null)
                {
                    Log.Error("Consulta de APi sin UAT o UAT invalida");
                    context.Result = new BadRequestObjectResult(new RespuestaAPI { UAT = uatPost, Mensaje = "UAT invalida", Status = 403 });
                    return;
                }

                Log.Warning($"Request API: {req.HttpContext.Connection.RemoteIpAddress} - {req.Path} -  {jsonBody}");
                await next();
                return;
            }

            // Otros métodos: seguir sin validar body
            Log.Warning($"Request API: {req.HttpContext.Connection.RemoteIpAddress} - {req.Path} -  {jsonBody}");
            await next();
        }
    }
}

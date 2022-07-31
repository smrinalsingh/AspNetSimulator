using System.Net;
using System.Text;
using AspNetSimulator.Data.Contracts;

namespace AspNetSimulator.Data.Implementations
{
    internal class RequestHandler: IRequestHandler
    {
        IRoute _routeConfig;
        public RequestHandler(IRoute routeConfig)
        {
            _routeConfig = routeConfig;
        }

        public void Handle(HttpListenerContext context)
        {
            string? reqController = GetControllerName(context);
            if (reqController == null)
            {
                //context.Response.OutputStream.Write(Encoding.UTF8.GetBytes("Not Found: " + reqPath));
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.Close();
            }

            else
            {
                context.Response.OutputStream.Write(Encoding.UTF8.GetBytes("Found Controller: " + reqController));
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.Close();
            }
        }

        private string? GetControllerName(HttpListenerContext context)
        {
            var controllerBase = typeof(IControllerBase);
            var controllers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => controllerBase.IsAssignableFrom(p))
                .Select(c => c.Name)
                .ToList();

            List<string>? reqPath = context.Request?.Url?.AbsolutePath?.ToLower()[1..].Split("/").ToList();

            string? curController = "";

            if (reqPath?.Count > 0)
            {
                if (reqPath?[0] == "")
                    curController = _routeConfig.DefaultController.ToLower();
                else
                    curController = reqPath?[0];
            }

            string? foundPath = controllers?.FirstOrDefault(c => c.Contains(curController, StringComparison.CurrentCultureIgnoreCase));
            return foundPath;
        }
    }
}
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
            Type? reqController = GetControllerName(context);
            if (reqController != null)
            {
                string actionName = GetActionName(context);
                string? retValue = reqController?
                    .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)?
                    .Where(x => x.Name.Equals(actionName, StringComparison.InvariantCultureIgnoreCase))?
                    .FirstOrDefault()?
                    .Invoke(Activator.CreateInstance(reqController), null)?
                    .ToString();

                    context.Response.OutputStream.Write(Encoding.UTF8.GetBytes("Found Controller: " + reqController + ". Got Value from method: " + retValue));
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.Close();
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.Close();
            }
        }

        private Type? GetControllerName(HttpListenerContext context)
        {
            var controllerBase = typeof(IControllerBase);
            var controllers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => controllerBase.IsAssignableFrom(p))
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

            Type? foundPath = controllers?.FirstOrDefault(c => c.Name.Contains(curController, StringComparison.CurrentCultureIgnoreCase));
            return foundPath;
        }

        private string GetActionName(HttpListenerContext context)
        {
            List<string> reqPath = context.Request?.Url?.AbsolutePath?.ToLower()[1..].Split("/").ToList();
            if (reqPath.Count > 1)
            {
                if (reqPath[1] == "")
                    return _routeConfig.DefaultAction.ToLower();
                else
                    return reqPath[1];
            }
            else
                return _routeConfig.DefaultAction.ToLower();
        }
    }
}
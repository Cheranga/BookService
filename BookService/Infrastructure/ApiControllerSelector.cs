using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace BookService.Infrastructure
{
    /// <summary>
    /// This will select the appropriate controller depending on the version provided (or not)
    /// </summary>
    public class ApiControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">The HTTP configuration</param>
        public ApiControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Gets the controller name. If a version is provided in the 'accept' header, it will be taken into consideration
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The controller name, depending on the version</returns>
        public override string GetControllerName(HttpRequestMessage request)
        {
            var controllerName = request.GetRouteData().Values["controller"].ToString();

            var requiredVersion = GetVersionFromHeader(request);

            if (string.IsNullOrEmpty(requiredVersion))
            {
                return base.GetControllerName(request);
            }

            return $"{controllerName}v{requiredVersion}";
        }

        private string GetVersionFromHeader(HttpRequestMessage request)
        {
            var acceptHeader = request.Headers.Accept;
            foreach (var header in acceptHeader)
            {
                if (header.MediaType == "application/json")
                {
                    var version = header.Parameters.FirstOrDefault(x => x.Name.Equals("version", StringComparison.OrdinalIgnoreCase));
                    return version?.Value;
                }
            }

            return null;
        }
    }
}
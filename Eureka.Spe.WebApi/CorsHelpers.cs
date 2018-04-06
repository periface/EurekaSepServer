using System.Web.Http;

namespace Eureka.Spe
{
    public class CorsHelpers
    {
        public static class WebApiConfig
        {
            public static void Register(HttpConfiguration config)
            {
                config.EnableCors();
            }
        }
    }
}

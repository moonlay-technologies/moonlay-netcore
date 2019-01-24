using Core;

namespace Core.Mvc
{
    public class WebApiContext : WorkContext, IWebApiContext
    {
        public string ApiVersion { get; }
    }
}

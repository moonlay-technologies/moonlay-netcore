using Core;

namespace Core.Mvc
{
    public class WebApiContext : WorkContext, IWebApiContext
    {
        public string ApiVersion { get; private set; }
        public WebApiContext SetApiVersion(string value)
        {
            return this;
        }
    }
}

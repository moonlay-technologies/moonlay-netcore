namespace Core.Mvc
{
    public interface IWorkContext
    {
        string CurrentUser { get; }
    }

    public interface IWebApiContext : IWorkContext
    {
        string ApiVersion { get; }
    }
}
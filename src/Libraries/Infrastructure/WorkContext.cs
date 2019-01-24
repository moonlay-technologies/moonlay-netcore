using Core;

namespace Core.Mvc
{
    public class WorkContext : IWorkContext
    {
        public virtual string CurrentUser { get; }
    }
}

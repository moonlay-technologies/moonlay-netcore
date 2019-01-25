using Core;

namespace Core.Mvc
{
    public class WorkContext : IWorkContext
    {
        public virtual string CurrentUser { get; private set; }
        public WorkContext SetCurrentUser(string value)
        {
            CurrentUser = value;
            return this;
        }
    }
}

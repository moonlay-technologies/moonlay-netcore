using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.ExtCore.Mvc.Abstractions
{
    public interface IWorkContext
    {
        string CurrentUser { get; }
    }
}

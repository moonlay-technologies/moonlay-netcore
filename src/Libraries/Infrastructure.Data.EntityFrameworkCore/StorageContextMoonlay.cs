using ExtCore.Data.EntityFramework;
using Microsoft.Extensions.Options;

namespace Core.ExtCore.EntityFrameworkCore
{
    public class StorageContextMoonlay : StorageContextBase
    {
        public StorageContextMoonlay(IOptions<StorageContextOptions> options) : base(options)
        {
        }
    }
}
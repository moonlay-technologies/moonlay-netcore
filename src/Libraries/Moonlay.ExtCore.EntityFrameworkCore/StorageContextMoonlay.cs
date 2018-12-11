using ExtCore.Data.Abstractions;
using ExtCore.Data.EntityFramework;
using Microsoft.Extensions.Options;
using Moonlay.Domain;

namespace Moonlay.ExtCore.EntityFrameworkCore
{
    public class StorageContextMoonlay : StorageContextBase, IUnitOfWork
    {
        public StorageContextMoonlay(IOptions<StorageContextOptions> options) : base(options)
        {
        }
    }
}

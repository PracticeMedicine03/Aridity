using System;
using PracticeMedicine.Aridity.Services;

namespace Aridity.Services
{
    [AridityService("Aridity.SUpdate", 
        FallbackImplementation = typeof(FallbackUpdateService))]
    public interface IUpdateService
    {
        void CheckForUpdates();
    }

    sealed class FallbackUpdateService : SquirrelUpdaterUpdateService
    {
        public FallbackUpdateService() : base() { }
    }
}

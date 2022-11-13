using Infrastructure;

namespace Tests.Common
{
    public abstract class TestServicesBase : IDisposable
    {
        protected readonly WebSkladDbContext Context;

        protected TestServicesBase()
        {
            Context = ContextCreator.CreateContext();
        }


        public void Dispose()
        {
            ContextCreator.Destroy(Context);
        }
    }
}

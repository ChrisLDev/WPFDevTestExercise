using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public class ServiceScope<T> : IServiceScope<T> where T : class
    {
        private readonly IServiceScope _scope;

        public ServiceScope(IServiceScope scope) =>
            _scope = scope;

        /// <inheritdoc/>
        public T GetRequiredServices() => _scope.ServiceProvider.GetRequiredService<T>();

        #region dispose
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool calledFromCodeNotFromGarbageCollector)
        {
            if (_disposed)
                return;

            if (calledFromCodeNotFromGarbageCollector)
            {
                _scope.Dispose();
            }

            _disposed = true;
        }

        ~ServiceScope() { Dispose(false); }

        #endregion
    }
}

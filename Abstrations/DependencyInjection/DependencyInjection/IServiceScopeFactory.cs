namespace DependencyInjection
{
    /// <summary>
    /// A factory interface for creation of a service within a scope.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IServiceScopeFactory<T> where T : class
    {
        /// <summary>
        /// Create an IServiceScope which contains an IServiceProvider used to resolve dependencies from a newly created scope.
        /// </summary>
        /// <returns>An IServiceScope controlling the lifetime of the scope, Once disposed,
        /// any scope services that have been resolved from the serviceProvider will also be dispose</returns>
        IServiceScope<T> CreateScope();
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace Espeon.souchy.celebi.espeon
{
    public class ScopeID
    {
        private static readonly IUIdGenerator scopeIdGenerator = new UIdGenerator();
        public IID id { get; init; } = scopeIdGenerator.next();

        public static implicit operator IID(ScopeID i) => i.id;
    }

    public class Scopes
    {
        private static IHost host;
        public static readonly Dictionary<IID, IServiceScope> scopes = new Dictionary<IID, IServiceScope>();

        public static void init(string[] args, Action<IServiceCollection> configureDelegate)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices(configureDelegate);
            host = builder.Build();
            host.RunAsync();
        }

        public static ScopeID NewScope()
        {
            IServiceScope scope = host.Services.CreateScope();
            ScopeID scopeid = scope.ServiceProvider.GetRequiredService<ScopeID>();
            scopes.Add(scopeid, scope);
            return scopeid;
        }

        public static T? GetScoped<T>(IID scopeId)
        {
            return scopes[scopeId].ServiceProvider.GetService<T>();
        }

        public static T GetRequiredScoped<T>(IID scopeId) where T : notnull
        {
            return scopes[scopeId].ServiceProvider.GetRequiredService<T>();
        }

        public static IUIdGenerator GetUIdGenerator(IID scopeID)
        {
            return scopes[scopeID].ServiceProvider.GetRequiredService<IUIdGenerator>();
        }

        public static void DisposeIID(IID scopeId, IID entityId)
        {
            try
            {
                scopes[scopeId].ServiceProvider.GetRequiredService<IUIdGenerator>().dispose(entityId);
            }
            catch (Exception) { }
        }

        public void Dispose()
        {
            foreach (var scope in scopes.Values) scope.Dispose();
            scopes.Clear();
        }
    }
}
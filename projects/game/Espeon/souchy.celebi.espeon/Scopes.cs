using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.espeon
{
    public class ScopeID
    {
        //private static readonly IUIdGenerator scopeIdGenerator = new UIdGenerator();
        //public IID id { get; init; } = scopeIdGenerator.next();
        public ObjectId id { get; init; } = ObjectId.GenerateNewId();

        public static implicit operator ObjectId(ScopeID i) => i.id;
    }

    public class Scopes
    {
        public static readonly Dictionary<ObjectId, IServiceScope> scopes = new Dictionary<ObjectId, IServiceScope>();

        public static ScopeID NewScope()
        {
            IServiceScope scope = Espeon.host.Services.CreateScope();
            ScopeID scopeid = scope.ServiceProvider.GetRequiredService<ScopeID>();
            scopes.Add(scopeid, scope);
            return scopeid;
        }

        public static T? GetScoped<T>(ObjectId scopeId)
        {
            return scopes[scopeId].ServiceProvider.GetService<T>();
        }

        public static T GetRequiredScoped<T>(ObjectId scopeId) where T : notnull
        {
            return scopes[scopeId].ServiceProvider.GetRequiredService<T>();
        }

        //public static IUIdGenerator GetUIdGenerator(ObjectId scopeID)
        //{
        //    return scopes[scopeID].ServiceProvider.GetRequiredService<IUIdGenerator>();
        //}
        //public static void DisposeIID(ObjectId scopeId, ObjectId entityId)
        //{
        //    try
        //    {
        //        scopes[scopeId].ServiceProvider.GetRequiredService<IUIdGenerator>().dispose(entityId);
        //    }
        //    catch (Exception) { }
        //}

        public void Dispose()
        {
            //Eevee.DisposeEventBus(this);
            foreach (var scope in scopes.Values) scope.Dispose();
            scopes.Clear();
        }
    }
}

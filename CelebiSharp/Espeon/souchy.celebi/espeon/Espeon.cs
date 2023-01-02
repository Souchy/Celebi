using Espeon.souchy.celebi.eeevee.impl;
using Espeon.souchy.celebi.eeevee.impl.controllers;
using Espeon.souchy.celebi.espeon.eevee.impl.controllers;
using Espeon.souchy.celebi.espeon.eevee.impl.objects;
using Espeon.souchy.celebi.espeon.impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.io;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Espeon.souchy.celebi.espeon
{

    /// <summary>
    /// Espeon is the server
    /// </summary>
    internal class Espeon
    {

        public static readonly IDiamondModels models = new DiamondModels();
        //public static readonly IRedInstances instances = new RedInstances();
        //public static readonly IUIdGenerator uIdGenerator = new UIdGenerator();
        public static readonly Dictionary<IID, IServiceScope> scopes = new Dictionary<IID, IServiceScope>();

        public static IHost host;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices(configureServices);
            host = builder.Build();
            

            var board = host.Services.GetService<IBoard>();
            Console.WriteLine("board id: " + board?.entityUid);
            
            //host.Services.GetService()
            IServiceScope scope = host.Services.CreateScope();
            scope.ServiceProvider.GetService<IUIdGenerator>();
            
            host.RunAsync();

            newFight();
        }

        public static void newFight()
        {
            //IID uid = host.Services.GetRequiredService<IUIdGenerator>().next();
            IServiceScope scope = host.Services.CreateScope();
            {
                //IUIdGenerator uidGenerator = scope.ServiceProvider.GetRequiredService<IUIdGenerator>();
                ScopeID scopeid = scope.ServiceProvider.GetRequiredService<ScopeID>();

                scopes.Add(scopeid, scope);
                //scopeid.id = uidGenerator.next();
                //instances.fights.Add(scopeid.id, fight);

                IFight fight = scope.ServiceProvider.GetRequiredService<IFight>();
                new Example(fight);
                //IBoard board = scope.ServiceProvider.GetRequiredService<IBoard>();
                //Console.WriteLine("board id: " + board?.entityUid);


                // dispose
                scopes.Remove(scopeid);
                scope.Dispose();
            }

            // create fight
            // create board
            // create players?
            // create 6 creatures
            // create all their spells
            // create map instance
            // 
        }


        static void configureServices(IServiceCollection services)
        {
            services.AddScoped<IUIdGenerator, UIdGenerator>();
            services.AddScoped<ScopeID, ScopeID>();
            services.AddScoped<IRedInstances, RedInstances>();
            services.AddScoped<IFight, Fight>(); 
            services.AddScoped<IBoard, Board>();

            //services.AddTransient<IMapModel, MapModel>();
            //services.AddTransient<ICreatureModel, CreatureModel>();
            //services.AddTransient<ISpellModel, SpellModel>();
            //services.AddTransient<IEffectModel, EffectModel>();

            //services.AddTransient<IMapSkin, MapSkin>();
            //services.AddTransient<ICreatureSKin, CreatureSkin>();
            //services.AddTransient<ISpellSkin, SpellSkin>();
            //services.AddTransient<IEffectSkin, EffectSkin>();

            //services.AddTransient<IMap, Map>();
            services.AddTransient<ICreature, Creature>();
            services.AddTransient<ISpell, Spell>();
            services.AddTransient<IEffect, Effect>();
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
            scopes[scopeId].ServiceProvider.GetRequiredService<IUIdGenerator>().dispose(entityId);
        }

    }


    public class ScopeID
    {
        public IID id { get; init; }
        public ScopeID(IUIdGenerator uIdGenerator)
        {
            this.id = uIdGenerator.next();
        }
        public static implicit operator IID(ScopeID i) => i.id;
    }

}
using Espeon.souchy.celebi.eeevee.impl;
using Espeon.souchy.celebi.eeevee.impl.controllers;
using Espeon.souchy.celebi.espeon.eevee.impl.controllers;
using Espeon.souchy.celebi.espeon.eevee.impl.objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.io;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using Microsoft.AspNetCore.Hosting;

namespace Espeon.souchy.celebi.espeon
{
    /// <summary>
    /// Espeon is the server
    /// </summary>
    internal class Espeon
    {
        public static readonly IDiamondModels models = new DiamondModels();

        internal static IHost host;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            /*Scopes.*/init(args, configureServices);

            //new Example(Scopes.GetRequiredScoped<IFight>(Scopes.NewScope()));
            //new Example(Scopes.GetRequiredScoped<IFight>(Scopes.NewScope()));

            new Espeon();
        }

        public Espeon()
        {
            new PlayfabServer();
        }

        public static void init(string[] args, Action<IServiceCollection> configureDelegate)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices(configureDelegate);
            host = builder.Build();
            host.RunAsync();
        }

        public static void configureServices(IServiceCollection services)
        {
            services.AddScoped<ScopeID, ScopeID>();

            services.AddScoped<IUIdGenerator, UIdGenerator>();
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
            services.AddTransient<IPlayer, Player>();
            services.AddTransient<IStats, Stats>();
            services.AddTransient<ICreature, Creature>();
            services.AddTransient<ISpell, Spell>();
            services.AddTransient<IEffect, Effect>();
        }


    }
}
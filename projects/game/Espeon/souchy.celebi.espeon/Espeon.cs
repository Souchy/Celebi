﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using Microsoft.AspNetCore.Hosting;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.espeon.eevee.impl.controllers;

namespace souchy.celebi.espeon
{
    /// <summary>
    /// Espeon is the server
    /// </summary>
    internal class Espeon
    {
        //public static readonly IDiamondModels models = new DiamondModels();

        internal static IHost host;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            /*Scopes.*/
            init(args, configureServices);

            //new Example(Scopes.GetRequiredScoped<IFight>(Scopes.NewScope()));
            //new Example(Scopes.GetRequiredScoped<IFight>(Scopes.NewScope()));

            new Espeon();
        }

        public Espeon()
        {
            //new PlayfabServer();
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
            //services.AddScoped<IFight, RedInstances>();
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
            //services.AddTransient<ICreature, Creature>();
            //services.AddTransient<ISpell, Spell>();
            //services.AddTransient<IEffect, Effect>();
        }


    }
}

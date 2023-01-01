using Espeon.souchy.celebi.eeevee.impl;
using Espeon.souchy.celebi.eeevee.impl.controllers;
using Espeon.souchy.celebi.espeon.impl.eevee.controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.io;
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
        public static readonly IRedInstances instances = new RedInstances();
        public static readonly IUIdGenerator uIdGenerator = new UIdGenerator();

        public static IHost host;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices(configureServices);
            host = builder.Build();

            var board = (IBoard) host.Services.GetService(typeof(IBoard));
            Console.WriteLine("board id: " + board.entityUid);

            //host.Services.GetService()

            host.Run();
        }


        static void configureServices(IServiceCollection services)
        {
            services.AddTransient<IFight, Fight>();
            services.AddTransient<IBoard, Board>();
            services.AddSingleton<IUIdGenerator, UIdGenerator>();
        }

    }




}
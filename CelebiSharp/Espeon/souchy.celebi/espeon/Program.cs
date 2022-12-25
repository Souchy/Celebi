using Espeon.souchy.celebi.espeon.impl.eevee.controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using souchy.celebi.eevee.face.controllers;
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
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices(configureServices);
            var host = builder.Build();
            host.Services.GetService(typeof(IBoard));
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
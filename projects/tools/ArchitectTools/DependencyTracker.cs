using Microsoft.Extensions.Logging;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.spark;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectTools
{
    public class HierarchyTracer : App
    {
        public string Keyword => "d";
        private ILogger<HierarchyTracer> logger;

        public HierarchyTracer() //ILogger<DependencyTracker> logger)
        {
            //this.logger = logger;
            var fac = new LoggerFactory();
            this.logger = new Logger<HierarchyTracer>(fac);
        }

        public void run()
        {
            //Console.WriteLine("");
            Console.WriteLine("\n----------------------------");
            Console.WriteLine($"Welcome to {nameof(HierarchyTracer)}");

            string? input = Console.ReadLine();
            if (input == "x") return;

            var interf = ForName(input!);
            Console.WriteLine("Type input: " + interf?.FullName);

            if (interf == null) return;

            Console.WriteLine("Children: ");
            var subs = assemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                {
                    if (t.BaseType != null)
                    {
                        return t.GetInterfaces().Except(t.BaseType.GetInterfaces()).Contains(interf);
                    }
                    return t.GetInterfaces().Contains(interf);
                })
                //.BaseType == interf || interf.BaseType == t) //interf.IsAssignableFrom(t) && t.IsClass && t.)
                ;
            Console.WriteLine($"classes : [{string.Join(", ", subs.Where(s => s.IsClass).Select(s => s.Name).ToArray())}]\n");
            Console.WriteLine();
            Console.WriteLine($"!classes: [{string.Join(", ", subs.Where(s => !s.IsClass).Select(s => s.Name).ToArray())}]");
            
            input = Console.ReadLine();
            if (input == "c")
                openVSCode(subs.Where(s => s.IsClass));

            run();
        }

        private void openVSCode(IEnumerable<Type> types)
        {
            IEnumerable<string> files = types.Select(t =>
            {
                var files = Directory.GetFiles("C:\\Robyn\\godot\\Celebi\\projects\\", $"{t.Name}.cs", SearchOption.AllDirectories);
                return files.FirstOrDefault();
            }).Where(s => s != null)!;
            //TreeNode.NodeMap.Values.Where(n => n.Doc != null && n.Doc.GetType() == typeof(T)).Select(n => n.Doc.Path).Distinct();
            
            //if (!files.Any())
            //{
            //    Util.PrintLine($"There are 0 {typeof(T).Name} files found yet. Run a scan first.");
            //    return;
            //}
            foreach (string path in files)
            {
                Process process = Process.Start("CMD.exe", $"/C code {path}"); // {Util.Enquote(path)}");
                process.Dispose();
            }
        }

        private Type? ForName(string name)
        {
            //a.GetType(name)
            return assemblies()
                 .SelectMany(a => a.GetTypes())
                 .Where(t =>
                 {
                     //Console.WriteLine("namespace: " + t.Namespace);
                     return t.Namespace == null ? false : t.Namespace.StartsWith("souchy");
                 })
                 .Where(t =>
                 {
                     //Console.WriteLine($"check: {name} - {t.Name}");
                     return string.Equals(name, t.Name, StringComparison.OrdinalIgnoreCase);
                 })
                 .FirstOrDefault();
        }

        private Assembly[] assemblies()
        {
            return new Assembly[]{ 
                Assembly.GetAssembly(typeof(IEntity)), 
                Assembly.GetAssembly(typeof(Spark)) 
            };
            //return AppDomain.CurrentDomain.GetAssemblies();
        }

    }
}

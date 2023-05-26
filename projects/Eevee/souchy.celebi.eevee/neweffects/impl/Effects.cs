using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl
{
    public static class Effects
    {


        public static IEnumerable<Type> schemaTypes = typeof(IEffectSchema).Assembly.GetTypes()
            .Where(t => t.IsClass && t.IsAssignableTo(typeof(IEffectSchema)));

        public static IEnumerable<IEffectScript> scripts = typeof(IEffectScript).Assembly.GetTypes()
            .Where(t => t.IsClass && t.IsAssignableTo(typeof(IEffectScript)))
            .Select(t => (IEffectScript) Activator.CreateInstance(t));

        public static IEffectScript GetScript(Type schemaType) => scripts.First(s => s.SchemaType == schemaType);

        static Effects()
        {
            if (scripts.Count() != scripts.DistinctBy(s => s.SchemaType).Count())
                throw new Exception("Multiple effect Scripts implementations for the same effect Schema.");
            foreach (var s in schemaTypes)
                if (scripts.FirstOrDefault(t => t.SchemaType == s) == null)
                    throw new Exception($"Missing Script for Schema {s.FullName}"); 
            foreach(var s in scripts)
                if(schemaTypes.FirstOrDefault(t => t == s.SchemaType) == null)
                    throw new Exception($"Missing Schema for Script {s.GetType().FullName}");
        }

    }
}

﻿using Umbreon.sapphire.ui;
using Godot;
using SimpleInjector;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Container = SimpleInjector.Container;
//using CreatureData = souchy.celebi.eevee.impl.objects.Creature;

namespace Umbreon.src
{

    static class Registry
    {
        public static void Register(Container container)
        {
            GD.Print("DI registring");
            // do registrations here

            container.Register<IUIdGenerator, UIdGenerator>(Lifestyle.Singleton);
            //container.Register<ICreature, Creature>(Lifestyle.Transient);
        }
    }

    #region Low level
    public class InjectAttribute : Attribute { }

    public partial class DependencyInjectionSystem : Node
    {
        private Container container;
        public override void _EnterTree()
        {
            base._EnterTree();
            container = new Container();
            Registry.Register(container);
        }
        public object Resolve(Type type)
        {
            return container.GetInstance(type);
        }
    }

    public static class NodeExtensions
    {
        public static void Inject(this Node node)
        {
            var disPath = "/root/DependencyInjectionSystem";
            var dis = node.GetNode<DependencyInjectionSystem>(disPath);
            var at = typeof(InjectAttribute);
            var fields = node.GetType()
                .GetRuntimeFields()
                .Where(f => f.GetCustomAttributes(at, true).Any());
            foreach (var field in fields)
            {
                var obj = dis.Resolve(field.FieldType);
                try
                {
                    field.SetValue(node, obj);
                }
                catch (InvalidCastException)
                {
                    GD.PrintErr($"Error converting value " +
                        $"{obj} ({obj.GetType()})" +
                        $" to {field.FieldType}");
                    throw;
                }
            }
        }
    }
    #endregion

}

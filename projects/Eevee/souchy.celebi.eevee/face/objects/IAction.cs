using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects
{
    public interface IAction
    {
        public IFight fight { get; set; }
        public IID caster { get; set; }
        public IID targetCell { get; set; }
    }

    public interface IActionSpell : IAction
    {
        public IID spell { get; set; }
    }
    
    public interface ISubActionEffect : IAction
    {
        public object parent { get; set; } // could be ISpell, IEffect, ....
        public IEffect effect { get; set; }
    }
    public class SubActionEffect : ISubActionEffect
    {
        public IFight fight { get; set; }
        public IID caster { get; set; }
        public IID targetCell { get; set; }
        public object parent { get; set; } // could be ISpell, IEffect, ....
        public IEffect effect { get; set; }
    }

    public interface IActionMove : IAction
    {
        //public IID caster { get; set; }
        //public IID targetCell { get; set; }
    }
    public interface IActionPass : IAction
    {
        //public IID caster { get; set; }
        //public IID target { get; set; }
    }
    public interface IActionSwapOut : IAction
    {
        //public IID caster { get; set; }
        //public IID target { get; set; }
    }
}

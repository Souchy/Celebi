using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects
{
    public interface IAction
    {
        /// <summary>
        /// Null by default.
        /// This action's parent action. Null for root actions (spell cast, move). <br></br>
        /// A root effect action will refer to the spell action that casts it.
        /// </summary>
        public IAction parent { get; set; }
        /// <summary>
        /// 0 by default.
        /// Level starts at 0 and goes deeper. <br></br>
        /// Ex spell cast is at 0, root effects are at 1, etc
        /// </summary>
        public int depthLevel { get; init; }
        /// <summary>
        /// 
        /// </summary>
        public IFight fight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ObjectId caster { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ObjectId targetCell { get; set; }
        /// <summary>
        /// If we ever use IActionContext, then we can use the root action's context to compile actions results into
        /// </summary>
        public IAction getRootAction() {
            return parent == null ? this : parent.getRootAction();
        }
    }

    public class ActionContext
    {
        public List<ActionEffectTargetReturnValue> effectReturnValues = new List<ActionEffectTargetReturnValue>();
        public IEnumerable<ActionEffectTargetReturnValue> getReturnsForEffect(ObjectId effectEntityId)
        {
            return effectReturnValues.Where(e => e.action.effect.entityUid == effectEntityId);
        }
    }
    public class ActionEffectTargetReturnValue
    {
        public SubActionEffectTarget action { get; set; }
        public DataType value { get; set; }
    }

    /// <summary>
    /// for retrieving stats whenever, for the UI
    /// </summary>
    public record EmptyAction(IFight fight) : IAction
    {
        public IAction parent { get; set; }
        public int depthLevel { get; init; }
        public IFight fight { get; set; } = fight;
        public ObjectId caster { get; set; }
        public ObjectId targetCell { get; set; }
    }
    public interface IActionSpell : IAction
    {
        public ObjectId spell { get; set; }
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

    public interface ISubActionEffect : IAction
    {
        public IEffect effect { get; set; }
    }
    public class SubActionEffect : IAction
    {
        public IAction parent { get; set; } // ISpellAction or 
        public int depthLevel { get; init; }
        public IFight fight { get; set; }
        public ObjectId caster { get; set; }
        // maybe the original targetcell of the spell/status holder
        public ObjectId targetCell { get; set; } 

        public IEffect effect { get; set; }
        // all targets passed down to the child effect
        public IEnumerable<IBoardEntity> boardTargets { get; set; }
    }
    public class SubActionEffectTarget : ISubActionEffect
    {
        public IAction parent { get; set; } // SubActionEffect
        public int depthLevel { get; init; }  
        public IFight fight { get; set; }
        public ObjectId caster { get; set; }
        // maybe the original targetcell of the spell/status holder
        public ObjectId targetCell { get; set; }

        public IEffect effect { get; set; }
    }

}

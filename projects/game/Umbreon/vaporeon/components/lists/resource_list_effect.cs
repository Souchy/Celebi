using Godot;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects.effects;
using souchy.celebi.eevee.impl.util;
using System.Reflection;
using System.Xml.Linq;
using souchy.celebi.umbreon.vaporeon;

public partial class resource_list_effect : ResourceList
{
    #region Properties
    public PopupMenu CreatePopupMenu { get; set; } = new();
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        Eevee.models.effects.GetEntityBus().subscribe(this);

        // fill popup menu to create effects from effect types
        foreach (Type t in VaporeonUtil.effectTypes)
            CreatePopupMenu.AddItem(t.FullName);
        CreatePopupMenu.IndexPressed += EffectPopup_IndexPressed;
    }

    #region Functions
    public IEffect getSelectedItem() => Eevee.models.effects.Get(getSelectedItemID());
    public override void fillList()
    {
        foreach (IID effectId in Eevee.models.effects.Keys)
            createChildNode(effectId);
    }
    public override void createChildNode(IID effectId)
    {
        var effect = Eevee.models.effects.Get(effectId);
        if(Eevee.models.effectModels.Has(effect.modelUid))
        {
            IEffectModel model = Eevee.models.effectModels.Get(effect.modelUid);
            var name = model.GetName()?.ToString() ?? "uid " + model.entityUid.ToString();
            //var name = model.GetName(); 
            var desc = model.GetDescription();
            base.addChild(name, new Color().Random(), effect.entityUid);
        } else
        {
            base.addChild($"#{effect.entityUid}, {effect.GetType().Name}", new Color().Random(), effect.entityUid);
        }
    }
    public override void publishSelect(IID id)
    {
        var model = Eevee.models.effects.Get(id);
        this.GetVaporeon().bus.publish(VaporeonSignals.select, base.selectorForControl, model);
        base.selectorForControl = null;
    }
    #endregion

    #region GUI Handlers
    public override void onClickEdit() => this.GetVaporeon().openEditor(getSelectedItem());
    public override void onClickCopy() => this.GetVaporeon().CurrentObjectCopied = getSelectedItem();
    public override void onClickCreateBtn() => CreatePopupMenu.Show();
    private void EffectPopup_IndexPressed(long index)
    {
        Type effectType = VaporeonUtil.effectTypes[(int) index];
        var createMethod = effectType.GetMethod(nameof(EffectBase.Create), BindingFlags.Static);
        IEffect effect = (IEffect) createMethod.Invoke(null, new object[0]);
        //TODO IEffectSkin skin;
        Eevee.models.effects.Add(effect.entityUid, effect);
    }
    public override void onClickRemoveBtn()
    {
        if (base.selectedNode == null) return;
        IID id = (IID) base.selectedNode.GetMeta(VaporeonUtil.metaIID).AsString();
        base.selectedNode = null;
        var effect = Eevee.models.effects.Get(id);
        Eevee.models.effects.Remove(effect.entityUid);
    }
    #endregion


}

using Godot;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.util;
using System;
using Umbreon.vaporeon;

public partial class resource_list_spell_skin : ResourceList
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        Eevee.models.spellSkins.GetEntityBus().subscribe(this);
    }

    #region Functions
    public ISpellSkin getSelectedItem() => Eevee.models.spellSkins.Get(getSelectedItemID());
    public override void fillList()
    {
        foreach (IID creatureId in Eevee.models.spellSkins.Keys)
            createChildNode(creatureId);
    }
    public override void createChildNode(IID modelId)
    {
        ISpellSkin model = Eevee.models.spellSkins.Get(modelId);
        var spell = Eevee.models.spellModels.Get(model.spellModelUid);
        var name = spell.GetName()?.ToString() ?? "uid " + model.entityUid.ToString();
        //var desc = spell.GetDescription();
        base.addChild(name, new Color().Random(), model.entityUid);
    }
    public override void publishSelect(IID id)
    {
        var model = Eevee.models.spellSkins.Get(id);
        this.GetVaporeon().bus.publish(VaporeonSignals.select, base.selectorForControl, model);
        base.selectorForControl = null;
    }
    #endregion


    #region GUI Handlers
    public override void onClickEdit() => this.GetVaporeon().openEditor(getSelectedItem());
    public override void onClickCopy() => this.GetVaporeon().CurrentObjectCopied = getSelectedItem();
    public override void onClickCreateBtn() => throw new Exception("Can't create a spell skin without selecting a spell.");
    public override void onClickRemoveBtn()
    {
        if (base.selectedNode == null) return;
        IID id = (IID) base.selectedNode.GetMeta(VaporeonUtil.metaIID).AsString();
        var creature = Eevee.models.spellSkins.Get(id);
        Eevee.models.spellSkins.Remove(creature.entityUid);
        // dont delete the skins, keep them for later use and
        // TODO add new tabs to vaporeon for skins
        base.selectedNode = null;
        //base.GetVaporeon().CurrentCreatureModel = null;
    }
    #endregion
}

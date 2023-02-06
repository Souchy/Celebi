using Godot;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.util;
using Umbreon.vaporeon;

public partial class resource_list_creature_skin : ResourceList
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        Eevee.models.creatureSkins.GetEntityBus().subscribe(this);
    }

    #region Functions
    public ICreatureSkin getSelectedItem() => Eevee.models.creatureSkins.Get(getSelectedItemID());
    public override void fillList()
    {
        foreach (IID id in Eevee.models.creatureSkins.Keys)
            createChildNode(id);
    }
    public override void createChildNode(IID modelId)
    {
        ICreatureSkin model = Eevee.models.creatureSkins.Get(modelId);
        var name = model.GetName()?.ToString() ?? "uid " + model.entityUid.ToString();
        var desc = model.GetDescription();
        base.addChild(name, new Color().Random(), model.entityUid);
    }
    public override void publishSelect(IID id)
    {
        var model = Eevee.models.creatureSkins.Get(id);
        this.GetVaporeon().bus.publish(VaporeonSignals.select, base.selectorForControl, model);
        base.selectorForControl = null;
    }
    #endregion


    #region GUI Handlers
    public override void onClickEdit() => this.GetVaporeon().openEditor(getSelectedItem());
    public override void onClickCopy() => this.GetVaporeon().CurrentObjectCopied = getSelectedItem();
    public override void onClickCreateBtn()
    {
        // Skin
        var creatureSkin = CreatureSkin.Create();
        // Name
        var name = StringEntity.Create("Name for: " + creatureSkin.entityUid);
        creatureSkin.nameId = name.entityUid;
        Eevee.models.i18n.Add(creatureSkin.nameId, name);
        // Desc
        var desc = StringEntity.Create("Desc for: " + creatureSkin.entityUid);
        creatureSkin.descriptionId = desc.entityUid;
        Eevee.models.i18n.Add(creatureSkin.descriptionId, desc);
        // add Model + Skin
        Eevee.models.creatureSkins.Add(creatureSkin.entityUid, creatureSkin);
    }
    public override void onClickRemoveBtn()
    {
        if (base.selectedNode == null) return;
        IID id = (IID) base.selectedNode.GetMeta(VaporeonUtil.metaIID).AsString();
        var creature = Eevee.models.creatureSkins.Get(id);
        Eevee.models.creatureSkins.Remove(creature.entityUid);
        // dont delete the skins, keep them for later use and
        // TODO add new tabs to vaporeon for skins
        base.selectedNode = null;
        //base.GetVaporeon().CurrentCreatureModel = null;
    }
    #endregion
}

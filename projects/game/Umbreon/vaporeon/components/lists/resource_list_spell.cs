using Godot;
using Godot.Sharp.Extras;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.util;
using Umbreon.vaporeon;

public partial class resource_list_spell : ResourceList
{

    #region Nodes
    [NodePath] public CheckBox CheckWater { get; set; }
    [NodePath] public CheckBox CheckFire { get; set; }
    [NodePath] public CheckBox CheckEarth { get; set; }
    [NodePath] public CheckBox CheckAir { get; set; }
    #endregion


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        base._Ready();
        Eevee.models.spellModels.GetEntityBus().subscribe(this);

        CheckWater.Toggled += CheckWater_Toggled;
        CheckFire.Toggled += CheckFire_Toggled;
        CheckEarth.Toggled += CheckEarth_Toggled;
        CheckAir.Toggled += CheckAir_Toggled;
    }

    #region GUI Handlers
    private void CheckAir_Toggled(bool buttonPressed)
    {
    }
    private void CheckEarth_Toggled(bool buttonPressed)
    {
    }
    private void CheckFire_Toggled(bool buttonPressed)
    {
    }
    private void CheckWater_Toggled(bool buttonPressed)
    {
    }
    #endregion

    #region Functions
    public ISpellModel getSelectedItem() => Eevee.models.spellModels.Get(getSelectedItemID());
    public override void fillList()
    {
        foreach (IID spellId in Eevee.models.spellModels.Keys)
            createChildNode(spellId);
    }
    public override void createChildNode(IID modelID)
    {
        var model = Eevee.models.spellModels.Get(modelID);
        // get the first skin for that spell
        var skin = Eevee.models.spellSkins.Values.Where(skin => skin.spellModelUid == model.entityUid).FirstOrDefault();
        var icon = skin?.icon;
        var name = model.GetName()?.ToString() ?? "uid " + model.entityUid.ToString();
        //var name = model.GetName(); //Eevee.models.i18n.Get(spell.nameId);
        var desc = model.GetDescription(); //Eevee.models.i18n.Get(spell.descriptionId);
        base.addChild(name, new Color().Random(), model.entityUid);
    }
    public override void publishSelect(IID id)
    {
        var model = Eevee.models.spellModels.Get(id);
        this.GetVaporeon().bus.publish(VaporeonSignals.select, base.selectorForControl, model);
        base.selectorForControl = null;
    }
    #endregion

    #region GUI Handlers
    public override void onClickEdit() => this.GetVaporeon().openEditor(getSelectedItem());
    public override void onClickCopy() => this.GetVaporeon().CurrentObjectCopied = getSelectedItem();
    public override void onClickCreateBtn()
    {
        var spellmodel = SpellModel.Create();
        var spellskin = SpellSkin.Create(); // base skin
        spellskin.spellModelUid = spellmodel.entityUid;

        var name = StringEntity.Create("Name for: " + spellmodel.entityUid);
        spellmodel.nameId = name.entityUid;
        Eevee.models.i18n.Add(name.entityUid, name);

        var desc = StringEntity.Create("Desc for: " + spellmodel.entityUid);
        spellmodel.descriptionId = desc.entityUid;
        Eevee.models.i18n.Add(desc.entityUid, desc);

        Eevee.models.spellModels.Add(spellmodel.entityUid, spellmodel);
        Eevee.models.spellSkins.Add(spellskin.entityUid, spellskin);
    }
    public override void onClickRemoveBtn()
    {
        if (base.selectedNode == null) return;
        IID id = (IID) base.selectedNode.GetMeta(VaporeonUtil.metaIID).AsString();
        base.selectedNode = null;
        var spell = Eevee.models.spellModels.Get(id);
        Eevee.models.spellModels.Remove(spell.entityUid);
        Eevee.models.spellSkins.Remove(s => s.spellModelUid == spell.entityUid);
    }
    #endregion


}

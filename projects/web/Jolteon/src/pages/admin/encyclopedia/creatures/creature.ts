import { IEventAggregator, bindable, inject } from "aurelia";
import { IRouteableComponent, IRouter, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { ICreatureModel, IStatusModel, SpellModel } from "../../../../jolteon/services/api/data-contracts";
import { CreatureModelController } from "../../../../jolteon/services/api/CreatureModelController";
import { StatsType } from "../stats/statscomponent";
import { TOAST_PLACEMENT, TOAST_STATUS, TOAST_THEME, Toast, ToastConfigOptions, ToastOptions } from "bootstrap-toaster";
import { SpellModelController } from "../../../../jolteon/services/api/SpellModelController";
import { Stringcomponent } from "../strings/stringcomponent";
import { CreatureSkinController } from "../../../../jolteon/services/api/CreatureSkinController";


@inject(IEventAggregator, IRouter, CreatureModelController, CreatureSkinController)
export class Creature implements IRouteableComponent {

    //#region input
    @bindable
    public model: ICreatureModel;
    @bindable
    public isvignette: boolean = false;
    /** Creature modelID from Route: https://celebi.com/creatures/{id} */
    public uid: string;
    //#endregion

    // binded view-model
    // public name: Stringcomponent;
    // public desc: Stringcomponent;

    constructor(
        private readonly ea: IEventAggregator,
        private readonly router: IRouter,
        private readonly creatureController: CreatureModelController,
        // private readonly spellController: SpellModelController,
        private readonly skinController: CreatureSkinController
    ) {
    }

    /**
     * Hook to attribute binding
     */
    binding() {
        // console.log("creature binding: " + this.uid + ", " + JSON.stringify(this.model))
    }
    /**
     * Hook to route loading
     */
    async loading?(parameters: Parameters, instruction: RoutingInstruction, navigation: Navigation): Promise<void> {
        this.uid = parameters["uid"] as string;
        // console.log("creature loading: " + this.uid)
        try {
            let res = await this.creatureController.getCreature(this.uid)
            this.model = res.data;
            console.log("nav to new creature")
            this.ea.publish("navcrumb:spell", null)
            this.ea.publish("navcrumb:status", null)
            this.ea.publish("navcrumb:creature", {
                modeluid: this.model.modelUid,
                nameuid: this.model.nameId
            })
            // console.log("creature loading: " + JSON.stringify(this.model))
        } catch (rej) {
            this.router.load("editor");
        }
    }

    
    public clickCreature() {
        this.router.load("/editor/creature/" + this.model.modelUid);
    }

    public clickRemove() {
        // TODO: ask confirmation before delete, it's too easy to missclick
        this.creatureController.deleteCreature(this.model.modelUid);
    }

    
    public async clickNewSkin() {
        // create skin
        let res = await this.skinController.postSkin();
        // add to spell & update
        this.model.skinIds.push(res.data.entityUid);
        // let res2 = await 
        this.creatureController.putCreature(this.model.modelUid, this.model);
        // this.model = res2.data;
    }

    // callback from spell list
    public onAddSpell(spell: SpellModel) {
        console.log("creature onAddSpell: " + spell.modelUid)
        this.model.spellIds.push(spell.entityUid);
        this.creatureController.putSpells(this.model.modelUid, this.model.spellIds);
    }
    // callback from spell list
    public onRemoveSpell(spell: SpellModel) {
        // console.log("creature onRemoveSpell: " + spell.modelUid)
        let idx = this.model.spellIds.indexOf(spell.entityUid);
        if (idx == -1) return;
        this.model.spellIds.splice(idx, 1);
        this.creatureController.putSpells(this.model.modelUid, this.model.spellIds);
    }
    
    // callback from status list
    public onAddStatus(status: SpellModel) {
        console.log("creature onAddStatus: " + status.modelUid)
        this.model.statusPassiveIds.push(status.entityUid);
        this.creatureController.putCreature(this.model.modelUid, this.model);
    }
    // callback from status list
    public onRemoveStatus(status: IStatusModel) {
        // console.log("creature onRemoveStatus: " + status.modelUid)
        let idx = this.model.statusPassiveIds.indexOf(status.entityUid);
        if (idx == -1) return;
        this.model.statusPassiveIds.splice(idx, 1);
        this.creatureController.putCreature(this.model.modelUid, this.model);
    }

}

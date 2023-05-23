import { IEventAggregator, bindable, inject } from "aurelia"
import { IEffect, SchemaDescription, SpellModel } from "../../../jolteon/services/api/data-contracts"
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController"
import { IRouteableComponent, IRouter, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { Characteristics } from "../../../jolteon/constants";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { Stringcomponent } from "../strings/stringcomponent";
import { SpellSkinController } from "../../../jolteon/services/api/SpellSkinController";

@inject(IEventAggregator, IRouter, SpellModelController, SpellSkinController)
export class Spell {
    public readonly Characteristics: Characteristics = Characteristics; // static reference

    //#region input
    @bindable
    public model: SpellModel;
    @bindable
    public isvignette: boolean = false;
    /** SpellModel modelID from Route: https://celebi.com/spells/{id} */
    public uid: string;
    //#endregion

    @bindable
    public mode: string = 'root';
    @bindable
    public callbackremove = (spell: SpellModel) => { };
    @bindable
    public callbackadd = (spell: SpellModel) => { };

    // view-model ref
    public name: Stringcomponent
    public desc: Stringcomponent

    constructor(
        private readonly ea: IEventAggregator,
        private readonly router: IRouter,
        private readonly spellController: SpellModelController,
        // private readonly creatureController: CreatureModelController,
        private readonly skinController: SpellSkinController
    ) {
        // ea.subscribe("operation:saved", this.toastSaved);
        // ea.subscribe("operation:failed", this.toastFailed);
    }

    /**
     * Hook to attribute binding
     */
    binding() {
        // console.log("creature binding: " + this.uid + ", " + JSON.stringify(this.model))
        // console.log("bind spell mode: " + this.mode);
    }
    /**
     * Hook to route loading
     */
    async loading?(parameters: Parameters, instruction: RoutingInstruction, navigation: Navigation): Promise<void> {
        this.uid = parameters["uid"] as string;
        try {
            let res = await this.spellController.getSpell(this.uid)
            this.model = res.data;
        } catch (rej) {
            this.router.load("editor");
        }
    }

    public onChangeCost(characid: string) {
        this.spellController.putSpell(this.model.modelUid, this.model);
    }

    public clickSpell() { //spell: SpellModel) {
        // console.log("click spell: " + this.mode)
        if (this.mode == 'root' || this.mode == 'creature') {
            // console.log("click spell " + modelUid)
            this.router.load("/editor/spell/" + this.model.modelUid);
        }
        if (this.mode == 'search') {
            // console.log("spell search click: " + this.model.modelUid)
            this.callbackadd(this.model);
        }
    }

    public async clickRemove() {
        // console.log("spell clickRemove")
        this.callbackremove(this.model);
    }

    public async addEffect(schema: SchemaDescription) {
        // console.log("add effect: " + schema) 
        this.spellController.postEffect(this.model.modelUid, {
            schemaName: schema.name
        }).then(res => location.reload())
    }

    public async clickNewSkin() {
        // create skin
        let res = await this.skinController.postSkin();
        // add to spell & update
        this.model.skinIds.push(res.data.entityUid);
        // let res2 = await 
        this.spellController.putSpell(this.model.modelUid, this.model);
        // this.model = res2.data;
    }


    public onMoveEffectUp(e: IEffect) {
        let idx = this.model.effectIds.indexOf(e.entityUid);
        if (idx == -1) {
            console.error("Spell.moveEffectUp: effect not found: " + JSON.stringify(e));
            return;
        }
        this.model.effectIds.splice(idx, 1);
        this.model.effectIds.splice(idx - 1, 0, e.entityUid);
        this.spellController.putSpell(this.model.modelUid, this.model);
    }
    public onMoveEffectDown(e: IEffect) {
        let idx = this.model.effectIds.indexOf(e.entityUid);
        console.log("effect ids: " + idx + ", " + JSON.stringify(this.model.effectIds));
        if (idx == -1) {
            console.error("Spell.moveEffectDown: effect not found: " + JSON.stringify(e));
            return;
        }
        this.model.effectIds.splice(idx, 1);
        this.model.effectIds.splice(idx + 1, 0, e.entityUid);
        this.spellController.putSpell(this.model.modelUid, this.model);
    }
    public onRemoveEffect(e: IEffect) {
        console.log("spell remove eff")
        let idx = this.model.effectIds.indexOf(e.entityUid);
        this.model.effectIds.splice(idx, 1);
        this.spellController.putSpell(this.model.modelUid, this.model);
    }



}

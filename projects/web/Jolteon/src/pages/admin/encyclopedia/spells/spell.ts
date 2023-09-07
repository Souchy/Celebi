import { IEventAggregator, bindable, inject } from "aurelia"
import { IEffect, SchemaDescription, SpellModel } from "../../../../jolteon/services/api/data-contracts"
import { SpellModelController } from "../../../../jolteon/services/api/SpellModelController"
import { IRouteableComponent, IRouter, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { Characteristics } from "../../../../jolteon/constants";
import { Stringcomponent } from "../strings/stringcomponent";
import { SpellSkinController } from "../../../../jolteon/services/api/SpellSkinController";

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

    // input bind
    public iconfile: any;

    constructor(
        private readonly ea: IEventAggregator,
        private readonly router: IRouter,
        private readonly spellController: SpellModelController,
        private readonly skinController: SpellSkinController
    ) {
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
            this.ea.publish("navcrumb:status", null)
            this.ea.publish("navcrumb:spell", {
                modeluid: this.model.modelUid,
                nameuid: this.model.nameId
            })
        } catch (rej) {
            this.router.load("editor");
        }
    }

    public clickFileIcon(f) {
        // console.log("click icon")
        // console.log(f);
        this.model.icon = this.iconfile[0].name;
        this.save();
    }

    public onChangeCost(characid: string) {
        this.save();
        // this.spellController.putSpell(this.model.modelUid, this.model);
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

    public async clickNewSkin() {
        // create skin
        let res = await this.skinController.postSkin();
        // add to spell & update
        this.model.skinIds.push(res.data.entityUid);
        // this.spellController.putSpell(this.model.modelUid, this.model);
        // this.model = res2.data;
        this.save();
    }

    public save() {
        this.spellController.putSpell(this.model.modelUid, this.model).then(
            res => {
                this.model = res.data;
                this.ea.publish("operation:saved");
            }
        )
    }



}
import { IEventAggregator, bindable, inject } from "aurelia"
import { SpellModel } from "../../../jolteon/services/api/data-contracts"
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController"
import { IRouteableComponent, IRouter, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { Characteristics } from "../../../jolteon/constants";

@inject(SpellModelController, IEventAggregator, IRouter)
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
    public mode:string = 'root';

    constructor(
        private readonly controller: SpellModelController,
        private readonly ea: IEventAggregator, 
        private readonly router: IRouter
    ) {
        // ea.subscribe("operation:saved", this.toastSaved);
        // ea.subscribe("operation:failed", this.toastFailed);
    }

    /**
     * Hook to attribute binding
     */
    binding() {
        // console.log("creature binding: " + this.uid + ", " + JSON.stringify(this.model))
        console.log("bind spell mode: " + this.mode);
    }
    /**
     * Hook to route loading
     */
    async loading?(parameters: Parameters, instruction: RoutingInstruction, navigation: Navigation): Promise<void> {
        this.uid = parameters["uid"] as string;
        try {
            let res = await this.controller.getSpell(this.uid)
            this.model = res.data;
        } catch (rej) {
            this.router.load("editor");
        }
    }

    public onChangeCost(characid: string) {
        console.log("on change cost: " + characid);
        this.controller.putSpell(this.model.modelUid, this.model);
    }

    
    public clickSpell(){ //spell: SpellModel) {
        console.log("click spell: " + this.mode)
        if(this.mode == 'root' || this.mode == 'creatureSpells') {
            // console.log("click spell " + modelUid)
            this.router.load("/editor/spell/" + this.model.modelUid);
        }
        else 
        if (this.mode == 'search') {
            this.ea.publish('spells:search:select', this.model.entityUid);
        }
    }
    public clickRemove() {
        // remove from all spells
        // remove from creature spells
        if(this.mode == 'root' || this.mode == 'creatureSpells') {
        } 
        else 
        if (this.mode == 'search') {
            this.ea.publish('spells:search:select', this.model.entityUid);
        }
    }

    
}

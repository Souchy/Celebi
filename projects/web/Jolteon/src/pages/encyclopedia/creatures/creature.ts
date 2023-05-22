import { IEventAggregator, bindable, inject } from "aurelia";
import { IRouteableComponent, IRouter, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { ICreatureModel } from "../../../jolteon/services/api/data-contracts";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { StatsType } from "../stats/statscomponent";
import { TOAST_PLACEMENT, TOAST_STATUS, TOAST_THEME, Toast, ToastConfigOptions, ToastOptions } from "bootstrap-toaster";
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController";


@inject(IEventAggregator, IRouter, CreatureModelController, SpellModelController)
export class Creature implements IRouteableComponent {

    //#region input
    @bindable
    public model: ICreatureModel;
    @bindable
    public isvignette: boolean = false;
    /** Creature modelID from Route: https://celebi.com/creatures/{id} */
    public uid: string;
    //#endregion

    constructor(
        private readonly ea: IEventAggregator,
        private readonly router: IRouter,
        private readonly creatureController: CreatureModelController,
        private readonly spellController: SpellModelController
    ) {
        this.ea.subscribe('spells:search:select', (spellUid: string) => {
            console.log("creature receive add spell: " + spellUid)
            if(!this.model.spellIds) this.model.spellIds = [];
            this.model.spellIds.push(spellUid);
            this.creatureController.putSpells(this.model.modelUid, this.model.spellIds);
        });
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
            // console.log("creature loading: " + JSON.stringify(this.model))
        } catch (rej) {
            this.router.load("editor");
        }
    }

    
    public clickCreature() {
        this.router.load("/editor/creature/" + this.model.modelUid);
    }

    public clickRemove() {
        // TODO: ask confirmation before delete, it'S too easy to missclick
        // this.creatureController.deleteCreature(this.model.modelUid);
    }
    public clickCreateSpell() {
        this.spellController.postNew().then(res => {
            this.model.spellIds.push(res.data.entityUid);
        });
    }


}

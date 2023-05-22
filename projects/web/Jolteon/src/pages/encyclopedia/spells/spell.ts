import { IEventAggregator, bindable, inject } from "aurelia"
import { IEffect, SchemaDescription, SpellModel } from "../../../jolteon/services/api/data-contracts"
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
    public mode: string = 'root';

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
        // console.log("bind spell mode: " + this.mode);
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
        this.controller.putSpell(this.model.modelUid, this.model);
    }

    public clickSpell() { //spell: SpellModel) {
        // console.log("click spell: " + this.mode)
        if (this.mode == 'root' || this.mode == 'creatureSpells') {
            // console.log("click spell " + modelUid)
            this.router.load("/editor/spell/" + this.model.modelUid);
        }
        else
            if (this.mode == 'search') {
                this.ea.publish('spells:search:select', this.model.entityUid);
            }
    }

    public async clickRemove() {
        // remove from all spells
        // remove from creature spells
        if (this.mode == 'root') {
            this.controller.deleteSpell(this.model.modelUid).then(
                res => {
                    console.log("deleted spell from all")
                    this.ea.publish('spells:root:remove', this.model.modelUid);
                },
                rej => {
                    console.error(rej);
                }
            );
        }
        else
            if (this.mode == 'creatureSpells') {

            }
            else
                if (this.mode == 'search') {
                    this.ea.publish('spells:search:select', this.model.entityUid);
                }
    }

    public async addEffect(schema: SchemaDescription) {
        console.log("add effect: " + schema) // + ", " + parent?.entityUid)

        this.controller.postEffect(this.model.modelUid, {
                // effectParentId: "",
                schemaName: schema.name
            })
        .then(res => location.reload())
    }


    public onMoveEffectUp(e: IEffect) {
        let idx = this.model.effectIds.indexOf(e.entityUid);
        if (idx == -1) {
            console.error("Spell.moveEffectUp: effect not found: " + JSON.stringify(e));
            return;
        }
        this.model.effectIds.splice(idx, 1);
        this.model.effectIds.splice(idx - 1, 0, e.entityUid);
        this.controller.putSpell(this.model.modelUid, this.model);
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
        this.controller.putSpell(this.model.modelUid, this.model);
    }
    public onRemoveEffect(e: IEffect) {
        console.log("spell remove eff")
        let idx = this.model.effectIds.indexOf(e.entityUid);
        this.model.effectIds.splice(idx, 1);
        this.controller.putSpell(this.model.modelUid, this.model);
    }



}

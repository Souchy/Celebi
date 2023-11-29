import { IEventAggregator, bindable, inject } from "aurelia";
import { IEffect, SchemaDescription } from "../../../../jolteon/services/api/data-contracts";
import { EffectPermanentController } from "../../../../jolteon/services/api/EffectPermanentController";
import { IRouter } from "@aurelia/router";
import { Schemas } from "../../../../jolteon/constants";


/**
 * TODO
 */
@inject(IEventAggregator, IRouter, EffectPermanentController)
export class Effectlist {

    @bindable
    public title = "Effects";
    @bindable
    public effectids = []
    @bindable
    public modaluid = ""

    @bindable
    public callbacksave = () => { }
    // public callbackaddeffect = (schemaName: string) => {}

    // db data
    public schemas = Schemas.effects;

    constructor(
        private readonly ea: IEventAggregator,
        private readonly router: IRouter,
        private readonly effectController: EffectPermanentController
    ) {
    }

    binding() {
        // console.log("effectlist binding: " + this.effectids);
    }

    public save() {
        this.callbacksave();
    }

    public async onAddChild(schema: SchemaDescription) {
        // this.callbackaddeffect(schema.name);
        this.effectController.postNew({ schemaName: schema.name })
            .then(res => this.effectids.push(res.data.entityUid)) 
            .then(f =>  this.save())
    }

    public async clickPasteNewEffect() {
        let text = await navigator.clipboard.readText();
        let eff: IEffect = JSON.parse(text);
        this.effectController.postCopy(eff)
            .then(res => this.effectids.push(res.data.entityUid)) 
            .then(f =>  this.save())
    }


    public onMoveEffectUp(e: IEffect) {
        let idx = this.effectids.indexOf(e.entityUid);
        if (idx == -1) {
            console.error("effect list.moveEffectUp: effect not found: " + JSON.stringify(e));
            return;
        }
        this.effectids.splice(idx, 1);
        this.effectids.splice(idx - 1, 0, e.entityUid);
        // this.spellController.putSpell(this.model.modelUid, this.model);
        this.save();
    }
    public onMoveEffectDown(e: IEffect) {
        let idx = this.effectids.indexOf(e.entityUid);
        console.log("effect ids: " + idx + ", " + JSON.stringify(this.effectids));
        if (idx == -1) {
            console.error("effect list.moveEffectDown: effect not found: " + JSON.stringify(e));
            return;
        }
        this.effectids.splice(idx, 1);
        this.effectids.splice(idx + 1, 0, e.entityUid);
        // this.spellController.putSpell(this.model.modelUid, this.model);
        this.save();
    }
    public onRemoveEffect(e: IEffect) {
        console.log("effect list remove eff: " + e.entityUid)
        let idx = this.effectids.indexOf(e.entityUid);
        this.effectids.splice(idx, 1);
        // this.spellController.putSpell(this.model.modelUid, this.model);
        this.save();
    }

    // public onDrag(ev, effectid) {
    //     console.log("drag:")
    //     console.log(ev)
    //     ev.dataTransfer.setData("text", JSON.stringify({
    //         parent: "effect",
    //         id: this.model.entityUid
    //     }));
    // }
    // public onDragOver(ev, effectid) {
    //     ev.preventDefault();
    // }
    // public onDrop(ev, effectid) {
    //     console.log("drop:")
    //     console.log(ev)

    //     ev.preventDefault();
    //     let data: { parent: string, id: string } = JSON.parse(ev.dataTransfer.getData("text"));
    //     // dont drag & drop into itself
    //     if(data.id == this.model.entityUid) return;
    //     // remove from all parents, then add as child to this
    //     this.effectController.putRemoveEffect(data.id);
    //     this.effectController.putAddEffect(this.model.entityUid, data.id)
    //         .then(res => this.model = res.data)
    //         .then(eff => this.ea.publish("operation:saved"))
    //     // .then(this.handleUpdate);
        
    // }
    

}

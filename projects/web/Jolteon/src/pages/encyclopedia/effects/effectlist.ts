import { bindable } from "aurelia";
import { IEffect, SchemaDescription } from "../../../jolteon/services/api/data-contracts";
import { EffectPermanentController } from "../../../jolteon/services/api/EffectPermanentController";


/**
 * TODO
 */
export class Effectlist {

    @bindable
    public effectids = []
    @bindable
    public modaluid = ""

    @bindable
    public callbacksave = () => {}
    // public callbackaddeffect = (schemaName: string) => {}

    constructor(private readonly con: EffectPermanentController) {

    }

    public async addEffect(schema: SchemaDescription) {
        // this.callbackaddeffect(schema.name);
        this.con.postNew({
            schemaName: schema.name
        }).then(res => {
            let eff = res.data;
            this.effectids.push(eff.entityUid);
            this.save();
        })
        // console.log("add effect: " + schema) 
        // this.spellController.postEffect(this.model.modelUid, {
        //     schemaName: schema.name
        // }).then(res => {
        //     // location.reload()
        //     this.model = res.data;
        // })
    }
    public save() {
        this.callbacksave();
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

    
}

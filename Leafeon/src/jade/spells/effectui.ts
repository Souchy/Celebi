import { bindable, IEventAggregator, inject } from "aurelia";
import { Condition } from "../../../../arch/common/condition";
import { Effect } from "../../../../arch/common/effects";
import { Spell } from "../../../../arch/common/spell";
import { db } from "../../db";

@inject(db)
export class EffectUI {

    public db: db;

    // @bindable
    // public spell: Spell;
    // public effects: Effect[];
    @bindable
    public parent: any; // spell or effect have children
    @bindable
    public effect: Effect;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
    }

    public addEffect() {
        this.effect.effects.push(new Effect());
        this.save();
        console.log("effect parent: " + JSON.stringify(this.parent))
    }
    
    public deleteEffect(effect) {
        let index = this.parent.effects.indexOf(effect);
        this.parent.effects.splice(index, 1);
        this.save();
    }

    public save() {
        this.ea.publish("spells:save");
    }
    
}

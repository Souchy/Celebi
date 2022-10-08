import { bindable, IEventAggregator, inject } from "aurelia";
import { Condition } from "../../../../arch/common/condition";
import { Effect } from "../../../../arch/common/effects";
import { Spell } from "../../../../arch/common/spell";
import { db } from "../../db";

@inject(db)
export class Effects {

    public db: db;

    @bindable
    public spell: Spell;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
    }

    public addEffect() {
        this.spell.effects.push(new Effect());
        this.save();
    }
    public deleteEffect(effect) {
        let index = this.spell.effects.indexOf(effect);
        this.spell.effects.splice(index, 1);
        this.save();
    }

    public createCondition(effect) {
        effect.condition = new Condition();
    }

    public save() {
        this.ea.publish("spells:save");
    }
    
}

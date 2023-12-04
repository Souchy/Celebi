import { bindable, IEventAggregator } from "aurelia";
import { Spell } from "../../../../../../arch/common/spell";

export class SpellData {
    @bindable
    public spell: Spell;

    constructor(@IEventAggregator readonly ea: IEventAggregator) {

    }

    public save() {
        this.ea.publish("spells:save");
    }
}

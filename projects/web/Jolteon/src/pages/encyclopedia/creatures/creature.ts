import { bindable } from "aurelia";
import { ICreatureModel } from "../../../jolteon/services/api/data-contracts";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { StatsModelController } from "../../../jolteon/services/api/StatsModelController";
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController";
import { StringController } from "../../../jolteon/services/api/StringController";


export class Creature {
    
    // @bindable
    public model: ICreatureModel;
    /**
     * CreatureID from Route: https://celebi.com/creatures/{id}
     */
    public id: string;

    constructor(
        private readonly creatureController: CreatureModelController,
        private readonly statsController: StatsModelController,
        private readonly spellController: SpellModelController,
        private readonly stringController: StringController,
    ) {
        if(this.model === null) {
            this.creatureController.getCreature({ value: this.id }).then(res => {
                this.model = res.data;
            });
        }
    }

    

}

import { IStats, IStringEntity, StringEntity } from './../../../jolteon/services/api/data-contracts';
import { bindable } from "aurelia";
import { ICreatureModel } from "../../../jolteon/services/api/data-contracts";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController";
import { StringController } from "../../../jolteon/services/api/StringController";
import { StatsModelController } from "../../../jolteon/services/api/StatsModelController";

export class Creature {
    
    //#region input
    @bindable
    public model: ICreatureModel;
    @bindable
    public isvignette: boolean = false;
    /**
     * CreatureID from Route: https://celebi.com/creatures/{id}
     */
    public uid: string;
    //#endregion

    //#region extensions
    // public name: IStringEntity;
    // public description: IStringEntity;
    // public baseStats: IStats;
    // public growthStats: IStats;
    //#endregion

    constructor(
        private readonly creatureController: CreatureModelController,
        // private readonly statsController: StatsModelController,
        // private readonly spellController: SpellModelController,
        // private readonly stringController: StringController,
    ) {
        if(this.model === null) {
            this.creatureController
            .getCreature({ value: this.uid })
            .then(res => {
                this.model = res.data;
                // this.loadExtensions(this.model);
            });
        } else {
            // this.loadExtensions(this.model);
        }
    }

    // private async loadExtensions(crea: ICreatureModel) {
    //     this.stringController.getString(crea.nameId).then(res => this.name = res.data);
    //     this.stringController.getString(crea.descriptionId).then(res => this.description = res.data);

    //     this.statsController.getStats(crea.baseStats).then(res => this.baseStats = res.data);
    //     this.statsController.getStats(crea.growthStats).then(res => this.growthStats = res.data);
    // }

    

}

import { IStats, IStringEntity, StringEntity } from './../../../jolteon/services/api/data-contracts';
import { bindable } from "aurelia";
import { ICreatureModel } from "../../../jolteon/services/api/data-contracts";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController";
import { StringController } from "../../../jolteon/services/api/StringController";
import { StatsModelController } from "../../../jolteon/services/api/StatsModelController";
import { IRouteableComponent, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';


export class Creature implements IRouteableComponent {
    
    //#region input
    @bindable
    public model: ICreatureModel;
    @bindable
    public isvignette: boolean = false;
    /**
     * Creature modelID from Route: https://celebi.com/creatures/{id}
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
        
    }

    /**
     * Hook to attribute binding
     */
    binding() {
        console.log("creature binding: " + this.uid + ", " + JSON.stringify(this.model))
    }
    /**
     * Hook to route loading
     */
    async loading?(parameters: Parameters, instruction: RoutingInstruction, navigation: Navigation): Promise<void> {
        this.uid = parameters["uid"] as string;
        // console.log("creature loading: " + this.uid)

        let res = await this.creatureController.getCreature(this.uid);
        this.model = res.data;
        console.log("creature loading: " + JSON.stringify(this.model))
    }

    // private async loadExtensions(crea: ICreatureModel) {
    //     this.stringController.getString(crea.nameId).then(res => this.name = res.data);
    //     this.stringController.getString(crea.descriptionId).then(res => this.description = res.data);

    //     this.statsController.getStats(crea.baseStats).then(res => this.baseStats = res.data);
    //     this.statsController.getStats(crea.growthStats).then(res => this.growthStats = res.data);
    // }

    

}

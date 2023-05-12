import { ICreatureModel } from "../../../jolteon/services/api/data-contracts";
import { bindable } from "aurelia";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { IRouteableComponent, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { StatsType } from "../stats/statscomponent";


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

    public statType = StatsType.Creature;

    constructor(
        private readonly creatureController: CreatureModelController,
    ) {
       
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

        let res = await this.creatureController.getCreature(this.uid);
        this.model = res.data;
        console.log("creature loading: " + JSON.stringify(this.model))
    }


}

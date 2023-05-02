import { bindable } from "aurelia";
import { ICreatureModel } from "../../../jolteon/services/api/data-contracts";


export class Creature {
    
    @bindable
    public model: ICreatureModel;

}

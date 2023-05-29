import { bindable } from "aurelia";
import { IStats } from "../../../../jolteon/services/api/data-contracts";


export class Statsmini {

    // public stats type (StatusInstanceStats? CreatureStats? ... -> what properties are allowed?)

    @bindable
    public stats: IStats


    public clickAddStat() {

    }

}

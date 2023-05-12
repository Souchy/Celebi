import { bindable } from "aurelia";
import { IStat, IStats } from "../../../../jolteon/services/api/data-contracts";


export class Tablebool {

    @bindable
    public header: string = "Charac";
    // public headers: string[] = ["Charac", "Base", "Growth"];

    @bindable
    public characs: any[];
    @bindable
    public base: IStats;
    // @bindable
    // public growth: IStats | null;

    constructor() {

    }

}

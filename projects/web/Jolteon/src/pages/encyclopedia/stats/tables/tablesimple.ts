import { bindable } from "aurelia";
import { IStat, IStats } from "../../../../jolteon/services/api/data-contracts";


export class Tablesimple {

    @bindable
    public header: string = "Charac";
    // public headers: string[] = ["Charac", "Base", "Growth"];

    @bindable
    public characs: any[];
    @bindable
    public base: any;
    @bindable
    public growth: any;
    // @bindable
    // public dic: any;

    constructor() {

    }

    // binding() {
    //     console.log("table base stats: " + this.base);
    // }
    bound() {
        console.log("table base stats2: " + this.base);
    }

    public getBaseStat(id) {
        if(!this.base) return 666;

        if (this.base["dic"].includes(id))
            return this.base["dic"][id]
        else return 0;
    }
    public getGrowthStat(id) {
        if(!this.growth) return 1337;

        if (this.growth["dic"].includes(id))
            return this.growth["dic"][id]
        else return 0;
    }



}

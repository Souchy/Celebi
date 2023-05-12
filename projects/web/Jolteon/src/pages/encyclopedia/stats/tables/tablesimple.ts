import { bindable } from "aurelia";
import { IStat, IStats } from "../../../../jolteon/services/api/data-contracts";


export class Tablesimple {

    @bindable
    public header: string = "Charac";
    // public headers: string[] = ["Charac", "Base", "Growth"];

    @bindable
    public characs: any[];
    @bindable
    public base: any //= { dic: {}};
    @bindable
    public growth: any //= { dic: {}};

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

        if (this.base["dic"]?.hasOwnProperty(id)){
            console.log("something ba: ")
            console.log(this.base);
            return this.base["dic"][id]
        }
        else return 0;
    }
    public getGrowthStat(id) {
        if(!this.growth) return 1337;

        if (this.growth["dic"]?.hasOwnProperty(id)){
            console.log("something gr: " + JSON.stringify(this.growth))
            return this.growth["dic"][id]
        }
        else return 0;
    }



}

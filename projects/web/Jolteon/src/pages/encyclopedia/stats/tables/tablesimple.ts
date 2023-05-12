import { IStatSimple } from './../../../../jolteon/services/api/data-contracts';
import { Stats } from './../../../../../../../../arch/common/stats';
import { bindable } from "aurelia";
import { IStat, IStats } from "../../../../jolteon/services/api/data-contracts";


export class Tablesimple {

    @bindable
    public header: string = "Charac";

    @bindable
    public characs: any[];
    @bindable
    public base: Stats 
    @bindable
    public growth: Stats

    constructor() {

    }

    // binding() {
    //     console.log("table base stats: " + this.base);
    // }
    bound() {
        console.log("table base stats: " + this.base);
    }

    public getBaseStat(id) {
        if(!this.base) return 666;

        if (this.base["dic"]?.hasOwnProperty(id)){
            console.log("something ba: ")
            console.log(this.base);
            return this.base["dic"][id]
        }
        else {
            let stat: IStatSimple = {
                statId: id,
                value: 0
            }
            return stat;
        }
    }
    public getGrowthStat(id) {
        if(!this.growth) return 1337;

        if (this.growth["dic"]?.hasOwnProperty(id)){
            console.log("something gr: " + JSON.stringify(this.growth))
            return this.growth["dic"][id]
        }
        else {
            let stat: IStatSimple = {
                statId: id,
                value: 0
            }
            return stat;
        }
    }



}

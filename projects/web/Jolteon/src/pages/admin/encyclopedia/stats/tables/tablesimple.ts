import { IEventAggregator, bindable, inject } from "aurelia";
import { MathEquation, StatSimple, Stats } from '../../../../../jolteon/services/api/data-contracts';
import { IStat, IStats } from "../../../../../jolteon/services/api/data-contracts";
import { Constants } from "../../../../../jolteon/constants";

@inject(IEventAggregator)
export class Tablesimple {

    @bindable
    public hasheader: boolean = true;

    @bindable
    public characs: any[];
    @bindable
    public stats: Stats = null

    constructor(private readonly ea: IEventAggregator) {
    }

    bound() {
        // console.log("table base stats: " + this.characs.map(c => c.id));
        // console.log(this.stats);
    }

    public getBaseStat(id): StatSimple {
        if (this.stats?.base?.dic?.hasOwnProperty(id)){
            let stat = this.stats.base.dic[id];
            return stat;
        }
        else {
            let stat: StatSimple = {
                statId: id,
                value: 0
            };
            return stat;
        }
    }
    public getGrowthEquation(id): MathEquation {
        if (this.stats?.growth?.dic?.hasOwnProperty(id)){
            let equation = this.stats.growth.dic[id]
            return equation;
        }
        else {
            let equation: MathEquation = {
                functions: [
                    {
                        xFromIncluded: Constants.MAX_INT,
                        xToExcluded: Constants.MIN_INT,
                        slopes: [0]
                    }
                ]
            }
            return equation;
        }
    }


}

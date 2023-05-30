import { bindable, inject, observable } from "aurelia";
import { watch } from '@aurelia/runtime-html';
import { IStat, IStats, MathEquation, StatValueType, Stats } from "../../../../jolteon/services/api/data-contracts";
import { StatsModelController } from "../../../../jolteon/services/api/StatsModelController";
import { Characteristics, Constants, Enums } from "../../../../jolteon/constants";

@inject(StatsModelController)
export class Statsmini {

    public Enums: Enums = Enums;
    // public stats type (StatusInstanceStats? CreatureStats? ... -> what properties are allowed?)

    @bindable
    public characsallowed: any[] = Characteristics.allSectioned;
    @bindable
    public hasgrowth: boolean = false;
    @bindable
    public hasadddelete: boolean = true;
    @bindable
    public callbacksavestat = () => { };

    @bindable @observable
    public stats: Stats;
    @bindable
    public statsuid: string;


    constructor(private readonly statsController: StatsModelController) {
        
    }

    /**
     * hook
     */
    binding() {
        if (this.statsuid && !this.stats) {
            this.statsController.getStats(this.statsuid).then(
                res => {
                    this.stats = res.data;
                    // console.log("stats: ")
                    // console.log(this.stats)
                }
            )
        }
        // console.log("stats mini stats: ");
        // console.log(this.stats);
        // console.log("ref: ")
        // console.log(this.statselector)
    }

    // @watch("stats.dic")
    public get getDicValues() {
        // console.log("sdf: " + JSON.stringify(sdf))
        return Object.values(this.stats.dic);
    }
    // public get getGrowthValues() {
    //     return Object.values(this.stats.growth);
    // }
    public getGrowth(statId) {
        return this.stats.growth[statId];
    }

    public getCharacName(statId: string) {
        // console.log("getCharacName for: " + statId)
        return Characteristics.getCharac(statId).baseName;
    }
    public getCharacType(statId: string) {
        // console.log("getCharacType: " + statId)
        return Characteristics.getCharac(statId);
    }

    public isSimple(stat: IStat) {
        if (!stat) return;
        let characType = this.getCharacType(stat.statId);
        return characType.statValueType == StatValueType.Simple && characType.enumValueConstraint == null
    }
    public isBool(stat: IStat) {
        if (!stat) return;
        return this.getCharacType(stat.statId).statValueType == StatValueType.Bool
    }
    public isEnum(stat: IStat) {
        if (!stat) return;
        let characType = this.getCharacType(stat.statId);
        return characType.statValueType == StatValueType.Simple && characType.enumValueConstraint != null
    }
    public getEnum(stat: IStat) {
        if (!stat) return;
        let characType = this.getCharacType(stat.statId);
        if(!characType.enumValueConstraint) return "";
        let data = characType.enumValueConstraint.split(",")[0];
        let data2 = data.split(".");
        return data2[data2.length - 1].trim();
    }

    public onChangeStatValue() {
        // console.log("Statsmini onChangeStatValue: save effect");
        this.save();
    }
    public clickRemoveStat(stat: IStat) {
        delete this.stats.dic[stat.statId];
        // console.log("Statsmini clickRemoveStat: save effect");
        this.save();
    }

    public onAddStat(property) {
        // console.log("Statsmini.onAddStat: " + JSON.stringify(property))
        this.statsController.postStat({ characID: property.id }).then(
            res => {
                this.stats.dic[res.data.statId] = res.data;
                
                if(this.hasgrowth) {
                    let equation: MathEquation = {
                        functions: [
                            {
                                xFromIncluded: Constants.MAX_INT,
                                xToExcluded: Constants.MIN_INT,
                                slopes: [0]
                            }
                        ]
                    }
                    this.stats.growth[res.data.statId] = equation; //res.data;
                }
                console.log("Statsmini bubble up callback: ")
                console.log(this.stats)
                this.save();
            }
        )
    }

    public save() {
        this.callbacksavestat();
        if(this.statsuid) {
            this.statsController.putStats(this.statsuid, this.stats).then(
                res => this.stats = res.data
            )
        }
    }

}

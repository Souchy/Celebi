import { bindable, inject, observable } from "aurelia";
import { IStat, IStats, StatValueType, Stats } from "../../../../jolteon/services/api/data-contracts";
import { StatsModelController } from "../../../../jolteon/services/api/StatsModelController";
import { Characteristics, Enums } from "../../../../jolteon/constants";

@inject(StatsModelController)
export class Statsmini {

    public Enums: Enums = Enums;
    // public stats type (StatusInstanceStats? CreatureStats? ... -> what properties are allowed?)

    @bindable
    public stats: Stats
    @bindable
    public callbacksavestat = () => {};

    constructor(private readonly statsController: StatsModelController) {

    }

    /**
     * hook
     */
    binding() {
        // console.log("stats mini stats: ");
        // console.log(this.stats);
        // console.log("ref: ")
        // console.log(this.statselector)
    }

    public getDicValues() {
        return Object.values(this.stats.dic);
    }
    public getGrowthValues() {
        return Object.values(this.stats.growth);
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
        if(!stat) return;
        return this.getCharacType(stat.statId).statValueType == StatValueType.Simple
    }
    public isBool(stat: IStat) {
        if(!stat) return;
        return this.getCharacType(stat.statId).statValueType == StatValueType.Bool
    }


    public onChangeStatValue() {
        // console.log("Statsmini onChangeStatValue: save effect");
        this.callbacksavestat();
    }
    public clickRemoveStat(stat: IStat) {
        delete this.stats.dic[stat.statId];
        // console.log("Statsmini clickRemoveStat: save effect");
        this.callbacksavestat();
    }

    public onAddStat(property) {
        // console.log("Statsmini.onAddStat: " + JSON.stringify(property))
        this.statsController.postStat({ characID: property.id }).then(
            res => {
                this.stats.dic[res.data.statId] = res.data;
                // console.log("Statsmini bubble up callback")
                this.callbacksavestat();
            }
        )
    }

}

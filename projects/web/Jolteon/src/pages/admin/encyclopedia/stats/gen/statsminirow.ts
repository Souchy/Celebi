import { bindable, inject } from "aurelia";
import { Characteristics, Enums } from "../../../../../jolteon/constants";
import { IStat, MathEquation, StatValueType } from "../../../../../jolteon/services/api/data-contracts";

@inject()
export class StatsMiniRow {

    // Enum hooks
    public Enums: Enums = Enums;
    public Characteristics: Characteristics = Characteristics;

    @bindable
    public hasadddelete: boolean
    @bindable
    public hasgrowth: boolean

    @bindable
    public statid: string
    @bindable
    public base: IStat;
    @bindable
    public growth: MathEquation;

    @bindable
    public callbacksavestat = () => { };
    @bindable
    public callbackdeletestat = (s) => { };


    constructor() { //private readonly base: IStat, private readonly growth: MathEquation) {
    }

    binding() {
        // console.log("stats mini row: " + this.statId + ", " + JSON.stringify(Characteristics.getCharac(this.statId)))
        // console.log(this.base)
    }

    public get statId() {
        return this.statid;
        // return this.base.statId;
    }
    public getBase() {
        return this.base;
    }
    public getGrowth() {
        return this.growth;
    }

    public getCharacName() {
        // console.log("statsminirow.getCharacName for: " + this.statid + ", " + JSON.stringify(this.base))
        return Characteristics.getCharac(this.statId).baseName;
    }
    public getCharacType() {
        // console.log("getCharacType: " + statId)
        return Characteristics.getCharac(this.statId);
    }

    public isSimple() {
        let characType = this.getCharacType();
        return characType.statValueType == StatValueType.Simple && characType.enumValueConstraint == null
    }
    public isBool() {
        return this.getCharacType().statValueType == StatValueType.Bool
    }
    public isEnum() {
        let characType = this.getCharacType();
        return characType.statValueType == StatValueType.Simple && characType.enumValueConstraint != null
    }
    public getEnum() {
        let characType = this.getCharacType();
        if (!characType.enumValueConstraint) return "";
        let data = characType.enumValueConstraint.split(",")[0];
        let data2 = data.split(".");
        return data2[data2.length - 1].trim();
    }


    public onChangeStatValue() {
        this.save();
    }
    public clickRemoveStat(statid: string) {
        this.callbackdeletestat(statid);
        this.save();
    }
    public save() {
        // bubble up to statsmini.ts
        this.callbacksavestat();
    }

}

import { IEventAggregator, bindable, containerless, inject } from "aurelia";
import { Characteristics } from "../../../../../jolteon/constants";
import { StatBool } from "../../../../../jolteon/services/api/data-contracts";

@inject(IEventAggregator)
@containerless
export class Statbool {

    @bindable
    public basestat: StatBool
    // @bindable
    // public growthstat: StatSimple

    constructor(private readonly ea: IEventAggregator) { }

    public onChangeBase() {
        // console.log("onChangeBase");
        this.ea.publish("stat:base:change", this.basestat); //{ id: this.characid, value: this.basevalue });
    }
    // public onChangGrowth() {
    //     // console.log("onChangeGrowth");
    //     this.ea.publish("stat:growth:change", this.growthstat);// { id: this.characid, value: this.growthvalue });
    // }

    binding(){
        // console.log("binding StatSimple: " + JSON.stringify(this.basestat))
    }

    public get characName() {
        return Characteristics.getCharac(this.basestat.statId).baseName;
    }
    

}

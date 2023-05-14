import { IEventAggregator, bindable, containerless, inject } from "aurelia";
import { IStatSimple, StatBool, StatSimple, Stats } from "../../../../jolteon/services/api/data-contracts";
import { Characteristics } from "../../../../jolteon/constants";

@inject(IEventAggregator)
@containerless
export class Statsimple {

    @bindable
    public basestat: StatSimple
    @bindable
    public growthstat: StatSimple

    constructor(private readonly ea: IEventAggregator) { }

    public onChangeBase() {
        // console.log("onChangeBase");
        this.ea.publish("stat:base:change", this.basestat); //{ id: this.characid, value: this.basevalue });
    }
    public onChangGrowth() {
        // console.log("onChangeGrowth");
        this.ea.publish("stat:growth:change", this.growthstat);// { id: this.characid, value: this.growthvalue });
    }

    binding(){
        // console.log("binding StatSimple: " + JSON.stringify(this.basestat))
    }

    public get characName() {
        return Characteristics.getCharac(this.basestat.statId).baseName;
    }
    


}
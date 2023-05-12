import { IEventAggregator, bindable, containerless, inject } from "aurelia";
import { IStatSimple } from "../../../../jolteon/services/api/data-contracts";

@inject(IEventAggregator)
@containerless
export class Statsimple {

    @bindable
    public basestat: IStatSimple
    @bindable
    public growthstat: IStatSimple
    // @bindable
    // public basevalue: number = 0;
    // @bindable
    // public growthvalue: number = 0;
    // @bindable
    // public hasgrowth: boolean = false;

    // @bindable
    // public characid: string; //{ id: string, baseName: string }
    // @bindable
    // public characname: string;

    constructor(private readonly ea: IEventAggregator) {

    }

    public onChangeBase() {
        this.ea.publish("stat:base:change", this.basestat); //{ id: this.characid, value: this.basevalue });
    }
    public onChangGrowth() {
        this.ea.publish("stat:growth:change", this.growthstat);// { id: this.characid, value: this.growthvalue });
    }


}

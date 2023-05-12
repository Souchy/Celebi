import { IEventAggregator, bindable, containerless, inject } from "aurelia";

@inject(IEventAggregator)
@containerless
export class Statsimple {

    @bindable
    public basevalue: number = 0;
    @bindable
    public growthvalue: number = 0;
    @bindable
    public hasgrowth: boolean = false;

    @bindable
    public characid: string; //{ id: string, baseName: string }
    @bindable
    public characname: string;

    constructor(private readonly ea: IEventAggregator) {

    }

    public onChangeBase() {
        this.ea.publish("stat:base:change", { id: this.characid, value: this.basevalue });
    }
    public onChangGrowth() {
        this.ea.publish("stat:growth:change", { id: this.characid, value: this.growthvalue });
    }


}

import { bindable, customElement } from "aurelia";
import { ActorType, Direction8Type, Direction9Type, IZone, Rotation4Type, ZoneType } from "../../../jolteon/services/api/data-contracts";
import { Enums } from "../../../jolteon/constants";


@customElement('zone')
export class Zone {
    // hook enums
    public readonly Enums: Enums = Enums;

    @bindable
    public zone: IZone;
    @bindable
    public uid: string;
    
    // 
    public minimized: boolean = true;

    bound() {
        // console.log("binded zone:")
        // console.log(this.zone)
    }
    

    public save() {

    }

    public clickMinimize() {
        this.minimized = !this.minimized;
    }

}

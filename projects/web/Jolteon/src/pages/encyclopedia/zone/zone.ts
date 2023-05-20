import { bindable, customElement } from "aurelia";
import { ActorType, Direction8Type, Direction9Type, IZone, Rotation4Type, ZoneType } from "../../../jolteon/services/api/data-contracts";
import { Enums } from "../../../jolteon/constants";


@customElement('zone')
export class Zone {
    // hook enums
    public readonly Enums: Enums = Enums;

    @bindable
    public zone: IZone;

    bound() {
        console.log("binded zone:")
        console.log(this.zone)
    }
    
    public zoneTypes() {
        return Object.keys(ZoneType).filter(k => isNaN(+k));
    }
    public direction8() {
        return Object.keys(Direction8Type).filter(k => isNaN(+k));
    }
    public direction9() {
        return Object.keys(Direction9Type).filter(k => isNaN(+k));
    }
    public rotation4() {
        return Object.keys(Rotation4Type).filter(k => isNaN(+k));
    }
    public actors() {
        return Object.keys(ActorType).filter(k => isNaN(+k));
    }


}

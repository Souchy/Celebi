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
    @bindable
    public callbacksave = () => {};
    
    // 
    public minimized: boolean = true;

    bound() {
        // console.log("binded zone:")
        // console.log(this.zone)
    }

    public zoneSizeHasZ() {
        return [
            ZoneType.cross,
            ZoneType.xcross,
            ZoneType.star,
            ZoneType.crossHalf,
            ZoneType.xcrossHalf,
            ZoneType.rectangle,
            ZoneType.ellipse,
            ZoneType.ellipseHalf,
        ].includes(this.zone.zoneType["value"])
    }
    public zoneSizeHasY() {
        return false;
    }    

    public save() {
        console.log("Zone save")
        this.callbacksave();
    }

    public clickMinimize() {
        this.minimized = !this.minimized;
    }

}

import { bindable } from "aurelia";
import { Characteristics, Enums } from "../../../../jolteon/constants";
import { StatValueType } from "../../../../jolteon/services/api/data-contracts";

export class Statpropertyselector {

    public Characteristics: Characteristics = Characteristics;

    @bindable
    public callbackselect = (p) => { }

    //
    public filter: string

    constructor() {
        // console.log(Characteristics.sectioned);
    }
    
    public clickCharacteristic(property) {
        // console.log("Statpropertyselector.clickCharacteristic: " + JSON.stringify(property))
        this.callbackselect(property);
    }

    // public search() {

    // }

    public getSectionName(properties: any[]) {
        return properties[0].nameModelUid.split(".")[1];
    }

    public getValueType(property: any) {
        let i = property.statValueType;
        return StatValueType[i];
    }


}

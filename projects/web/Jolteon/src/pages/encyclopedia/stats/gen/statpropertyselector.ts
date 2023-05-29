import { Characteristics, Enums } from "../../../../jolteon/constants";
import { StatValueType } from "../../../../jolteon/services/api/data-contracts";

export class Statpropertyselector {

    public Characteristics: Characteristics = Characteristics;

    public filter: string

    public callbackselect = (p) => { }

    constructor() {
        console.log(Characteristics.sectioned);
    }
    
    // public clickCharacteristic(property) {
    //     this.callbackselect(property);
    // }

    // public search() {

    // }

    // public getSectionName(properties: any[]) {
    //     return properties[0].nameModelUid.split(".")[1];
    // }

    // public getValueType(property: any) {
    //     let i = property.statValueType;
    //     return StatValueType[i];
    // }


}

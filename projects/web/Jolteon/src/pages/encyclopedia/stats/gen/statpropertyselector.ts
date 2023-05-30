import { bindable } from "aurelia";
import { Characteristics, Enums } from "../../../../jolteon/constants";
import { StatValueType } from "../../../../jolteon/services/api/data-contracts";

export class Statpropertyselector {

    public readonly Characteristics: Characteristics = Characteristics;

    @bindable
    public characs: any[] = Characteristics.allSectioned
    @bindable
    public callbackselect = (p) => { }

    // TODO stat selector filter
    // public filter: string = ""

    constructor() {
        // console.log(Characteristics.sectioned);
    }

    binding() {
        // console.log("stat selector: ")
        // console.log(this.characs);
    }

    public hasMultipleSections() {
        if(!this.characs) return false;
        // console.log("stat selector has sections? : " + JSON.stringify(this.characs))
        return Array.isArray(this.characs[0])
    }
    public get sections() {
        if(this.hasMultipleSections()) {
            // let test = this.characs.map(section => this.filter ? section.filter(s => s.baseName.includes(this.filter)) : section);
            // console.log("test: ")
            // console.log(test)
            return this.characs;
        } else {
            // let test = [this.characs.filter(s => this.filter ? s.baseName.includes(this.filter) : s)];
            // console.log("test: ")
            // console.log(test)
            return [this.characs];
        }
    }
    
    public clickCharacteristic(property) {
        // console.log("Statpropertyselector.clickCharacteristic: " + JSON.stringify(property))
        this.callbackselect(property);
    }

    public search() {

    }

    public getSectionName(properties: any[]) {
        // console.log("getSectionName: " + JSON.stringify(properties))
        return properties[0].nameModelUid.split(".")[1];
    }

    public getValueType(property: any) {
        let i = property.statValueType;
        return StatValueType[i];
    }


}

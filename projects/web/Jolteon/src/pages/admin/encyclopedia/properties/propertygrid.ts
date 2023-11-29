import { bindable } from "aurelia";
import { SchemaDescription } from "../../../../jolteon/services/api/data-contracts";
import { Characteristics, Enums, Schemas } from '../../../../jolteon/constants';


export default class PropertyGrid {
    // hook enums
    public readonly Enums: Enums = Enums;
    public readonly Characteristics: Characteristics = Characteristics;

    @bindable
    public data: any;
    @bindable
    public schema: SchemaDescription;
    @bindable
    public uid: string;
    @bindable
    public callbacksave = () => {};


    constructor() {
    }

    binding() {
        // if(this.schema.name == "DirectDamage") {
        //     console.log("PropertyGrid data: ")
        //     console.log(this.data);
        // }
    }

    public get keys() {
        return Object.keys(this.data).filter(k => !Schemas.ignoredProperties.includes(k));
    }
    public get values() {
        return Object.values(this.data);
    }
    public type(prop) {
        return typeof this.data[prop];
    }
    public propName(prop: string) {
        return this.schema.properties[prop];
    }

    public onChange() {
        // console.log("propertygrid onchange")
        this.callbacksave();
    }

}


import { bindable } from "aurelia";
import { SchemaDescription } from "../../../../jolteon/services/api/data-contracts";
import { Enums } from '../../../../jolteon/constants';


export default class PropertyGrid {
    // hook enums
    public readonly Enums: Enums = Enums;

    @bindable
    public data: any;
    @bindable
    public schema: SchemaDescription;


    constructor() {
    }

    public get keys() {
        return Object.keys(this.data);
    }
    public get values() {
        return Object.values(this.data);
    }
    public typof(prop) {
        return typeof this.data[prop];
    }
    public propName(prop: string) {
        return this.schema.properties[prop];
    }

}


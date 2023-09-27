import { bindable, customElement, inject } from 'aurelia';
import { SchemaDescription } from '../../../../../jolteon/services/api/data-contracts';
import { Schemas } from '../../../../../jolteon/constants';

// @customElement('ce-effecttype-selector')
export class TriggerModelSelector {

    // @bindable
    // public selectFunction;
    // public onSelect;
    @bindable
    public onselectcallback = (schema) => {};

    // string filter
    public filter: string = "";

    // db data
    public schemasDescriptions: SchemaDescription[] = []
    //
    public filteredSchemas: SchemaDescription[] = []

    constructor() {
        // console.log("ctor trigger model selector")
        this.schemasDescriptions = Schemas.triggers;
        this.filteredSchemas = Schemas.triggers;
    }

    public len(schema: SchemaDescription) {
        return Object.keys(schema.properties).length
    }

    public strin(schema: SchemaDescription) {
        return JSON.stringify(schema.properties);
    }
    public props(schema: SchemaDescription) {
        return Object.keys(schema.properties).filter(k => k != "$type");
    }

    public search() {
        // console.log("search: " + this.filter);
        let str = this.filter;
        if(!str) this.filteredSchemas = this.schemasDescriptions;
        str = str.toLowerCase();
        this.filteredSchemas = this.schemasDescriptions.filter(sc => {
            if(sc.name.toLowerCase().includes(str)) return true;
            if(this.props(sc).some(k => k.toLowerCase().includes(str))) return true;
            if(this.props(sc).some(k => sc.properties[k].toLowerCase().includes(str))) return true;
        })
    }

    public clickTriggerModel(schema) {
        // console.log("click " + schema.name)
        this.onselectcallback(schema);
    }


}

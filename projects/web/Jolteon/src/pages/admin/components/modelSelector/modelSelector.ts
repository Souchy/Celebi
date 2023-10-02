import { bindable, customElement, inject } from 'aurelia';
import { SchemaDescription } from '../../../../jolteon/services/api/data-contracts';
import { Schemas } from '../../../../jolteon/constants';

// @customElement('ce-effecttype-selector')
export class ModelSelector {

    @bindable
    public schemas: SchemaDescription[] 

    // @bindable
    // public selectFunction;
    // public onSelect;
    @bindable
    public onselectcallback = (schema) => {};

    // string filter
    public filter: string = "";

    // db data
    public filteredSchemas: SchemaDescription[] = []

    constructor() {
        // console.log("ctor model selector")
    }
    binding() {
        // console.log("binding model selector: ");
        // console.log(this.schemas)
        this.search();
    }

    public len(schema: SchemaDescription) {
        return Object.keys(schema.properties).length
    }

    public strin(schema: SchemaDescription) {
        return JSON.stringify(schema.properties);
    }
    public props(schema: SchemaDescription) {
        return Object.keys(schema.properties).filter(k => !Schemas.ignoredProperties.includes(k));
    }

    public search() {
        // console.log("search: " + this.filter);
        let str = this.filter;
        if(!str) {
            this.filteredSchemas = this.schemas;
            return;
        }
        str = str.toLowerCase();
        this.filteredSchemas = this.schemas.filter(sc => {
            if(sc.name.toLowerCase().includes(str)) return true;
            if(this.props(sc).some(k => k.toLowerCase().includes(str))) return true;
            if(this.props(sc).some(k => sc.properties[k].toLowerCase().includes(str))) return true;
        })
    }

    public clickModel(schema) {
        // console.log("click " + schema.name)
        this.onselectcallback(schema);
    }


}

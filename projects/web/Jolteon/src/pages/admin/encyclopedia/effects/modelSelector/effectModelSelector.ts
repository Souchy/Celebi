import { bindable, customElement, inject } from 'aurelia';
import { PropertiesController } from '../../../../../jolteon/services/api/PropertiesController';
import { SchemaDescription } from '../../../../../jolteon/services/api/data-contracts';
import { Effects } from '../../../../../jolteon/constants';

// @customElement('ce-effecttype-selector')
@inject(PropertiesController)
export class EffectModelSelector {

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

    constructor(private readonly propertiesController: PropertiesController) {
        // console.log("ctor effect model selector")
        this.schemasDescriptions = Effects.schemas;
        this.filteredSchemas = Effects.schemas;
        // this.propertiesController.getEffectsSchemas()
        //     .then(res => {
        //         this.schemasDescriptions = res.data;
        //         this.filteredSchemas = this.schemasDescriptions;
        //     });
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

    public clickEffectModel(schema) {
        // console.log("click " + schema.name)
        this.onselectcallback(schema);
    }


}

import { bindable } from "aurelia";

/**
 * This ui module is used to create or modify the i18n value of a i18n key
 * So you have an i18n field somewhere that opens this module with the key that's inside the field
 * Then you can edit the value that is stored at that key
 */
export class i18n_module {
    
    @bindable
    public key: string

    public value: string

    constructor() {

    }


    public close() {
        this.save();
    }
    public save() {

    }

}

import { bindable } from "aurelia";
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController";

export class SpellList {

    @bindable
    public spellids: string[]

    constructor(private readonly spellController: SpellModelController) {

    }

    binding() {
        if(this.spellids != undefined) {
            this.spellController.getList(this.spellids);
        }
    }

}
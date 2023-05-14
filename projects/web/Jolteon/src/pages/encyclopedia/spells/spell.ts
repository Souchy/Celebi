import { bindable, inject } from "aurelia"
import { SpellModel } from "../../../jolteon/services/api/data-contracts"
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController"

@inject(SpellModelController)
export class Spell {

    // comes from either html bind or route parameter
    @bindable
    public spellid: string
    @bindable
    public isvignette: boolean = false;

    // db data
    public model: SpellModel

    constructor(private readonly controller: SpellModelController) {

    }

    loading() {
        if(this.spellid) this.loadSpell();
    }
    binding() {
        if(this.spellid) this.loadSpell();
    }

    private loadSpell() {
        try {
            this.controller.getSpell(this.spellid);
        }
    }

}

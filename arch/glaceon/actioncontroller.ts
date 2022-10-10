import { Cell } from "../common/entity";
import { SpellResult } from "../common/red/spellresult";
import { Spell } from "../common/spell";

// 1st-2nd class input handler
export class ActionController {
    public selectedSpell: Spell;
    public selectedSpellResult: SpellResult;

    // when selected a spell with shortcuts or left-click
    public selectSpell(s: Spell) {
        this.selectedSpell = s;
    }

    // when hovering a new cell with the selected spell
    public highlight(cell: Cell) {
        this.compileSpell(cell);
        // highlight stuff (damage done, aoe, movements..)
    }

    // when left-clicking a cell with the selected spell
    public apply(cell: Cell) {
        /*
        actually just send the packet to the server, then it will apply the spell directly 
        */
        // this.compileSpell(cell);
        // // apply compiled effects
    }

    private compileSpell(cell: Cell) {
        if(this.selectedSpellResult && this.selectedSpellResult.cellCast == cell) 
            return; // already compiled for this cell
        // otherwise compile 
    }
}

import { EffectType } from "../effects";
import { Cell, Creature } from "../entity";
// import { Cost, Spell } from "../common/spells";


export class SpellResult {
    // public spell: Spell;
    public source: Creature
    public cellCast: Cell;
    public finalCosts: {}; // public finalCosts: Cost[];
    public children: EffectResult[];
}

export class EffectResult {
    public type: EffectType;
    public parent: any;
    public children: EffectResult[];
    public data: any;
}



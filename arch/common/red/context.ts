import { EffectResult, SpellResult } from "./spellresult";

export class Context {

}
export class FightContext extends Context {
    public fightid: number;
    public rounds: Map<number, RoundContext>
}
export class RoundContext extends Context {
    public roundid: number;
    // source creatures better than turns i think? 
    // easier to check if theyre in the same team too
    // public creatures: Map<number, TurnContext>
    public turns: TurnContext[]; // just an array in order of play?
}
export class TurnContext extends Context {
    public creatureid: number;
    public spells: Map<number, SpellContext>
}
export class SpellContext extends Context {
    public spellid: number; // public spellcopy: Spell;
    public spellResult: SpellResult;
    public effects: EffectContext[]; // public effects: Map<number, EffectContext>
}
export class EffectContext extends Context {
    // ??
    // public effectcopy: Effect;
    // need compiled effect
    public effectResult: EffectResult
}

export enum ContextType {
    fight,
    round,
    turn,
    spell,
    effect
}
export const contextTypes = Object.values(ContextType).filter(item => isNaN(Number(item))).map(item => item as ContextType)
export enum ContextCharacteristicType {
    life_gained,
    life_reduced,
    ap_gained,
    ap_reduced,
    mp_gained,
    mp_reduced,
    special_gained,
    special_reduced,

    number_targets_affected,
}
export const contextCharacteristicTypes = Object.values(ContextCharacteristicType).filter(item => isNaN(Number(item))).map(item => item as ContextCharacteristicType)

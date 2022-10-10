import { CharacteristicType, StateType } from "./characteristics"
import { Condition } from "./components/condition" 
import { TargetFilter } from "./components/target";
import { Vector2, Vector2s, Vector3, Vector3s } from './util/util'
import { Zone } from './components/zone'

export enum EffectType {
    damage, heal,
    resource_steal, resource_increase, resource_reduce,
    push, pull,
    teleport, swap,
    random,
    spell, trap, glyph,
}
export const effectTypes = Object.values(EffectType).filter(item => isNaN(Number(item))).map(item => item as EffectType)

export class Effect {
    private static counter = 0;
    public readonly uid: number;
    public readonly isEffect = true;
    public type: EffectType
    public parent: any; // either an effect or spell at the root
    public effects: Effect[] = [] // children

    public data: any = {}
    public zone: Zone = new Zone()
    public necessaryForSpellCast: boolean
    public visibleInTooltip: boolean = true
    public condition: Condition = new Condition()
    
    public towerDirection: number = 0 // 1: up, -1 down, 0 extends both ways from start
    public towerStart: number = 0 // 1: top, 0: ground, -1: underground
    public towerLength: number = 0 //  number of tower floors to travel in each direction
    
    
    // public appliesToState: Map<State, boolean>
    public appliesToState: {}
    
    constructor() {
        this.uid = Effect.counter++
    }
    // public parse(json: string) {
    //     let obj = JSON.parse(json);
    // }
}

// class EffectData {}

export class DamageData {
    // needs to be a string bc it could be 5% for example
    // really need a ValueMaker thing
    public value: string; 
    public element: Element;

    public calculate() { } // aka compile
    public parse(jsonData: string) { }
}
export class StatusData {

}
export class SubEffet {

}

export class MoveData {}
export class PushData { public cells: number; } // push a target by x cells
export class PushToData {} // push the first creature in line up to the target cell
export class PullData {} // pull the target by x cells
export class PullToData {} // pull the first creature in line to the target cell
export class DashData {} 
export class DashToData {}
export class SwapData {}
export class TeleportData {}
// need to be able to change the pivot actor and the teleporting actor
// ex: teleport self above x, teleport x above self, teleport x above y
export class TeleportSymmetricallyData {} 

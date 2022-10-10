import { CharacteristicType, StateType } from "./characteristics"
import { Condition } from "./condition" 

export enum EffectType {
    Damage, Heal,
    ApReduce, ApIncrease,
    MpReduce, MpIncrease,
    Push, Pull,
    Teleport, Swap,
    Random,
}
export const effectTypes = Object.values(EffectType).filter(item => isNaN(Number(item))).map(item => item as EffectType)

export class Effect {
    private static counter = 0;
    public readonly uid: number = Effect.counter++;
    public readonly isEffect = true;
    public type: EffectType
    public parent: any; // either an effect or spell at the root
    public effects: Effect[] = [] // children

    public data: string = ""
    public targetMask: string = ""
    public necessaryForSpellCast: boolean
    public condition: Condition = new Condition()

    public towerDirection: number = 0 // 1: up, -1 down, 0 extends both ways from start
    public towerStart: number = 0 // 1: top, 0: ground, -1: underground
    public towerLength: number = 0 //  number of tower floors to travel in each direction
    
    public visibleInTooltip: boolean = true

    // public appliesToState: Map<State, boolean>
    public appliesToState: {}
    

    // public parse(json: string) {
    //     let obj = JSON.parse(json);
    // }
    constructor() {

    }

    // public getHashCode() {
    //     // let str = JSON.stringify(this);
    //     // var hash = 0;
    //     // for (var i = 0; i < str.length; i++) {
    //     //     var code = str.charCodeAt(i);
    //     //     hash = ((hash<<5)-hash)+code;
    //     //     hash = hash & hash; // Convert to 32bit integer
    //     // }
    //     // return hash;
    //     return "" + this.type;
    // }
}




class DamageEffect extends Effect {
    public ratios: Map<CharacteristicType, number>
    public calculate() { // aka compile
        // ...
    }
    public parse(jsonData: string) {
        //base.parse(str)
        // then parse ratios or other specific effect data
        this.ratios = JSON.parse(jsonData);
    }
}
class StatusEffect {

}
class SubEffet {

}

class MoveEffect {}
class PushEffect { public cells: number; } // push a target by x cells
class PushToEffect {} // push the first creature in line up to the target cell
class PullEffect {} // pull the target by x cells
class PullToEffect {} // pull the first creature in line to the target cell
class DashEffect {} 
class DashToEffect {}
class SwapEffect {}
class TeleportEffect {}
// need to be able to change the pivot actor and the teleporting actor
// ex: teleport self above x, teleport x above self, teleport x above y
class TeleportSymmetricallyEffect {} 

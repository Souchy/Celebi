import { ConditionActorType } from './../components/condition';
import { TargetFilter } from "../components/target";
import { Effect, EffectType } from "../effects";
import { Stats } from "../stats";

// soso red
export class Status {
    public spellId: number
    // public sourceId: number
    public effects: TriggeredEffect[] = []; //Effect[];
    public maxStacks: number = 0 // 0 = infini
    public maxDuration: number = 0 // 0 = infini
    public refreshAllOnNewInstance: boolean = false
}

// super red
export class StatusInstance {
    public sourceId: number
    public stats: Stats
    public duration: number
}

export class TriggeredEffect extends Effect {
    public triggers: Trigger[] = [];
    public whoCastTheEffect: TriggerActor = TriggerActor.statusHolder // who casts the triggered effect? either the status source (creator), status holder, or the one who triggers the trigger
}

export class Trigger {
    public trigger: TriggerType;
    public data: any = {}
    public onEffectType: EffectType // ex: on damage, on heal...
    public onReceiveEffect: boolean // ex: on receive damage
    public onDoEffect: boolean      // ex: on do damage
    // dont need to filter target/source on the trigger, it's always the holder of the status
    // ex: Return damage status: 
    //          on receive damage:
    //              return damage always, but only to enemies (the effect has a filter, not the trigger)
}

export enum MergeStrategy {
    takeBest,
    takeWorst,
    takeMostRecent,
    takeOldest,
    // mergeAdd,
}

export enum TriggerActor {
    statusHolder,
    statusCreator,
    triggerSource
}

export enum TriggerType {
    onFightStart,
    onFightEnd,
    onRoundStart,
    onRoundEnd,
    onTurnStart,
    onTurnEnd,
    onPass, // if pass turn before the end of the timer
    onTurnTimer,

    onCast,
    onWalk, // every cell in the walking path
    onWalkHalt, // only the last cell in the walking path
    onSwapIn,
    onSwapOut,
    onDeath,

    onEffect
}

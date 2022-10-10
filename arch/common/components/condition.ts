import { TargetFilter } from './target';
import { CharacteristicType, StateType } from "../characteristics";
import { Spell } from '../spell';
import { Effect } from '../effects';
import { ContextCharacteristicType, ContextType } from '../red/context'

export class Condition {
    public readonly isCondition = true;
    // condition < effect < spell 
    public parent: any;
    public children: Condition[] = []
    public childrenLink: ConditionLinkType = ConditionLinkType.and

    public actor: ConditionActorType = ConditionActorType.target

    public type: ConditionType = ConditionType.passthrough
    public data: ConditionData = {}

    // Context
    public scope: string = "local"; // "spell"
    public parse(json: string) {
        // condition scope
        // condition type
        // condition sign 
        // condition data
    }
}

export class ConditionData {}

export class ConditionDataStat extends ConditionData {
    public stat: CharacteristicType = CharacteristicType.life;
    public comparator: ConditionComparatorType = ConditionComparatorType.ge;
    public value: string = "0";
}

export class CondditionDataState extends ConditionData {
    public state: StateType = StateType.carried
    public value: string = "true"
}

export class ConditionDataContext extends ConditionData {
    public contextType: ContextType = ContextType.fight
    public contextCharac: ContextCharacteristicType = ContextCharacteristicType.life_gained;
    public comparator: ConditionComparatorType = ConditionComparatorType.ge;
    public value: string = "0";
}

export class ConditionFilter {
    public filter: TargetFilter = new TargetFilter;
}

export enum ConditionActorType {
    self,
    target,
    targets
}
export const conditionActorTypes = Object.values(ConditionActorType).filter(item => isNaN(Number(item))).map(item => item as ConditionActorType)
export enum ConditionLinkType {
    and,
    or
}
export const conditionLinkTypes = Object.values(ConditionLinkType).filter(item => isNaN(Number(item))).map(item => item as ConditionLinkType)
export enum ConditionComparatorType {
    lt,
    le,
    e,
    ge,
    gt
}
export const conditionComparatorTypes = Object.values(ConditionComparatorType).filter(item => isNaN(Number(item))).map(item => item as ConditionComparatorType)
export enum ConditionType {
    passthrough,
    stat,
    state,
    context
}
export const conditionTypes = Object.values(ConditionType).filter(item => isNaN(Number(item))).map(item => item as ConditionType)


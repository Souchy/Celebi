import { ResourceType } from './characteristics';
import { Effect } from './effects';

export class Spell {
    public _id: number
    public name: string = "unnamed"
    public targetMask: string = ""
    public costs: {} = {};

    public effects: Effect[] = [];

    public maxCastPerTurn: number = 0
    public maxCastPerTarget: number = 0
    public cooldown: number = 0
    public cooldownInitial: number = 0
    public cooldownGlobal: number = 0

    public iconId: number = 0
    public animationId: number = 0
    public scriptId: number = 0

    constructor() {
        let resources = Object.values(ResourceType);
        for(let res of resources.splice(0, resources.length / 2)) {
            this.costs[res] = 0;
        }
    }
}

// export class Cost {
//     public res: ResourceType
//     public value: number
// }

// export class Costs {
//     public life: number = 0
//     public ap: number = 0
//     public mp: number = 0
//     public special: number = 0
// }

import { ResourceType, resourceTypes } from './characteristics';
import { Effect } from './effects';
import { TargetFilter } from './components/target';

export class Spell {
    public readonly isSpell = true;

    public _id: number
    public name: string = "unnamed"
    // public costs: {} = {};
    public costs: Cost[] = [];
    public filter: TargetFilter = new TargetFilter()
    // public targetMask: string = ""

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
        // let resources = Object.values(ResourceType);
        // for(let res of resources.splice(0, resources.length / 2)) {
        //     this.costs[res] = 0;
        // }
        for(let res of resourceTypes) {
            let c = new Cost()
            c.res = res;
            this.costs.push(c);
        }
    }

}

export class Cost {
    public res: ResourceType
    public value: string = "0"
}

// export class Costs {
//     public life: number = 0
//     public ap: number = 0
//     public mp: number = 0
//     public special: number = 0
// }

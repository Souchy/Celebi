import { Stats } from "../stats";

export class Status {
    public spellId: number
    public sourceId: number
    public bonus: Stats
    public maxStacks: number;
    public maxDuration: number;
    public refreshAllOnNewInstance: boolean;
}

export class StatusInstance {
    public stats: Stats;
    public duration: number;
}



enum MergeStrategy {
    takeBest,
    takeWorst,
    takeMostRecent,
    takeOldest,
    // mergeAdd,
}

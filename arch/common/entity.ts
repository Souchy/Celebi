import { Status } from "./red/status";
import { Stats } from "./stats";

export class Entity {
    public _id;
    public statuses: Status[]
    public pos: Position
}

export class Cell extends Entity {
    public creatures: [];
}

export class Creature extends Entity {
    public name: string = "unnamed";
    public stats: Stats;
    public spells: []
}

export class Position {
    public x;
    public y;
    public z;
}

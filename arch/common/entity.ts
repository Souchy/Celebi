import { Status } from "./red/status";
import { Stats } from "./stats";

export class Entity {
    public id;
    public statuses: Status[]
    public pos: Position
}

export class Cell extends Entity {
    public creatures: [];
}

export class Creature extends Entity {
    public stats: Stats;
    public spells: []
}

export class Position {
    public x;
    public y;
    public z;
}

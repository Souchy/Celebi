
import { Vector2, Vector2s, Vector3, Vector3s } from '../util/util'
import { TargetFilter } from './target'

export class Zone {
    // X is used for length of lines, circle radius...
    // Y is used for rectangles and cones (half-circle)
    // Z is used for arcs and rings thickness
    public size: Vector3s = new Vector3s("1", "0", "0");
    public type: ZoneType = ZoneType.point
    // public targetMask: string = ""
    public filter: TargetFilter = new TargetFilter()
    public localOffset: Vector2s = new Vector2s("0", "0"); // offset from cast cell to local origin in the direction of the orientation
    public localOrigin: Direction9Type = Direction9Type.center // center for circle, start for line.. 
    public orientation: Direction9Type = Direction9Type.top // rotation?
    public checkers: boolean = false;
    public checkersOffset: string = "0";
    public checkersSize: string = "1"; // >1 means multiple black squares for 1 white. <1 means multiple white for 1 black where black=selected and white=discarded
}
export enum ZoneType {
    point, 
    line, //lineT,
    square, circle,
    squareRing, circleRing,
    squareArc, circleArc, // xy: [yyxxxyy] or [yxy]
    rectangle,
    cone,
}
export const zoneTypes = Object.values(ZoneType).filter(item => isNaN(Number(item))).map(item => item as ZoneType)

export enum Direction9Type {
    center,
    top, topright,
    right, bottomright, 
    bottom, bottomleft,
    left, topleft
}
export const direction9Types = Object.values(Direction9Type).filter(item => isNaN(Number(item))).map(item => item as Direction9Type)





export class Vector2 {
    public x: number = 0
    public y: number = 0
    public constructor(x: number, y: number) {
        this.x = x;
        this.y = y;
    }
}

export class Vector3 extends Vector2 {
    public z: number = 0
    public constructor(x: number, y: number, z: number) {
        super(x, y);
        this.z = z;
    }
}

export class Position extends Vector3 {

}



export class Vector2s {
    public x: string = "0"
    public y: string = "0"
    public constructor(x: string, y: string) {
        this.x = x;
        this.y = y;
    }
}
export class Vector3s extends Vector2s{
    public z: string = "0"
    public constructor(x: string, y: string, z: string) {
        super(x, y);
        this.z = z;
    }
}

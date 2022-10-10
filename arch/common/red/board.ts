import { Cell } from "../entity";

export class Board {
    // aka "height" on the 2d plane
    private readonly maxY: number = 100;
    // aka "width" on the 2d plane
    private readonly maxX: number = 100;
    // max Z
    private readonly maxHeight: number = 100;
    // min Z
    private readonly maxDepth: number = 100;

    
    public cells: Cell[];

    public getCellById(id: number): Cell {
        return this.cells[id];
    }
    public getCell(x: number, y: number): Cell {
        let id = x + y * this.maxX;
        return this.cells[id];
    }

}

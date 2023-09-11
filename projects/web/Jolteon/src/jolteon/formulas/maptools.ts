
export default class MapTools {

    // 2 rows of 14 at a time, offset by a diagonal
    public static readonly mapWidth = 20; // 14
    // 20 rows times 2
    public static readonly mapHeight = 20;
    // length of cell list 400
    public static readonly cellCount = MapTools.mapWidth * MapTools.mapHeight;
    // length of a segment on a cell
    public static readonly segmentLength = 40;
    // square width of a cell
    public static readonly u = 2 * Math.sin(60 * Math.PI / 180) * MapTools.segmentLength;
    // square height of a cell
    public static readonly v = 2 * Math.sin(30 * Math.PI / 180) * MapTools.segmentLength;
    // height of a block
    public static readonly w = 15;
    // half width of cell in 3d
    public static readonly cellhalf = 0.5;

    public static generateMap() {
        // Array.
    }

    public static getCellIdForPosition(cellx: number, celly: number) {
        return cellx * MapTools.mapWidth + celly;
    }

    public static getPositionForCellId(cellid: number) {
        let x = cellid % MapTools.mapWidth;
        let y = (cellid - x) / MapTools.mapWidth
        return { x, y }
    }

}

export enum CellType {
    hole,
    floor,
    block
}

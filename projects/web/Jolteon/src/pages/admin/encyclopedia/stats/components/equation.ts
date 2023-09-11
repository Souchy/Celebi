import { bindable } from "aurelia";
import { MathEquation, MathFunction } from "../../../../../jolteon/services/api/data-contracts";


export class Equation {

    @bindable
    public characid: string //CharacteristicId;
    @bindable
    public eq: MathEquation;

    constructor() {
        // this.eq.functions[0].xToExcluded
        // this.eq.functions[0].slopes[0]
    }


    
    // public get degree() {
    //     return this.eq.
    // }

    public changeSlopes(event, func: MathFunction) {
        console.log("changeSlopes:")
        console.log(event)
    }

}

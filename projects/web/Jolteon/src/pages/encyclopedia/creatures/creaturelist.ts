import { inject } from "aurelia";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { ICreatureModel, CreatureModel } from './../../../jolteon/services/api/data-contracts';

@inject(CreatureModelController)
export class CreatureList {

    public creatures: ICreatureModel[] = [];
    public selectedCreatures: [] = [];

    constructor(readonly http: CreatureModelController) {
        console.log("list ctor")
        this.refresh();
    }

    public refresh() {
        this.http.getAll().then(res => this.creatures = res.data);
    }

    public clickCreate() {
        let crea: CreatureModel = {
            entityUid: { value: "456" }
        }; //new ICreatureModel();
        this.creatures.push(crea);
        this.http.postCreature(crea);
    }

    public clickDelete() {
        for(let crea of this.selectedCreatures) {
            let id = "selected creature id";
            this.http.deleteCreature(id);
        }
    }
    
}

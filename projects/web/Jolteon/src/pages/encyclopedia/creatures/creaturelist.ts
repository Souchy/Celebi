import { inject } from "aurelia";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { ICreatureModel, CreatureModel } from './../../../jolteon/services/api/data-contracts';

@inject(CreatureModelController)
export class CreatureList {

    public creatures: ICreatureModel[] = [];
    public selectedCreatures: ICreatureModel[] = [];

    constructor(readonly http: CreatureModelController) {
        console.log("list ctor")
        this.refresh();
    }

    public refresh() {
        this.http.getAll().then(res => this.creatures = res.data);
    }

    public clickCreate() {
        let crea: CreatureModel = {
            // entityUid: { value: "456" }
        };
        this.http.postCreature(crea).then(res => {
            this.creatures.push(res.data);
        });
    }

    public clickDelete() {
        for (let crea of this.selectedCreatures) {
            let id = crea.entityUid;
            this.http.deleteCreature(id).then(res => {
                if (res.data.deletedCount !== 1) return;
                let index = this.creatures.findIndex(c => c.entityUid === id);
                if (index === -1) return;
                this.creatures.splice(index, 1);

            })
        }
        this.selectedCreatures = [];
    }

}

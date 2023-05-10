import { inject } from "aurelia";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { ICreatureModel, CreatureModel, IID } from './../../../jolteon/services/api/data-contracts';
import { IRouter } from "@aurelia/router";

@inject(CreatureModelController)
export class CreatureList {

    public creatures: ICreatureModel[] = [];
    public selectedCreatures: ICreatureModel[] = [];

    constructor(
        private readonly http: CreatureModelController,
        @IRouter private router: IRouter
    ) {
        // console.log("list ctor")
        this.refresh();
    }

    public refresh() {
        // TODO: limit 50 per page? add filters for name, element type...
        this.http.getAll().then(res => this.creatures = res.data);
    }

    public clickCreate() {
        // let crea: CreatureModel = {
        //     // entityUid: { value: "456" }
        // };
        // this.http.postCreature(crea).then(res => {
        //     this.creatures.push(res.data);
        // });
        this.http.postNew().then(res => {
            this.creatures.push(res.data);
        });
    }

    public clickDelete() {
        for (let crea of this.selectedCreatures) {
            let id: IID = crea.modelUid;
            this.http.deleteCreature(id).then(res => {
                if (res.data.deletedCount !== 1) return;
                let index = this.creatures.findIndex(c => c.modelUid === id);
                if (index === -1) return;
                this.creatures.splice(index, 1);

            })
        }
        this.selectedCreatures = [];
    }

    public clickCreature(modelUid: IID) {
        this.router.load("/editor/creature/" + modelUid.value);
    }

}

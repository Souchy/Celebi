import { inject } from "aurelia";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { ICreatureModel, CreatureModel } from './../../../jolteon/services/api/data-contracts';
import { IRouteableComponent, IRouter } from "@aurelia/router";

@inject(CreatureModelController)
export class CreatureList implements IRouteableComponent {

    public creatures: ICreatureModel[] = [];
    public selectedCreatures: ICreatureModel[] = [];

    // TODO pages for Creature list and Spell list
    public readonly numPerPage = 50;
    public page: number = 0;

    constructor(
        private readonly creatureController: CreatureModelController,
        @IRouter private router: IRouter
    ) {
        // console.log("list ctor")
        this.refresh();
    }

    public refresh() {
        // TODO: limit 50 per page? add filters for name, element type...
        let filter = {
            skip: this.page * this.numPerPage,
            limit: this.numPerPage
        }
        this.creatureController.getAll().then(res => this.creatures = res.data);
    }

    public clickCreate() {
        // let crea: CreatureModel = {
        //     // entityUid: { value: "456" }
        // };
        // this.http.postCreature(crea).then(res => {
        //     this.creatures.push(res.data);
        // });
        this.creatureController.postNew().then(res => {
            this.creatures.push(res.data);
        });
    }

}

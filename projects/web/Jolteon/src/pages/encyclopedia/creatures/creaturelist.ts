import { inject } from "aurelia";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { ICreatureModel, CreatureModel } from './../../../jolteon/services/api/data-contracts';
import { IRouteableComponent, IRouter } from "@aurelia/router";
import { Creature } from "./creature";

@inject(CreatureModelController)
export class CreatureList implements IRouteableComponent {

    public creatures: ICreatureModel[] = [];
    public filteredCreatures: ICreatureModel[] = []
    public selectedCreatures: ICreatureModel[] = [];
    public filter: string = ""

    // view-model refs
    public refs: Creature[] = []


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
        this.creatureController.getAll().then(res => {
            this.creatures = res.data;
            this.filteredCreatures = this.creatures;
        });
    }

    public clickCreate() {
        this.creatureController.postNew().then(res => {
            this.creatures.push(res.data);
            this.filteredCreatures.push(res.data);
        });
    }

    public onSearch() {
        if(!this.filter) {
            this.filteredCreatures = this.creatures;
            return;
        }
        let str = this.filter.toLowerCase();
        // console.log("creature search: " + str);
        console.log(this.refs);
        this.filteredCreatures = this.refs.filter(c => {
            console.log("crea search: " + c?.name?.entity?.value)
            return  c?.name?.entity?.value.toLowerCase().includes(str) ||
                    c?.desc?.entity?.value.toLowerCase().includes(str);
        }).map(v => this.creatures.find(c => c.modelUid == v.model.modelUid));
    }

}

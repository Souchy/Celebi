import { IStats, AffinityTypes, ResourceTypes, StateTypes, ResistanceTypes, ContextualTypes, OtherPropertyTypes, SpellPropertyTypes, StatusModelPropertyTypes, SpellModelPropertyTypes } from './../../../jolteon/services/api/data-contracts';
import { bindable } from "aurelia";
import { StatsModelController } from "../../../jolteon/services/api/StatsModelController";
import { IRouteableComponent } from "@aurelia/router";

export enum StatsType {
    Creature = "Creature",
    Spell = "Spell",
    SpellModel = "SpellModel",
    StatusModel = "StatusModel"
}

export class Statscomponent implements IRouteableComponent {

    // input
    @bindable
    public uid: string;
    @bindable
    public type: StatsType;

    // db data
    public stats: IStats;

    // creature properties
    private affinities = Object.values(AffinityTypes);
    private resistances = Object.values(ResistanceTypes);
    private resources = Object.values(ResourceTypes);
    private states = Object.values(StateTypes);
    private others = Object.values(OtherPropertyTypes);
    private contextuals = Object.values(ContextualTypes);
    // spell properties
    private spells = Object.values(SpellPropertyTypes);
    private spellModels = Object.values(SpellModelPropertyTypes);
    // status properties
    private statuses = Object.values(StatusModelPropertyTypes);

    constructor(
        private readonly statsController: StatsModelController
    ) {
    }

    binding() { //initiator: IHydratedController, parent: IHydratedController, flags: LifecycleFlags) {
        this.statsController
            .getStats(this.uid)
            .then(res => this.stats = res.data);
    }

    public keys(types) {
        // let asdf: keyof typeof AffinityTypes = 'Air'
        // let t = AffinityTypes;
        console.log("asdf: " + Object.values(types));
        // console.log(typeof AffinityTypes)
        // console.log("keys: " + JSON.stringify(AffinityTypes['Air']))
        // return Object.keys(types);
    }
    public onInputChange() {
        console.log("onInputChange: " + JSON.stringify("{}"));
    }
}


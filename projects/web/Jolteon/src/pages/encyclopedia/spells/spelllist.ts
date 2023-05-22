import { TOAST_STATUS } from 'bootstrap-toaster';
import { Toast } from 'bootstrap-toaster';
import { IEventAggregator, bindable, inject } from "aurelia";
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController";
import { ISpellModel, SpellModel } from "../../../jolteon/services/api/data-contracts";
import { HttpResponse } from "../../../jolteon/services/api/http-client";
import { IRouter } from '@aurelia/router';

@inject(SpellModelController, IEventAggregator, IRouter)
export class SpellList {

    @bindable
    public mode: string = 'root';
    @bindable
    public spellids: string[] = []
    // @bindable
    // public creatureid: string // TODO

    // db data
    public spells: SpellModel[] = []
    public selectedSpells: SpellModel[] = [];

    // TODO pages for Creature list and Spell list
    public readonly numPerPage = 50;
    public page: number = 0;

    constructor(
        private readonly spellController: SpellModelController,
        private readonly ea: IEventAggregator,
        @IRouter private router: IRouter
    ) {
        this.ea.subscribe('spells:root:remove', (modelUid) => {
            // this.spellids.spl
            let idx = this.spells.findIndex(s => s.modelUid == modelUid);
            console.log("spelllist remove: " + modelUid + " at " + idx);
            if (idx != -1) {
                this.spells.splice(idx, 1);
            }
        });
    }

    async binding() {
        // console.log("spell list binding")
        await this.refresh();
    }

    public async refresh() {
        if (!this.mode) return;
        console.log("spelllist refresh: mode=" + this.mode + ", ids=" + this.spellids)

        // TODO: limit 50 per page? add filters for name, element type...
        let filter = {
            skip: this.page * this.numPerPage,
            limit: this.numPerPage
        }
        let promise: Promise<HttpResponse<ISpellModel[], any>>;
        if (this.mode == 'creatureSpells') { //this.spellids != undefined) {
            promise = this.spellController.getList({ list: this.spellids });
        } else {
            promise = this.spellController.getAll();
        }
        try {
            let res = await promise;
            // console.log(res);
            this.spells = res.data;
        } catch (rej) {
            // console.log(rej);
            Toast.create({
                title: "Spells",
                message: "Failed to fetch spells from server",
                status: TOAST_STATUS.DANGER,
                timeout: 2000
            })
        }
    }

    public async clickCreate() {
        let res = await this.spellController.postNew();
        this.spells.push(res.data);
        
        if (this.mode == 'creatureSpells') {
            this.ea.publish('spells:search:select', res.data.entityUid);
        }
    }

    public clickDelete() {
        for (let spell of this.selectedSpells) {
            let id: string = spell.modelUid; // IID
            this.spellController.deleteSpell(id).then(res => {
                if (res.data.deletedCount !== 1) return;
                let index = this.spells.findIndex(c => c.modelUid === id);
                if (index === -1) return;
                this.spells.splice(index, 1);
            })
        }
        this.selectedSpells = [];
    }


}

import { TOAST_STATUS } from 'bootstrap-toaster';
import { Toast } from 'bootstrap-toaster';
import { IEventAggregator, bindable, inject } from "aurelia";
import { SpellModelController } from "../../../jolteon/services/api/SpellModelController";
import { ISpellModel, SpellModel } from "../../../jolteon/services/api/data-contracts";
import { HttpResponse } from "../../../jolteon/services/api/http-client";
import { IRouter } from '@aurelia/router';
import { error } from 'console';
import { CreatureModelController } from '../../../jolteon/services/api/CreatureModelController';
import { Spell } from './spell';
import { SpellSkinController } from '../../../jolteon/services/api/SpellSkinController';

@inject(IEventAggregator, IRouter, SpellModelController, CreatureModelController)
export class SpellList {

    @bindable
    public mode: string = 'root';
    @bindable
    public spellids: string[] = []
    @bindable
    public creatureid: string // TODO

    @bindable
    public callbackremove = (spell: SpellModel) => { };
    @bindable
    public callbackadd = (spell: SpellModel) => { };

    // db data
    public spells: SpellModel[] = []
    public filteredSpells: SpellModel[] = [];
    public selectedSpells: SpellModel[] = [];

    public filter: string = "";
    // view-model refs
    public refs: Spell[] = []

    // TODO pages for Creature list and Spell list
    public readonly numPerPage = 50;
    public page: number = 0;

    constructor(
        private readonly ea: IEventAggregator,
        @IRouter private router: IRouter,
        private readonly spellController: SpellModelController,
        private readonly creatureController: CreatureModelController
    ) {
    }

    async binding() {
        // console.log("spell list binding")
        await this.refresh();
    }

    public async refresh() {
        if (!this.mode) return;
        // console.log("spelllist refresh: mode=" + this.mode + ", ids=" + this.spellids)
        // breadcrumb navigation
        if(this.mode == 'root') {
            this.ea.publish("navcrumb:spell", null);
        }

        // TODO: limit 50 per page? add filters for name, element type...
        let filter = {
            skip: this.page * this.numPerPage,
            limit: this.numPerPage
        }
        let promise: Promise<HttpResponse<ISpellModel[], any>>;
        if (this.mode == 'creature') { //this.spellids != undefined) {
            promise = this.spellController.getList({ list: this.spellids });
        } else {
            promise = this.spellController.getAll();
        }
        try {
            let res = await promise;
            // console.log(res);
            this.spells = res.data;
            this.filteredSpells = [...this.spells];
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
        this.filteredSpells.push(res.data);
        this.callbackadd(res.data);
    }

    public onSearch() {
        if (!this.filter) {
            this.filteredSpells = [...this.spells];
            return;
        }
        let str = this.filter.toLowerCase();
        this.spellController.getByString(str).then(res => {
            this.filteredSpells = res.data;
        });
    }
    
    public async clickAddToCreature(spell: SpellModel) {
        console.log("spell list click add (creature) " + spell.modelUid)
        this.spellids.push(spell.entityUid);
        this.spells.push(spell);

        if (this.mode == 'creature') {
            this.callbackadd(spell);
        }
    }

    public async clickRemove(spell: SpellModel) {
        let idx = this.spells.findIndex(s => s.modelUid == spell.modelUid);
        // console.log("spell list click remove idx: " + idx);
        if (idx == -1) return;

        if (this.mode == 'creature') {
            this.callbackremove(spell);
            this.spells.splice(idx, 1);
            this.filteredSpells.splice(idx, 1)
        }

        if (this.mode == 'root') {
            this.spellController.deleteSpell(spell.modelUid).then(
                res => {
                    this.spells.splice(idx, 1)
                    this.filteredSpells.splice(idx, 1)
                }
            );
        }

        // let promise;
        // if (this.mode == 'root') {
        //     promise = this.spellController.deleteSpell(spell.modelUid);
        // }
        // if (this.mode == 'creature') {
        //     let copy = [...this.spellids];
        //     promise = this.creatureController.putSpells(this.creatureid, copy);
        // }
        // try {
        //     let res = await promise;
        //     // console.log("spelllist remove: " + spell.modelUid + " at " + idx);
        //     this.spells.splice(idx, 1);
        // } catch (ex) {
        //     console.error(ex);
        // }
    }

    // public clickDelete() {
    //     for (let spell of this.selectedSpells) {
    //         let id: string = spell.modelUid; // IID
    //         this.spellController.deleteSpell(id).then(res => {
    //             if (res.data.deletedCount !== 1) return;
    //             let index = this.spells.findIndex(c => c.modelUid === id);
    //             if (index === -1) return;
    //             this.spells.splice(index, 1);
    //         })
    //     }
    //     this.selectedSpells = [];
    // }


}

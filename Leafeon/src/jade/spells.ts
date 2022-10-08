// import { ObjectId } from "mongodb";
import * as dotenv from "dotenv";
import { IEventAggregator, inject } from "aurelia";
import { loadavg } from "os";
import { watch } from '@aurelia/runtime-html';
import { db } from "../db";
import { Spell } from '../../../arch/common/spell';
import { Effect } from "../../../arch/common/effects";
import { Condition } from "../../../arch/common/condition";

@inject(db)
export class Spells {

    public db: db;
    public spells: Spell[] = [];
    public spell: Spell;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
        ea.subscribe("addItem:spells", () => this.addSpell());
        ea.subscribe("delete:spells", (items) => this.deleteSpells(items as Spell[]));
        ea.subscribe("selectItem:spells", (item) => this.selectSpell(item as Spell));
        ea.subscribe("refresh:spells", () => this.load());
        ea.subscribe("spells:save", () => this.save());
        this.load();
    }

    public async load() {
        let data = await this.db.spells.find({}).toArray();
        let spells = data.map(o => o as Spell);
        this.spells = spells;
        this.spell = this.spells[0];
    }

    public addSpell() {
        let s = new Spell();
        // for(let res of this.db.resourceTypes)
        //     s.costs[res] = 0;
        this.db.spells.insert(s).then(() => {
            // .sort({ _id: -1 })
            this.db.spells.find({}).toArray().then(arr => {
                let s = arr[arr.length - 1] as Spell;
                this.spells.push(s);
                this.ea.publish("externalSelect:spells", s);
            })
        })
    }

    public deleteSpells(items: Spell[]) {
        if (items.length > 0 && items[0]) {
            for (let item of items) {
                let index = this.spells.indexOf(item);
                this.spells.splice(index, 1);
                this.db.spells.remove({ _id: item._id })
                this.ea.publish("externalSelect:spells", this.spells[index]);
            }
        }
        else {
            let last = this.spells.pop();
            this.db.spells.remove({ _id: last._id })
            this.ea.publish("externalSelect:spells", this.spells[this.spells.length - 1]);
        }
    }

    public selectSpell(item: Spell) {
        this.spell = item;
    }

    // @watch('spell')
    public save() {
        console.log("save: " + this.spell.name)
        this.db.spells.update({ _id: this.spell._id }, this.spell)
    }


}

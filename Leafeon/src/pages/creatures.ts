// import { ObjectId } from "mongodb";
import * as dotenv from "dotenv";
import { Creature } from '../../../arch/common/entity';
import { IEventAggregator, inject } from "aurelia";
import { db } from "../db";

@inject(db)
export class Creatures {
    public db: db;

    // public list: HTMLTemplateElement;
    public creatures: Creature[] = [];
    public creature: Creature;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
        ea.subscribe("addItem:creatures", this.addCreature);
        ea.subscribe("delete:creatures", this.deleteCreatures);
        ea.subscribe("selectItem:creatures", this.selectCreature);
        ea.subscribe("refresh:creatures", () => this.load());

        ea.subscribe("creatures:save", () => this.save());
        this.load();
    }

    public save() {
        console.log("save: " + this.creature.name)
        this.db.spells.update({ _id: this.creature._id }, this.creature)
    }
    // public spells: Spell[] = [];

    // constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
    //     this.db = db;
    //     ea.subscribe("addItem:spells", () => this.addSpell());
    //     ea.subscribe("delete:spells", (items) => this.deleteSpells(items as Spell[]));
    //     ea.subscribe("selectItem:spells", (item) => this.selectSpell(item as Spell));
    //     ea.subscribe("refresh:spells", () => this.load());
    //     ea.subscribe("spells:save", () => this.save());
    //     this.load();
    // }

    public async load() {
        let data = await this.db.creatures.find({}).toArray();
        let creatures = data.map(o => o as Creature);
        this.creatures = creatures;
        this.creature = this.creatures[0];
    }

    public addCreature() {
        let s = new Creature();
        // s._id = this.creatures.length;
        // this.creatures.push(s);
        // this.db.creatures.insert(s);
        
        this.db.creatures.insert(s).then(() => {
            this.db.creatures.find({}).toArray().then(arr => {
                let s = arr[arr.length - 1] as Creature;
                this.creatures.push(s);
                this.ea.publish("externalSelect:creatures", s);
            })
        })
    }

    public deleteCreatures(creatures: Creature[]) {
        for (let s of creatures) {
            this.db.creatures.remove({ id: s._id })
            this.creatures.splice(this.creatures.indexOf(s), 1);
        }
    }

    public selectCreature(s) {
        this.creature = s;
    }

}

// import { ObjectId } from "mongodb";
import * as dotenv from "dotenv";
import { Creature } from '../../../arch/common/entity';
import { IEventAggregator, inject } from "aurelia";
import { db } from "../db";

@inject(db)
export class Creatures {
    public db: db;

    // public list: HTMLTemplateElement;
    public creaturesData: Creature[] = [];
    public creature: Creature;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
        ea.subscribe("addItem:creatures", this.addCreature);
        ea.subscribe("delete:creatures", this.deleteCreatures);
        ea.subscribe("selectItem:creatures", this.selectCreature);
    }
    
    public addCreature() {
        let s = new Creature();
        s.id = this.creaturesData.length;
        this.creaturesData.push(s);
        this.db.creatures.insert(s);
    }

    public deleteCreatures(creatures: Creature[]) {
        for(let s of creatures) {
            this.db.creatures.remove({ id: s.id })
            this.creaturesData.splice(this.creaturesData.indexOf(s), 1);
        }
    }

    public selectCreature(s) {
        this.creature = s;
    }

}

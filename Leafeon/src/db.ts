// import * as mongoDB from "mongodb";
import * as dotenv from "dotenv";
import zango from 'zangodb';
import { DI, Registration } from 'aurelia';
import { HttpClient } from '@aurelia/fetch-client';
import { fs } from "@tauri-apps/api";

import { Creature } from "../../arch/common/entity";
import { Spell } from "../../arch/common/spell";
import { Effect, EffectType, effectTypes } from "../../arch/common/effects";
import { resourceTypes, characteristicTypes, stateTypes } from "../../arch/common/characteristics";
import { conditionActorTypes, conditionComparatorTypes, conditionLinkTypes, conditionTypes } from "../../arch/common/condition";
import { contextTypes, contextCharacteristicTypes } from "../../arch/common/red/context";

export class db {
    public zdb: zango.Db; //, { people: ['age'] });
    public creatures: zango.Collection;
    public spells: zango.Collection;
    public effects: zango.Collection;

    public resourceTypes = resourceTypes;
    public effectTypes = effectTypes;
    public conditionTypes = conditionTypes;
    public conditionActorTypes = conditionActorTypes;
    public conditionComparatorTypes = conditionComparatorTypes;
    public conditionLinkTypes = conditionLinkTypes;
    public characteristicTypes = characteristicTypes;
    public contextTypes = contextTypes
    public contextCharacteristicTypes = contextCharacteristicTypes;
    public stateTypes = stateTypes;

    constructor() {
        this.zdb = new zango.Db('leafeon', 0, { creatures: new Creature(), spells: new Spell(), effects: new Effect() });
        this.creatures = this.zdb.collection('creatures');
        this.spells = this.zdb.collection('spells');
        this.effects = this.zdb.collection('effects');
    }

    public async export() {
        this.creatures.find({}).toArray().then(creatures => {
            fs.writeTextFile("creatures.json", JSON.stringify(creatures))
        })
        this.spells.find({}).toArray().then(spells => {
            fs.writeTextFile("spells.json", JSON.stringify(spells))
        })
        this.effects.find({}).toArray().then(effects => {
            fs.writeTextFile("effects.json", JSON.stringify(effects))
        })
    }

    public async doThing() {
        let s = new Spell();
        // s.id = 0;
        s.name = "Demo"

        let obj = await this.spells.find({ _id: 1 })
        let arr = await obj.toArray();
        console.log("found: " + JSON.stringify(arr))
        // this.spells.insert(s)
    }

    /*
    public creatures?: mongoDB.Collection
    public spells?: mongoDB.Collection
    public effects?: mongoDB.Collection

    public async connectToDatabase() {
        dotenv.config();

        const client: mongoDB.MongoClient = new mongoDB.MongoClient(process.env.DB_CONN_STRING);
        await client.connect();
        const db: mongoDB.Db = client.db(process.env.DB_NAME);

        const creaturesCol: mongoDB.Collection = db.collection(process.env.CREATURES_COLLECTION_NAME);
        this.creatures = creaturesCol;

        const spellsCol: mongoDB.Collection = db.collection(process.env.SPELLS_COLLECTION_NAME);
        this.spells = spellsCol;

        const effectsCol: mongoDB.Collection = db.collection(process.env.EFFECTS_COLLECTION_NAME);
        this.effects = effectsCol;

        console.log(`Successfully connected to database: ${db.databaseName}`);
    }
    */
}


const container = DI.createContainer();
container.register(
    Registration.singleton(db, db)
);

import { SchemaDescription } from './../../../jolteon/services/api/data-contracts';
import { IRouter } from "@aurelia/router";
import { IEventAggregator, bindable, inject } from "aurelia";
import { EffT, IEffect } from "../../../jolteon/services/api/data-contracts";
import { EffectPermanentController } from "../../../jolteon/services/api/EffectPermanentController";
import { PropertiesController } from "../../../jolteon/services/api/PropertiesController";
import { SpellModelController } from '../../../jolteon/services/api/SpellModelController';
import { Effects } from '../../../jolteon/constants';

@inject(IEventAggregator, IRouter) //, EffectPermanentController, PropertiesController) //, SpellModelController)
export class Effect {

    @bindable
    public uid: string;
    @bindable
    public callbackmoveup = (e) => { };
    @bindable
    public callbackmovedown = (e) => { };
    @bindable
    public callbackremove = (e) => { };

    public minimized: boolean = false;

    // db data
    private model: IEffect;
    // db data
    private schema: SchemaDescription

    constructor(
        private readonly ea: IEventAggregator,
        private readonly router: IRouter,
        private readonly effectController: EffectPermanentController,
        private readonly propertiesController: PropertiesController,
    ) {
    }

    binding() {
        this.effectController.getEffect(this.uid).then(
            res => {
                this.model = res.data;
                // console.log("binded effect: ")
                // console.log(this.model);
                this.schema = Effects.schemas.find(s => s.name == this.getModelName());
                // this.propertiesController.getEffectsSchema(this.getModelName())
                //     .then(res => this.schema = res.data);
            },
            rej => {
                console.log(rej);
            }
        )
    }

    public getModelName() {
        let id: number = +this.model.modelUid;
        let enu = EffT[id]; //id as EffT;
        // console.log("enu: " + enu.toString())
        return enu.toString();
        // let key = Object.keys(EffT)[id];
    }

    //#region click handlers
    public clickMinimize() {
        // console.log("click minimize")
        this.minimized = !this.minimized;
    }
    public clickMoveUp() {
        // console.log("Effect.clickMoveUp")
        this.callbackmoveup(this.model);
    }
    public clickMoveDown() {
        // console.log("Effect.clickMoveDown")
        this.callbackmovedown(this.model);
    }
    public clickRemove() {
        console.log("Effect.remove: " + this.model.entityUid);
        this.callbackremove(this.model);
    }
    //#endregion

    //#region callback handlers
    public onMoveEffectUp(e: IEffect) {
        let idx = this.model.effectIds.indexOf(e.entityUid);
        this.model.effectIds.splice(idx, 1);
        this.model.effectIds.splice(idx - 1, 0, e.entityUid);
        // update db
        this.effectController.putEffect(this.model.entityUid, this.model)
            .then(res => this.model = res.data);
    }
    public onMoveEffectDown(e: IEffect) {
        let idx = this.model.effectIds.indexOf(e.entityUid);
        this.model.effectIds.splice(idx, 1);
        this.model.effectIds.splice(idx + 1, 0, e.entityUid);
        // update db
        this.effectController.putEffect(this.model.entityUid, this.model)
            .then(res => this.model = res.data);
    }
    // remove of child of this
    public onRemoveEffect(e: IEffect) {
        console.log("Effect remove eff: " + e.entityUid)
        let idx = this.model.effectIds.indexOf(e.entityUid);
        this.model.effectIds.splice(idx, 1);
        // update db
        this.effectController.putEffect(this.model.entityUid, this.model)
            .then(res => {
                this.model = res.data
                this.effectController.deleteEffect(e.entityUid);
            });
    }
    public onAddChild(schema: SchemaDescription) {
        console.log("add child to: " + this.model.entityUid)
        // update db
        this.effectController.postChild(this.model.entityUid, {
            schemaName: schema.name
        })
            .then(res => this.model.effectIds.push(res.data.entityUid));
        // .then(res => location.reload())
    }
    public onSave() {
        // update db
        this.effectController.putEffect(this.model.entityUid, this.model)
            .then(res => {
                    this.model = res.data
                    this.ea.publish("operation:saved");
                },
                rej => {
                    this.ea.publish("operation:failed");
                });
    }
    //#endregion

}

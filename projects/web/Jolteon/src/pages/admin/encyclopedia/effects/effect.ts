import { HttpResponse } from './../../../../jolteon/services/api/http-client';
import { SchemaDescription, ITriggerModel, TriggerOrderType, TriggerType, IZone, IEffect, EffT } from '../../../../jolteon/services/api/data-contracts';
import { IRouter } from "@aurelia/router";
import { watch } from '@aurelia/runtime-html';
import { IEventAggregator, bindable, inject, observable } from "aurelia";
import { EffectPermanentController } from "../../../../jolteon/services/api/EffectPermanentController";
import { PropertiesController } from "../../../../jolteon/services/api/PropertiesController";
import { Schemas } from '../../../../jolteon/constants';
import { TOAST_STATUS, Toast } from 'bootstrap-toaster';
import { TriggerModelController } from '../../../../jolteon/services/api/TriggerModelController';

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
    // db data, important for propertygrid
    // private schema: SchemaDescription

    constructor(
        private readonly ea: IEventAggregator,
        private readonly router: IRouter,
        private readonly effectController: EffectPermanentController,
        private readonly propertiesController: PropertiesController,
        private readonly triggerController: TriggerModelController
    ) {
    }

    binding() {
        this.effectController.getEffect(this.uid).then(
            res => {
                this.model = res.data;
                // console.log("binded effect: ")
                // console.log(this.model);
                // this.schema = Effects.schemas.find(s => s.name == this.modelName);
                // this.propertiesController.getEffectsSchema(this.getModelName())
                //     .then(res => this.schema = res.data);
            },
            rej => {
                console.log(rej);
            }
        )
    }

    // @watch(eff => eff?.model?.modelUid)
    public get modelName() {
        let id: number = +this.model.modelUid;
        let enu = EffT[id]; //id as EffT;
        // console.log("enu: " + id + ": " + enu.toString())
        return enu.toString();
        // let key = Object.keys(EffT)[id];
    }
    public get schema(): SchemaDescription {
        let desc = Schemas.effects.find(s => s.name == this.modelName);
        return desc;
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
        // console.log("Effect.remove: " + this.model.entityUid);
        this.callbackremove(this.model);
    }
    public clickCopy() {
        navigator.clipboard.writeText(JSON.stringify(this.model));
        Toast.create({
            title: "Effect",
            message: "Copied",
            status: TOAST_STATUS.INFO,
            timeout: 1000
        })
    }
    public async clickPaste() {
        let text = await navigator.clipboard.readText();
        let eff: IEffect = JSON.parse(text);
        this.effectController.putEffect(this.model.entityUid, eff)
            .then(res => this.model = res.data)
            .then(f => this.ea.publish("operation:saved"))
            // .then(this.handleUpdate);
    }
    public onChangeSchemaType(schema: SchemaDescription) {
        this.effectController.putSchema(this.model.entityUid, { schemaName: schema.name })
            .then(res => this.model = res.data)
            .then(f => this.ea.publish("operation:saved"))
            // .then(this.handleUpdate);
    }
    /** Add Trigger */
    public onAddTrigger(schema: SchemaDescription) {
        this.triggerController.postTrigger({ schemaName: schema.name })
            .then(res => this.model.triggers.push(res.data))
            .then(f => this.onSave());
    }
    //#endregion


    //#region callback handlers
    public onSave() {
        // console.log("effect save")
        // update db
        this.effectController.putEffect(this.model.entityUid, this.model)
            .then(
                res => {
                    this.model = res.data
                    this.ea.publish("operation:saved");
                },
                rej => {
                    this.ea.publish("operation:failed");
                }
            );
    }
    //#endregion

}

import { TOAST_STATUS } from 'bootstrap-toaster';
import { Toast } from 'bootstrap-toaster';
import { IEventAggregator, bindable, inject } from "aurelia";
import { StatusModelController } from "../../../jolteon/services/api/StatusModelController";
import { IStatusModel, StatusModel } from "../../../jolteon/services/api/data-contracts";
import { HttpResponse } from "../../../jolteon/services/api/http-client";
import { IRouter } from '@aurelia/router';

@inject(IEventAggregator, IRouter, StatusModelController)
export class Statuslist {

    @bindable
    public mode: string = 'root';
    @bindable
    public statusids: string[] = []

    @bindable
    public callbackremove = (status: StatusModel) => { };
    @bindable
    public callbackadd = (status: StatusModel) => { };

    // db data
    public statuses: StatusModel[] = []
    public filteredStatuses: StatusModel[] = [];

    public filter: string = "";

    // TODO pages for Creature list and Status list
    public readonly numPerPage = 50;
    public page: number = 0;

    constructor(
        private readonly ea: IEventAggregator,
        @IRouter private router: IRouter,
        private readonly statusController: StatusModelController,
    ) {
    }

    async binding() {
        console.log("status list binding")
        await this.refresh();
    }

    public async refresh() {
        if (!this.mode) return;
        console.log("statuslist refresh: mode=" + this.mode + ", ids=" + this.statusids)
        // breadcrumb navigation
        if (this.mode == 'root') {
            this.ea.publish("navcrumb:status", null);
        }

        // TODO: limit 50 per page? add filters for name, element type...
        let filter = {
            skip: this.page * this.numPerPage,
            limit: this.numPerPage
        }
        let promise: Promise<HttpResponse<IStatusModel[], any>>;
        if (this.mode == 'creature') { //this.statusids != undefined) {
            // promise = this.statusController.getList({ list: this.statusids });
            promise = this.statusController.getFiltered({
                _id: { "in": this.statusids }
            });
        } else {
            promise = this.statusController.getAll();
        }
        try {
            let res = await promise;
            // console.log(res);
            this.statuses = res.data;
            this.filteredStatuses = [...this.statuses];
            console.log(this.filteredStatuses);
        } catch (rej) {
            // console.log(rej);
            Toast.create({
                title: "Statuses",
                message: "Failed to fetch status from server",
                status: TOAST_STATUS.DANGER,
                timeout: 2000
            })
        }
    }

    public async clickCreate() {
        let res = await this.statusController.postNew();
        console.log("status list click create : " + JSON.stringify(res.data))
        this.statuses.push(res.data);
        this.filteredStatuses.push(res.data);
        this.callbackadd(res.data);
    }

    public onSearch() {
        if (!this.filter) {
            this.filteredStatuses = [...this.statuses];
            return;
        }
        let str = this.filter.toLowerCase();
        this.statusController.getByString(str).then(res => {
            this.filteredStatuses = res.data;
        });
    }

    public async clickAddToCreature(status: StatusModel) {
        console.log("status list click add (creature) " + status.modelUid)
        this.statusids.push(status.entityUid);
        this.statuses.push(status);

        if (this.mode == 'creature') {
            this.callbackadd(status);
        }
    }

    public async clickRemove(status: StatusModel) {
        let idx = this.statuses.findIndex(s => s.entityUid == status.entityUid);
        // console.log("status list click remove idx: " + idx);
        if (idx == -1) return;

        if (this.mode == 'creature') {
            this.callbackremove(status);
            this.statuses.splice(idx, 1);
            this.filteredStatuses.splice(idx, 1)
        }

        if (this.mode == 'root') {
            this.statusController.deleteStatus(status.entityUid).then(
                res => {
                    this.statuses.splice(idx, 1)
                    this.filteredStatuses.splice(idx, 1)
                }
            );
        }
    }

}

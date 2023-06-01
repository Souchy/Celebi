import { fromState } from "@aurelia/state";
import { GlobalState } from "../../jolteon/initialstate";
import { AccountInfo, IpAccess } from "../../jolteon/services/api/data-contracts";

export class Profile {
    
    @fromState((state: GlobalState) => state.account?.displayName)
    public displayName?: string | null;
    // @fromState((state: GlobalState) => state.account?.name)
    // private name?: string | null;
    // @fromState((state: GlobalState) => state.account?.familyName)
    // private familyName?: string | null;
    // @fromState((state: GlobalState) => state.account?.picture)
    // private picture?: string | null;

    @fromState((state: GlobalState) => state.account?.currency)
    private currency?: number | null;
    @fromState((state: GlobalState) => state.account?.ownedModels)
    private ownedModels?: string[] | null;
    // @fromState((state: GlobalState) => state.account?.transactions)
    // private transactions?: string[] | null;
    @fromState((state: GlobalState) => state.account?.accessByIp)
    private accessByIp?: IpAccess[] | null;

    // @fromState((state: GlobalState) => state.account != null)
    // public isConnected: boolean;
    // private get accessByIp() {
    //     return this.account.accessByIp;
    // }
    // private get displayName() {
    //     return this.account.displayName;
    // }
    // private get ownedModels() {
    //     return this.account.ownedModels;
    // }


}

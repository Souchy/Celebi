import { IStore, fromState } from "@aurelia/state";
import { AccountInfo } from "../../jolteon/services/api/data-contracts";
import { LoginAction, LogoutAction } from "../../jolteon/action-handler";
import { GlobalState } from "../../jolteon/initialstate";
import { inject } from "aurelia";
// import { GlobalState } from "../../jolteon/initialstate";
// import { IStore, fromState } from "@aurelia/state";
// import { LoginAction, LogoutAction } from "../../jolteon/action-handler";


@inject(IStore)
export class AuthHeader {

    @fromState((state: GlobalState) => state.account != null)
    public isConnected: boolean;

    @fromState((state: GlobalState) => state.account?.displayName)
    public displayName: string;

	constructor(private readonly store: IStore<{}, LoginAction | LogoutAction>) { 
        
	}

    public getConnectedAccount(): AccountInfo {
        return null;
    }

    // public isConnected(): boolean {
    //     return 
    //     // check cookie
    //     return true;
    // }

}

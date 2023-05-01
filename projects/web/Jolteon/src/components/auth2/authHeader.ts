import { IStore, fromState } from "@aurelia/state";
import { AccountInfo } from "../../jolteon/services/api/data-contracts";
import { LoginAction, LogoutAction } from "../../jolteon/action-handler";
import { GlobalState } from "../../jolteon/initialstate";


export class AuthHeader {

    @fromState((state: GlobalState) => state.account != null)
    private isConnected: boolean;

    @fromState((state: GlobalState) => state.account.displayName)
    private displayName: string;

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

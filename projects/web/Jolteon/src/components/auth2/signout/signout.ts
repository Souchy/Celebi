import { IHttpClient, inject } from "aurelia";
import { SessionManager } from "../../../jolteon/util/sessionManager";
import { AuthController } from "../../../jolteon/services/api/AuthController";
import { IStore } from "@aurelia/state";
import { LoginAction, LogoutAction } from "../../../jolteon/action-handler";
import { GlobalState } from "../../../jolteon/initialstate";

@inject(AuthController, IStore)
export class Signout {

    constructor(private readonly auth: AuthController, private readonly store: IStore<GlobalState, LogoutAction>) {
    }

    public clickSignout(): void {
        // delete cookie
        console.log("click signout")
        this.auth.postIdentitySignout().then(res => {
            console.log("signout dispatch signout")
            this.store.dispatch(new LogoutAction());
        })
    }

}

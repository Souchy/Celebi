import { Account } from "../../services/api/data-contracts";


export class AuthHeader {


    public getConnectedAccount(): Account {
        return null;
    }

    public isConnected(): boolean {
        // check cookie
        return true;
    }

}

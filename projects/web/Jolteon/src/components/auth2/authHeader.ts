import { AccountInfo } from "../../services/api/data-contracts";


export class AuthHeader {


    public getConnectedAccount(): AccountInfo {
        return null;
    }

    public isConnected(): boolean {
        // check cookie
        return true;
    }

}

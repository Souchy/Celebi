import { AccountInfo } from './services/api/data-contracts';


export class GlobalState {
    account: AccountInfo = null;
}


export const initialState: GlobalState = new GlobalState(); 

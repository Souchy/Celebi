import { GlobalState } from "./initialstate";
import { AccountInfo } from "./services/api/data-contracts";

//#region Names
export class ActionNames {
  public static readonly login = 'login';
  public static readonly logout = 'logout';
}
//#endregion

//#region Actions
export class LoginAction {
  public readonly type: string = ActionNames.login
  // public value: AccountInfo
  constructor(readonly value: AccountInfo) { }
}
export class LogoutAction {
  readonly type: string = ActionNames.logout
}
//#endregion

//#region Handlers
export function LoginHandler(currentState: GlobalState, action: LoginAction): GlobalState {
  if (action.type !== ActionNames.login) return currentState;
  return {
    ...currentState,
    account: action.value
  };
}

export function LogoutHandler(currentState: GlobalState, action: LogoutAction): GlobalState {
  if (action.type !== ActionNames.logout) return currentState;
  return {
    ...currentState,
    account: null
  };
}
//#endregion

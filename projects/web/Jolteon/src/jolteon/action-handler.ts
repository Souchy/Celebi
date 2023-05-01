import { GlobalState } from "./initialstate";
import { AccountInfo } from "./services/api/data-contracts";


export function keywordsHandler(currentState, action) {
  return action.type === 'newKeywords'
    ? { ...currentState, keywords: action.value }
    : currentState
}

export type EditAction = { type: 'edit'; value: string }
export type ClearAction = { type: 'clear' }




export type LoginAction = { value: AccountInfo }
export function LoginHandler(currentState: GlobalState, action: LoginAction) {
  return {
    ...currentState,
    account: action.value
  };
}
export type LogoutAction = { }
export function LogoutHandler(currentState: GlobalState, action: LogoutAction) {
  return {
    ...currentState,
    account: null
  };
}


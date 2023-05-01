import { getCookie, setCookie } from 'typescript-cookie'
import { JwtUtil } from "../../../jolteon/util/JWT";
import { IHttpClient, inject } from 'aurelia';
import { SessionManager } from '../../../jolteon/util/sessionManager';
import { Constants } from '../../../jolteon/constants';
import { AuthController } from '../../../jolteon/services/api/AuthController';
import { LoginAction, LogoutAction } from '../../../jolteon/action-handler';
import { GlobalState } from '../../../jolteon/initialstate';
import { IStore } from '@aurelia/state';


declare global {
	interface Window {
		signinGoogleCallback: (response: any) => void;
		signinTwitterCallback: (response: any) => void;
		signinFacebookCallback: (response: any) => void;
	}
}

@inject(AuthController, IStore)
export class Signin {
	/**
	 * we dont do usernames, only emails 
	 */
	private identifier: string;
	/**
	 * pass
	 */
	private password: string;

	constructor(private readonly auth: AuthController, private readonly store: IStore<GlobalState, LoginAction>) { 
		window.signinGoogleCallback = (token) => this.googleCallback(token);
		window.signinTwitterCallback = (token) => this.twitterCallback(token);
		window.signinFacebookCallback = (token) => this.facebookCallback(token);
	}

	public clickSignin() {
		
	}

	public submitSignin() {
		console.log("submit signin: " + this.identifier + ", " + this.password)
		this.auth.postIdentitySignin({
			// displayName: "souchy",
			email: this.identifier,
			pass: this.password
		}).then(
			res => {
				this.store.dispatch(new LoginAction(res.data));
			},
			rej => {
				console.log(rej);
			}
		)
	}


	public googleCallback(token) {
		console.log("hi signin callback")
		console.log(token.credential);
		// console.log()
		// decodeJwtResponse() is a custom function defined by you
		// to decode the credential response.
		// setCookie("". "m");
		let responsePayload = JwtUtil.decodeJwtResponse(token.credential)
		console.log(responsePayload);
		console.log("ID: " + responsePayload.sub);
		console.log('Full Name: ' + responsePayload.name);
		console.log('Given Name: ' + responsePayload.given_name);
		console.log('Family Name: ' + responsePayload.family_name);
		console.log("Image URL: " + responsePayload.picture);
		console.log("Email: " + responsePayload.email);
		// document.cookie = token;
		setCookie('googleToken', token);
		// location.href = "home";
		// fetch()
	}
	public twitterCallback(token) {

	}
	public facebookCallback(token) {

	}

}

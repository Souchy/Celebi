import { getCookie, setCookie } from 'typescript-cookie'
import { JwtUtil } from "../../../util/JWT";
import { IHttpClient, inject } from 'aurelia';
import { SessionManager } from '../../../util/sessionManager';
import { AuthController } from '../../../services/api/AuthController';
import { Constants } from '../../../constants';


declare global {
	interface Window {
		signinGoogleCallback: (response: any) => void;
		signinTwitterCallback: (response: any) => void;
		signinFacebookCallback: (response: any) => void;
	}
}

@inject(IHttpClient)
export class Signin {
	/**
	 * we dont do usernames, only emails 
	 */
	public identifier: string;
	/**
	 * pass
	 */
	public password: string;

	private auth: AuthController;

	constructor(private readonly http: IHttpClient) { //http: IHttpClient) { //private readonly session: SessionManager) {
		this.auth = new AuthController(http);
		this.auth.aureliaClient.baseUrl = Constants.serverUrl;
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
				console.log(res);
			},
			rej => {
				console.log(rej);
			}
		)
	}

	// public clickGoogle() { }
	// public clickTwitter() { }
	// public clickFacebook() { }

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

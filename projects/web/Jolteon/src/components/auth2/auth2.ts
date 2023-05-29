import { HttpClient, IEventAggregator, IHttpClient, inject } from "aurelia";
import { AuthController } from "../../jolteon/services/api/AuthController";
import { JwtUtil } from "../../jolteon/util/JWT";
import { Constants } from "../../jolteon/constants";

declare global {
	interface Window {
		handleCredentialResponse: (response: any) => void;
	}
}


@inject(IEventAggregator, AuthController)
export class Auth2 {

	// public http = new HttpClient();
	public clientid = "850322629277-c9fu1umd1dlk7tjv325u6s33g32fb0ea.apps.googleusercontent.com";
	public redirect_uri = "http://localhost:7000/auth/google";
	public gapi_uri = "https://accounts.google.com/o/oauth2/v2/auth";
	public responseValue: string = "";
	// public readonly auth = new AuthController();
	// public auth:AuthController = null;

	constructor(readonly ea: IEventAggregator, readonly auth: AuthController) {
		// this.auth = new AuthController(http);

		console.log("ctor auth: " + this.auth);
		ea.subscribe("googleCallback", this.googleCallback);
		
		window.handleCredentialResponse = (token) => {
			ea.publish("googleCallback", token);
		}
	}

	public googleCallback(token) {
		console.log("hi callback")
		// decodeJwtResponse() is a custom function defined by you
		// to decode the credential response.
		let responsePayload = JwtUtil.decodeJwtResponse(token.credential)

		console.log("ID: " + responsePayload.sub);
		console.log('Full Name: ' + responsePayload.name);
		console.log('Given Name: ' + responsePayload.given_name);
		console.log('Family Name: ' + responsePayload.family_name);
		console.log("Image URL: " + responsePayload.picture);
		console.log("Email: " + responsePayload.email);
		
		console.log("set cookie: " + JSON.stringify(token));
		document.cookie = token;
	}

	public testRequest() {
		console.log("testrequest");
		this.auth.getPrivatePing().then(
			res => {
				// console.log("res: " + res.data)
				console.log(res);
				this.responseValue = res.data;
			},
			rej => {
				// if(rej.error) {}
				// console.error("rej: " + rej.error)
				console.error(rej);
				this.responseValue = rej.error;
			}
		)
	}

}

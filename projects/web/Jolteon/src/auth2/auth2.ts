import { HttpClient, IEventAggregator, inject } from "aurelia";
import { IRoute, IRouter, IRouteableComponent, ReloadBehavior, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { AuthController } from "../services/api/AuthController";
import { RequestParams } from "../services/api/http-client";

declare global {
	interface Window {
		handleCredentialResponse: (response: any) => void;
	}
}


@inject(IEventAggregator)
export class Auth2 {

	public http = new HttpClient();
	public clientid = "850322629277-c9fu1umd1dlk7tjv325u6s33g32fb0ea.apps.googleusercontent.com";
	public redirect_uri = "http://localhost:7000/auth/google";
	public gapi_uri = "https://accounts.google.com/o/oauth2/v2/auth";
	// private readonly auth = new AuthController();

	constructor(readonly ea: IEventAggregator) {
		ea.subscribe("googleCallback", this.googleCallback);
		
		window.handleCredentialResponse = (token) => {
			ea.publish("googleCallback", token);
		}
	}

	public googleCallback(token) {
		console.log("hi callback")
		console.log(token);
		document.cookie = token;
		// decodeJwtResponse() is a custom function defined by you
		// to decode the credential response.
		let responsePayload = decodeJwtResponse(token.credential)

		console.log("ID: " + responsePayload.sub);
		console.log('Full Name: ' + responsePayload.name);
		console.log('Given Name: ' + responsePayload.given_name);
		console.log('Family Name: ' + responsePayload.family_name);
		console.log("Image URL: " + responsePayload.picture);
		console.log("Email: " + responsePayload.email);

		let auth = new AuthController();
		
		let params: RequestParams = {
			headers: {
				"a token": "hi"
			}
		}
		auth.getPing(params).then(
			res => console.log(res),
			rej => console.log(rej)
		)
		auth.getPrivatePring(params).then(
			res => console.log(res),
			rej => console.log(rej)
		)
		this.testRequest();
	}

	public testRequest() {
		console.log("testrequest");
	}

}

function decodeJwtResponse(token) {
	let base64Url = token.split('.')[1]
	let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
	let jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
		return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
	}).join(''));
	return JSON.parse(jsonPayload)
}

import { getCookie, setCookie } from 'typescript-cookie'
import { JwtUtil } from "../../../util/JWT";


declare global {
	interface Window {
		signinGoogleCallback: (response: any) => void;
		signinTwitterCallback: (response: any) => void;
		signinFacebookCallback: (response: any) => void;
	}
}


export class Signin {
    /**
     * nvm, we dont do usernames, only emails ////can be email or username
     */
    public identifier: string;
    /**
     * pass
     */ 
    public password: string;

    constructor() {
		window.signinGoogleCallback = (token) => this.googleCallback(token);
		window.signinTwitterCallback = (token) => this.twitterCallback(token);
		window.signinFacebookCallback = (token) => this.facebookCallback(token);
    }

    public clickSignin() {
        
    }

    public submitSignin() {
        
    }

    // public clickGoogle() { }
    // public clickTwitter() { }
    // public clickFacebook() { }

	public googleCallback(token) {
		console.log("hi callback")
		console.log(token);
		// console.log()
		// decodeJwtResponse() is a custom function defined by you
		// to decode the credential response.
		// setCookie("". "m");
		let responsePayload = JwtUtil.decodeJwtResponse(token.credential)
		console.log("ID: " + responsePayload.sub);
		console.log('Full Name: ' + responsePayload.name);
		console.log('Given Name: ' + responsePayload.given_name);
		console.log('Family Name: ' + responsePayload.family_name);
		console.log("Image URL: " + responsePayload.picture);
		console.log("Email: " + responsePayload.email);
		// document.cookie = token;
		setCookie('googleToken', token);
		location.href = "home";
	}
	public twitterCallback(token) {

	}
	public facebookCallback(token) {
		
	}

}

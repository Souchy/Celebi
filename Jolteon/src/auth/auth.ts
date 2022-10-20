import { HttpClient, json } from '@aurelia/fetch-client';
import { IEventAggregator, IHttpClient } from 'aurelia';
import { newInstanceOf } from '@aurelia/kernel';
import { bindable, inject } from 'aurelia';
import { IRoute, IRouter, IRouteableComponent, ReloadBehavior, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';

export class Auth {

    private username = '';
    private password = '';


    private http = new HttpClient();
    // constructor(@newInstanceOf(IHttpClient) readonly http: IHttpClient) {

    // }

    constructor(@IEventAggregator readonly ea: IEventAggregator) {
    }

    
    canLoad(params: Parameters, instruction: RoutingInstruction, navigation: Navigation) {
        
    }
    loading(params: Parameters, instruction: RoutingInstruction, navigation: Navigation) {
        
    }
	// load(parameters: Parameters, instruction: RoutingInstruction, navigation: Navigation): void | Promise<void> {
	// 	// console.log("breeds load route: " + JSON.stringify(navigation.instruction))
	// 	// console.log("breeds load route: " + JSON.stringify(instruction.route.match.id))
	// 	let basicname = instruction.route.match.id;
	// 	// console.log("breed: " + this.breedId);
	// }


    public submit() {
        fetch('http://localhost:7000/auth?u=rob&p=gir', {
            method: 'POST',
            headers: {
                'Authorization': "Basic " + utf8_to_b64(this.username + ":" + this.password)
                // 'Authorization': 'Basic c291Y2h5Ono=',
            }
        }).then(async res => {
            if(res.ok) {
                // should have the token in the response here
                const token = await res.text();
                console.log("recv token: " + token)
                localStorage.setItem("token", token);
            }
        })
    }

    public signUp() {
        fetch("http://localhost:7000/news", {
            method: "GET",
            headers: {
                'Authorization': "Basic " + utf8_to_b64(this.username + ":" + this.password)
            }
        }).then(async res => {
            const token = await res.text();
            console.log("recv token: " + token)
            // localStorage.setItem("token", token);
        })
    }
    
    public signIn() {
        fetch("http://localhost:7000/news/1", {
            method: "GET",
            headers: {
                'Authorization': "Basic " + utf8_to_b64(this.username + ":" + this.password)
            }
        }).then(async res => {
            const token = await res.text();
            console.log("recv token: " + token)
            // localStorage.setItem("token", token);
        })
    }

    public google() {
        window.location.href = this.getGoogleOAuthURL();
    }

    public getGoogleOAuthURL(): string {
        let gapi = "https://accounts.google.com/o/oauth2/v2/auth";
        let clientid = "850322629277-c9fu1umd1dlk7tjv325u6s33g32fb0ea.apps.googleusercontent.com";
        let options = {
            client_id: clientid,
            redirect_uri: "http://localhost:7000/auth/google", // redirect_uri: "http://localhost:9000/welcome-page",
            response_type: "code",
            scope: [
                "https://www.googleapis.com/auth/userinfo.profile",
                "https://www.googleapis.com/auth/userinfo.email"
            ].join(" "),
            access_type: "offline",
            prompt: "select_account",
        }
        console.log({ options })
        let qs = new URLSearchParams(options);
        console.log(qs.toString())

        let url = gapi + "?" + qs.toString();
        console.log(url)
        return url;
    }

}

function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}

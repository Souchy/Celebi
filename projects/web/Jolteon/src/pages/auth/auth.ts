import { HttpClient, json } from '@aurelia/fetch-client';
import { IEventAggregator, IHttpClient } from 'aurelia';
import { newInstanceOf } from '@aurelia/kernel';
import { bindable, inject } from 'aurelia';
import { IRoute, IRouter, IRouteableComponent, ReloadBehavior, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { Api } from '../../api';

@inject(IEventAggregator, Api)
export class Auth {

    private signinUser = '';
    private signinPass = '';

    private signupPseudo = "";
    private signupUser = "";
    private signupMail = "";
    private signupPass = "";
    private signupPass2 = "";

    private api: Api;
    private bus: IEventAggregator;

    constructor(ea: IEventAggregator, api: Api) {
        this.api = api;
        this.bus = ea;
    }

    // canLoad(params: Parameters, instruction: RoutingInstruction, navigation: Navigation) {
    // this function crashes
    // }
    loading(params: Parameters, instruction: RoutingInstruction, navigation: Navigation) {
    }

    // load(parameters: Parameters, instruction: RoutingInstruction, navigation: Navigation): void | Promise<void> {
    // 	// console.log("breeds load route: " + JSON.stringify(navigation.instruction))
    // 	// console.log("breeds load route: " + JSON.stringify(instruction.route.match.id))
    // 	let basicname = instruction.route.match.id;
    // 	// console.log("breed: " + this.breedId);
    // }


    public submitSignin() {
        fetch('http://localhost:7000/auth?u=rob&p=gir', {
            method: 'POST',
            headers: {
                'Authorization': "Basic " + utf8_to_b64(this.signinUser + ":" + this.signinPass)
                // 'Authorization': 'Basic c291Y2h5Ono=',
            }
        }).then(async res => {
            if (res.ok) {
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
                'Authorization': "Basic " + utf8_to_b64(this.signinUser + ":" + this.signinPass)
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
                'Authorization': "Basic " + utf8_to_b64(this.signinUser + ":" + this.signinPass)
            }
        }).then(async res => {
            const token = await res.text();
            console.log("recv token: " + token)
            // localStorage.setItem("token", token);
        })
    }

    public google() {
        window.location.href = this.api.getGoogleOAuthURL();
    }

    public submitSignup() {
        if (this.signupPass != this.signupPass2) {
            return;
        }
        this.api.http.fetch("http://localhost:7000/auth/signup", {
            method: "POST",
            headers: {
                'Authorization': "Basic " + utf8_to_b64(this.signinUser + ":" + this.signinPass)
            },
            body: utf8_to_b64(JSON.stringify({
                "pseudo": this.signupPseudo,
                "username": this.signupUser,
                "email": this.signupMail,
                "password": this.signupPass,
            }))
        }).then(async res => {
            if (res.ok) {
                // should have the token in the response here
                const token = await res.text();
                console.log("recv token: " + token)
                localStorage.setItem("token", token);
            }
        });
    }

}

function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}

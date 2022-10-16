import { HttpClient, json } from '@aurelia/fetch-client';
import { IEventAggregator, IHttpClient } from 'aurelia';
import { newInstanceOf } from '@aurelia/kernel';

export class Auth {

    private username = '';
    private password = '';


    private http = new HttpClient();
    // constructor(@newInstanceOf(IHttpClient) readonly http: IHttpClient) {

    // }

    constructor(@IEventAggregator readonly ea: IEventAggregator) {
    }


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
        fetch("http://localhost:7000/news").then(async res => {
            const token = await res.text();
            console.log("recv token: " + token)
            localStorage.setItem("token", token);
        })
    }

}

function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}

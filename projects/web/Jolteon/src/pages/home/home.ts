import { IEventAggregator, IHttpClient, inject } from "aurelia";
import { Constants } from "../../jolteon/constants";
import { AuthController } from "../../jolteon/services/api/AuthController";


@inject(IEventAggregator, IHttpClient)
export class Home {
    
	public responseValue: string = "";
	// public readonly auth = new AuthController();
	public auth:AuthController = null;

	constructor(readonly ea: IEventAggregator, readonly http: IHttpClient) {
		this.auth = new AuthController(http);
		this.auth.aureliaClient.baseUrl = Constants.serverUrl;

		console.log("ctor auth: " + this.auth);
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

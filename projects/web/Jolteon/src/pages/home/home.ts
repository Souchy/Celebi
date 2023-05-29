import { IEventAggregator, IHttpClient, inject } from "aurelia";
import { Constants } from "../../jolteon/constants";
import { AuthController } from "../../jolteon/services/api/AuthController";


@inject(IEventAggregator, AuthController)
export class Home {
    
	public responseValue: string = "";
	// public readonly auth = new AuthController();
	// public auth:AuthController = null;

	constructor(readonly ea: IEventAggregator, readonly auth: AuthController) {
		// this.auth = new AuthController(http);

		console.log("ctor home auth: " + this.auth);
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

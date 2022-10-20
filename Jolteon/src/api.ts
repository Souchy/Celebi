
import { DI, Registration } from 'aurelia';
import { HttpClient } from '@aurelia/fetch-client';

export class Api {
    
	private http = new HttpClient();

    


}

const container = DI.createContainer();
container.register(
	Registration.singleton(Api, Api)
);

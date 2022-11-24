import { invoke } from '@tauri-apps/api'

export class Welcome {
  public message: string = 'Welcome to Aurelia 2!';

	constructor() {
		/*
		invoke('greet', { name: 'Jeff' })
		// `invoke` returns a Promise
		.then((response) => {
			console.log(response);
			this.message = response as string;
		})
		*/
	}

}

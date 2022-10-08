import { inject } from 'aurelia';
import 'bootstrap'; // Import the Javascript
import 'bootstrap/dist/css/bootstrap.css'; // Import the CSS
import { db } from './db';

import { Welcome } from './main/welcome';
// import { AboutPage } from './asdf/about-page';
import { Creatures } from './jade/creatures';
import { Spells } from './jade/spells';

@inject(db)
export class App {

    static routes = [
        {
            path: '',
            component: Welcome,
            title: 'Home'
        },
        {
            path: 'creatures',
            component: Creatures,
            title: 'Creatures'
        },
        {
            path: 'spells',
            component: Spells,
            title: 'Spells'
        }
    ];

    constructor(db: db) {
        // db.connectToDatabase();
    }

}



// function App() {
//   // now we can call our Command!
//   // Right-click the application background and open the developer tools.
//   // You will see "Hello, World!" printed in the console!
//   invoke('greet', { name: 'World' })
//   // `invoke` returns a Promise
//   .then((response) => console.log(response))

// //   return (
// //     // -- snip --
// //   )
// 	return
// }


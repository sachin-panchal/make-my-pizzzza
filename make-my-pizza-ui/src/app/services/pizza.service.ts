import { Injectable } from "@angular/core";
import { of } from "rxjs";
import { PizzaPrice } from "../models/pizza-price.model";
import { Pizza } from "../models/pizza.model";
import { DataService } from "../shared/services/data.service";

@Injectable({ providedIn: "root" })
export class PizzaService {
    constructor(private dataService: DataService) {

    }

    public getPizzaList() {
        return this.dataService.get<Pizza[]>('pizza/get');
    }
    public getPizzaCrustPrices(id: number) {
        return this.dataService.get<PizzaPrice[]>(`pizzaprice/getByPizzaId/${id}`);
    }         
        // return of(
        //     [
        //         {
        //             title: 'Spicy Mexicano',
        //             subTitle: '₹ 425',
        //             imageUrl: '../assets/SpicyMexicano.jpeg',
        //             desciption: 'A crazy fusion like never before. A delicious fusion of corn stuffed paratha and cheesy pizza.'
        //         },
        //         {
        //             title: 'Peri Peri Paneer',
        //             subTitle: '₹ 480',
        //             imageUrl: '../assets/PeriPeriPaneer.jpeg',
        //             desciption: 'An epic fusion of paratha and pizza with melting cheese & soft paneer fillings to satisfy all your indulgent cravings.'
        //         },
        //         {
        //             title: 'Paneer Tikka',
        //             subTitle: '₹ 425',
        //             imageUrl: '../assets/paneerTikka.jpeg',
        //             desciption: 'Flavorful trio of juicy paneer with onion'
        //         },
        //         {
        //             title: 'Italian Garden',
        //             subTitle: '₹ 460',
        //             imageUrl: '../assets/ItalianGarden.jpeg',
        //             desciption: 'Flavorful trio of Mushroom | Black Olive | Spicy Jalapeno | Golden Corn'
        //         },
        //         {
        //             title: 'Coca-Cola [500 ml]',
        //             subTitle: '₹ 60',
        //             imageUrl: '../assets/Coca-Cola.png',
        //             desciption: 'A bubbly drink that every pizza needs.'
        //         },
        //         {
        //             title: 'Classic Garlic Breadsticks + Cheesy Dip',
        //             subTitle: '₹ 139',
        //             imageUrl: '../assets/Classic Garlic Breadsticks + Cheesy Dip $139.jpeg',
        //             desciption: 'Classic Garlic Breadsticks + Cheesy Dip $139'
        //         },
        //         {
        //             title: 'Choco Lava Cake',
        //             subTitle: '₹ 99',
        //             imageUrl: '../assets/Choco Lava Cake $99.jpeg',
        //             desciption: 'With a crisp exterior & molten chocolate oozing out of it\'s center, our Choco Lava is HEAVENLY! Go for it.'
        //         },
        //         {
        //             title: 'Belgian Chocolate Mousse Cake',
        //             subTitle: '₹ 99',
        //             imageUrl: '../assets/Belgian Chocolate Mousse Cake.jpeg',
        //             desciption: 'Deliciously creamy, silky, and loaded with intense dark chocolate. It’s not just a dessert, this is love in a cup!'
        //         },
        //         {
        //             title: 'Butterscotch Mousse Cake',
        //             subTitle: '₹ 99',
        //             imageUrl: '../assets/Butterscotch Mousse Cake.jpeg',
        //             desciption: 'An explosion of creamy butterscotch & rich coco cake crumble in every bite. Experience the blizzard!'
        //         }
        //     ]
        // )

}
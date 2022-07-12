import { CrustSize } from "./crust-size";
import { Sauce } from "./sauce.model";
import { Topping } from "./topping.model";

export interface Pizza {
    id: number;
    name: string;
    description: string;
    imageUrl: string;
    size: CrustSize;
    price: number;
}

export interface PizzaAddon {
    id?: number;
    pizzaId: number;
    name: string;
    description: string;
    imageUrl: string;
    cheese?: boolean;
    size: CrustSize;
    price: number;
    toppings?: Topping[];
    sauces?: Sauce[];
    quantity?: number;
    totalPrice?: number;
}
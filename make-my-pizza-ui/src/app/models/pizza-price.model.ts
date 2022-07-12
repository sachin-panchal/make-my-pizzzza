import { CrustSize } from "./crust-size";

export interface PizzaPrice {
    id: number;
    pizzaId: number;
    size: CrustSize;
    price: number;
}
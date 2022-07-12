import { CrustSize } from "./crust-size";
import { Customer } from "./customer.model";

export interface Order {
    id?: number;
    userId?: number;
    orderDate: Date;
    pizzas: OrderPizza[];
    nonPizzas: OrderNonPizza[];
    drinks: OrderDrink[];
    status: string;
    price: number;
}

export interface OrderPizza {
    id?: number;
    pizzaId?: number;
    orderId: number;
    size: CrustSize;
    cheese?: OrderCheese;
    toppings?: OrderTopping[];
    sauces?: OrderSauce[];
    price: number;
    name: string;
    description: string;
    imageUrl: string;
    quantity?: number;
    totalPrice?: number;
}

export interface OrderTopping {
    id?: number;
    toppingId: number;
    orderPizzaId?: number;
    price: number;
    name: string;
}

export interface OrderSauce {
    id?: number;
    sauceId: number;
    orderPizzaId?: number;
    price: number;
    name: string;
}

export interface OrderCheese {
    id: number;
    orderPizzaId: number;
    price: number;
}

export interface OrderNonPizza {
    id?: number;
    nonPizzaId: number;
    orderId?: number;
    price: number;
    name: string;
    description: string;
    imageUrl: string;
    quantity: number
}

export interface OrderDrink {
    id?: number;
    drinkId: number;
    orderId?: number;
    price: number;
    name: string;
    description: string;
    imageUrl: string;
    quantity: number
}

export interface CustomerOrder {
  user: Customer;
  order: Order;
}

/*

{
    "id": 0,
    "userId": 0,
    "orderDate": "2022-06-22T12:40:48.597Z",
    "deliveryDate": "2022-06-22T12:40:48.597Z",
    "pizzas": [
      {
        "id": 0,
        "pizzaId": 0,
        "orderId": 0,
        "size": 0,
        "price": 0
      }
    ],
    "nonPizzas": [
      {
        "id": 0,
        "nonPizzaId": 0,
        "orderId": 0,
        "price": 0
      }
    ],
    "drinks": [
      {
        "id": 0,
        "drinkId": 0,
        "orderId": 0,
        "price": 0
      }
    ],
    "sauces": [
      {
        "id": 0,
        "sauceId": 0,
        "pizzaId": 0,
        "orderId": 0,
        "price": 0
      }
    ],
    "toppings": [
      {
        "id": 0,
        "toppingId": 0,
        "pizzaId": 0,
        "orderId": 0,
        "price": 0
      }
    ],
    "status": "string",
    "price": 0
  }

*/




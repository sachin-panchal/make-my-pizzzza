import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Customer } from "../models/customer.model";

import { CustomerOrder, Order } from "../models/order.model";
import { User } from "../models/user.model";
import { DataService } from "../shared/services/data.service";

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    shoppingCart: BehaviorSubject<Order>;

    constructor(private dataService: DataService) {
        const order: Order = {
            orderDate: new Date(),
            drinks: [],
            nonPizzas: [],
            pizzas: [],
            price: 0,
            status: ''
        }
        this.shoppingCart = new BehaviorSubject<Order>(order);
    }

    public placeDirectOrder(customerOrder: CustomerOrder) {
        return this.dataService.post<CustomerOrder>('order/PlaceDirectOrder', customerOrder);
    }

    public emptyMyCart() {
        const order: Order = {
            orderDate: new Date(),
            drinks: [],
            nonPizzas: [],
            pizzas: [],
            price: 0,
            status: ''
        }
        this.shoppingCart.next(order);
    }
}
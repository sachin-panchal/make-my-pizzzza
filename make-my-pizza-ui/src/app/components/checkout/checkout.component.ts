import { Component, OnInit } from "@angular/core";
import { Order } from "src/app/models/order.model";
import { OrderService } from "src/app/services/order.service";

@Component({
    selector: 'order-checkout',
    templateUrl: 'checkout.component.html',
    styleUrls: ['checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
    order!: Order;

    constructor(private orderService: OrderService) {

    }

    ngOnInit() {
        this.order = this.orderService.shoppingCart.getValue();
    }
}
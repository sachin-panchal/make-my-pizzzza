import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { Order } from "src/app/models/order.model";
import { OrderService } from "src/app/services/order.service";

@Component({
    selector: 'shopping-cart',
    templateUrl: 'shopping-cart.component.html',
    styleUrls: ['shopping-cart.component.scss']
})
export class ShoppingCartComponent implements OnInit {

    shoppingCart!: Order;
    constructor(private orderService: OrderService,
                private router: Router) {
        
    }

    ngOnInit() {
        this.orderService.shoppingCart
            .subscribe(order => this.shoppingCart = order);
    }

    checkout() {
        this.router.navigate(['checkout'])
    }
}
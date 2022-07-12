import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { debounceTime, distinctUntilChanged } from "rxjs";
import { CrustSize } from "src/app/models/crust-size";

import { OrderPizza, OrderSauce, OrderTopping } from "src/app/models/order.model";
import { PizzaPrice } from "src/app/models/pizza-price.model";
import { Pizza, PizzaAddon } from "src/app/models/pizza.model";
import { Sauce } from "src/app/models/sauce.model";
import { Topping } from "src/app/models/topping.model";
import { NonPizzaService } from "src/app/services/non-pizza.service";
import { OrderService } from "src/app/services/order.service";
import { PizzaService } from "src/app/services/pizza.service";
import { SauceService } from "src/app/services/sauce.service";
import { ToppingService } from "src/app/services/topping.service";

export interface DialogData {
    pizza: Pizza;
}

@Component({
    selector: 'add-ons',
    templateUrl: 'add-ons.component.html',
    styleUrls: ['add-ons.component.scss']
})
export class AddOnsComponent implements OnInit {
    pizzaAddOn: PizzaAddon;
    toppings: Topping[];
    sauces: Sauce[];
    pizzaCrustPrices: PizzaPrice[];
    CrustSize = CrustSize;
    cheesePrice: number;
    totalPrice: number;
    public pizzaForm!: FormGroup;
    public pizzaCrustPrice: number;

    constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData,
        public dialogRef: MatDialogRef<AddOnsComponent>,
        private pizzaService: PizzaService,
        private orderService: OrderService,
        private nonPizzaService: NonPizzaService,
        private toppingService: ToppingService,
        private sauceService: SauceService,
        private fb: FormBuilder
    ) {
        this.pizzaAddOn = <PizzaAddon>data.pizza;
        this.pizzaCrustPrice = data.pizza.price;
        this.pizzaAddOn.pizzaId = <number>this.pizzaAddOn.id;
        this.pizzaAddOn.id = 0;
        this.toppings = [];
        this.sauces = [];
        this.pizzaCrustPrices = [];
        this.cheesePrice = 0;
        this.totalPrice = this.pizzaAddOn.price;
        this.initPizzaForm(this.pizzaAddOn);
    }

    ngOnInit(): void {
        this.pizzaService.getPizzaCrustPrices(this.pizzaAddOn.pizzaId)
            .subscribe(pizzaPrices => this.pizzaCrustPrices = <PizzaPrice[]>pizzaPrices);

        this.toppingService.getToppings()
            .subscribe(toppings => this.toppings = <Topping[]>toppings);

        this.sauceService.getSauces()
            .subscribe(sauces => this.sauces = <Sauce[]>sauces);

        this.nonPizzaService.getCheesePrice()
            .subscribe(cheese => this.cheesePrice = <number>cheese?.price);

        this.calculateTotalPrice();
    }

    calculateTotalPrice() {

        this.pizzaForm.valueChanges
            .pipe(
                debounceTime(500),
                distinctUntilChanged()
            )
            .subscribe((value) => {
                let totalPrice = this.pizzaCrustPrices.find(p => p.size == value.size)?.price ?? 0;

                if (value.sauces) {
                    let sauceIds: number[] = <number[]>value.sauces;
                    for (const sauceId of sauceIds) {
                        totalPrice += this.sauces.find(s => s.id == sauceId)?.price ?? 0;
                    }
                }

                if (value.cheese) {
                    totalPrice += this.cheesePrice;
                }

                if (value.toppings) {
                    let toppingIds: number[] = <number[]>value.toppings;
                    for (const toppingId of toppingIds) {
                        totalPrice += this.toppings.find(s => s.id == toppingId)?.price ?? 0;
                    }
                }
                value.totalPrice = totalPrice;
                this.totalPrice = totalPrice;
            });
    }

    initPizzaForm(pizzaAddOn: PizzaAddon) {
        this.pizzaForm = this.fb.group({
            id: pizzaAddOn.id,
            pizzaId: pizzaAddOn.id,
            name: pizzaAddOn.name,
            description: pizzaAddOn.description,
            imageUrl: pizzaAddOn.imageUrl,
            size: pizzaAddOn.size,
            cheese: pizzaAddOn.cheese,
            price: pizzaAddOn.price,
            toppings: this.fb.control([]),
            sauces: this.fb.control([]),
            quantity: 1,
            totalPrice: pizzaAddOn.price
        })
    }

    addToCart() {
        let rawOrderPizza = this.pizzaForm.value;
        let orderPizza: OrderPizza;
        orderPizza = {
            id: rawOrderPizza.id,
            orderId: 0,
            description: rawOrderPizza.description,
            imageUrl: rawOrderPizza.imageUrl,
            name: rawOrderPizza.name,
            price: this.totalPrice,
            size: rawOrderPizza.size,
            pizzaId: rawOrderPizza.pizzaId,
            quantity: 1,
            cheese: {
                id: 0,
                orderPizzaId: 0,
                price: this.cheesePrice
            },
            sauces: this.getOrderSauces(rawOrderPizza.sauces),
            toppings: this.getOrderToppings(rawOrderPizza.toppings)
        };
        let order = this.orderService.shoppingCart.getValue();
        order?.pizzas?.push(orderPizza);
        order.price += orderPizza.price;
        this.orderService.shoppingCart.next(order);
        this.dialogRef.close();
    }

    private getOrderSauces(sauceIds: number[]) {
        let orderSauces: OrderSauce[] = [];
        for (const sauceId of sauceIds) {
            const sauce = this.sauces.find(s => s.id == sauceId);
            const orderSauce: OrderSauce = {
                id: 0,
                sauceId: sauceId,
                orderPizzaId: 0,
                name: <string>sauce?.name,
                price: sauce?.price ?? 0
            }
            orderSauces.push(orderSauce);
        }
        return orderSauces;
    }

    private getOrderToppings(toppingIds: number[]) {
        let orderToppings: OrderTopping[] = [];
        for (const toppingId of toppingIds) {
            const topping = this.toppings.find(s => s.id == toppingId);
            const orderTopping: OrderTopping = {
                id: 0,
                toppingId: toppingId,
                orderPizzaId: 0,
                name: <string>topping?.name,
                price: topping?.price ?? 0
            }
            orderToppings.push(orderTopping);
        }
        return orderToppings;
    }

    crustSizeChanged(crustSize: any) {
        this.pizzaCrustPrice = this.pizzaCrustPrices.find(p => p.size == crustSize)?.price ?? 0;
    }

}
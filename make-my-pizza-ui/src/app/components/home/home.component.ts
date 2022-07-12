import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { CrustSize } from "src/app/models/crust-size";
import { Drink } from "src/app/models/drink.model";
import { NonPizza } from "src/app/models/non-pizza.model";
import { OrderDrink, OrderNonPizza, OrderPizza } from "src/app/models/order.model";
import { PizzaPrice } from "src/app/models/pizza-price.model";
import { Pizza } from "src/app/models/pizza.model";
import { DrinkService } from "src/app/services/drink.service";
import { NonPizzaService } from "src/app/services/non-pizza.service";
import { OrderService } from "src/app/services/order.service";

import { PizzaService } from "../../services/pizza.service";

import { AddOnsComponent } from "../add-ons/add-ons.component";
import { MatCardOptions } from "../mat-card/card-option.model";

@Component({
  selector: 'home-page',
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.scss']
})
export class HomeComponent {
  public pizzaList: Pizza[];
  public nonPizzaList: NonPizza[];
  public drinkList: Drink[];

  constructor(private pizzaService: PizzaService,
    private nonPizzaService: NonPizzaService,
    private drinkService: DrinkService,
    private orderService: OrderService,
    public dialog: MatDialog) {
    this.pizzaList = [];
    this.nonPizzaList = [];
    this.drinkList = [];
  }

  ngOnInit() {
    this.pizzaService
      .getPizzaList()
      .subscribe(
        (pizzaList) => {
          this.pizzaList = <Pizza[]>pizzaList;
        }
      );
    this.nonPizzaService.getNonPizzaList()
      .subscribe(nonPizzaList => {
        this.nonPizzaList = <NonPizza[]>nonPizzaList;
      });
      this.drinkService.getDrinkList()
          .subscribe(drinks => {
            this.drinkList = <Drink[]>drinks;
          });
  }

  selectedPizza(id: number, pizza: Pizza) {
    this.dialog.open(AddOnsComponent, {
      data: {
        pizza: <Pizza>pizza
      },
      autoFocus: false
    });
  }

  selectedNonPizza(id: number, nonPizza: NonPizza) {
    let order = this.orderService.shoppingCart.getValue();
    let item: OrderNonPizza = {
      nonPizzaId: nonPizza.id,
      name: nonPizza.name,
      price: nonPizza.price,
      description: nonPizza.name,
      imageUrl: nonPizza.imageUrl,
      quantity: 1
    }
    order.nonPizzas.push(item);
    order.price += item.price;
    this.orderService.shoppingCart.next(order);
  }

  selectedDrink(id: number, drink: Drink) {
    let order = this.orderService.shoppingCart.getValue();
    let item: OrderDrink = {
      drinkId: drink.id,
      name: drink.name,
      price: drink.price,
      description: drink.name,
      imageUrl: drink.imageUrl,
      quantity: 1
    }
    order.drinks.push(item);
    order.price += item.price;
    this.orderService.shoppingCart.next(order);
  }

}
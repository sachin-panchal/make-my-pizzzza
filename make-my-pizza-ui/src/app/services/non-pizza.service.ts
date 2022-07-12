import { Injectable } from "@angular/core";
import { NonPizza } from "../models/non-pizza.model";
import { DataService } from "../shared/services/data.service";

@Injectable({
    providedIn: 'root'
})
export class NonPizzaService {
    constructor(private dataService: DataService) {
        
    }

    public getNonPizzaList() {
        return this.dataService.get<NonPizza[]>('nonpizza/get');
    }

    public getCheesePrice() {
        return this.dataService.get<NonPizza>('nonpizza/get/cheese');
    }
}
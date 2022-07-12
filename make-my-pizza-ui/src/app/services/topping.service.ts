import { Injectable } from "@angular/core";
import { Topping } from "../models/topping.model";
import { DataService } from "../shared/services/data.service";

@Injectable({
    providedIn: 'root'
})
export class ToppingService {
    constructor(private dataService: DataService) {
        
    }

    getToppings() {
        return this.dataService.get<Topping[]>('toppings/get');
    }
}
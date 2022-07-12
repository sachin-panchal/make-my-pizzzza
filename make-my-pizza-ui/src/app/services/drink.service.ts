import { Injectable } from "@angular/core";

import { Drink } from "../models/drink.model";
import { DataService } from "../shared/services/data.service";

@Injectable({
    providedIn: 'root'
})
export class DrinkService {
    constructor(private dataService: DataService) {
        
    }

    public getDrinkList() {
        return this.dataService.get<Drink[]>('drink/get');
    }
}
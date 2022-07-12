import { Injectable } from "@angular/core";
import { Sauce } from "../models/sauce.model";
import { DataService } from "../shared/services/data.service";

@Injectable({
    providedIn: 'root'
})
export class SauceService {
    constructor(private dataService: DataService) {
        
    }

    getSauces() {
        return this.dataService.get<Sauce[]>('sauce/get');
    }
}
import { Component, Input } from "@angular/core";

@Component({
    selector: 'checkout-item',
    templateUrl: 'checkout-item.component.html',
    styleUrls: ['checkout-item.component.scss']
})
export class CheckoutItemComponent {
    @Input() checkoutItem!: CheckoutItem;

    remove(id: number) {

    }
}


export interface CheckoutItem {
    id?: number;
    name: string;
    price?: number;
    description?: string;
    imageUrl?: string
}
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatCardModule } from "@angular/material/card";
import { MatInputModule } from "@angular/material/input";

import { SharedModule } from "src/app/shared/shared.module";
import { CheckoutItemComponent } from "./checkout-item/checkout-item.component";
import { CheckoutRoutingModule } from "./checkout-routing.module";
import { CheckoutComponent } from "./checkout.component";
import { DeliveryDetailsComponent } from "./delivery-details/delivery-details.component";

@NgModule({
    declarations: [
        CheckoutComponent,
        CheckoutItemComponent,
        DeliveryDetailsComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        CheckoutRoutingModule,
        MatInputModule,
        MatCardModule,
        SharedModule
    ]
})
export class CheckoutModule {}
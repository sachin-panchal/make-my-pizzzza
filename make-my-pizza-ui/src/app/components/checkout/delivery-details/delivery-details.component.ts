import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Customer } from "src/app/models/customer.model";
import { CustomerOrder, Order } from "src/app/models/order.model";
import { OrderService } from "src/app/services/order.service";
import { SnackbarType } from "src/app/shared/models/common.models";
import { SnackbarService } from "src/app/shared/services/snackbar.service";

@Component({
    selector: 'delivery-details',
    templateUrl: 'delivery-details.component.html',
    styleUrls: ['delivery-details.component.scss']
})
export class DeliveryDetailsComponent implements OnInit {
    customerForm!: FormGroup;
    order!: Order;

    constructor(private fb: FormBuilder,
        private orderService: OrderService,
        private router: Router,
        private snackbarService: SnackbarService) {
        this.initCustomerForm();
    }

    ngOnInit(): void {
        this.order = this.orderService.shoppingCart.getValue();
    }

    initCustomerForm() {
        this.customerForm = this.fb.group({
            username: '',
            firstName: ['', [Validators.required, Validators.maxLength(20)]],
            lastName: ['', [Validators.required, Validators.maxLength(20)]],
            phone: ['', [Validators.required, Validators.maxLength(10)]],
            email: '',
            address: ['', [Validators.required, Validators.maxLength(100)]],
            city: ['', [Validators.required, Validators.maxLength(50)]],
            pincode: ['', [Validators.required, Validators.maxLength(7)]]
        });
    }

    public hasError = (controlName: string, errorName: string) => {
        return this.customerForm.controls[controlName].hasError(errorName);
    }

    placeOrder() {
        
        if (this.customerForm.valid) {
            let rawCustomer = this.customerForm.value;
            let customer: Customer = {
                id: rawCustomer.id ?? 0,
                username: rawCustomer.username ?? '',
                firstName: rawCustomer.firstName,
                lastName: rawCustomer.lastName,
                phone: rawCustomer.phone,
                email: rawCustomer.email,
                address: rawCustomer.address,
                city: rawCustomer.city,
                pincode: rawCustomer.pincode
            };
            
            let customerOrder: CustomerOrder = {
                user: customer,
                order: this.order
            };

            this.orderService.placeDirectOrder(customerOrder)
                .subscribe(placedOrder => {
                    this.snackbarService.openMultiline([`Order placed successfully, your order id is ${placedOrder?.order.id}`], SnackbarType.success);
                    this.orderService.emptyMyCart();
                    this.router.navigate(['']);
                })
        }
    }
}
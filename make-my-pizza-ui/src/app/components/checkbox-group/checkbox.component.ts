import { Component, Input, Host } from '@angular/core';
import { CheckboxGroupComponent } from './checkbox-group.component';

@Component({
    selector: 'checkbox',
    styles: [`
        input[type='checkbox'] {
            accent-color: #ff4081;
        }
    `],
    template: `
    <div (click)="toggleCheck()" class="row" >
        <div class="col-1">
            <input type="checkbox" [checked]="isChecked()" [disabled]="isDisabled()" >
        </div>
     &nbsp;<ng-content></ng-content> &nbsp;
    </div>`
})
export class CheckboxComponent {
    @Input() value: any;
    constructor(@Host() private checkboxGroup: CheckboxGroupComponent) {

    }

    toggleCheck() {
        this.checkboxGroup.addOrRemove(this.value);
    }

    isChecked() {
        return this.checkboxGroup.contains(this.value);
    }

    isDisabled() {
        return this, this.checkboxGroup.isDisabled;
    }
}
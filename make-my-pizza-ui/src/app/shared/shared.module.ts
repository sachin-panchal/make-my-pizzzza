import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatSnackBarModule } from "@angular/material/snack-bar";

import { SnackbarComponent } from "./components/snackbar/snackbar.component";

@NgModule({
    declarations: [
        SnackbarComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        MatSnackBarModule,
        MatButtonModule,
        MatIconModule
    ],
    exports: [
        MatSnackBarModule,
        MatButtonModule,
        MatIconModule
    ]
})
export class SharedModule {

}
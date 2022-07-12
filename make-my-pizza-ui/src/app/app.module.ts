import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { MatDividerModule } from '@angular/material/divider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';

import { ConfigService } from './shared/services/config.service';
import { Configuration } from './shared/models/common.models';
import { HomeComponent } from './components/home/home.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { AddOnsComponent } from './components/add-ons/add-ons.component';

import { MatCardComponent } from './components/mat-card/mat-card.component';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { CheckboxGroupComponent } from './components/checkbox-group/checkbox-group.component';
import { CheckboxComponent } from './components/checkbox-group/checkbox.component';

export function initializeApp(appConfig: ConfigService) {
  return appConfig.loadConfig();
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ShoppingCartComponent,
    AddOnsComponent,
    MatCardComponent,
    CheckboxGroupComponent,
    CheckboxComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MatCardModule,
    MatDialogModule,
    MatRadioModule,
    MatInputModule,
    MatSlideToggleModule,
    MatDividerModule
  ],
  providers: [{
    provide: APP_INITIALIZER,
    useFactory: initializeApp,
    deps: [ConfigService, Configuration],
    multi: true
  },
    Configuration
    // ,
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: AuthInterceptorService,
    //   multi: true
    // }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

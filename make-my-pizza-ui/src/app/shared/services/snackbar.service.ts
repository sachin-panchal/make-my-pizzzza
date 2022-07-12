import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

import { SnackbarComponent } from '../components/snackbar/snackbar.component';
import { SnackbarMessage, SnackbarType } from '../models/common.models';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private snackBar: MatSnackBar) { }

  open(message: string,
    snackbarType: SnackbarType = SnackbarType.info,
    duration: number = 3000,
    verticalPosition: MatSnackBarVerticalPosition = 'bottom',
    horizontalPosition: MatSnackBarHorizontalPosition = 'center') {
    this.openMultiline([message], snackbarType, duration, verticalPosition, horizontalPosition);
  }

  openMultiline(messages: string[],
    snackbarType: SnackbarType = SnackbarType.info,
    duration: number = 3000,
    verticalPosition: MatSnackBarVerticalPosition = 'bottom',
    horizontalPosition: MatSnackBarHorizontalPosition = 'center') {

    let data: SnackbarMessage = {
      messages: messages,
      type: snackbarType
    };

    let panelClass = this.getPanelClass(snackbarType);

    this.snackBar.openFromComponent(SnackbarComponent, {
      data: data,
      duration: duration,
      verticalPosition: verticalPosition,
      horizontalPosition: horizontalPosition,
      panelClass: panelClass
    });

  }

  private getPanelClass(snackbarType: SnackbarType): string[] {
    let panelClass: string[] = [];
    switch (snackbarType) {
      case SnackbarType.info:
        panelClass = ['snackbar-info', 'mat-evelation-z8'];
        break;
      case SnackbarType.success:
        panelClass = ['snackbar-success', 'mat-evelation-z8'];
        break;
      case SnackbarType.warning:
        panelClass = ['snackbar-warning', 'mat-evelation-z8'];
        break;
      case SnackbarType.error:
        panelClass = ['snackbar-error', 'mat-evelation-z8'];
        break;
      default:
        panelClass = ['snackbar-info', 'mat-evelation-z8'];
        break;
    }
    return panelClass;
  }
}

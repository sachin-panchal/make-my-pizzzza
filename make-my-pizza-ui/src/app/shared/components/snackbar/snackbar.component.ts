import { Component, Inject, OnInit } from '@angular/core';
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import { SnackbarMessage, SnackbarType } from '../../models/common.models';

@Component({
  selector: 'app-snackbar-local',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.scss']
})
export class SnackbarComponent {

  constructor(@Inject(MAT_SNACK_BAR_DATA) public snackbarMessage: SnackbarMessage,
    private _snackRef: MatSnackBarRef<SnackbarComponent>) {

  }

  public dismiss() {
    this._snackRef.dismiss();
  }

  public getmatIcon() {
    let matIcon: string;
    switch (this.snackbarMessage.type) {
      case SnackbarType.success:
        matIcon = 'check';
        break;
      case SnackbarType.error:
        matIcon = 'error';
        break;
      case SnackbarType.warning:
        matIcon = 'warning'
        break;
      case SnackbarType.info:
        matIcon = 'info'
        break;
      default:
        matIcon = 'info'
    }
    return matIcon;
  }
}

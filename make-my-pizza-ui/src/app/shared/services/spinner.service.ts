import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  public loadStatus: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  public display(value: boolean) {
    this.loadStatus.next(value);
  }
}

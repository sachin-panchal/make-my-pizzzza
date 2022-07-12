import { Component, OnInit } from '@angular/core';
import { SpinnerService } from './shared/services/spinner.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Make My Pizza';

  public showDataLoading: boolean;
  public users: string[] = [];

  constructor(private spinnerService: SpinnerService) {
    this.showDataLoading = false;
  }

  ngOnInit(): void {
    this.spinnerService.loadStatus
        .subscribe( (displayLoading) => this.showDataLoading = displayLoading);

  }

}

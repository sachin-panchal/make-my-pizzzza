import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatCardOptions } from './card-option.model';

@Component({
  selector: 'mat-panel',
  templateUrl: 'mat-card.component.html',
  styleUrls: ['mat-card.component.scss'],
})
export class MatCardComponent implements OnInit {
  @Input() options!: MatCardOptions;
  @Output() selectedId: EventEmitter<number>;

  constructor() {
    this.selectedId = new EventEmitter<number>();
  }

  ngOnInit(): void {
    
  }

  add(id: number) {
    this.selectedId.emit(id);
  }

}

import { Injectable } from "@angular/core";
import { of } from "rxjs";
import { ImageSlider } from "../models/common.models";

import { DataService } from './data.service';

@Injectable({
    providedIn: 'root'
  })
export class SliderService {
    constructor(private dataService: DataService) { 

    }

    public getImageSliderList() {
        var slider: ImageSlider[] = [
                {img: '../assets/pizza1.jpeg', alt: 'pizza', text: 'pizza' },
                {img: '../assets/pizza2.jpeg', alt: 'pizza', text: 'pizza' },
                {img: '../assets/pizza3.jpeg', alt: 'pizza', text: 'pizza' }
              ];
        return of(slider);
    }
}
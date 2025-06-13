import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { APIService } from '../Services/APIService';

@Component({
  selector: 'app-city-search-component',
  imports: [FormsModule],
  templateUrl: './city-search-component.html',
  styleUrl: './city-search-component.css'
})
export class CitySearchComponent {
  location:string ="";
  constructor(private apiService : APIService){}

  handleSubmit(){
    this.apiService.getWeather(this.location);
  }
}

import { Component } from '@angular/core';
import { CitySearchComponent } from "./city-search-component/city-search-component";
import { WeatherCardComponent } from "./weather-card-component/weather-card-component";

@Component({
  selector: 'app-root',
  imports: [CitySearchComponent, WeatherCardComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'WeatherApp';
}

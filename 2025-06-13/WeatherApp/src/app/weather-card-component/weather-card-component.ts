import { Component, signal, WritableSignal } from '@angular/core';
import { APIService } from '../Services/APIService';
import { WeatherReport } from '../Models/WeatherReport';

@Component({
  selector: 'app-weather-card-component',
  imports: [],
  templateUrl: './weather-card-component.html',
  styleUrl: './weather-card-component.css'
})
export class WeatherCardComponent {

  public weatherReport : WritableSignal<WeatherReport|null> = signal<WeatherReport|null>(null);

  constructor(private apiService : APIService){
      this.apiService.apiResponse$.subscribe({
            next : (data:any) => {
                this.weatherReport.set(data);
            },
            error : (err) =>{
                console.log(err);
            },
            complete : () =>{
                console.log("Success");
            }
      });
  }
}

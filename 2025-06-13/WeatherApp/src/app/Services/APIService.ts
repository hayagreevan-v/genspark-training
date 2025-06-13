import { Injectable, signal, WritableSignal } from "@angular/core";
import { WeatherReport } from "../Models/WeatherReport";
import { BehaviorSubject, Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class APIService{
    private apiSubject : BehaviorSubject<WeatherReport|null> = new BehaviorSubject<WeatherReport|null>(null);
    public apiResponse$ : Observable<WeatherReport|null> = this.apiSubject.asObservable();

    constructor(private http:HttpClient){}

    getWeather(location : string){
        this.http.get(`http://api.weatherapi.com/v1/current.json?key=aa8ced6b8c404db282d140908231311&q=${location}`).subscribe({
            next : (data:any) => {
                if(data == null || data == undefined){
                    this.apiSubject.next(null);
                }
                if(data.error != null){
                    this.apiSubject.next(null);
                }
                let wr = new WeatherReport(
                    data.location.name,
                    data.current.temp_c,
                    data.current.condition.text,
                    data.current.wind_kph,
                    data.current.humidity,
                    data.current.condition.icon
                );
                this.apiSubject.next(wr);
            },
            error : (err) =>{
                console.log(err);
            },
            complete : () =>{
                console.log("Success");
            }
        })
    }
}
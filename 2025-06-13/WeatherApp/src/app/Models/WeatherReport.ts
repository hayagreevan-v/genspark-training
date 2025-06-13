export class WeatherReport  {
    constructor(
        public cityName : string ="",
        public temperatureInC : number = 0,
        public condition : string ="",
        public windKmph : number = 0,
        public humidity : number =0,
        public weatherIcon : string = ""
    ){}
}
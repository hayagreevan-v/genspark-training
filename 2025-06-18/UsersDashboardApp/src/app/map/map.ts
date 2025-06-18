import { AfterViewInit, Component, ViewChild } from '@angular/core';
import {IgxGeographicMapComponent, IgxGeographicMapModule, IgxGeographicProportionalSymbolSeriesComponent} from 'igniteui-angular-maps';
import { IgxDataChartInteractivityModule, IgxSizeScaleComponent, IgxValueBrushScaleComponent, MarkerType } from 'igniteui-angular-charts';
import { UserService } from '../services/UserService';
@Component({
  selector: 'app-map',
  imports: [IgxDataChartInteractivityModule,IgxGeographicMapModule],
  templateUrl: './map.html',
  styleUrl: './map.css'
})
export class MapComponent implements AfterViewInit {
  stateMap : Map<string,any> = new Map<string,any>();
  maxPopulation : number = 0;
  @ViewChild("map")
    public map?: IgxGeographicMapComponent;
    constructor(private userService : UserService) {
      this.userService.getUsers().subscribe({
        next : (data:any) => {
          data.users.forEach((u:any) => {
            if(this.stateMap.has(u.address.state)){
              let v = this.stateMap.get(u.address.state);
              v.count+=1;
              if(this.maxPopulation < v.count){
                this.maxPopulation = v.count;
              }
              this.stateMap.set(u.address.state,v);
            }
            else {
              let v = u.address;
              v.count=1;
              v.lat = u.address.coordinates.lat;
              v.lng = u.address.coordinates.lng;
              this.stateMap.set(u.address.state,v);
            }
          });
          console.log(this.stateMap);


          const sizeScale = new IgxSizeScaleComponent();
        sizeScale.minimumValue = 4;
        sizeScale.maximumValue = 60;

        const brushScale = new IgxValueBrushScaleComponent();
        brushScale.brushes = ["#a8e6cf", "#ffd3b6", "#ff8b94"];
        brushScale.minimumValue = 0;
        brushScale.maximumValue = this.maxPopulation;

        const symbolSeries = new IgxGeographicProportionalSymbolSeriesComponent();
        symbolSeries.dataSource = [...this.stateMap.values()];
        symbolSeries.markerType = MarkerType.Circle;
        symbolSeries.fillScale = brushScale;
        symbolSeries.radiusScale = sizeScale;
        symbolSeries.fillMemberPath = "count";
        symbolSeries.radiusMemberPath = "count";
        symbolSeries.latitudeMemberPath = "lat";
        symbolSeries.longitudeMemberPath = "lng";
        symbolSeries.markerOutline = "rgba(0,0,0,0.3)";

        this.map!.series.add(symbolSeries);
        }
      })
    }
    public ngAfterViewInit(): void {
        this.map!.windowRect = { left: 0.2, top: 0.1, width: 0.7, height: 0.7 };

        
    }

}

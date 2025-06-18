import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Dashboard } from "./dashboard/dashboard";
import { MapComponent } from "./map/map";

@Component({
  selector: 'app-root',
  imports: [Dashboard, MapComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'UsersDashboardApp';
}

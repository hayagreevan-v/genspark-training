import { Component } from '@angular/core';
import { AppFirst } from "./app-first/app-first";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [AppFirst]
})
export class App {
  protected title = 'myApp';
}

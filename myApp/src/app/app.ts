import { Component } from '@angular/core';
import { Menu } from "./menu/menu";
import { Login } from "./login/login";
import { Products } from './products/products';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [RouterOutlet, Menu]
})
export class App {
  protected title = 'myApp';
}

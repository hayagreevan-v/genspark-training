import { Component } from '@angular/core';
import { Menu } from "./menu/menu";
import { Login } from "./login/login";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Menu, Login]
})
export class App {
  protected title = 'myApp';
}

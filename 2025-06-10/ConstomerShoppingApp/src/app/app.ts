import { Component } from '@angular/core';
import { User } from "./user/user";
import { Products } from "./products/products";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [User, Products]
})
export class App {
  protected title = 'ConstomerShoppingApp';
}

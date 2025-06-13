import { Component } from '@angular/core';
import { Loginpage } from "./loginpage/loginpage";
import { Welcomepage } from "./welcomepage/welcomepage";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Loginpage, Welcomepage]
})
export class App {
  protected title = 'LoginApp';
}

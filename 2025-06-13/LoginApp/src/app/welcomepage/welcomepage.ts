import { Component } from '@angular/core';

@Component({
  selector: 'app-welcomepage',
  imports: [],
  templateUrl: './welcomepage.html',
  styleUrl: './welcomepage.css'
})
export class Welcomepage {
  // user : string|null = localStorage.getItem("user");
  user : string|null = sessionStorage.getItem("user");
  logout(){
    localStorage.removeItem("user");
    sessionStorage.removeItem("user");
  }
}

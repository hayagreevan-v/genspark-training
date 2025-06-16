import { Component, signal, Signal } from '@angular/core';
import { UserService } from '../services/UserService';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-menu',
  imports: [RouterLink],
  templateUrl: './menu.html',
  styleUrl: './menu.css'
})
export class Menu {
  user  = signal<string|null>(null);
  constructor(private userService : UserService){
    this.userService.username$.subscribe({
      next : (value) => {
        this.user.set(value);
      }
    })
  }
}

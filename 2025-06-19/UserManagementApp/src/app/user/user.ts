import { Component, Input } from '@angular/core';
import { UserModel } from '../models/user.model';

@Component({
  selector: 'app-user',
  imports: [],
  templateUrl: './user.html',
  styleUrl: './user.css'
})
export class User {

  @Input() user : UserModel | null = null;
}

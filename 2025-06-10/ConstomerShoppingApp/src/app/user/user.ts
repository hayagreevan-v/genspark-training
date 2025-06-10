import { Component } from '@angular/core';

@Component({
  selector: 'app-user',
  imports: [],
  templateUrl: './user.html',
  styleUrl: './user.css'
})
export class User {
    user = {name : "Hex",age : 25,likeCount:0, dislikeCount:0};
    onLike(){
      this.user.likeCount+=1;
    }
    onDislike(){
      this.user.dislikeCount+=1;
    }
}

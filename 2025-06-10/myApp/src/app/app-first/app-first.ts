import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-first',
  imports: [FormsModule],
  templateUrl: './app-first.html',
  styleUrl: './app-first.css'
})
export class AppFirst {

  name:string ="Hex";
  onButtonClick(name:string){
    this.name = name;
  }
  balloonClass : string = "bi bi-balloon-heart";
  like : boolean = false;
  toggleBalloon(){
    this.like = !this.like;
    if(this.like){
      this.balloonClass = "bi bi-balloon-heart-fill";
    }else{
      this.balloonClass = "bi bi-balloon-heart";
    }
  }
}

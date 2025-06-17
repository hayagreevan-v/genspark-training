import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RouterOutlet],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit{
  name : string = "";
  route = inject(ActivatedRoute);
  ngOnInit(): void {
    console.log("init");
    this.name = this.route.snapshot.params["un"];
  }

}

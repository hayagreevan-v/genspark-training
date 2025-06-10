import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
@Component({
  selector: 'app-products',
  templateUrl: './products.html',
  styleUrl: './products.css',
  imports: [CommonModule]
})
export class Products {
  products = [
      {Name : "Pen", Price : 12.00, Image : "https://imgs.search.brave.com/zJOf5bzDbC-l-_bdxOyNW0qDylb-kHjI0ESUA5KhVVc/rs:fit:500:0:0:0/g:ce/aHR0cHM6Ly9zdGF0/aWMzLmRlcG9zaXRw/aG90b3MuY29tLzEw/MDE2MjcvMTM0L2kv/NDUwL2RlcG9zaXRw/aG90b3NfMTM0OTEx/MS1zdG9jay1waG90/by1zaGluaW5nLWJh/bGwtcG9pbnQtcGVu/LmpwZw"},
      {Name : "Pencil", Price: 7.00, Image : "https://imgs.search.brave.com/DSmXRROjtCtjyvBPwuWZ8UzMLDbY3f6kUQlDj0p2yVc/rs:fit:500:0:0:0/g:ce/aHR0cHM6Ly9pbWcu/ZnJlZXBpay5jb20v/ZnJlZS12ZWN0b3Iv/cGVuY2lsXzI0OTA4/LTU0NjMwLmpwZz9z/ZW10PWFpc19oeWJy/aWQmdz03NDA"},
      {Name : "Marker", Price: 20.00, Image : "https://imgs.search.brave.com/dxtvRZR5apkC_aQraUMn6gbwZ3pWmxAd4cjEbTcn8KA/rs:fit:500:0:0:0/g:ce/aHR0cHM6Ly9jZG4u/Y3JlYXRlLnZpc3Rh/LmNvbS9hcGkvbWVk/aWEvc21hbGwvMjA4/MzkyMjEvc3RvY2st/cGhvdG8tY29sb3Jl/ZC1tYXJrZXJz.jpeg"}
    ]
  cartCount : number = 0;
  addToCart(){
    this.cartCount+=1;
  }
}

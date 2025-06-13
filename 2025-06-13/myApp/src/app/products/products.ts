import { Component } from '@angular/core';
import { Product } from '../product/product';
import { ProductService } from '../services/ProductService';
import { ProductModel } from '../models/ProductModel';
import { CartItem } from '../models/CartItem';

@Component({
  selector: 'app-products',
  imports: [Product],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  products : ProductModel[]| null = null;
  cart : CartItem[]= [];
  cartCount:number=0;
  constructor(private productService : ProductService){}

  handleAddtoCart(event : number){
    let index:number = this.cart.findIndex((c) =>c.id === event );
    if(index==-1){
      this.cart.push(new CartItem(event, this.products?.find((p)=> p.id== event)?.title,1));
    }
    else {
      this.cart[index].count++;
    }
    this.cartCount++;
  }

  ngOnInit() : void {
    this.productService.getProducts().subscribe({
      next : (data : any) => {
        console.log(data);
        this.products = data.products as ProductModel[];
      },
      error : (err) => {
        console.error(err);
      },
      complete : ()=> {
        console.log("Completed");
      }
    })
  }
}

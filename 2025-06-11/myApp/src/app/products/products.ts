import { Component } from '@angular/core';
import { Product } from '../product/product';
import { ProductService } from '../services/ProductService';
import { ProductModel } from '../models/ProductModel';

@Component({
  selector: 'app-products',
  imports: [Product],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  products : ProductModel[]| null = null;
  constructor(private productService : ProductService){}

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

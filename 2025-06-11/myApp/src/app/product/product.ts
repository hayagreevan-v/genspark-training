import { Component, inject, Input } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { ProductService } from '../services/ProductService';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
    @Input() product:ProductModel|null = new ProductModel();

    private productService = inject(ProductService);

    constructor(){
      // this.productService.getProduct(1).subscribe({
      //   next : (data) => {
      //     this.product = data as ProductModel;
      //     console.log(this.product);
      //   },
      //   error : (err)=>{
      //     console.error(err);
      //   },
      //   complete : ()=>{
      //     console.log("Completed");
      //   }
      // })
    }
}

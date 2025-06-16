import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
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
    @Input() product:ProductModel|null|undefined = new ProductModel();

    @Output() addToCart : EventEmitter<number> = new EventEmitter<number>();
    private productService = inject(ProductService);

    handleAddToCart = (pid : number|undefined) => {
      if(pid){
        this.addToCart.emit(pid);
      }
    }

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

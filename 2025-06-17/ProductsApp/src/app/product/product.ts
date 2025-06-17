import { Component, inject, Input, Output } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { CurrencyPipe } from '@angular/common';
import { ProductService } from '../services/ProductService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
  @Input() product : ProductModel = new ProductModel();
  private router : Router = inject(Router);
  getProductPage(){
    this.router.navigate(['products',this.product.id]);
  }
}

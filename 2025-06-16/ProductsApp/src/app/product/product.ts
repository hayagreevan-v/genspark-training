import { Component, Input } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
  @Input() product : ProductModel = new ProductModel();
}

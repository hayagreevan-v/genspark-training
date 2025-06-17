import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../models/ProductModel';
import { CurrencyPipe } from '@angular/common';
import { ProductService } from '../services/ProductService';
import { ActivatedRoute } from '@angular/router';
import { ProductDescriptiveModel } from '../models/ProductDescriptiveMode';

@Component({
  selector: 'app-productpage',
  imports: [CurrencyPipe],
  templateUrl: './productpage.html',
  styleUrl: './productpage.css'
})
export class Productpage implements OnInit{
  product : ProductDescriptiveModel| null = null;
  productId : number =0;
  constructor(private route:ActivatedRoute,private productService:ProductService){}
  ngOnInit(): void {
    this.productId = this.route.snapshot.params["id"];
    this.productService.getProductById(this.productId).subscribe({
      next:(data:any)=>{
        this.product=data
      }
    });
  }

}

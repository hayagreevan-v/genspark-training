import { Component, HostListener } from '@angular/core';
import { BehaviorSubject, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs';
import { Product } from '../product/product';
import { ProductService } from '../services/ProductService';
import { ProductModel } from '../models/ProductModel';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-products',
  imports: [Product,FormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  searchQuery : string= "";
  productSubject : BehaviorSubject<string> = new BehaviorSubject<string>("");
  loading : boolean = false;
  limit = 12;
  skip = 0;
  total = 0;

  products : ProductModel[] = [];
  constructor(private productService : ProductService){}


  searchProducts(){
    this.productSubject.next(this.searchQuery);
  }

  ngOnInit(){
    this.productSubject.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      tap(() => (this.loading= true)),
      switchMap((query) => this.productService.getProductsBySearch(query,this.limit,0)),
      tap(() => (this.loading= false))
    ).subscribe({
      next : (data)=>{
        this.products = data.products;
        this.total = data.total;
        this.skip=0;
        console.log(data);
      }
    });
  }

  @HostListener("window:scroll")
  onScroll(){
    let scrollPosition = window.innerHeight+ window.scrollY;
    let threshold = document.body.offsetHeight - 100;
    if(scrollPosition>= threshold && !this.loading && this.total>this.products.length){
        this.loadMore();
    }
  }

  loadMore(){
    this.loading= true;
    this.skip+=this.limit;
    this.productService.getProductsBySearch(this.searchQuery,this.limit,this.skip)
    .subscribe({
      next: (data)  => {
        this.products=[...this.products,...data.products],
        this.loading= false;
      }
    });
  }
}

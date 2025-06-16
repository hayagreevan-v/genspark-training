import { Component, HostListener } from '@angular/core';
import { Product } from '../product/product';
import { ProductService } from '../services/ProductService';
import { ProductModel } from '../models/ProductModel';
import { CartItem } from '../models/CartItem';
import { FormsModule } from '@angular/forms';
import { debounce, debounceTime, distinctUntilChanged, Subject, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-products',
  imports: [Product,FormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  products : ProductModel[]=[];
  cart : CartItem[]= [];
  cartCount:number=0;
  searchString : string = "";
  loading:boolean = false;
  limit=10;
  skip=0;
  total =0;

  searchSubject  = new Subject<string>();
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

  handleSearchProducts(){
    this.searchSubject.next(this.searchString);
    // this.productService.getProductSearchResult(this.searchString).subscribe({
    //   next : (data : any) => {
    //     console.log(data);
    //     this.products = data.products as ProductModel[];
    //   },
    //   error : (err) => {
    //     console.error(err);
    //   },
    //   complete : ()=> {
    //     console.log("Completed");
    //   }
    // })
  }

  ngOnInit() : void {
    // this.productService.getProducts().subscribe({
    //   next : (data : any) => {
    //     console.log(data);
    //     this.products = data.products as ProductModel[];
    //   },
    //   error : (err) => {
    //     console.error(err);
    //   },
    //   complete : ()=> {
    //     console.log("Completed");
    //   }
    // })

    this.searchSubject.pipe(
      debounceTime(5000),
      distinctUntilChanged(),
      tap(()=> this.loading = true),
      switchMap((query:string)=>this.productService.getProductSearchResult(query,this.limit,this.skip)),
      tap(() => this.loading = false)).subscribe({
        next : (data:any) => {
          this.products = data.products;
          this.total = data.total;
        }
      });
  }

  @HostListener('window:scroll',[])
  onScroll(): void{
    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight - 100;
    if(scrollPosition >= threshold && !this.loading && this.products.length< this.total){
      console.log(scrollPosition);
      console.log(threshold);
      this.loadMore();
    }
  }

  loadMore(){
    this.loading = true;
    this.skip+=this.limit;
    this.productService.getProductSearchResult(this.searchString,this.limit,this.skip).subscribe({
      next : (data:any) =>{
         this.products = [...this.products,...data.products];
        this.loading=false;
      }
    })
  }
}

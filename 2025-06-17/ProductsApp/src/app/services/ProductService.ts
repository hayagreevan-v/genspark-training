import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable()
export class ProductService {
    http = inject(HttpClient);
    getProductsBySearch(searchData : string = "", limit : number = 10, skip : number = 0){
        console.log("API called");
        return this.http.get<any>(`https://dummyjson.com/products/search?q=${searchData}&limit=${limit}&skip=${skip}`)
    }
    getProductById(id:number){
        return this.http.get<any>(`https://dummyjson.com/products/${id}`);   
    }
}
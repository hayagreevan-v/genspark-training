import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NewsModel } from "../models/news.model";
import { UserModel } from "../models/user.model";

@Injectable()
export class NewsService {
  private apiUrl = 'http://localhost:5043/api/News';
  private apiManagementUrl = 'http://localhost:5043/api/NewsManagement';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<NewsModel[]>(this.apiUrl);
  }
  create(news : NewsModel, user : UserModel){
    return this.http.post(`${this.apiManagementUrl}/Create`,news, {
        headers : {
            Authorization : `Bearer ${user.accessToken}`
        }
    });
  }
  update(newsId:number, news : NewsModel, user : UserModel){
    return this.http.post(`${this.apiManagementUrl}/Edit/${newsId}`,news,{
        headers : {
            Authorization : `Bearer ${user.accessToken}`
        }      
    });
  }
  delete(newsId:number, user : UserModel){
    return this.http.post(`${this.apiManagementUrl}/Delete/${newsId}`,{},{
        headers : {
            Authorization : `Bearer ${user.accessToken}`
        }      
    });
  }
}

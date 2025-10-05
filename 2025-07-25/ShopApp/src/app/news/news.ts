import { Component } from '@angular/core';
import { NewsModel } from '../models/news.model';
import { NewsService } from '../services/news.service';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule, DatePipe } from '@angular/common';
import { UserModel } from '../models/user.model';
import { UserService } from '../services/user.service';
import { FormsModule } from '@angular/forms';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Dialog } from '../dialog/dialog';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-news',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    DatePipe,
    FormsModule

  ],
  templateUrl: './news.html',
  styleUrl: './news.css'
})
export class News {
  newsList: NewsModel[] = [];
  showModal: boolean = false;
  isEditMode: boolean = false;
  selectedNews: NewsModel = new NewsModel();
  currentUser : UserModel | null = null;
  snackbar = new MatSnackBar();

  constructor(private newsService: NewsService, private userService : UserService, private dialog : MatDialog) {
    this.userService.user$.subscribe({
      next: data => this.currentUser = data
    })
    this.loadNews();
  }

  loadNews() {
    this.newsService.getAll().subscribe({
      next: data => this.newsList = data,
      error: err => console.error(err)
    });
  }

  openCreateModal() {
    this.selectedNews = new NewsModel();
    this.isEditMode = false;
    this.showModal = true;
  }

  openEditModal(news: NewsModel) {
    this.selectedNews = { ...news }; // clone to avoid mutating the original
    this.isEditMode = true;
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }

  submitNews() {
    if (this.isEditMode) {
      this.newsService.update(this.selectedNews.newsId as number,this.selectedNews,this.currentUser as UserModel).subscribe({
        next: () => {
          this.loadNews();
          this.closeModal();
        },
        error: err => console.error(err)
      });
    } else {
      this.newsService.create(this.selectedNews, this.currentUser as UserModel).subscribe({
        next: () => {
          this.loadNews();
          this.closeModal();
        },
        error: err => console.error(err)
      });
    }
  }

  deleteNews(id : number) {
    this.newsService.delete(id, this.currentUser as UserModel).subscribe({
      next : () =>{
        this.loadNews();
      }
    })
  }
  openDeleteDialog(message : string, id : number | null){
  this.dialog.open(Dialog,{
    data : {
      message : `Want to delete ${message}`, 
      onAccept : ()=>{
        this.deleteNews(id as number);
        this.snackbar.open(`News : ${message} deleted successfully!`,undefined,{duration: 3000});
        }
      }
    })
  }
}

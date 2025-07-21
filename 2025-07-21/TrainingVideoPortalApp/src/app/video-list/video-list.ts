import { Component } from '@angular/core';
import { VideoService } from '../services/video.service';
import { TrainingVideo } from '../models/trainingvideo.model';

@Component({
  selector: 'app-video-list',
  imports: [],
  templateUrl: './video-list.html',
  styleUrl: './video-list.css'
})
export class VideoList {
  videos : TrainingVideo[] =[];
  constructor(private videoService : VideoService){
    this.videoService.getVideos().subscribe({
      next: (data:any) => {
        this.videos = data.$values;
        console.log(this.videos);
      }
    })
  }
}

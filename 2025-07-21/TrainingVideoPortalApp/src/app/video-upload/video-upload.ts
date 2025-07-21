import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { VideoService } from '../services/video.service';

@Component({
  selector: 'app-video-upload',
  imports: [FormsModule],
  templateUrl: './video-upload.html',
  styleUrl: './video-upload.css'
})
export class VideoUpload {
  title : string = "";
  description : string = "";
  file : File| undefined;

  constructor(private videoService: VideoService){}
  
  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      if (!file.type.startsWith('video/')) {
        return;
      }

      const maxSize = 30 * 1024 * 1024; // 30MB
      if (file.size > maxSize) {
        return;
      }
      this.file = file;
      console.log(this.file);
    }
  }

  upload(){
    if(!this.file){
      alert("File is not uploaded");
      return;
    }
    let form = new FormData();
    form.append('title',this.title);
    form.append('description',this.description);
    form.append('video',this.file!);
    console.log(form);

    this.videoService.uploadVideo(form).subscribe({
      next : (data : any) => {
        alert("Successfully uploaded");
        console.log(data);
      },
      error : (err : any) => {
        console.error(err);
      }
    })
  }
}

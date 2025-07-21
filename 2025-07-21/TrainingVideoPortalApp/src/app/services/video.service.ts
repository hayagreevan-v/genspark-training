import { HttpClient, HttpEventType } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, map, Observable } from "rxjs";
import { TrainingVideo } from "../models/trainingvideo.model";

@Injectable()
export class VideoService {
    private apiUrl = "http://localhost:5160/api/videos"
    constructor(private http: HttpClient) { }

    getVideos(): Observable<TrainingVideo[]> {
        return this.http.get<TrainingVideo[]>(this.apiUrl);
    }

    uploadVideo(formData: FormData): Observable<any> {
        return this.http.post(`${this.apiUrl}/upload`, formData, {
            reportProgress: true,
            observe: 'events'
        });
    }
}
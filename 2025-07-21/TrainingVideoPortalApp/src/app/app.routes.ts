import { Routes } from '@angular/router';
import { VideoList } from './video-list/video-list';
import { VideoUpload } from './video-upload/video-upload';

export const routes: Routes = [
    {path: "", component: VideoList},
    {path: "add", component: VideoUpload}
];

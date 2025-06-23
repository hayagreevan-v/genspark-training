import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";

@Injectable()
export class BulkInsertService{
    constructor(private http: HttpClient){}
    
    public processData(file: File): Observable<any> {
        const resultSubject = new Subject<any>();
        const worker = new Worker(new URL('../workers/file-parser.worker', import.meta.url));

        worker.onmessage = ({ data }) => {
            if (typeof data !== 'string') {
                console.error('Unexpected worker data:', data);
                resultSubject.error('Invalid data from worker');
                return;
            }

            const body = { csvContent: data };

            this.http.post('http://localhost:5001/api/Sample/FromCsv', body).subscribe({
                next: res => {
                resultSubject.next(res);    
                resultSubject.complete();   
                },
                error: err => {
                console.error('API error:', err);
                resultSubject.error(err);  
                }
            });
        };

        worker.onerror = () => {
            resultSubject.error('Worker failed to read file');
        };

        worker.postMessage({ file });

        return resultSubject.asObservable();
    }   

}
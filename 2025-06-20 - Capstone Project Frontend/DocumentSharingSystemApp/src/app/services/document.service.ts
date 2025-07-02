import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UserModel } from "../models/user.model";
import { environment } from "../../environments/environment";
import { DocumentSearchModel } from "../models/document.search.model";
import { DocumentDetailsModel } from "../models/document.details.model";

@Injectable()
export class DocumentService {
    private http = inject(HttpClient);
    user : UserModel | null = null;

    getAll(user: UserModel| null){
        if(user!=null){
            return this.http.get(environment.serverUrl+'/documents',{
                headers : {
                    Authorization : `Bearer ${user.accessToken}`
                }
            })

        }
        return null;
    }

    getByFilter(user: UserModel, documentSearch : DocumentSearchModel){
        // if(user!=null){
            return this.http.post(environment.serverUrl+'/documents/filter',documentSearch, {
                headers : {
                    Authorization : `Bearer ${user.accessToken}`
                }
            })
        // }
        // return null;
    }
    deleteDocument(user : UserModel, id : string) {
        return this.http.delete(environment.serverUrl+'/documents/'+id, {
                headers : {
                    Authorization : `Bearer ${user.accessToken}`
                }
            })
    }
    downloadDocument(user : UserModel, id : string) {
        return this.http.get(environment.serverUrl+'/documents/download/'+id, {
                headers : {
                    Authorization : `Bearer ${user.accessToken}`
                },
                responseType : 'blob'
            })
    }
    uploadDocument(user : UserModel,fileData : DocumentDetailsModel,document : File) {
        console.log("Document:", document);
        console.log("Is file:", document instanceof File);
        let formData = new FormData();
        formData.append('description',fileData.description??"");
        formData.append('teamId', fileData.teamId.toString());
        formData.append('visibility',fileData.visibility??"Public");
        formData.append('formFile',document);
        return this.http.post(environment.serverUrl+'/documents/upload',formData, {
                headers : {
                    Authorization : `Bearer ${user.accessToken}`
                }
            })
    }
    getDocumentById(user : UserModel, id : string){
        return this.http.get(environment.serverUrl+'/documents/'+id, {
                headers : {
                    Authorization : `Bearer ${user.accessToken}`
                }
            })
    }
    updateDocumentDetails(user : UserModel, id : string, fileDate : DocumentDetailsModel){
        return this.http.put(environment.serverUrl+'/documents/'+id,fileDate ,{
                headers : {
                    Authorization : `Bearer ${user.accessToken}`
                }
            })
    }
}
import { Injectable } from "@angular/core";
import { LoginCredentials } from "../Models/LoginCredentials";

@Injectable()
export class LoginService
{
    storedCredentials : LoginCredentials[] = [
        new LoginCredentials("hex@mail.com","hex"),
        new LoginCredentials("admin@mail.com","admin")
    ];

     validate(creds: LoginCredentials ){
        let storedCredential : LoginCredentials| undefined = this.storedCredentials.find((lc) => lc.email === creds.email);
        if(storedCredential == null || storedCredential== undefined) return false;
        if(creds.password == storedCredential.password){
            return true;
        }
        return false;
    }
}
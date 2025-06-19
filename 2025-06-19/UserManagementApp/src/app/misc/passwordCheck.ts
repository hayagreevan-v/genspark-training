import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordCheck(): ValidatorFn {
    return (control : AbstractControl) : ValidationErrors | null => {
        const value = control.value as string;
        let lower=0,upper=0,digit=0;
        if(value?.length < 6){
            return {passwordCheck : "Min length should be 6"};
        }
        value?.split('').forEach(v =>{
            if(v.toLowerCase() === v) lower++;
            else if(v.toUpperCase() === v) upper++;
            if(v >= "0" && v<="9") digit++;
        })
        if(lower==0 || upper==0 || digit ==0){
            return {passwordCheck : "Password should contain atleast 1 uppercase, 1 lowercase and 1 digit"};
        }
        return null;
    }
}
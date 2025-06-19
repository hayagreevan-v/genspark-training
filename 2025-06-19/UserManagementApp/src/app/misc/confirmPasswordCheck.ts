import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function confirmPasswordCheck(): ValidatorFn {
    return (group : AbstractControl) : ValidationErrors | null => {
        let password = group.get("password")?.value as string;
        let confirmPassword = group.get("confirmPassword")?.value as string;
        // console.log(password, confirmPassword);
        if(confirmPassword != password){
            return{ confirmPasswordCheck : "Confirm Password should be same as password"};
        }
        return null;
    }
}
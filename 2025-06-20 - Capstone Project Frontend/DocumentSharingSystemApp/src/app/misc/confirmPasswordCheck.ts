import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function confirmPasswordCheck() : ValidatorFn {
    return (group : AbstractControl) : ValidationErrors | null => {
        if(!group.get('password')?.value && !group.get('confirmPassword')?.value){
            return null;
        }
        const password = group.get('password')?.value as string;
        const confirmPassword = group.get('confirmPassword')?.value as string;
        if(group.get('password')?.invalid){
            group.get('confirmPassword')?.setErrors({confirmPasswordCheck : "Password is invalid"});
            return{ confirmPasswordCheck : "Password is invalid"};
        }
        if(confirmPassword != password){
            group.get('confirmPassword')?.setErrors({confirmPasswordCheck : "Confirm Password should be same as password"});
            return{ confirmPasswordCheck : "Confirm Password should be same as password"};
        }
        return null;
    }
}

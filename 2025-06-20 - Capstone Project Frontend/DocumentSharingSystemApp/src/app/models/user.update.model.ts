export class UserUpdateModel {
    constructor(public name : string = '', public email : string = "", public teamId : number =0, public password : string | null = null ){}
}
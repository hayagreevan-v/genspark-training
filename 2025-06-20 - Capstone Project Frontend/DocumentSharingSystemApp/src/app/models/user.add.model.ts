export class UserAddModel {
    constructor(
        public name : string = '', 
        public role : string = 'User', 
        public email : string = "", 
        public teamId : number = 0,
        public password : string = ""
    ){}
}
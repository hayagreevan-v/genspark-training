export class UserModel {
    constructor(
        public id : string ="",
        public name : string = "",
        public role : string = "",
        public email : string = "",
        public teamId : number = 0,
        public teamName : string = "",
        public isDeleted: boolean = false,
        public createdByUserId: string = "",
        public createdByUserName: string = "",
        public createdByUserEmail: string = "",
        public createdAt : Date = new Date(),
        public lastUpdatedByUserId: string = "",
        public lastUpdatedByUserName: string = "",
        public lastUpdatedByUserEmail: string = "",
        public lastUpdatedAt : Date = new Date(),
        public accessToken : string = "",
        public refreshToken : string = ""
    ){}
}
export class TeamModel {
    constructor(
        public id : number =0,
        public name : string = "",
        public isDeleted : boolean = false,
        public createdByUserId : string = "",
        public createdByUserName : string = "",
        public createdByUserEmail : string = "",
        public createdAt : Date = new Date(),
        public lastUpdatedByUserId : string = "",
        public lastUpdatedByUserName : string = "",
        public lastUpdatedByUserEmail : string = "",
        public lastUpdatedAt : Date = new Date()
    ){}
}
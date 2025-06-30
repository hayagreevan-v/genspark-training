export class DocumentModel {
    constructor(
        public id : string ="",
        public storedFileName : string = "",
        public originalFileName : string = "",
        public description : string = "",
        public visibility : string = "",
        public teamId : number = 0,
        public teamName : string = "",
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

    static fromData(data: any){
        return new DocumentModel(
            data.id,
            data.storedFileName,
            data.originalFileName, 
            data.description,
            data.visibility,
            data.teamId,
            data.teamName,
            data.isDeleted,
            data.createdByUserId, 
            data.createdByUserName,
            data.createdByUserEmail,
            new Date(data.createdAt),
            data.lastUpdatedByUserId,
            data.lastUpdatedByUserName,
            data.lastUpdatedByUserEmail,
            new Date(data.lastUpdatedAt)
        )
    }
}
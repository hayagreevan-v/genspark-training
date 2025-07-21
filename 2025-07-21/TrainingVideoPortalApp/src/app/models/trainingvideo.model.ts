export class TrainingVideo {
    constructor(
        public id : number,
        public title : string = "",
        public description : string = "",
        public createdAt : Date,
        public blobUrl : string = ""
    ){}
}
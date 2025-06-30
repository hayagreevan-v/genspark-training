export class DocumentDetailsModel {
    constructor(
        public description : string | null = null,
        public teamId : number =0,
        public visibility : string | null = 'Public'
    ){}
}
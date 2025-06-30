export class DocumentSearchModel {
    constructor(
        public searchByOriginalFileName : string | null = null,
        public searchByCreatedUserEmail : string | null = null,
        public searchByCreatedTime : string | null = null,
        public SortBy : string | null = null,
        public SortOrder : string | null = null,
        public pageNo : number | null = null,
        public pageSize : number | null = null,
        public view : string | null = 'All'
    ){}
}
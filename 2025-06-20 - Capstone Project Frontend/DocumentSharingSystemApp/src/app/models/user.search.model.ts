export class UserSearchModel {
    constructor ( 
        public searchQuery : string | null, 
        public sortOrder : string |null ,
        public role : string | null
    ){}
}
export class NewsModel {
    constructor(
        public newsId: number | null = null,
        public userId: number | null = null,
        public title: string = '',
        public shortDescription: string = '',
        public image: string = '',
        public content: string = '',
        public createdDate: Date = new Date(),
        public status: number | null = null,
    ) { }
}

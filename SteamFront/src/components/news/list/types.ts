export interface INewsItem {
    id: number;
    title: string;
    description: string;
    dateOfRelease: Date;
    image: string;
    videoURL: string;
    game: IGameItem;
    totalCount: number;
}

export interface IGameItem {
    id: number;
    name: string;
}

export interface INewsData {
    items: INewsItem[]; // Change list to items
    pageIndex: number;
    pageSize: number;
    totalCount: number;
}
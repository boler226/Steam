export interface INewsItem {
    id: number;
    title: string;
    description: string;
    dateOfRelease: Date;
    image: string;
    videoURL: string;
    game: IGameItem;
}

export interface IGameItem {
    id: number;
    name: string;
}

export interface INewsSearch {
    title?: string;
    description?: string;
    page: number;
    pageSize: number;
}
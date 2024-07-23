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

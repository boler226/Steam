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

export interface INewsCreate {
    title: string
    description: string
    image: File
    gameId: number
}
import {IComments} from "../account";
import {IMediaItem} from "../helpers";

export interface INewsItem {
    id: number;
    title: string;
    description: string;
    dateOfRelease: Date;
    media: IMediaItem[];
    comments: IComments[];
    rating: number;
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

export interface INewsFilter {
    ByRelease?: boolean
}
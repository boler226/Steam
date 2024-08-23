import {INewsItem} from "../news";
import {IUser, IComments} from "../account";
import {IMediaItem} from "../helpers";

export interface IGameItem {
    id: number;
    name: string;
    price: number;
    systemRequirements: ISystemRequirements;
    developer: IUser;
    rating: number;
    dateOfRelease: Date;
    news: INewsItem[];
    comments: IComments[];
    categories: ICategoryItem[];
    media: IMediaItem[];
}

export interface IGameCreate {
    name: string;
    price: number;
    description: string;
    systemRequirements: ISystemRequirements;
    categories: number[];
    images: File[];
}

export interface ICategoryItem {
    id: number;
    name:string;
}

export interface ISystemRequirements {
    id: number;
    operatingSystem: string;
    processor: string;
    RAM: number;
    videoCard: string;
    diskSpace: number;
}



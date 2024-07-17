import {INewsItem} from "../news/list/types.ts";

export interface IGameItem {
    id: number;
    name: string;
    price: number;
    systemRequirements: string;
    dateOfRelease: Date;
    news: INewsItem[];
    categories: ICategoryItem[];
    images: IImageItem[];
}

export interface ICategoryItem {
    id: number;
    name:string;
}

export interface IImageItem {
    id: number;
    name: string;
    priority: string;
}
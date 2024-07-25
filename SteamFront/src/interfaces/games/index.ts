import {INewsItem} from "../news";

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

export interface IGameCreate {
    name: string;
    price: number;
    description: string;
    systemRequirements: string;
    categories: number[];
    images: File[];
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
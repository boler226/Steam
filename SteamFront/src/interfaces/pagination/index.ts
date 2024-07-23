export interface PaginationOptions {
    pageIndex?: number;
    pageSize?: number;
}

export interface IPage<T> {
    data: T[];
    pagesAvailable: number;
    itemsAvailable: number;
}

export interface IRegister {
    UserName: string;
    Password: string;
    Email: string;
    Image: File;
}

export interface ILogin {
    Email: string;
    Password: string;
}

export interface IComments {
    id: number;
    comment: string;
    rating: number;
    user: IUser;
}

export interface IUser {
    id: number;
    userName: string;
    image: string;
}
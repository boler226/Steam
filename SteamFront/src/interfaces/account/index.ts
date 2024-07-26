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
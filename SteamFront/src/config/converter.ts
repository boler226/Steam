import {UploadChangeParam} from 'antd/es/upload';

export const imageConverterToFileArray = (e: UploadChangeParam) => {
    console.log(e.file);
    return e?.fileList.map(file => file.originFileObj);
};

export const imageConverterToFile = (e: UploadChangeParam) => {
    console.log(e.file);
    return e?.file;
};
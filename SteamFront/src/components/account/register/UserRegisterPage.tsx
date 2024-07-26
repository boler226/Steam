import { IRegister} from "../../../interfaces/account";
import {Form, Spin, Flex, Input, Upload, Button, Layout} from "antd";
import { useState } from "react";
import { Status } from "../../../enums";
import { useNavigate } from "react-router-dom";
import http_common from "../../../api/http_common.ts";
import {imageConverterToFileArray} from "../../../config/converter.ts";
import {DownloadOutlined} from "@ant-design/icons";
import {UploadChangeParam} from "antd/lib/upload/interface";

const { Content } = Layout;

const UserRegisterPage = () => {
    const [form] = Form.useForm<IRegister>();
    const [status, setStatus] = useState<Status>(Status.IDLE);
    const [formValues, setFormValues] = useState<Partial<IRegister>>({});
    const [imageFile, setImageFile] = useState<File | null>(null);

    const navigate = useNavigate();

    const onFinish = async (values: IRegister) => {
        try {
            setStatus(Status.LOADING);
            console.log("Submit form", values);

            const formData = new FormData();
            formData.append("UserName", values.UserName);
            formData.append("Password", values.Password);
            formData.append("Email", values.Email);

            if (imageFile)
            {
                formData.append("Image", imageFile);
            }

            await http_common.post("/api/Account/Registration", formData, {
                headers: {
                    "Content-Type": "multipart/form-data"
                }
            });
            navigate(`/`);
        }
        catch (error) {
            console.log("error", error);
        }
        finally {
            setStatus(Status.IDLE);
        }
    }

    const handleUploadChange = (info: UploadChangeParam) => {
        if (info.fileList.length > 0) {
            const file = info.fileList[0].originFileObj as File; // Отримати оригінальний File об'єкт
            setImageFile(file); // Зберегти файл зображення
        }
    };

    const handleFormChange = (allValues: any) => {
        setFormValues({...formValues, ...allValues});
    };

    return(
        <Spin spinning={status === Status.LOADING}>
            <Content style={{ minHeight: '600px'}}>
                <Flex justify="space-around" align="center">
                    <Form
                        form={form}
                        onFinish={onFinish}
                        onValuesChange={handleFormChange}
                        layout="vertical"
                        style={{
                            width: '500px',
                            display: 'flex',
                            flexDirection: 'column',
                            justifyContent: 'center',
                            padding: 20,
                        }}
                    >
                        <Form.Item
                            label="Назва користувача"
                            name="UserName"
                            htmlFor="UserName"
                            rules={[
                                {required: true, message: 'Це поле є обов\'язковим!'},
                                {min: 3, message: 'Назва повинна містити мінімум 3 символи!'}
                            ]}
                            className="custom-label"
                        >
                            <Input autoComplete="UserName"/>
                        </Form.Item>

                        <Form.Item
                            label="Адреса електронної пошти"
                            name="Email"
                            rules={[
                                { required: true, message: 'Це поле є обов\'язковим!' },
                                { type: 'email', message: 'Неправильний формат електронної пошти!' }
                            ]}
                            className="custom-label"
                        >
                            <Input autoComplete="Email" />
                        </Form.Item>

                        <Form.Item
                            label="Пароль"
                            name="Password"
                            rules={[
                                { required: true, message: 'Це поле є обов\'язковим!' },
                                { min: 6, message: 'Пароль повинен містити мінімум 6 символів!' }
                            ]}
                            className="custom-label"
                        >
                            <Input.Password autoComplete="Password" />
                        </Form.Item>

                        <Form.Item
                            label="Фото"
                            name="Image"
                            htmlFor="Image"
                            valuePropName="file"
                            getValueFromEvent={imageConverterToFileArray}
                            rules={[{ required: true, message: 'Це поле є обов\'язковим!' }]}
                            className="custom-label"
                        >
                            <Upload
                                listType="picture-card"
                                maxCount={1}
                                multiple={false}
                                showUploadList={{ showPreviewIcon: false }}
                                beforeUpload={() => false}
                                accept="image/*"
                                onChange={handleUploadChange}
                            >
                                <Button type="primary" shape="round" icon={<DownloadOutlined/>}/>
                            </Upload>
                        </Form.Item>

                        <Form.Item>
                            <Button type="primary" htmlType="submit" style={{ width: '140px', height: '40px', marginTop: '20px'}}>
                                Створити
                            </Button>
                        </Form.Item>
                    </Form>
                </Flex>
            </Content>
        </Spin>
    )
}

export default UserRegisterPage;
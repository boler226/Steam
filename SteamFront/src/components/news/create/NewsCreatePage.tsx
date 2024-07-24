import {INewsCreate} from "../../../interfaces/news";
import {Form, Spin, Button, Input, message, Card, Select, Upload, Flex} from "antd";
import {Status} from "../../../enums";
import { imageConverterToFileArray } from "../../../config/converter.ts";
import TextArea from "antd/es/input/TextArea";
import {ICategoryName} from "../../../interfaces/categories";
import {DownloadOutlined } from '@ant-design/icons';
import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import http_common from "../../../api/http_common.ts";
import { UploadChangeParam } from 'antd/lib/upload/interface';
import "./style/style.css";



const NewsCreatePage = () => {
    const [form] = Form.useForm<INewsCreate>();
    const [status] = useState<Status>(Status.IDLE);
    const [imageUrl, setImageUrl] = useState<string | undefined>();
    const [game, setGame] = useState<ICategoryName[]>();
    const [formValues, setFormValues] = useState<Partial<INewsCreate>>({});
    const [imageFile, setImageFile] = useState<File | null>(null);


    const navigate = useNavigate();

    const [messageApi, contextHolder] = message.useMessage();

    useEffect(() => {
        http_common.get<ICategoryName[]>("/api/Games/List")
            .then(resp => {
                setGame(resp.data);
            });
    }, []);

    const onFinish = async (values: INewsCreate) => {
        try
        {
            console.log("Submit form", values);

            const formData = new FormData();
            formData.append("title", values.title);
            formData.append("description", values.description);
            formData.append("gameId", values.gameId.toString());
            if (imageFile)
            {
                formData.append("image", imageFile);
            }
            await http_common.post("/api/News/Create", formData,{
                headers: {
                    "Content-Type": "multipart/form-data"
                }
            });
            navigate(`/news`);
        }
        catch (error) {
            console.log("error", error);
            console.log(messageApi.error);
        }
    };

    const handleUploadChange = (info: UploadChangeParam) => {
        if (info.fileList.length > 0) {
            const file = info.fileList[0].originFileObj as File; // Отримати оригінальний File об'єкт
            const url = URL.createObjectURL(file); // Створити URL для зображення
            setImageUrl(url); // Зберегти URL для відображення у <img>
            setImageFile(file); // Зберегти файл зображення
        } else {
            setImageUrl(undefined); // Скинути URL, якщо файл видалено
        }
    };

    const handleFormChange = (allValues: any) => {
        setFormValues({...formValues, ...allValues});
    };


    return(
        <Spin spinning={status === Status.LOADING}>
            <Flex justify="space-around" align="start">
                {contextHolder}
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
                        label="Назва"
                        name="title"
                        htmlFor="title"
                        rules={[
                            {required: true, message: 'Це поле є обов\'язковим!'},
                            {min: 3, message: 'Назва повинна містити мінімум 3 символи!'}
                        ]}
                        className="custom-label"
                    >
                        <Input autoComplete="title"/>
                    </Form.Item>

                    <Form.Item
                        label="Опис"
                        name="description"
                        htmlFor="description"
                        rules={[
                            {required: true, message: 'Це поле є обов\'язковим!'},
                            {min: 10, message: 'Опис повинен містити мінімум 10 символів!'}
                        ]}
                        className="custom-label"
                    >
                        <TextArea style={{ minHeight: '400px'}} />
                    </Form.Item>

                    <Form.Item
                        label="Ігра"
                        name="gameId"
                        htmlFor="gameId"
                        className="custom-label"
                    >
                        <Select
                            placeholder="Оберіть гру: "
                            options={game?.map(g => ({
                                value: g.id,
                                label: g.name
                            }))}
                        />
                    </Form.Item>

                    <Form.Item
                        label="Фото"
                        name="image"
                        htmlFor="image"
                        valuePropName="file"
                        getValueFromEvent={imageConverterToFileArray}
                        rules={[
                            {required: true, message: 'Це поле є обов\'язковим!'},
                        ]}
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


                <div>
                        <div className="news-card" role="button">
                            <Card hoverable className="news-card-inner" style={{ width: '900px' }}>
                                <Flex justify='space-between'>
                                    <div className="news-content">
                                        <Flex vertical justify='space-between' align='flex-start' gap='small'
                                              style={{height: '200px', maxWidth:'400px'}}>
                                            <p className="news-title">{formValues.title}</p>
                                            <p className="news-text">{formValues.description}</p>
                                            <Flex justify='space-between' gap='50%'>
                                                <p className="news-text">{new Date().toLocaleDateString()}</p>
                                                <p className="news-text" style={{whiteSpace: 'nowrap'}}>
                                                    {game?.find(g => g.id === formValues.gameId)?.name}
                                                </p>
                                            </Flex>
                                        </Flex>
                                    </div>
                                    <img className="news-card-image" style={{ width: '400px'}} alt={formValues.title}
                                         src={imageUrl} />

                                </Flex>
                            </Card>
                        </div>
                </div>
            </Flex>
        </Spin>
    )
}

export default NewsCreatePage;